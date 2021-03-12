using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{
    // controllers
    public static MageController staticController;
    private PlayerController player;

    // cooldown timers
    public float maxSpellCooldown = 0.35f;
    private float spellCooldown;
    public float maxLightningCooldown = 2f;
    private float lightningCooldown;
    public float maxSpellCircleCooldown = 4f;
    private float spellCircleCooldown;
    public float maxDefenseCooldown = 8f;
    private float defenseCooldown;
    public float maxBlastCooldown = 12f;
    private float blastCooldown;

    // mana values
    public int maxManaValue;
    private float currentMana;
    private ManaBar manaBar;
    public float manaRegenValue { get => 0.015f / maxManaValue; }
    
    // mana slider costs
    public float manaSpellCost = 2f;
    public float ManaSpellCost { get => manaSpellCost / maxManaValue; }
    public float manaLightningCost = 7f;
    public float ManaLightningCost { get => manaLightningCost / maxManaValue; }
    public float manaSpellCircleCost = 15f;
    public float ManaSpellCircleCost { get => manaSpellCircleCost / maxManaValue; }
    public float manaDefenseCircleCost = 25f; 
    public float ManaDefenseCircleCost { get => manaDefenseCircleCost / maxManaValue; }
    public float manaBlastCost = 40f;
    public float ManaBlastCost { get => manaBlastCost / maxManaValue; }

    //temp 
    private int lightningCounter = 0;
    private int blastCounter = 0;
    private bool alpha2Pressed = false;
    public float directionAngle
    {
        get { return Mathf.Atan2(player.LookDirection.y, player.LookDirection.x) * Mathf.Rad2Deg; }
    }

    // properties for UI
    public float getLightningCooldown 
    { 
        get 
        { 
            if (lightningCooldown <= 0) return 0;
            else return lightningCooldown;
        } 
    }
    public float getSpellCircleCooldown 
    {
        get 
        {
            if (spellCircleCooldown <= 0) return 0;
            else return spellCircleCooldown;
        }
    }
    public float getDefenseCooldown
    {
        get
        {
            if (defenseCooldown <= 0) return 0;
            else return defenseCooldown;
        }
    }
    public float getBlastCooldown
    {
        get
        {
            if (blastCooldown <= 0) return 0;
            else return blastCooldown;
        }
    }

    private void Awake()
    {
        staticController = this;
    }

    // initial behaviour
    private void Start() 
    {
        player = gameObject.GetComponent<PlayerController>();
        manaBar = ManaBar.staticBar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            LaunchProjectile();
            // Debug.Log("FIRE!");
        }
        ListenAbilities();
    }

    private void FixedUpdate()
    {
        // add UI request to show all this values in UI
        if (spellCooldown > 0) spellCooldown -= Time.fixedDeltaTime;
        if (lightningCooldown > 0) lightningCooldown -= Time.fixedDeltaTime;
        if (spellCircleCooldown > 0) spellCircleCooldown -= Time.fixedDeltaTime;
        if (defenseCooldown > 0) defenseCooldown -= Time.fixedDeltaTime;
        if (blastCooldown > 0) blastCooldown -= Time.fixedDeltaTime;
        
        GradualManaRestoration();
    }

    private void GradualManaRestoration()
    {
        if (manaBar.GetValue() < 1)
            manaBar.IncreaseValue( manaRegenValue );
            //manaBar.SetValue(manaBar.GetValue() + manaRegenValue / maxManaValue);
    }

    private void LaunchProjectile()
    {
        if (spellCooldown <= 0 && manaBar.GetValue() >= ManaSpellCost )
        {
            GameObject projectile = Instantiate(Resources.Load("Spell"), transform.position, Quaternion.identity) 
            as GameObject;
            projectile.GetComponent<Projectile>().SetDirection(player.LookDirection);
            manaBar.DecreaseValue( ManaSpellCost );
            spellCooldown = maxSpellCooldown;
        }
    }

    public void ListenAbilities()
    {
        // FIRST ABILITY (instant lighting)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (lightningCooldown <= 0 && manaBar.GetValue() >= ManaLightningCost)
            {
                // make UI request for storing number of remained spells 
                lightningCounter++;
            
                GameObject lightning = Instantiate( Resources.Load("Lightning"),
                transform.position, 
                Quaternion.AngleAxis( directionAngle, Vector3.forward ) ) as GameObject;

                lightning.transform.SetParent(this.gameObject.transform, false);
                lightning.transform.position = transform.position + (Vector3) player.LookDirection;

                manaBar.DecreaseValue(ManaLightningCost); // using mana for the first ability 
                if (lightningCounter == 3) 
                {
                    lightningCooldown = maxLightningCooldown;
                    lightningCounter = 0;
                }
            }
        }

        // SECOND ABILITY (circle of projectiles) 
        if (Input.GetKey(KeyCode.Alpha2))
        {
            // if the button is not pressed already and cooldown passed (alpha2Pressed == false)
            if (spellCircleCooldown <= 0 && alpha2Pressed != true && manaBar.GetValue() >= ManaSpellCircleCost )
            {
                alpha2Pressed = true; // fix pressing (button bressed) and implement code below one time
                float angle = 0;
                for (int i = 0; i < 8; i++)
                {
                    SpawnProjectileAsChild(angle, 12);
                    angle += 45;
                }

                manaBar.DecreaseValue(ManaSpellCircleCost); // using mana for the second ability
                spellCircleCooldown = maxSpellCircleCooldown;
                // Debug.Log("Implemented function of pressing");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            alpha2Pressed = false;
            LaunchSpellCircle();
        }

        // THIRD ABILITY (defense circle)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (defenseCooldown <= 0 && manaBar.GetValue() >= ManaDefenseCircleCost)
            {
                GameObject defenseCircle = Instantiate(Resources.Load("DefenceCircle"),
                transform.position, 
                Quaternion.identity) as GameObject;

                defenseCircle.transform.SetParent(this.gameObject.transform, false);
                defenseCircle.transform.position = transform.position;

                manaBar.DecreaseValue(ManaDefenseCircleCost); // using mana for third ability
                defenseCooldown = maxDefenseCooldown;
            }
        }

        // FOURTH ABILITY (ultra blast with projectiles)
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (blastCooldown <= 0 && manaBar.GetValue() >= ManaBlastCost)
            {
                while (blastCounter < 100)
                {
                    blastCounter++;
                    GameObject projectile = Instantiate(Resources.Load("Spell"), 
                    transform.position,
                    Quaternion.identity) as GameObject;

                    projectile.GetComponent<Projectile>().SetDirection(player.LookDirection);

                    projectile.transform.position += (Vector3) (player.LookDirection * blastCounter) 
                    + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
                
                    projectile.GetComponent<Projectile>().SetSpeed(15);
                }
                manaBar.DecreaseValue(ManaBlastCost); // using mana for fourth ability
                blastCooldown = maxBlastCooldown;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            blastCounter = 0;
        }
    

    }

    private void SpawnProjectileAsChild(float angle, int radius)
    {
        Vector2 spawnPosition = new Vector3();
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
        GameObject projectile = Instantiate(Resources.Load("ASpell"), transform.position, Quaternion.identity) 
        as GameObject;
        projectile.transform.SetParent(this.gameObject.transform, false);
        projectile.transform.position = transform.position + direction * radius;
    }

    // VULNERABLE SCRIPT, IF YOU ADD CHILD TO MAGE GameObj, CHAGE FOR CYCLE "i < NEW_VALUE"
    private void LaunchSpellCircle()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++) // can create two, becayse we have health bar
        {
            AProjectile aprojComponent = gameObject.transform.GetChild(i).GetComponent<AProjectile>();
            if ( aprojComponent != null)
                aprojComponent.SetDirection(player.LookDirection);
        }
    }

}

