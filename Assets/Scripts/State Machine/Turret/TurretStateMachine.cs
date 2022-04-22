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

    [HideInInspector]
    public GameObject player;

   
    

    [Header("Game Objects/Variables to set")]
    public GameObject bullet;
    public GameObject bulletOrigin;
    public GameObject explosion;
    public Transform gun;
    public AudioSource deathSound;
    public AudioSource shootSound;

    // ------------------------

    public float rotationSpeed = 1f;
    public float attackSpeed = 2f;
    public int damage;
    public bool playerFound;

    // ------------------------
    // Private
    Quaternion idlePos;
    private Enemy tEnemy;

    void Start()
    {
        //Set the player
        player = GameObject.FindWithTag("Player");

        tEnemy = this.GetComponent<Enemy>(); 

        deathSound = this.GetComponent<AudioSource>();

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

        if(tEnemy.currentHealth <= 0)
        {
            SwitchState(idleState);
            Death();
        }
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


    void Death()
    {
        bool exploded = false;
        deathSound.Play();
        Destroy(gameObject);

        if (!exploded)
        {
            GameObject iExplosion = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(iExplosion, 1.5f);
        }

    }

}
