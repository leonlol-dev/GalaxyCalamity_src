using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBotChase : FlyBotBaseState
{
    public override void Start(FlyBotStateMachineScripted fbot)
    {

    }
    public override void EnterState(FlyBotStateMachineScripted fbot)
    {
        fbot.agent.speed = fbot.chasingSpeed;
    }
    public override void UpdateState(FlyBotStateMachineScripted fbot)
    {
        fbot.agent.SetDestination(fbot.player.transform.position);

        Quaternion rotation = Quaternion.LookRotation(fbot.player.transform.position - fbot.transform.position);
        fbot.transform.rotation = Quaternion.RotateTowards(fbot.transform.rotation, rotation, Time.deltaTime * fbot.rotationSpeed);
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
}
