using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WormISM : MonoBehaviour
{
    //Agent Stuff
    [Header("Agent and world")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask ground, whatIsPlayer;
    public float rotationSpeed = 50f;
    [Space(10)]

    //Patrolling
    [Header("Patrolling")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
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
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            searchWalkPoint();
        }

        else
        {
            //If the walkpoint is behind, look behind before going to the position.
            //if (Vector3.Dot(walkPoint, transform.forward) < 0)
            //{
            //    StartCoroutine(doRotationAtTargetDirection());
            //}

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


}
