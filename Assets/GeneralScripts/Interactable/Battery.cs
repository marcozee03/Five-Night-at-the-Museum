using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : InteractableObject
{
    public override void Interact(PlayerController player)
    {
        player.gameObject.GetComponentInChildren<Flashlight>().spareBatteries += 1;
        
        Destroy(gameObject);
    }
    public override string HoverTextMnK()
    {
        return "[F] pickup Battery ";
    }
    public override string HoverTextController()
    {
        return "(Y) pickup Battery";
    }
}
