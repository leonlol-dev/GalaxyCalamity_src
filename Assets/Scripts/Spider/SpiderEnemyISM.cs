using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderEnemyISM : MonoBehaviour
{
    [Header("Set objects")]
    public GameObject grenade;
    public GameObject gun;
    [Space(10)]

    //Agent Stuff
    [Header("Agent and world")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask ground, whatIsPlayer;
    public float rotationSpeed = 50f;

    //Attacking
    public float attackSpeed;
    bool alreadyAttacked;

    [Space(10)]

    //Patrolling
    [Header("Patrolling")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float patrollingSpeed = 4;
    public float patrollingAcceleration = 1;

    public float chaseSpeed = 11;
    public float chaseAcceleration = 11;
    [Space(10)]

    ////States
    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    //Rig
    [Header("Rig")]
    public GameObject rig;
    public float rigUpAmount;

    public float bodyY;
    public GameObject body;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
      
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
    }

    private void Patrolling()
    {
        agent.speed = patrollingSpeed;
        agent.acceleration = patrollingAcceleration;

        if (!walkPointSet)
        {
            searchWalkPoint();
        }

        else 
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        transform.rotation = Quaternion.LookRotation(agent.velocity);

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;

        }

    }

    private void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
        {
            walkPointSet = true;
        }

    }

    IEnumerator doRotationAtTargetDirection()
    {
        //Debug.Log("walkpoint is behind spider");
        Vector3 dir = (walkPoint - transform.position).normalized;
        Quaternion look = Quaternion.LookRotation(dir);

        do
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        while (Quaternion.Angle(transform.rotation, look) > 0.01f);
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


    private void ChasePlayer()
    {
        agent.acceleration = chaseAcceleration;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        transform.rotation = Quaternion.LookRotation(agent.velocity);
        
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        gun.transform.LookAt(player);



        //transform.LookAt(player);

        //if (!alreadyAttacked)
        //{
        //    AudioSource.PlayClipAtPoint(arrowSFX, transform.position);
        //    Rigidbody rb = Instantiate(projectile, bulletSpawn.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //    rb.AddForce(transform.forward * 34f, ForceMode.Impulse);
        //    rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        //    alreadyAttacked = true;
        //    Invoke(nameof(ResetAttack), attackSpeed);

        //}

    }



}


////Agent stuff
//public NavMeshAgent agent;
//public Transform player;
//public LayerMask ground, whatIsPlayer;
//public GameObject projectile;
//public Enemy bowEnemy;

////Patrolling 
//public Vector3 walkPoint;
//public bool walkPointSet;
//public float walkPointRange;

////Attacking
//public float attackSpeed;
//bool alreadyAttacked;
//public GameObject bulletSpawn;

////States
//public float sightRange;
//public float attackRange;
//public bool playerInSightRange;
//public bool playerInAttackRange;

////Misc
//public GameObject explosionParticles;
//public AudioClip explosionSound;
//public AudioClip arrowSFX;

//private void Awake()
//{
//    player = GameObject.Find("Player").transform;
//    agent = GetComponent<NavMeshAgent>();
//}

//private void Update()
//{
//    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
//    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

//    if (!playerInSightRange && !playerInAttackRange)
//    {
//        Patrolling();
//    }

//    if (playerInSightRange && !playerInAttackRange)
//    {
//        ChasePlayer();
//    }

//    if (playerInAttackRange && playerInSightRange)
//    {
//        AttackPlayer();
//    }

//    if (bowEnemy.currentHealth <= 0)
//    {
//        Die();
//    }

//}

//private void Patrolling()
//{
//    if (!walkPointSet)
//    {
//        SearchWalkPoint();
//    }

//    if (walkPointSet)
//    {
//        agent.SetDestination(walkPoint);
//    }

//    Vector3 distanceToWalkPoint = transform.position - walkPoint;
//    transform.rotation = Quaternion.LookRotation(agent.velocity);

//    if (distanceToWalkPoint.magnitude < 1f)
//    {
//        walkPointSet = false;
//    }
//}

//private void SearchWalkPoint()
//{
//    float randomZ = Random.Range(-walkPointRange, walkPointRange);
//    float randomX = Random.Range(-walkPointRange, walkPointRange);

//    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

//    if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
//    {
//        walkPointSet = true;
//    }

//}

//private void ChasePlayer()
//{
//    agent.SetDestination(player.position);
//    transform.rotation = Quaternion.LookRotation(agent.velocity);
//}

//private void AttackPlayer()
//{
//    agent.SetDestination(transform.position);
//    bulletSpawn.transform.LookAt(player);



//    transform.LookAt(player);

//    if (!alreadyAttacked)
//    {
//        AudioSource.PlayClipAtPoint(arrowSFX, transform.position);
//        Rigidbody rb = Instantiate(projectile, bulletSpawn.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
//        rb.AddForce(transform.forward * 34f, ForceMode.Impulse);
//        rb.AddForce(transform.up * 8f, ForceMode.Impulse);

//        alreadyAttacked = true;
//        Invoke(nameof(ResetAttack), attackSpeed);

//    }

//}

//private void ResetAttack()
//{
//    alreadyAttacked = false;
//}

//private void OnDrawGizmosSelected()
//{
//    Gizmos.color = Color.red;
//    Gizmos.DrawWireSphere(transform.position, attackRange);
//    Gizmos.color = Color.yellow;
//    Gizmos.DrawWireSphere(transform.position, sightRange);
//}

//private void Die()
//{
//    Instantiate(explosionParticles, transform.position, transform.rotation);
//    AudioSource.PlayClipAtPoint(explosionSound, transform.position);
//    Destroy(gameObject);
//}

//}