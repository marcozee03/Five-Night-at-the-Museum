using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickupBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){

            if(LevelManager.stamina < 100)
                if(LevelManager.stamina + 30 > 100){
                    LevelManager.stamina = 100;
                }
                else{
                    LevelManager.stamina += 30;
                }

            Destroy(gameObject);
        }
}
    //can add OnDestroy if more functionality desired, but not needed in current state
}
