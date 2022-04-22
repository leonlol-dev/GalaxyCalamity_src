using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretShootingState : TurretBaseState
{
    private bool alreadyAttacked;
    private float nextTimeToFire = 0f;
    private bool canAttack;
    public override void Start(TurretStateMachine turret)
    {
        alreadyAttacked = false;
    }
    public override void EnterState(TurretStateMachine turret)
    {
        Debug.Log("turret shoot player");
        turret.playerFound = true;
    }
    public override void UpdateState(TurretStateMachine turret)
    {
        //Attack the player
        if (Time.time >= nextTimeToFire && turret.playerFound)
        {
            nextTimeToFire = Time.time + 1f / turret.attackSpeed;
            Attack(turret);
        }



    }
    public override void OnCollisionEnter(TurretStateMachine turret, Collider collider)
    {

    }

    public override void OnTriggerEnter(TurretStateMachine turret, Collider other)
    {

    }

    public override void OnTriggerExit(TurretStateMachine turret, Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            turret.SwitchState(turret.idleState);
        }
    }
        
    public override void FixedUpdateState(TurretStateMachine turret)
    {
        Quaternion rotation = Quaternion.LookRotation(turret.player.transform.position - turret.gun.transform.position);

        //turret.gun.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, Time.deltaTime * turret.rotationSpeed);
        turret.gun.rotation = Quaternion.RotateTowards(turret.gun.transform.rotation, rotation, Time.deltaTime * turret.rotationSpeed);
    }

    private void Attack(TurretStateMachine turret)
    {
        turret.shootSound.Play();
        Rigidbody rb = GameObject.Instantiate(turret.bullet, turret.bulletOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(turret.gun.transform.forward * 34f, ForceMode.Impulse);
        rb.GetComponent<TurretBullet>().damage = turret.damage;
 



        GameObject.Destroy(rb, 1.5f);

        



    }


}





