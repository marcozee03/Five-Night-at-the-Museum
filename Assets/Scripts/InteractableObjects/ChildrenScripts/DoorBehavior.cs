using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : InteractableObject
{

public override void Interact(PlayerController player)
    {
        if(LevelManager.hasLevel1Keycard){
            gameObject.GetComponent<Animator>().SetTrigger("PlayerOpenDoor");
            FindObjectOfType<LevelManager>().LevelBeat();
            //play animation for door open 

        }
        
        
      
        
        
    }
    override public string HoverTextMnK()
    {
        if(LevelManager.hasLevel1Keycard){
            return "[F] to proceed to next level";
        }  
        else{
            return "Need Keycard to open";
        } 
    }
}
