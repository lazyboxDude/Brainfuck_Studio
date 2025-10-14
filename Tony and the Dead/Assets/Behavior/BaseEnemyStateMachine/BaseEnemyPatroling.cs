using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyPatroling : StateMachineBehaviour
{
    float timer;
    public float patrolDuration = 10f;

    Transform player;
    NavMeshAgent navMeshAgent;
    public float detectionAreaRadius = 18f;
    public float patrolSpeed = 1f;

    List<Transform> patrolPoints = new List<Transform>();


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Initialize variables --- //
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = animator.GetComponent<NavMeshAgent>();

        navMeshAgent.speed = patrolSpeed;
        timer = 0;

        // --- Get patrol points --- //
        GameObject waypointsParent = GameObject.FindGameObjectWithTag("PatrolPoint");
        foreach (Transform waypoint in waypointsParent.transform)
        {
            patrolPoints.Add(waypoint);
        }  
        
        Vector3 nextPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
        navMeshAgent.SetDestination(nextPatrolPoint);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //--- State Behaviors --- //

        // --- Check if Agent reached the patrol point --- //
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Count)].position);
        }

        // --- Transition to Idle after patrolDuration seconds --- //
        timer += Time.deltaTime;
        if (timer >= patrolDuration)
        {
            animator.SetBool("isPatroling", false);
        }

        // --- Transition to Chasing if player is within detection area --- // 
        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceToPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop the NavMeshAgent when exiting the patrol state
        navMeshAgent.SetDestination(navMeshAgent.transform.position);
    }
    

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
