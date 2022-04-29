using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : SpiderBaseState
{
    private float nextTimeToFire = 0f;
    public override void Start(SpiderStateMachine spider)
    {

    }

    public override void EnterState(SpiderStateMachine spider)
    {
        MoveAndRotate(spider);
        spider.player.GetComponent<PlayerMovement>().speed = 1f;
    }

    // Update is called once per frame
    public override void UpdateState(SpiderStateMachine spider)
    {
        MoveAndRotate(spider);
        Flamethrower(spider);

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / spider.attackSpeed;

            spider.player.GetComponent<PlayerHealth>().currentHealth -= spider.damage;

        }
    }

    public override void FixedUpdateState(SpiderStateMachine spider)
    {

    }
    public override void OnTriggerEnter(SpiderStateMachine spider, Collider collider)
    {

    }

    public void Flamethrower(SpiderStateMachine spider)
    {
        spider.fire.Play();
        spider.fireSound.Play();
    }

    public void MoveAndRotate(SpiderStateMachine spider)
    {
        spider.agent.SetDestination(spider.transform.position);

        Quaternion rotation = Quaternion.LookRotation(spider.player.transform.position - spider.transform.position);
        //spider.transform.rotation = Quaternion.RotateTowards(spider.transform.rotation, rotation, Time.deltaTime);
        spider.transform.LookAt(spider.player);
    }


}
