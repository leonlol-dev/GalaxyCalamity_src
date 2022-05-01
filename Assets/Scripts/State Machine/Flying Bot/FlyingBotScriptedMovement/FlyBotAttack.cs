using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBotAttack : FlyBotBaseState
{
    private bool alreadyAttacked;
    private float nextTimeToFire = 0f;
    public override void Start(FlyBotStateMachineScripted fbot)
    {

    }
    public override void EnterState(FlyBotStateMachineScripted fbot)
    {

    }

    public override void UpdateState(FlyBotStateMachineScripted fbot)
    {
        fbot.agent.SetDestination(fbot.transform.position);

        Quaternion rotation = Quaternion.LookRotation(fbot.player.transform.position - fbot.transform.position);
        fbot.transform.rotation = Quaternion.RotateTowards(fbot.transform.rotation, rotation, Time.deltaTime * fbot.rotationSpeed);

        //fbot.transform.LookAt(fbot.player.transform);

        //Attack the player
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fbot.attackSpeed;
            Attack(fbot);
        }

        if (!fbot.playerInSightRange && !fbot.playerInAttackRange)
        {
            fbot.SwitchState(fbot.patrolState);
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

    private void Attack(FlyBotStateMachineScripted fbot)
    {
        Rigidbody rb = GameObject.Instantiate(fbot.projectile, fbot.bulletOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(fbot.bulletOrigin.transform.forward * fbot.projectileForce, ForceMode.Impulse);
        rb.AddForce(fbot.bulletOrigin.transform.up * fbot.upTrajectory, ForceMode.Impulse);

        //play sound
        fbot.zap.Play();

        GameObject.Destroy(rb, 1.5f);


    }
}
