using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public static bool hiding;
    void Start()
    {
        hiding = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // enemyHit ++;
            // if(enemyHit == 2)
            // {
            //     //LevelManager lm = FindObjectOfType<LevelManager>();
            //     //lm.LevelLost();
            //     Destroy(gameObject);
            // }
            //FindObjectOfType<LevelManager>().LevelLost();
            //Destroy(gameObject);   
        }
    }
}
