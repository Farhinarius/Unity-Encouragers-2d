using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    private PlayerController player;
    private bool dashProcessing;
    private float dashTime;
    private ManaBar staminaBar;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        staminaBar = GetComponentInChildren<ManaBar>();
    }

    // Update is called once per frame

    private void FixedUpdate() {
        ListenDash();
        GradualStaminaRestoration();
    }

    private void GradualStaminaRestoration()
    {
        if (staminaBar.GetValue() < 1)
            staminaBar.IncreaseValue(0.01f / 100f);
    }

    private void ListenDash()
    {
        if ((Input.GetButtonDown("Fire2")) && dashProcessing == false && staminaBar.GetValue() >= 10f / 100f)
        {
            Debug.Log("Dash");
            dashProcessing = true;
            staminaBar.DecreaseValue(10f / 100f);
            dashTime = 0f;

        }
        if (dashProcessing)
        {
            Debug.Log("Dashed");
            player.needDash = true;
            dashTime += Time.fixedDeltaTime;
            if (dashTime >= 0.1f)
            {
                dashProcessing = false;
                player.needDash = false;
            }
        }
    }
}
