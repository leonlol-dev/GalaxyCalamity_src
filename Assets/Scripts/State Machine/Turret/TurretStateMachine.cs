using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine : MonoBehaviour
{
    [SerializeField]
    TurretBaseState currentState; 

    TurretShootingState shootingState = new TurretShootingState();
    TurretIdleState idleState = new TurretIdleState();

    void Start()
    {
        //Set the starting state for the turret state machine.
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        
    }
}
