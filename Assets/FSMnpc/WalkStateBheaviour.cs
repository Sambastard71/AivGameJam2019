using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkStateBheaviour : StateMachineBehaviour
{
    public float WalkRadius;
    public float WalkTime;
    public float POIoffset;

    Vector3 agentDestination;
    RoomManager roomMng;
    NavMeshAgent agent;
    Character character;
    float counter;
    List<Transform> POI;
    bool goingToPOI;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        roomMng = FindObjectOfType<RoomManager>();
        agent = animator.transform.GetComponent<NavMeshAgent>();
        POI = new List<Transform>();

        if (Random.Range(0.0f, 1.0f) <= 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * WalkRadius;
            randomDirection += animator.transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, WalkRadius, 1))
            {
                finalPosition = hit.position;
            }

            agent.destination = finalPosition;
            goingToPOI = false;
        }
        else
        {
            character = animator.transform.GetComponent<Character>();
            for (int i = 0; i < roomMng.POI.Length; i++)
            {
                POI.Add(roomMng.POI[i].GetTransformByRoom(character.RoomID));
                
            }

            Transform finalPos = null;
            while(finalPos == null)
            {
                finalPos = POI[Random.Range(0, POI.Count)];
            }
            agent.destination = finalPos.position;
            goingToPOI = true;
        }
        counter = WalkTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentDestination = agent.destination;
        counter -= Time.deltaTime;

        if (goingToPOI)
        {
            float distToPOI = (agentDestination - animator.transform.position).magnitude;
            if (distToPOI <= POIoffset)
            {
                animator.transform.LookAt(agentDestination); 
                agent.destination = animator.transform.position;
                animator.SetBool("Walk", false);
            }
        }
        else
        {
            if (animator.transform.position == agentDestination || counter <= 0.0f)
            {
                animator.SetBool("Walk", false);
            }
        }

        if (roomMng.IsAllarmOn)
        {
            animator.SetTrigger("CrazyRun");
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
}
