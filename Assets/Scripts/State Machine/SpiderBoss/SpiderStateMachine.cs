using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderStateMachine : MonoBehaviour
{
    
    SpiderBaseState currentState;

    public SpiderWander wanderState = new SpiderWander();
    public SpiderChase chaseState = new SpiderChase();
    public SpiderAttack attackState = new SpiderAttack();

    [HideInInspector]
    public Transform player;

    [Header("Game Objects")]
    public NavMeshAgent agent;
    public LayerMask ground, whatIsPlayer;
    public float rotationSpeed = 50f;

    [Header("Rig")]
    public GameObject rig;
    public float rigUpAmount;
    public float bodyY;
    public GameObject body;

    [Header("Range")]
    public float walkPointRange;
    public float sightRange;
    public float attackRange;

    [Header("Patrolling")]
    public float patrollingSpeed = 4;
    public float patrollingAcceleration = 1;
    public float chaseSpeed = 11;
    public float chaseAcceleration = 11;

    [Header("Attacking")]
    public float attackSpeed;
    public int damage;

    [Header("Debug")]
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public bool walkPointSet = false;
    public Vector3 walkPoint;

    //Health
    private Enemy me;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        me = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();

        //Start the States
        wanderState.Start(this);
        chaseState.Start(this);
        attackState.Start(this);

        //Set the starting state.
        SwitchState(wanderState);

        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    public void SwitchState(SpiderBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
