using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RouteBotElectricStateMachine : MonoBehaviour
{
    //States
    public RouteBotPatrollingState patrolState = new RouteBotPatrollingState();
    public RouteBotChasing chasingState = new RouteBotChasing();
    public RouteBotAttacking attackingState = new RouteBotAttacking();

    RouteBotElectricBaseState currentState;

    public Enemy fEnemy;

    //Variables to set.
    [Header("Variables to set")]
    public float attackRange;
    public float sightRange;

    [Header("Attacking")]
    public float attackSpeed;
    public int damage;

    [Header("Speed")]
    public float defaultSpeed;
    public float defaultAcceleration;
    public float chasingSpeed;
    public float chasingAcceleration;
    public float rotationSpeed;

    [Space(10)]

    //Game Objects
    [Header("Game Objects to set")]

    [Space(10)]

    public Transform[] waypoints;
    public GameObject explosion;
    public AudioSource zap;
    public AudioSource deathSound;
    public ParticleSystem lightning;



    [Space(10)]

    //Game Objects that don't need to be set.
    [Header("Game Objects not needed to be set.")]
    public NavMeshAgent agent;
    public GameObject player;


    [Space(10)]

    //Layer mask
    [Header("Layer Mask")]
    public LayerMask whatIsPlayer;

    [Space(10)]

    //Debug
    [Header("Debug")]
    public bool playerInSightRange;
    public bool playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        //Find game objects without setting them.
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        fEnemy = this.GetComponent<Enemy>();


        patrolState.Start(this);
        chasingState.Start(this);
        attackingState.Start(this);

        currentState = patrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);


        if (playerInAttackRange && playerInSightRange)
        {
            SwitchState(attackingState);
        }

        if(fEnemy.currentHealth <= 0)
        {
            Death();
        }
    }

    public void SwitchState(RouteBotElectricBaseState state)
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

    public void StopEffects()
    {
        //damageBox.SetActive(false);
        lightning.Stop();
    }

    void Death()
    {
        bool exploded = false;
        deathSound.Play();
        Destroy(gameObject);

        if (!exploded)
        {
            GameObject iExplosion = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(iExplosion, 1.5f);
        }

    }
}
