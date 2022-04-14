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
    public float walkPointRange;
    public float sightRange;
    public float attackRange;
    public float attackSpeed;

    public NavMeshAgent agent;
    public GameObject player;
    public GameObject projectile;
    public GameObject bulletOrigin;
    public LayerMask whatIsPlayer;
    

    public bool playerInSightRange;
    public bool playerInAttackRange;


    //flying debugging
    public bool walkPointSet = false;
    public Vector3 walkPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        attackState.Start(this);
        flyingState.Start(this);

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
