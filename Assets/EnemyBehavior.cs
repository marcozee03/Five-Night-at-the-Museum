using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2;
    //public AudioClip enemySFX;
    Rigidbody rb;
    bool playerInView = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelManager.isGameOver)
        {
            if(playerInView)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards
                (transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                //implement random behavior for patrolling
            }
            
        }
    }
}
