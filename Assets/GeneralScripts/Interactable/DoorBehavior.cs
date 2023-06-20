using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : InteractableObject
{

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
        if(LevelManager.clearConditionSatisfied){
            return "[F] to proceed to next level";
        }  
        else if(LevelManager.currentLevel == 1){
            return "Need Keycard to open";
        } 
        else if(LevelManager.currentLevel == 2){
            return "Enter passcode to open";
        }
        else{
            return "default string";
        }
    }
}
