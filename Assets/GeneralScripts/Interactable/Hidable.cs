using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidable : InteractableObject
{
    // Start is called before the first frame update
    private bool playerInside;
    public Vector3 tpOffset;
    public Vector3 exitOffset;
    public override void Interact(PlayerController player)
    {
        if (!playerInside)
        {
            player.DisableMovement = true;
            player.gameObject.transform.position = transform.position + tpOffset;
            playerInside = true;
        }
        else
        {
            player.Invoke("EnableMovement", .5f);
            player.gameObject.transform.position = transform.position + exitOffset;
            playerInside = false;
        }
    }



    public override string HoverTextMnK()
    {
        if (!playerInside)
        {
            return "[F] to Hide";
        }
        else return "[F] to Exit";
    }
}
