using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public static MageController staticController;
    private PlayerController player;
    public const float maxSpellCooldown = 0.3f;
    private float spellCooldown;
    public const float maxLightningCooldown = 2f;
    private float lightningCooldown;
    public const float maxSpellCircleCooldown = 4f;
    private float spellCircleCooldown;
    public const float maxDefenseCooldown = 8f;
    private float defenseCooldown;
    public const float maxBlastCooldown = 0.1f;
    private float blastCooldown;
    
    //temp 
    private int lightningCounter = 0;
    private int blastCounter = 0;
    private bool alpha2Pressed = false;
    public float directionAngle
    {
        get { return Mathf.Atan2(player.LookDirection.y, player.LookDirection.x) * Mathf.Rad2Deg; }
    }

    private void Start() 
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        
        // listen fire input
        if (Input.GetButton("Fire1"))
        {
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        if (spellCooldown <= 0)
        {
            GameObject projectile = Instantiate(Resources.Load("Spell"), transform.position, Quaternion.identity) 
            as GameObject;
            projectile.GetComponent<Projectile>().SetDirection(player.LookDirection);
            spellCooldown = maxSpellCooldown;
        }
    }

    public void ListenAbilities()
    {
        // FIRST ABILITY (instant lighting)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (lightningCooldown <= 0)
            {
                // make UI request for storing number of remained spells 
                lightningCounter++;
            
                GameObject lightning = Instantiate( Resources.Load("Lightning"),
                transform.position, 
                Quaternion.AngleAxis( directionAngle, Vector3.forward ) ) as GameObject;

                lightning.transform.SetParent(this.gameObject.transform, false);
                lightning.transform.position = transform.position + (Vector3) player.LookDirection;

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
            if (spellCircleCooldown <= 0 && alpha2Pressed != true) // if the button is not pressed already and cooldown passed
            {
                alpha2Pressed = true; // fix pressing (button bressed) and implement code below one time
                float angle = 0;
                for (int i = 0; i < 8; i++)
                {
                    SpawnProjectileAsChild(angle, 12);
                    angle += 45;
                }
                spellCircleCooldown = maxSpellCircleCooldown;
                Debug.Log("Implemented function of pressing");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            alpha2Pressed = false;
            LaunchSpellCircle();
        }

        // THIRD ABILITY (defense circle) add
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (defenseCooldown <= 0)
            {
                GameObject defenseCircle = Instantiate(Resources.Load("DefenceCircle"),
                transform.position, 
                Quaternion.identity) as GameObject;
                defenseCircle.transform.SetParent(this.gameObject.transform, false);
                defenseCircle.transform.position = transform.position;
                GetComponent<Rigidbody2D>().mass = 5000; //unmovable defense circle

                defenseCooldown = maxDefenseCooldown;
            }
        }

        // FOURTH ABILITY (ultra blast with projectiles)
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (blastCooldown <= 0)
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
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            blastCooldown = maxBlastCooldown;
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

