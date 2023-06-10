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
    public float detectionRange = 5;
    bool isMoving;
    UnityEngine.AI.NavMeshAgent myNavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!HUDManager.isGameOver)
        {
            Detected();
            if (playerInView)
            {
                float distance = Vector3.Distance(transform.position, player.position);
                //Debug.Log("player in view");
                transform.LookAt(player);
                //transform.position = Vector3.MoveTowards
                //    (transform.position, player.position, moveSpeed * Time.deltaTime);
                myNavMeshAgent.SetDestination(player.position);
                isMoving = true;
                moveSpeed *= 3;
                gameObject.GetComponent<Animator>().SetInteger("animState", 1);
                if (PlayerBehavior.hiding)
                {
                    isMoving = false;
                    playerInView = false;
                    //Debug.Log("continue patrol");
                    moveRandom();
                }
                // else if(distance > detectionRange)
                // {
                //     playerInView = false;
                //     moveSpeed /= 3;
                // }
                //enemySFX.Play(); chase music
            }
            //else if (HUDManager.distracted)
            //{
            //    myNavMeshAgent.SetDestination(HUDManager.distractLoc);
            //    Debug.Log("distracted: " + transform.position + " going to " + HUDManager.distractLoc);
            //    isMoving = true;
            //    float distance = Vector3.Distance(transform.position, HUDManager.distractLoc);
            //    if (distance < 2)
            //    {
            //        isMoving = false;
            //        HUDManager.distracted = false;
            //        Debug.Log("continue patrol");
            //        moveRandom();
            //    }
            //}
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

    int index = 0;
    void moveRandom()
    {
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
}
