using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public static BuffManager BuffController;
    
    public bool fourxDamageIsActive;
    private float fourxDamage;
    public bool speedUpIsActive;
    private float speedUp;
    public bool invincibilityIsActive;
    private float invincibility;
    private Transform damageObject;
    private Transform speedObject;
    private Transform invincibilityObject;

    // Start is called before the first frame update
    void Start()
    {
        damageObject = transform.Find("OverlayUI/Buff Canvas/damage");
        speedObject = transform.Find("OverlayUI/Buff Canvas/speed");
        invincibilityObject = transform.Find("OverlayUI/Buff Canvas/invincibility");

    }

    // Update is called once per frame
    void Update()
    {
        if (fourxDamageIsActive == true)
        {
            damageObject.GetComponent<BuffTest>().recoveryState = true;
            FourxdamageBuff();
        }

        if (speedUpIsActive == true)
        {
            speedObject.GetComponent<BuffTest>().recoveryState = true;
            SpeedUpBuff();
        }

        if (invincibilityIsActive == true)
        {
            InvincibilityBuff();
            invincibilityObject.GetComponent<BuffTest>().recoveryState = true;
        }
    }




    private void FourxdamageBuff()
    {
        if (fourxDamage < 7f)
        {
            fourxDamage += Time.deltaTime;
        }
        else
        {
            PlayerController.staticController.damageModifier = 1;
            fourxDamage = 0f;
            fourxDamageIsActive = false;

        }
    }

    private void SpeedUpBuff()
    {
        if (speedUp < 10f)
        {
            speedUp += Time.deltaTime;
        }
        else
        {
            PlayerController.staticController.speedModifier = 1;
            speedUp = 0f;
            speedUpIsActive = false;
        }
    }

    private void InvincibilityBuff()
    {
        if (invincibility < 10f)
        {
            invincibility += Time.deltaTime;
        }
        else
        {
            GetComponent<DamageablePlayer>().invincibleTimer = 0f;
            invincibility = 0f;
            invincibilityIsActive = false;
        }
    }
}
