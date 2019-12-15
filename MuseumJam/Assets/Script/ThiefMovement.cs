using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ThiefMovement : MonoBehaviour
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
            anim.SetInteger("Run", (int)RuningSpeed);
        }
        else
        {
            agent.speed = WalkingSpeed;
            anim.SetInteger("Run", (int)WalkingSpeed);

        }

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove != 0 || verticalMove != 0)
        {
            anim.SetInteger("Walk", (int)WalkingSpeed);
        }
        else 
        {
            anim.SetInteger("Walk", 0);
            anim.SetInteger("Run", 0);
            anim.SetInteger("IdleType", Random.Range(1,6));
        }

        agent.velocity = new Vector3(horizontalMove, 0, verticalMove) * agent.speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }
}
