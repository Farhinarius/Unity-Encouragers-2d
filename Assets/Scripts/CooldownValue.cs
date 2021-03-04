using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownValue: MonoBehaviour
{
    public Text[] cdValues;     // where text is gameObject;
    private void FixedUpdate() 
    {
        cdValues[0].text = MageController.staticController.getLightningCooldown.ToString();
        cdValues[1].text = MageController.staticController.getSpellCircleCooldown.ToString();
        cdValues[2].text = MageController.staticController.getDefenseCooldown.ToString();
        cdValues[3].text = MageController.staticController.getBlastCooldown.ToString();
    }
}
