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

    // Start is called before the first frame update
    void Start()
    {
        fourxDamageIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fourxDamageIsActive == true)
        {
            FourxdamageBuff();
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

        if (speedUpIsActive == true)
        {
            SpeedUpBuff();
        }

        if (invincibilityIsActive == true)
        {
            InvincibilityBuff();
        }
>>>>>>> 6e58b4681a9c3674477dccc3dd582c64a772b868
=======
        Debug.Log(fourxDamageIsActive);
>>>>>>> parent of 1efb7be (updated game manager and added a pick-up coin system displayed in the UI)
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
