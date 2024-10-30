using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
 public float moveSpeed;
    public float detectionRange;
    public float hitCooldown = 2f;
    public int damage = 1;
    public float pushForce;

    public GameObject player;
    private Transform playerPosition;

    #region StateMachine
    private EnemyStateMachine stateMachine;

    public ChasingState chasingState{get; private set;}
    public IdleState idleState{get; private set;}
    private EnemyWaitState waitState;
    #endregion

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        
        stateMachine = new EnemyStateMachine();
        chasingState = new ChasingState(stateMachine,this);
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
