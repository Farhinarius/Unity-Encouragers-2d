using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public static MageController staticController;
    private PlayerController player;
    public const float maxSpellCooldown = 0.4f;
    private float spellCooldown;
    public const float maxLightningCooldown = 2f;
    private float lightningCooldown;
    private int counter = 0;
    public float directionAngle
    {
        get { return Mathf.Atan2(player.LookDirecton.y, player.LookDirecton.x) * Mathf.Rad2Deg; }
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
        if (spellCooldown >= 0) spellCooldown -= Time.fixedDeltaTime;
        if (lightningCooldown >= 0) lightningCooldown -= Time.fixedDeltaTime;

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
            projectile.GetComponent<Projectile>().SetDirection(player.LookDirecton);
            spellCooldown = maxSpellCooldown;
        }
    }

    public void ListenAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (lightningCooldown <= 0)
            {
                // call UI for storing number of remained spells
                counter++;
            
                GameObject lightning = Instantiate(Resources.Load("Lightning"),
                transform.position, 
                Quaternion.AngleAxis( directionAngle, Vector3.forward ) ) as GameObject;

                lightning.transform.SetParent(gameObject.transform, false);
                lightning.transform.position = transform.position + (Vector3) player.LookDirecton;

                if (counter == 3) 
                {
                    lightningCooldown = maxLightningCooldown;
                    counter = 0;
                }
            }
        }
    }

}

