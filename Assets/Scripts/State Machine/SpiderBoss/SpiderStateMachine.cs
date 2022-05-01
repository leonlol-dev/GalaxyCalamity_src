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
    public SpiderGun gun;
    public ParticleSystem fire;
    public AudioSource fireSound;

    public GameObject explosion;
    public AudioSource explosionSound;

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
    public bool angry = false;
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

        if (!playerInSightRange && !playerInAttackRange)
        {
            SwitchState(wanderState);

        }

        if (playerInSightRange && !playerInAttackRange)
        {
            SwitchState(chaseState);
        }

        if (playerInAttackRange)
        {
            SwitchState(attackState);
        }

        //When HP gets to 75%
        if(me.currentHealth <= me.maxHealth / 1.35)
        {
            angry = true;
            chaseAcceleration = 25f;
            chaseSpeed = 25f;
        }

        //When HP gets to 50%
        if (me.currentHealth <= me.maxHealth / 2)
        {
            angry = true;
            chaseAcceleration = 44f;
            chaseSpeed = 44f;
        }

        if (me.currentHealth <= 0)
        {
            Death();
        }
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

    public void StopEffects()
    {
        fire.Stop();
        fireSound.Stop();
        player.GetComponent<PlayerMovement>().speed = player.GetComponent<PlayerMovement>().defaultSpeed;
    }

    private void Death()
    {
        bool exploded = false;
        explosionSound.Play();
        Destroy(gameObject);

        if (!exploded)
        {
            GameObject iExplosion = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(iExplosion, 1.5f);
        }
    }
}
