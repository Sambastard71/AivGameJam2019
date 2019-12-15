using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ThiefMovementGood : MonoBehaviour
{
    public float WalkingSpeed;
    public float RuningSpeed;
    
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            agent.speed = RuningSpeed;
            anim.SetBool("Run", true);
        }
        else
        {
            agent.speed = WalkingSpeed;
            anim.SetBool("Run", false);
        }

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove != 0 || verticalMove != 0)
        {
            anim.SetBool("Walk", true);
        }
        else 
        {
            anim.SetBool("Walk", false);
            anim.SetInteger("IdleType", Random.Range(1,6));
        }

        agent.velocity = new Vector3(horizontalMove, 0, verticalMove) * agent.speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (agent.velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }
}
