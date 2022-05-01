using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteBotPatrollingState : RouteBotElectricBaseState
{
    float timeStamp;
    float goToNextWayPoint = 2f;

    public int waypointIndex;
    private float dist;

    public override void Start(RouteBotElectricStateMachine rbot)
    {
        waypointIndex = 0;
        rbot.transform.LookAt(rbot.waypoints[waypointIndex].position);
    }
    public override void EnterState(RouteBotElectricStateMachine rbot)
    {
        rbot.agent.speed = rbot.defaultSpeed;
        rbot.agent.acceleration = rbot.defaultAcceleration;
        rbot.StopEffects();
    }
    public override void UpdateState(RouteBotElectricStateMachine rbot)
    {
        dist = Vector3.Distance(rbot.transform.position, rbot.waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex(rbot);
        }
        Patrol(rbot);

        //If player is in sight range and not in attack range.
        if (rbot.playerInSightRange && !rbot.playerInAttackRange)
        {
            rbot.SwitchState(rbot.chasingState);
        }

        //If the player shoots at the bot.
        if (rbot.fEnemy.currentHealth < rbot.fEnemy.maxHealth)
        {
            rbot.SwitchState(rbot.chasingState);
        }
    }

    void Patrol(RouteBotElectricStateMachine rbot)
    {

        rbot.agent.SetDestination(rbot.waypoints[waypointIndex].position);
        Quaternion rotation = Quaternion.LookRotation(rbot.waypoints[waypointIndex].position - rbot.transform.position);

        rbot.transform.rotation = Quaternion.RotateTowards(rbot.transform.rotation, rotation, Time.deltaTime * 25f);

    }


    void IncreaseIndex(RouteBotElectricStateMachine rbot)
    {
        waypointIndex++;
        if (waypointIndex >= rbot.waypoints.Length)
        {
            waypointIndex = 0;
        }

        rbot.transform.LookAt(rbot.waypoints[waypointIndex].position);

    }
    void StartTimer()
    {
        //Time stamp because enumerator doesnt work on non monobehaviour scripts.
        timeStamp = Time.time + goToNextWayPoint;
    }
}

