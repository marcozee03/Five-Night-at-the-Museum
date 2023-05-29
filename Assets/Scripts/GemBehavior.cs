using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
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

     
        LevelManager.points += 200;

        Destroy(gameObject);
    }

}
    //can add OnDestroy if more functionality desired, but not needed in current state
}
