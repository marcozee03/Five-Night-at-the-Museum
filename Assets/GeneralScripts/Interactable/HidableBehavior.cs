using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidableBehavior : InteractableObject
{
    // Start is called before the first frame update
    public bool hiding;

    private Vector3 startingPlayerPos;
    private Quaternion startingPlayerRot;

    public override void Interact(PlayerController player)
    {
        if (!hiding)
        {
            startingPlayerPos = player.transform.position;
            startingPlayerRot = player.transform.rotation;

            player.DisableMovement();
            player.gameObject.transform.position = transform.position + new Vector3(0, -0.2f, -1f);
            player.transform.rotation = this.transform.rotation;
            hiding = true;
            player.OverrideInteraction(this);
        }
        else
        {
            player.Invoke("EnableMovement", Time.fixedDeltaTime);
            player.transform.position = startingPlayerPos;
            player.transform.rotation = startingPlayerRot;
            //player.gameObject.transform.position = transform.position + exitOffset;
            hiding = false;
        }
    }



    public override string HoverTextMnK()
    {
        if (!hiding)
        {
            return "[F] to Hide";
        }
        else return "[F] to Exit";
    }

    public override string HoverTextController()
    {
        if (!hiding)
        {
            return "(Y) to Hide";
        }
        else return "(Y) to Exit";
    }
}
