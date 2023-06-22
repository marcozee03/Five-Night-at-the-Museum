using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : InteractableObject
{
    public string LockedMessage;
    public string UnlockedMessage = "proceed to next level";
public override void Interact(PlayerController player)
    {
        if(LevelManager.clearConditionSatisfied){
            gameObject.GetComponent<Animator>().SetTrigger("PlayerOpenDoor");
            FindObjectOfType<LevelManager>().LevelBeat();
            //play animation for door open 
            LevelManager.clearConditionSatisfied = false;

        }     
    }
    override public string HoverTextMnK()
    {
        if (LevelManager.clearConditionSatisfied)
        {
            return "[F] to proceed to next level";
        }
        else return LockedMessage;
    }
    public override string HoverTextController()
    {
        if (LevelManager.clearConditionSatisfied)
        {
            return "(Y)" + UnlockedMessage;
        }
        else return LockedMessage;
    }
}
