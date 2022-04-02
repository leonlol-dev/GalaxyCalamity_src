using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretBaseState
{
    public override void EnterState(TurretStateMachine turret)
    {
        Debug.Log("TURRET IDLE STATE");
    }
    public override void UpdateState(TurretStateMachine turret)
    {

    }
    public override void OnCollisionEnter(TurretStateMachine turret, Collider collider)
    {
        if (collider == null)
        {
            return;
        }
    }

    public override void OnTriggerEnter(TurretStateMachine turret, Collider other)
    {

    }

}
