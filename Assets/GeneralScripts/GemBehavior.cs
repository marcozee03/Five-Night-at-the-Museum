using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : InteractableObject
{
    public int Value = 100;
    public AudioClip pointsSFX;
   
    public override void Interact(PlayerController player)
    {
        AudioSource.PlayClipAtPoint(pointsSFX, Camera.main.transform.position);
        HUDManager.points += Value;
        Destroy(gameObject);
        
        
    }
    override public string HoverTextMnK()
    {
        return "[F] to Collect [$" + Value + "]";
    }
}
