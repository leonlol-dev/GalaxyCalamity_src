using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingBotStateMachine : MonoBehaviour
{
    public FlyingBotAttackState attackState = new FlyingBotAttackState();
    public FlyingBotFlyingState flyingState = new FlyingBotFlyingState();
    public FlyingBotChasingState chasingState = new FlyingBotChasingState();

    FlyingBotBaseState currentState;

    //Variables
    [Header("Variables to set")]
    public float walkPointRange;
    public float sightRange;
    public float attackRange;

    [Header("Speed")]
    public float defaultSpeed = 3.5f;
    public float chasingSpeed = 7f;
    public float rotationSpeed;
    

    [Header("Attacking")]
    public float attackSpeed;
    public float upTrajectory = 8f;
    public float projectileForce = 34f;
    [Space(10)]

    [Header("Game Objects to set")]
    public NavMeshAgent agent;
    public GameObject player;
    public GameObject projectile;
    public GameObject bulletOrigin;
    public AudioSource zap;
    [Space(10)]

    [Header("Layer Mask")]
    public LayerMask whatIsPlayer;
    [Space(10)]
    
    [Header("Debug")]
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public bool walkPointSet = false;
    public Vector3 walkPoint;


    // Start is called before the first frame update
    void Start()
    {
        //Find game sources without setting them.
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        zap = this.GetComponent<AudioSource>();

        //Initalise all starts on states.
        chasingState.Start(this);
        attackState.Start(this);
        flyingState.Start(this);

        //Set current state to the flying state. 
        currentState = flyingState;
        currentState.EnterState(this);  

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange)
        {
            SwitchState(flyingState);
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            SwitchState(chasingState);
        }

        if (playerInAttackRange && playerInSightRange)
        {
            SwitchState(attackState);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }


    public void SwitchState(FlyingBotBaseState state)
    {
        currentState = state;
        state.EnterState(this); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);


        //Walkpoint
        Gizmos.DrawSphere(walkPoint, 1);
        Gizmos.DrawLine(walkPoint, transform.position);

    }
}
