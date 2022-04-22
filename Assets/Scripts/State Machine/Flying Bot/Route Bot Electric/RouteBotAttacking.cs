using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteBotAttacking : RouteBotElectricBaseState
{
    private float nextTimeToFire = 0f;
    public override void Start(RouteBotElectricStateMachine rbot)
    {

    }
    public override void EnterState(RouteBotElectricStateMachine rbot)
    {

    }
    public override void UpdateState(RouteBotElectricStateMachine rbot)
    {
        rbot.agent.SetDestination(rbot.transform.position);

        Quaternion rotation = Quaternion.LookRotation(rbot.player.transform.position - rbot.transform.position);
        rbot.transform.rotation = Quaternion.RotateTowards(rbot.transform.rotation, rotation, Time.deltaTime * rbot.rotationSpeed);

        //rbot.transform.LookAt(rbot.player.transform);

        //Attack the player
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / rbot.attackSpeed;
            //Attack(rbot);
            Electrify(rbot);
            rbot.player.GetComponent<PlayerHealth>().currentHealth -= rbot.damage;

        }
    }

    private void Electrify(RouteBotElectricStateMachine rbot)
    {
        //Particle Effect
        rbot.lightning.Play();

        //SFX
        rbot.zap.Play();

    }
}
