using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


public class NoteBehavior : InteractableObject
{
    
    public GameObject note;
    private GameObject player;
    public float viewableDistance = 2.0f;
    void Update(){

        if(playerViewing && Vector3.Distance(player.transform.position, transform.position) > viewableDistance){
            note.SetActive(false);
            player = null;
        }

    }

    private bool playerViewing => player != null;
    public override void Interact(PlayerController player)
    {
        note.SetActive(true);
        this.player = player.gameObject;
    }
    override public string HoverTextMnK()
    {
        return "[F] to view note";
    }
}
