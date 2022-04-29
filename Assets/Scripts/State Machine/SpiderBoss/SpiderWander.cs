using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWander : SpiderBaseState
{
    Vector3 walkPoint;
    public override void Start(SpiderStateMachine spider)
    {

    }

    public override void EnterState(SpiderStateMachine spider)
    {
        spider.StopEffects();
    }

    public override void UpdateState(SpiderStateMachine spider)
    {
        Patrolling(spider);
    }

    public override void FixedUpdateState(SpiderStateMachine spider)
    {

    }
    public override void OnTriggerEnter(SpiderStateMachine spider, Collider collider)
    {

    }

    private void Patrolling(SpiderStateMachine spider)
    {
        spider.agent.speed = spider.patrollingSpeed;
        spider.agent.acceleration = spider.patrollingAcceleration;

        if(!spider.walkPointSet)
        {
            searchWalkPoint(spider);
        }

        else
        {
            spider.agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = spider.transform.position - walkPoint;
        spider.transform.rotation = Quaternion.LookRotation(spider.agent.velocity);

        if(distanceToWalkPoint.magnitude < 1f)
        {
            spider.walkPointSet = false;
        }

    }

    private void searchWalkPoint(SpiderStateMachine spider)
    {
        float randomZ = Random.Range(-spider.walkPointRange, spider.walkPointRange);
        float randomX = Random.Range(-spider.walkPointRange, spider.walkPointRange);

        walkPoint = new Vector3(spider.transform.position.x + randomX, spider.transform.position.y, spider.transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -spider.transform.up, 2f, spider.ground))
        {
            spider.walkPointSet = true;
        }

        
    }
}
