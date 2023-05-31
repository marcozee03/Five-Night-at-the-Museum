using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2;
    //public AudioClip enemySFX;
    Rigidbody rb;
    bool playerInView = false;
    public float detectionRange;
    bool isMoving;
    NavMeshAgent myNavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        myNavMeshAgent = GetComponent<NavMeshAgent>();
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
                //transform.position = Vector3.MoveTowards
                //    (transform.position, player.position, moveSpeed * Time.deltaTime);
                myNavMeshAgent.SetDestination(player.position);
                isMoving = true;
                if(PlayerBehavior.hiding)
                {
                    isMoving = false;
                    playerInView = false;
                    Debug.Log("continue patrol");
                    moveRandom();
                }
                //enemySFX.Play(); chase music
            }
            else if(LevelManager.distracted)
            {
                myNavMeshAgent.SetDestination(LevelManager.distractLoc);
                Debug.Log("distracted: " + transform.position + " going to " + LevelManager.distractLoc);
                isMoving = true;
                float distance = Vector3.Distance(transform.position, LevelManager.distractLoc);
                if(distance < 2)
                {
                    isMoving = false;
                    LevelManager.distracted = false;
                    Debug.Log("continue patrol");
                    moveRandom();
                }
            }
            else
            {
                //implement random behavior for patrolling
                moveRandom();
            }
        }
    }

    void Detected()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance < detectionRange)
        {
            Vector3 playerDirection = transform.position - player.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                playerInView = true;
            }
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
            {
                if(hit.transform.name == "Player")
                {
                    playerInView = true;
                }
            }
        }
    }

    int index = 0;
    void moveRandom()
    {
        GameObject[] randomSpots = GameObject.FindGameObjectsWithTag("Random");
        if(!isMoving)
        {
            GameObject thisSpot = randomSpots[index];
            if(transform.position.x != thisSpot.transform.position.x || transform.position.z != thisSpot.transform.position.z)
            {
                // transform.position = Vector3.MoveTowards
                //     (transform.position, thisSpot.transform.position, moveSpeed * Time.deltaTime);
                myNavMeshAgent.SetDestination(thisSpot.transform.position);
            }
            else 
            {
                index++;
            }
            if(index == randomSpots.Length)
            {
                index = 0;
            }
        }
    }
}
