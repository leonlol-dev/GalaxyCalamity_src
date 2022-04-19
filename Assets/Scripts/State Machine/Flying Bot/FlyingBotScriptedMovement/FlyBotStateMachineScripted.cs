using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyBotStateMachineScripted : MonoBehaviour
{
    //States
    public FlyBotPatrol patrolState = new FlyBotPatrol();   
    public FlyBotChase chaseState = new FlyBotChase();
    public FlyBotAttack attackState = new FlyBotAttack();

    FlyBotBaseState currentState;

    //Variables to set.
    [Header("Variables to set")]
    public float attackRange;
    public float sightRange;

    [Header("Attacking")]
    public float attackSpeed;
    public float upTrajectory = 8f;
    public float projectileForce = 34f;

    [Header("Speed")]
    public float defaultSpeed;
    public float chasingSpeed;
    public float rotationSpeed;

    [Space(10)]

    //Game Objects
    [Header("Game Objects to set")]

    [Space(10)]

    public Transform[] waypoints;
    public GameObject projectile;
    public GameObject bulletOrigin;

    [Space(10)]

    //Game Objects that don't need to be set.
    [Header("Game Objects not needed to be set.")]
    public NavMeshAgent agent;
    public GameObject player;
    public AudioSource zap;

    [Space(10)]

    //Layer mask
    [Header("Layer Mask")]
    public LayerMask whatIsPlayer;

    [Space(10)]

    //Debug
    [Header("Debug")]
    public bool playerInSightRange;
    public bool playerInAttackRange;


    private void Start()
    {
        //Find game objects without setting them.
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        zap = this.GetComponent<AudioSource>();

        patrolState.Start(this);
        chaseState.Start(this);
        attackState.Start(this);

        currentState = patrolState;
        currentState.EnterState(this);

    }

    private void Update()
    {
        currentState.UpdateState(this);

        if(!playerInSightRange && !playerInAttackRange)
        {
            SwitchState(patrolState);
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            SwitchState(chaseState);
        }

        if(playerInAttackRange && playerInSightRange)
        {
            SwitchState(attackState);
        }

       
        
    }

    public void SwitchState(FlyBotBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void FixedUpdate()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);


        //Walkpoint
        Gizmos.DrawLine(waypoints[patrolState.waypointIndex].position, transform.position);

    }
}
  