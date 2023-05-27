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
    public float detectionRange;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
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
            Detected();
            if(playerInView)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards
                    (transform.position, player.position, moveSpeed * Time.deltaTime);
                isMoving = true;
                //enemySFX.Play();
            }
            else
            {
                //implement random behavior for patrolling
                moveRandom();
            }
        }
    }

    bool Detected()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance < detectionRange)
        {
            Vector3 playerDirection = transform.position - player.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                //playerInView = true;
            }
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if(hit.transform.name == "Player")
                {
                    //playerInView = true;
                }
            }
        }
    }

    void moveRandom()
    {
        GameObject[] randomSpots = FindGameObjectsWithTag("Random");
        //GameObject[] listOfRandom = randomSpots; //does this change original list?
        int index = 0;
        if(!isMoving)
        {
            isMoving = true;
            GameObject thisSpot = randomSpots[index];
            transform.position = Vector3.MoveTowards
                    (transform.position, thisSpot.transform.position, moveSpeed * Time.deltaTime);
            index++;
            if(index == randomSpots.Length)
            {
                index = 0;
            }
        }
    }
}
