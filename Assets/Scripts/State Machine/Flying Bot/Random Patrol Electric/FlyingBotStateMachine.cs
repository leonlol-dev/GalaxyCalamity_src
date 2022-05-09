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
    public float defaultAcceleration = 1;
    public float chasingSpeed = 7f;
    public float chasingAcceleration = 20f;
    public float rotationSpeed;
    

    [Header("Attacking")]
    public float attackSpeed;
    public int damage;
    [Space(10)]

    [Header("Game Objects to set")]
    public NavMeshAgent agent;
    public GameObject player;
    public GameObject projectile;
    public GameObject bulletOrigin;
    public AudioSource zap;
    public ParticleSystem lightning;
    public AudioSource deathSound;
    public GameObject explosion;
    [Space(10)]

    [Header("Layer Mask")]
    public LayerMask whatIsPlayer;
    [Space(10)]
    
    [Header("Debug")]
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public bool walkPointSet = false;
    public bool playerInside = false;
    public Vector3 walkPoint;

    public Enemy fEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Find game sources without setting them.
        agent = this.GetComponent<NavMeshAgent>();
        fEnemy = this.GetComponent<Enemy>();

        //NOTE: DON'T USE FINDGAMEOBJECTWITHTAG it doesn't work with build!
        //2ND NOTE: ACTUALLY JUST LOOKED IT UP I THINK I HAVE SOME OF THE PLAYER'S CHILDREN SET AS PLAYERS.
        player = GameObject.FindGameObjectWithTag("Player");

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



        if (!playerInSightRange && !playerInAttackRange)
        {
            SwitchState(flyingState);

        }

        //If player is in sight range and not in attack range.
        if (playerInSightRange && !playerInAttackRange)
        {
            SwitchState(chasingState);
        }

        if (playerInAttackRange && playerInSightRange)
        {
            SwitchState(attackState);
        }


        if (fEnemy.currentHealth <= 0)
        {
            Death();
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

    private void FixedUpdate()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
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
