using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : InteractableObject
{

    public float batteryAmount = 5;
    override
    public void Interact(PlayerController player)
    {
        player.gameObject.GetComponentInChildren<FlashlightBehavior>().batteryLife += this.batteryAmount;
        Destroy(gameObject);
    }
    public override string HoverTextMnK()
    {
        return "[F] pickup Battery [" + batteryAmount * 10 + "%]";
    }
}
