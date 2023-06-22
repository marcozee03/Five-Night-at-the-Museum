using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    bool play = true;
    AudioSource jumpscare;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        jumpscare = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerInClearFOV() && play)
        {
            jumpscare.Play();
            play = false;
        }
    }

    [ContextMenu("jump")]
    public void jump()
    {
        //if(IsPlayerInClearFOV() && play)
        //{
            jumpscare.Play();
            play = false;
        //}
    }
    
    bool IsPlayerInClearFOV()
    {
        // RaycastHit hit;
        // Vector3 directionToPlayer = player.transform.position - transform.position;
        // if(Vector3.Angle(directionToPlayer, transform.forward) <= 45f)
        // {
        //     if(Physics.Raycast(transform.position, directionToPlayer, out hit, 15))
        //     {
        //         if(hit.collider.CompareTag("Player"))
        //         {
        //             print("Player in sight!");
        //             return true;
        //         }
        //         return false;
        //     }
        //     return false;
        // }
        // return false;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 15)
        {
            print("Player in sight!" );
            Vector3 playerDirection = transform.position - player.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            print("angle" + angle);
            //&& Mathf.Abs(angle) < 270
            if (Mathf.Abs(angle) < 90)
            {
                print("Player in sight!");
                return true;
            }
            // RaycastHit hit;
            // if (Physics.Raycast(transform.position, transform.forward, out hit, 15))
            // {
            //     if (hit.collider.gameObject.CompareTag("Player"))
            //     {
            //         print("Player in sight!");
            //         return true;
            //     }
            // }
            return true;
        }
        return false;
    }
}
