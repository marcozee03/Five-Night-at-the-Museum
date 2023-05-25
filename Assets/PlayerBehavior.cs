using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    AudioSource distractSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        distractSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(!LevelManager.isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                if(LevelManager.distractions > 0)
                {
                    Distract();
                }
            }
        }
        else
        {
            // stops the player when the game is over
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<LevelManager>().LevelLost();
            Destroy(gameObject);
        }
    }
    
    void Distract()
    {
        distractSFX.Play();
        //player throws item
    }
    
}
