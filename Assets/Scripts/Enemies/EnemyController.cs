using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
 public float moveSpeed;
    public float detectionRange;
    public float hitCooldown = 2f;
    public int damage = 1;
    public float pushForce;

    public GameObject player;
    private Transform playerPosition;
    public NavMeshAgent navMeshAgent;


    #region StateMachine
    private EnemyStateMachine stateMachine;

    public ChasingState chasingState{get; private set;}
    public IdleState idleState{get; private set;}
    private EnemyWaitState waitState;
    #endregion

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        // Inicializa el navmesh
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        stateMachine = new EnemyStateMachine();
        // Aqui agregué el nav mesh
        chasingState = new ChasingState(stateMachine,this, navMeshAgent);
        waitState = new EnemyWaitState(stateMachine,this);
        idleState = new IdleState(stateMachine,this);

        stateMachine.Initializa(idleState);
    }

    void Update()
    {
        stateMachine.CurrentState.FrameUpdate();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent( out PlayerHealth playerHealth)){
                playerHealth.TakeDamage(damage);
                // Empujar al jugador
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                playerHealth.PushBack(pushDirection, pushForce);
                StartCoroutine(WaitAfterHit());
                stateMachine.ChangeState(waitState);
            }
        }
    }

    public IEnumerator WaitAfterHit()
    {
        stateMachine.ChangeState(waitState);
        yield return new WaitForSeconds(hitCooldown);
        stateMachine.ChangeState(idleState);
    }
}
