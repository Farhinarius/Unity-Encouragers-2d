using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public static BuffManager BuffController;
    
    public bool fourxDamageIsActive;
    private float fourxdamage;

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
        Debug.Log(fourxDamageIsActive);
    }




    private void FourxdamageBuff()
    {
        if (fourxdamage < 5f)
        {
            fourxdamage += Time.deltaTime;
        }
        else
        {
            PlayerController.staticController.damageModifier = 1;
            fourxdamage = 0f;
            fourxDamageIsActive = false;
        }
    }


}
