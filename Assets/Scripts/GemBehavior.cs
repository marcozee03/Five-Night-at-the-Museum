using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : InteractableObject
{
    public int Value = 100;
    public override void Interact(PlayerController player)
    {
        LevelManager.points += 200;
        Destroy(gameObject);
    }
}
