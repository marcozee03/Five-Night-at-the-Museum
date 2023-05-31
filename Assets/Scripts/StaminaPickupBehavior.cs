using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickupBehavior : InteractableObject
{
    public PlayerMove player;
    public float StaminaAmount = 30;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    override
    public void Interact(PlayerController player)
    {
            player.ReplenishStamina(StaminaAmount);
            Destroy(gameObject);
    }
    //can add OnDestroy if more functionality desired, but not needed in current state
}
