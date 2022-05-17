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
    [HideInInspector]
    public Enemy me;

    [Header("Game Objects")]
    public CinemachineSmoothPath path;
    public CinemachineDollyCart cart;
    public GameObject explosion;
    public AudioSource explosionSound;
    public GameManager gameManager;

    [Header("Variables")]
    public float sightRange;
    public float wanderRange = 2.3f;
    public int damage = 1;
    public float damageTick = 1f;
    public float startPosValue = 15f;
    public float endPosValue = 7f;

    [Header("Player layer")]
    public LayerMask whatIsPlayer;

    [Header("Debug")]
    public Vector3 startPosition;
    public Vector3 endPosition;
    public bool playerInSight;

    
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


        if (me.currentHealth <= 0)
        {
            Death();
        }
        
    }

    public void SwitchState(WormBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }


    private void FixedUpdate()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPosition, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPosition, 1);

    }

    //Since my states are not of monobehaviour class, I have to create a function to allow my states to use StartCoroutine.
    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private void Death()
    {
        gameManager.wormBossKilled = true;
        bool exploded = false;
        explosionSound.Play();
        Destroy(gameObject);

        if (!exploded)
        {
            GameObject iExplosion = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(iExplosion, 1.5f);
        }
    }
}
