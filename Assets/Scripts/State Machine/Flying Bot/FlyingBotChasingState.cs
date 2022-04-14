using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBotChasingState : FlyingBotBaseState
{
    public override void Start(FlyingBotStateMachine fbot)
    {

    }
    public override void EnterState(FlyingBotStateMachine fbot)
    {

    }

    public override void UpdateState(FlyingBotStateMachine fbot)
    {
        fbot.agent.SetDestination(fbot.player.transform.position);
        fbot.transform.rotation = Quaternion.LookRotation(fbot.agent.velocity);
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
}
