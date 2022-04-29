using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderChase : SpiderBaseState
{
    public override void Start(SpiderStateMachine spider)
    { 

    }

    public override void EnterState(SpiderStateMachine spider)
    {
        spider.agent.acceleration = spider.chaseAcceleration;
        spider.agent.speed = spider.chaseSpeed;
        spider.StopEffects();
        Debug.Log("how many times does this enter");
        

    }

    // Update is called once per frame
    public override void UpdateState(SpiderStateMachine spider)
    {
        spider.agent.SetDestination(spider.player.position);
        //spider.transform.rotation = Quaternion.LookRotation(spider.agent.velocity);
        spider.transform.rotation = Quaternion.Slerp(spider.transform.rotation, Quaternion.LookRotation(spider.agent.velocity), Time.deltaTime);


    }

    public override void FixedUpdateState(SpiderStateMachine spider)
    {

    }
    public override void OnTriggerEnter(SpiderStateMachine spider, Collider collider)
    {

    }

    private void AngryChase(SpiderStateMachine spider)
    {
        spider.agent.acceleration = spider.chaseAcceleration + 12;
        spider.agent.acceleration = spider.chaseSpeed + 12;
    }

}

