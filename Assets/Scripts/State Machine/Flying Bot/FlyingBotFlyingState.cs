using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBotFlyingState : FlyingBotBaseState
{




    public override void Start(FlyingBotStateMachine fbot)
    {
       
    }
    public override void EnterState(FlyingBotStateMachine fbot)
    {

    }

    public override void UpdateState(FlyingBotStateMachine fbot)
    {


        Debug.Log("patrolling updates");
        Patrolling(fbot);



    }
    public override void FixedUpdateState(FlyingBotStateMachine fbot)
    {

    }
    public override void OnCollisionEnter(FlyingBotStateMachine fbot, Collider collider)
    {

    }
    public override void OnTriggerEnter(FlyingBotStateMachine fbot, Collider other)
    {

    }
    public override void OnTriggerExit(FlyingBotStateMachine fbot, Collider other)
    {

    }

    void Patrolling(FlyingBotStateMachine fbot)
    {
        if(!fbot.walkPointSet)
        {
            SearchWalkPoint(fbot);
        }

        else
        {
            fbot.agent.SetDestination(fbot.walkPoint);
        }

        Vector3 distanceToWalkPoint = fbot.transform.position - fbot.walkPoint;
        fbot.transform.rotation = Quaternion.LookRotation(fbot.agent.velocity);

        if(distanceToWalkPoint.magnitude < 1f)
        {
            fbot.walkPointSet = false;
        }
    }

    void SearchWalkPoint(FlyingBotStateMachine fbot)
    {
        float randomZ = Random.Range(-fbot.walkPointRange, fbot.walkPointRange);
        float randomX = Random.Range(-fbot.walkPointRange, fbot.walkPointRange);

        fbot.walkPoint = new Vector3(fbot.transform.position.x + randomX, fbot.transform.position.y, fbot.transform.position.z + randomZ);

        RaycastHit hit;

        if (Physics.SphereCast(fbot.walkPoint, 2f, -fbot.transform.forward, out hit))
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject);
                fbot.walkPoint = new Vector3(fbot.transform.position.x + randomX, fbot.transform.position.y, fbot.transform.position.z + randomZ);
            }
        }
     
            

        fbot.walkPointSet = true;


    }

    private void OnDrawGizmosSelected(FlyingBotStateMachine fbot)
    {


        //Walkpoint
        Gizmos.DrawSphere(fbot.walkPoint, 1);
        Gizmos.DrawLine(fbot.walkPoint, fbot.transform.position);
    }
}

