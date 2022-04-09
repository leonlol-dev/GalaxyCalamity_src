using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : TurretBaseState
{
    Quaternion idlePos;
    public override void Start(TurretStateMachine turret)
    {
        idlePos = turret.transform.rotation;
        turret.playerFound = false;
    }

    public override void EnterState(TurretStateMachine turret)
    {
        turret.playerFound = false;

        Debug.Log("TURRET IDLE STATE");

        //Set turret rotation to the idle position.
        turret.transform.rotation = idlePos;

    }
    public override void UpdateState(TurretStateMachine turret)
    {

    }
    public override void OnCollisionEnter(TurretStateMachine turret, Collider collider)
    {
        
    }

    public override void OnTriggerEnter(TurretStateMachine turret, Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            turret.SwitchState(turret.shootingState);
        }
    }

    public override void FixedUpdateState(TurretStateMachine turret)
    {
            
    }

    public override void OnTriggerExit(TurretStateMachine turret, Collider other)
    {
       
    }

}
