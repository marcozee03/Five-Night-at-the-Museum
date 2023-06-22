using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2;
    AudioSource chaseSFX;
<<<<<<< HEAD
    public AudioSource deadSFX;
=======
    AudioSource deadSFX;
>>>>>>> parent of cd97d8a9 (Please enter the commit message for your changes. Lines starting)
    NavMeshAgent myNavMeshAgent;
    public FSMStates currentState;
    public Animator anim;
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    public GameObject[] randomSpots;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        anim = GetComponent<Animator>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        currentState = FSMStates.Idle;
        //chaseSFX = GetComponent<AudioSource>();
        deadSFX = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        //moveRandom();
         randomSpots = GameObject.FindGameObjectsWithTag("Random");
    }
    void Update()
    {
        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
        }
        if (!LevelManager.isGameOver)
        {
            StateTransitions();
        }
    }
    private void LateUpdate()
    {
        switch (currentState)
        {
            //in late update so that head will turn and not be overwritten by animation
            case FSMStates.Chase:
                UpdateChaseState();
                break;
        }
    }
    #region Start States
    //Functions Called upon switching to respective state
    [Header("Speed Stats")]
    public float PatrolSpeed = 3;
    public float ChaseSpeed = 9;
    void StartPatrolState()
    {
        anim.SetInteger("animState", 2);
        myNavMeshAgent.speed = PatrolSpeed;
    }
    void StartChaseState()
    {
        anim.SetInteger("animState", 1);
        myNavMeshAgent.speed = ChaseSpeed;
    }
    void StartAttackState()
    {
<<<<<<< HEAD
        //
=======
>>>>>>> parent of cd97d8a9 (Please enter the commit message for your changes. Lines starting)
        transform.LookAt(player);
        Debug.Log("started Attack State");
        myNavMeshAgent.speed = 0;
        anim.SetInteger("animState", 3);
        //chaseSFX.Pause();
        currentState = FSMStates.Attack;
        player.GetComponent<PlayerController>().DisableMovement();
        StatTracker.hud.HideHUD();
        player.GetComponentInChildren<FirstPersonCamera>().DisableCameraMovement();
        FindAnyObjectByType<LevelManager>().LevelLost();
<<<<<<< HEAD
        deadSFX.Play();
=======
>>>>>>> parent of cd97d8a9 (Please enter the commit message for your changes. Lines starting)
    }
    #endregion

    #region StateBehaviors
    int index = 0;
    void UpdatePatrolState()
    {
        print("Patrolling!");
        //if(play)
        //{
        // chaseSFX.Pause();
        //     play = false;
        // }

        GameObject[] randomSpots = GameObject.FindGameObjectsWithTag("Random");
            GameObject thisSpot = randomSpots[index];
            if (!SameApproximateHorizontalLocation(thisSpot.transform.position, transform.position))
            {
                myNavMeshAgent.SetDestination(thisSpot.transform.position);
            }
            else
            {
                IncrementRandomSpotsIndex();
            }
        
    }
    public Transform Head;
    void UpdateChaseState()
    {
        print("Chasing!");
        Head.LookAt(player);
        myNavMeshAgent.SetDestination(player.position);
        CalculateHeat();
        
        //chaseSFX.PlayOneShot(chaseSFX.clip, 0.5f);
    }
    
    void UpdateAttackState()
    {
        player.transform.LookAt(Head);
<<<<<<< HEAD
        //transform.LookAt(player);
=======
        transform.LookAt(player);
>>>>>>> parent of cd97d8a9 (Please enter the commit message for your changes. Lines starting)
    }
    //Handles the cases when states should switch when state switches Start X state is called
    void StateTransitions()
    {
        FSMStates target;

        if (DetectPlayer() || currentHeat > 0)
        {
            target = FSMStates.Chase;
            if (TargetInAttackRange())
            {
                target = FSMStates.Attack;
            }
        }
        else
        {
            target = FSMStates.Patrol;
        }

        if (!target.Equals(currentState))
        {
            currentState = target;
            switch (currentState)
            {
                case FSMStates.Patrol:
                    StartPatrolState();
                    break;
                case FSMStates.Chase:
                    StartChaseState();
                    break;
                case FSMStates.Attack:
                    StartAttackState();
                    break;
                case FSMStates.Dead:
                    //UpdateDeadState();
                    break;
            }
        }
    }
    #endregion
    
    #region Vision
    [Header("Enemy Vision")]
    public float enemyFOV;
    public Transform eyes;
    [Min(0)] public float viewDistance;
    private bool TargetInAttackRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < attackRange;
    }
    private bool DetectPlayer()
    {
        Vector3 directionToTarget = player.transform.position - eyes.transform.position;
        if (Vector3.Angle(directionToTarget, eyes.transform.forward) <= enemyFOV / 2)
        {
            if (Physics.Raycast(eyes.transform.position, directionToTarget, out RaycastHit hit, viewDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region heat
    [Header("heat")]
    public float HeatTime = 3;
    private float currentHeat;
    void CalculateHeat()
    {
        if (DetectPlayer())
        {
            currentHeat = HeatTime;
        }else if(HeatTime > 0)
        {
            currentHeat -= Time.deltaTime;
        }
    }
    #endregion

    #region extra Utility Functions
    private bool SameApproximateHorizontalLocation(Vector3 vector, Vector3 other)
    {
        return Mathf.Approximately(vector.x, other.x) && Mathf.Approximately(vector.z, other.z);
    }
    private void IncrementRandomSpotsIndex()
    {
        index++;
        index %= randomSpots.Length;
    }
    private Vector3 RotateYaw(Vector3 original, float rotation)
    {
        float x, z;
        x = original.x * Mathf.Cos(Mathf.Deg2Rad * rotation) - original.z * Mathf.Sin(Mathf.Deg2Rad * rotation);
        z = original.x * Mathf.Sin(Mathf.Deg2Rad * rotation) + original.z * Mathf.Cos(Mathf.Deg2Rad * rotation);

        return new Vector3(x, original.y, z);
    }
    #endregion

    #region Editor

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(eyes.position, RotateYaw((eyes.transform.forward * viewDistance), enemyFOV / 2) + eyes.transform.position);
        Gizmos.DrawLine(eyes.position, RotateYaw((eyes.transform.forward * viewDistance), -enemyFOV / 2) + eyes.transform.position);
        Gizmos.DrawLine(eyes.position, (eyes.transform.forward * viewDistance) + eyes.transform.position);
    }
    #endregion

    #region old Code
    /* else if (LevelManager.distracted)
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
 }*/
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
    #endregion
}
