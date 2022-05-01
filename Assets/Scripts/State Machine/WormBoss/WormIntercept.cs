using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormIntercept : WormBaseState
{
    public override void Start(WormStateMachine worm)
    {

    }

    public override void EnterState(WormStateMachine worm)
    {

    }

    public override void UpdateState(WormStateMachine worm)
    {
        // Less than half HP switch to chase state.
        if (worm.me.currentHealth >= worm.me.maxHealth / 2)
        {
            worm.SwitchState(worm.chaseState);
        }
    }

    public override void FixedUpdateState(WormStateMachine worm)
    {

    }

    public override void OnTriggerEnter(WormStateMachine worm, Collider collider)
    {

    }

    public void FindPath(WormStateMachine worm)
    {
        Vector3 playerPosition = worm.player.transform.position + (worm.player.GetComponent<PlayerMovement>().velocity * 1.25f);
        playerPosition.y = Mathf.Max(5, playerPosition.y);

        Vector3 randomRange = Random.insideUnitSphere * worm.wanderRange;
        Debug.Log(randomRange);
        randomRange.y = 0;

        worm.startPosition = playerPosition + randomRange;

        Vector3 randomRange2 = Random.insideUnitSphere * worm.wanderRange;
        Debug.Log(randomRange2);
        randomRange2.y = 0;

        worm.endPosition = playerPosition - randomRange2;

        worm.path.m_Waypoints[0].position = worm.startPosition + (Vector3.down * worm.startPosValue);
        worm.path.m_Waypoints[1].position = playerPosition + (Vector3.up * 10);
        worm.path.m_Waypoints[2].position = worm.endPosition + (Vector3.down * worm.endPosValue);

        worm.path.InvalidateDistanceCache();
        worm.cart.m_Position = 0;

        //worm.cart.m_Speed = worm.cart.m_Path.PathLength / 1500;

    }

    public void Move(WormStateMachine worm)
    {
        FindPath(worm);
        worm.StartChildCoroutine(FollowPath());
        IEnumerator FollowPath()
        {
            while (true)
            {
                //Wait til the position is nearly finished then wait for a little bit then find another path.
                yield return new WaitUntil(() => worm.cart.m_Position >= 0.99f);
                //yield return new WaitForSeconds(1f);

                FindPath(worm);
                yield return new WaitUntil(() => worm.cart.m_Position >= 0.05f);

            }
        }



    }



}
