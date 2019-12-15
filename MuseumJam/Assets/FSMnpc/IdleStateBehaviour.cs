using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleStateBehaviour : StateMachineBehaviour
{
    public float walkRadius = 5.0f;
    public float WaitTime = 5.0f;

    float counter;
    Character character;

    RoomManager roomManager;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.transform.GetComponent<Character>();
        int idleType = Random.Range(1, 6);
        animator.SetInteger("IdleType", idleType);
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>(); 
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter += Time.deltaTime;

        if (counter >= WaitTime)
        {
            animator.SetInteger("Walk", 100);

            if (Random.Range(0.0f, 10.0f)<= 5.0f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += animator.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPos = hit.position;
                animator.GetComponent<NavMeshAgent>().destination = finalPos;
            }

            counter = 0.0f;
        }

        if (roomManager.IsAllarmOn)
        {
            animator.SetTrigger("CrazyRun");
        }

    }
}


// OnStateExit is called before OnStateExit is called on any state inside this state machine
//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//{
//    
//}

// OnStateMove is called before OnStateMove is called on any state inside this state machine
//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//{
//    
//}

// OnStateIK is called before OnStateIK is called on any state inside this state machine
//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//{
//    
//}

// OnStateMachineEnter is called when entering a state machine via its Entry Node
//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
//{
//    
//}

// OnStateMachineExit is called when exiting a state machine via its Exit Node
//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
//{
//    
//}

//}
