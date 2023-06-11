using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


public class NoteBehavior : InteractableObject
{
    
    public GameObject note;
    public GameObject player;
    public float viewableDistance = 2.0f;
    void Update(){

        if(Vector3.Distance(player.transform.position, transform.position) > viewableDistance){
            note.SetActive(false);
        }

    }
   
    public override void Interact(PlayerController player)
    {
        note.SetActive(true);

       
        
    }
    override public string HoverTextMnK()
    {
        return "[F] to view note";
    }
}
