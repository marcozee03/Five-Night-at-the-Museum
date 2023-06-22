using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickupBehavior : InteractableObject
{
    public float StaminaAmount = 30;
    override
    public void Interact(PlayerController player)
    {
            player.ReplenishStamina(StaminaAmount);
            Destroy(gameObject);
    }
    //can add OnDestroy if more functionality desired, but not needed in current state
}
