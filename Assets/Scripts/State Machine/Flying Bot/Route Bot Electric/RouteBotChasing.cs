using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteBotChasing : RouteBotElectricBaseState
{
    public override void Start(RouteBotElectricStateMachine rbot)
    {

    }
    public override void EnterState(RouteBotElectricStateMachine rbot)
    {
        rbot.agent.speed = rbot.chasingSpeed;
        rbot.agent.acceleration = rbot.chasingAcceleration;
    }
    public override void UpdateState(RouteBotElectricStateMachine rbot)
    {
        rbot.agent.SetDestination(rbot.player.transform.position);

        Quaternion rotation = Quaternion.LookRotation(rbot.player.transform.position - rbot.transform.position);
        rbot.transform.rotation = Quaternion.RotateTowards(rbot.transform.rotation, rotation, Time.deltaTime * rbot.rotationSpeed);
    }

}
