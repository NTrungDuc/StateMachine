using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    //Infor Bot
    public int id;
    public float enemyMaxHealth = 100;
    public float currentHealth;
    public float damageSlash = 2;
    private float attackRadius = 2;
    //
    string currentAnimName;
    public Animator anim;
    [SerializeField] Rigidbody rb;
    //
    public NavMeshAgent agent;
    public float range;
    public Vector3 randomPoint;
    //
    [SerializeField] Transform Target;
    private float chaseRadius = 12;
    private IState currentState;
    public IState CurrentState { get => currentState; set => currentState = value; }
    void Start()
    {
        OnInit();
    }


    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void OnInit()
    {
        ChangeState(new OriginalState());
        currentHealth = enemyMaxHealth;
    }
    public void randomMove()
    {
        ChangeAnim("run");
        agent.isStopped = false;       
        randomPoint = GetRandomPointOnNavMesh(transform.position, range);
        agent.SetDestination(randomPoint);
    }
    public void TargetObj()
    {
        float distance = Vector3.Distance(Target.position, transform.position);
        if (distance < chaseRadius)
        {
            agent.SetDestination(Target.position);
        }
    }
    Vector3 GetRandomPointOnNavMesh(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0f;
        randomDirection.Normalize();

        Vector3 randomPoint = origin + randomDirection * Random.Range(0f, distance);

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomPoint, out navMeshHit, distance, NavMesh.AllAreas);

        return navMeshHit.position;
    }
    public void ChangeState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
    public void stopMoving()
    {
        agent.isStopped = true;
    }
    public bool isTargetInRange()
    {
        float distance = Vector3.Distance(Target.position, transform.position);
        if (distance < attackRadius)
        {
            return true;
        }
        return false;
    }
    public void Attack()
    {
        stopMoving();
        ChangeAnim("attack");
    }
    public void takeDamage(float damageAmout)
    {
        currentHealth -= damageAmout;
        if (currentHealth <= 0)
        {

        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }
}
