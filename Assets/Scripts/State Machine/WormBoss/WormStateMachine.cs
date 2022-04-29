using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class WormStateMachine : MonoBehaviour
{
    WormBaseState currentState;

    public WormWander wanderState = new WormWander();
    public WormChase chaseState = new WormChase();
    public WormIntercept interceptState = new WormIntercept();

    [HideInInspector]
    public Transform player;

    [Header("Game Objects")]
    public CinemachineSmoothPath path;
    public CinemachineDollyCart cart;

    [Header("Variables")]
    public float walkPointRange;
    public float sightRange;
    public float attackRange;

    public float wanderRange = 2.3f;
    public int damage;
    public float damageTick = 1f;
    public float startPosValue = 15f;
    public float endPosValue = 7f;

    [Header("Debug")]
    public Vector3 startPosition;
    public Vector3 endPosition;


    private Enemy me;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        me = GetComponent<Enemy>();

        //Start the states
        wanderState.Start(this);
        chaseState.Start(this);
        interceptState.Start(this);

        //Set the current state
        SwitchState(wanderState);

        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        
    }

    public void SwitchState(WormBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPosition, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPosition, 1);

    }

    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
