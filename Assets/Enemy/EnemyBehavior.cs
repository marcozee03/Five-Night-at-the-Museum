using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2;
    AudioSource chaseSFX;
    AudioSource deadSFX;
    Rigidbody rb;
    bool playerInView = false;
    public float detectionRange = 5;
    bool isMoving;
    NavMeshAgent myNavMeshAgent;
    public FSMStates currentState;
    bool play = true;
    bool play2 = true;

    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        currentState = FSMStates.Patrol;
        chaseSFX = GetComponent<AudioSource>();
        deadSFX = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        //moveRandom();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                //UpdateDeadState();
                break;
        }
        if(!LevelManager.isGameOver)
        {
            Detected();
            if (playerInView)
            {
                currentState = FSMStates.Chase;
            }
            else if (LevelManager.distracted)
            {
               myNavMeshAgent.SetDestination(LevelManager.distractLoc);
               //Debug.Log("distracted: " + transform.position + " going to " + LevelManager.distractLoc);
               isMoving = true;
               float distance = Vector3.Distance(transform.position, LevelManager.distractLoc);
               if (distance < 2)
               {
                   isMoving = false;
                   LevelManager.distracted = false;
                   //Debug.Log("continue patrol");
                   //moveRandom();
                   currentState = FSMStates.Patrol;
               }
            }
            else
            {
                //implement random behavior for patrolling
                //moveRandom();
                currentState = FSMStates.Patrol;
            }
        }
        else
        {
            //GameObject.FindObjectOfType
            currentState = FSMStates.Attack;
        }
    }

    int index = 0;
    void UpdatePatrolState()
    {
        print("Patrolling!");
        //if(play)
        //{
            chaseSFX.Pause();
        //     play = false;
        // }
        play = true;
        play2 = true;
        gameObject.GetComponent<Animator>().SetInteger("animState", 2);
        GameObject[] randomSpots = GameObject.FindGameObjectsWithTag("Random");
        if (!isMoving)
        {
            GameObject thisSpot = randomSpots[index];
            if (transform.position.x != thisSpot.transform.position.x || transform.position.z != thisSpot.transform.position.z)
            {
                // transform.position = Vector3.MoveTowards
                //     (transform.position, thisSpot.transform.position, moveSpeed * Time.deltaTime);
                myNavMeshAgent.SetDestination(thisSpot.transform.position);
                Debug.Log("pos: " + transform.position + "to pos" + thisSpot.transform.position);
            }
            else
            {
                index++;
            }
            if (index == randomSpots.Length)
            {
                index = 0;
            }
        }
    }

    void UpdateChaseState()
    {
        print("Chasing!");
        float distance = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);
        //transform.position = Vector3.MoveTowards
        //    (transform.position, player.position, moveSpeed * Time.deltaTime);
        myNavMeshAgent.SetDestination(player.position);
        isMoving = true;
        //moveSpeed *= 3;
        myNavMeshAgent.speed = 7;
        gameObject.GetComponent<Animator>().SetInteger("animState", 1);
        play2 = true;
        if(play)
        {
            chaseSFX.Play();
            play = false;
        }
        //chaseSFX.PlayOneShot(chaseSFX.clip, 0.5f);
        if (PlayerBehavior.hiding)
        {
            isMoving = false;
            playerInView = false;
            //Debug.Log("continue patrol");
            //moveRandom();
            chaseSFX.Pause();
            currentState = FSMStates.Patrol;
        }
        else if(distance > detectionRange)
        {
            playerInView = false;
            myNavMeshAgent.speed = 3;
            //moveRandom();
            chaseSFX.Pause();
            currentState = FSMStates.Patrol;
        }
        if(distance <= 2)
        {
            myNavMeshAgent.speed = 0;
            gameObject.GetComponent<Animator>().SetInteger("animState", 3);
            FindObjectOfType<LevelManager>().LevelLost();
            chaseSFX.Pause();
            currentState = FSMStates.Attack;
        }
    }

    void UpdateAttackState()
    {
        print("Attack!");
        myNavMeshAgent.speed = 0;
        if(play2)
        {
            deadSFX.Play();
            play2 = false;
        }
        play = true;
        gameObject.GetComponent<Animator>().SetInteger("animState", 3);
        FindObjectOfType<LevelManager>().LevelLost();
    }

    void Detected()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRange)
        {
            Vector3 playerDirection = transform.position - player.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                playerInView = true;
            }
            RaycastHit hit;
            playerInView = true;
            if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    playerInView = true;
                }
            }
        }
    }

    
    // void moveRandom()
    // {
        
    // }

    // bool IsPlayerInClearFOV()
    // {
    //     RaycastHit hit;
    //     Vector3 directionToPlayer = player.transform.position - enemyEyes.position;
    //     if(Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
    //     {
    //         if(Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
    //         {
    //             if(hit.collider.CompareTag("Player"))
    //             {
    //                 print("Player in sight!");
    //                 return true;
    //             }
    //             return false;
    //         }
    //         return false;
    //     }
    //     return false;
    // }
}
