using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            LevelManager.distractions += 1;
            Debug.Log("distractions: " + LevelManager.distractions);
            //collect sound clip?
            Destroy(gameObject);
        }
    }
}
