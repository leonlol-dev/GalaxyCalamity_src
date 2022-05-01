using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBotPatrol : FlyBotBaseState
{
    float timeStamp;
    float goToNextWayPoint = 2f;

    public int waypointIndex;
    private float dist;


    public override void Start(FlyBotStateMachineScripted fbot)
    {
        waypointIndex = 0;
        fbot.transform.LookAt(fbot.waypoints[waypointIndex].position);
    }
    public override void EnterState(FlyBotStateMachineScripted fbot)
    {

    }
    public override void UpdateState(FlyBotStateMachineScripted fbot)
    {
        dist = Vector3.Distance(fbot.transform.position, fbot.waypoints[waypointIndex].position); 
        if(dist < 1f)
        {
            IncreaseIndex(fbot);
        }
        Patrol(fbot);

        //If player is in sight range and not in attack range.
        if (fbot.playerInSightRange && !fbot.playerInAttackRange)
        {
            fbot.SwitchState(fbot.chaseState);
        }

        //If the player shoots at the bot.
        if (fbot.fEnemy.currentHealth < fbot.fEnemy.maxHealth)
        {
            fbot.SwitchState(fbot.chaseState);
        }    
    }
    public override void FixedUpdateState(FlyBotStateMachineScripted fbot)
    {

    }
    public override void OnCollisionEnter(FlyBotStateMachineScripted fbot, Collider collider)
    {

    }
    public override void OnTriggerEnter(FlyBotStateMachineScripted fbot, Collider other)
    {

    }
    public override void OnTriggerExit(FlyBotStateMachineScripted fbot, Collider other)
    {

    }

    void Patrol(FlyBotStateMachineScripted fbot)
    {
        //for (int i = 0; i < fbot.waypoints.Length; i++)
        //{
        //    if (timeStamp <= Time.time)
        //    { 
        //        fbot.agent.SetDestination(fbot.waypoints[i].transform.position);
        //        StartTimer();
        //    }

        //    if (i <= fbot.waypoints.Length)
        //    {
        //        i = 0;
        //    }

        //}

        fbot.agent.SetDestination(fbot.waypoints[waypointIndex].position);
        Quaternion rotation = Quaternion.LookRotation(fbot.waypoints[waypointIndex].position - fbot.transform.position);

        fbot.transform.rotation = Quaternion.RotateTowards(fbot.transform.rotation, rotation, Time.deltaTime * 100f);
        
    }  

    
    void IncreaseIndex(FlyBotStateMachineScripted fbot)
    {
        waypointIndex++;    
        if(waypointIndex >= fbot.waypoints.Length)
        {
            waypointIndex = 0;
        }

        fbot.transform.LookAt(fbot.waypoints[waypointIndex].position);

    }
    void StartTimer()
    {
        //Time stamp because enumerator doesnt work on non monobehaviour scripts.
        timeStamp = Time.time + goToNextWayPoint;
    }
}
