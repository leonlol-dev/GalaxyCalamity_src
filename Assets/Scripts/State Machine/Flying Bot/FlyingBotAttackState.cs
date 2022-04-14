using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBotAttackState : FlyingBotBaseState
{
    private bool alreadyAttacked;
    private float nextTimeToFire = 0f;
    public override void Start(FlyingBotStateMachine fbot)
    {

    }
    public override void EnterState(FlyingBotStateMachine fbot)
    {

    }

    public override void UpdateState(FlyingBotStateMachine fbot)
    {
        fbot.agent.SetDestination(fbot.transform.position);

        fbot.transform.LookAt(fbot.player.transform);

        //Attack the player
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fbot.attackSpeed;
            Attack(fbot);
        }
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

    private void Attack(FlyingBotStateMachine fbot)
    {
        Rigidbody rb = GameObject.Instantiate(fbot.projectile, fbot.bulletOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(fbot.bulletOrigin.transform.forward * 34f, ForceMode.Impulse);
        rb.AddForce(fbot.bulletOrigin.transform.up * 8f, ForceMode.Impulse);


        GameObject.Destroy(rb, 1.5f);


    }

}
