using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine : MonoBehaviour
{
    [SerializeField]
    TurretBaseState currentState; 

    public TurretShootingState shootingState = new TurretShootingState();
    public TurretIdleState idleState = new TurretIdleState();

    public GameObject player;
    public GameObject bullet;
    public GameObject bulletOrigin;
    
    public Transform gun;
    public float rotationSpeed = 1f;
    public float bulletRange;
    public float attackSpeed = 2f;
    public int damage;

    Quaternion idlePos;

    void Start()
    {
        //Set the player
        player = GameObject.FindWithTag("Player");

        //Starting states
        shootingState.Start(this);
        idleState.Start(this);

        //Set the starting state for the turret state machine.
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    public void SwitchState(TurretBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }




}
