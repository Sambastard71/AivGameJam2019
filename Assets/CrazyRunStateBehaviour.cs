using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrazyRunStateBehaviour : StateMachineBehaviour
{
    public float RunRadius;
    public float RunTime;

    Vector3 agentDestination;
    RoomManager roomMng;
    NavMeshAgent agent;

    float counter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        roomMng = FindObjectOfType<RoomManager>();
        agent = animator.transform.GetComponent<NavMeshAgent>();

        SetRandomDestToAgent(animator);

        counter = RunTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentDestination = agent.destination;
        counter -= Time.deltaTime;

        if (animator.transform.position == agentDestination || counter <= 0.0f)
        {
            SetRandomDestToAgent(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

    private void SetRandomDestToAgent(Animator animator)
    {

        Vector3 randomDirection = Random.insideUnitSphere * RunRadius;
        randomDirection += animator.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, RunRadius, 1))
        {
            finalPosition = hit.position;
        }

        agent.destination = finalPosition;
    }
}
