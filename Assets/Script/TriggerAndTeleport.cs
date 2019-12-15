using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TriggerAndTeleport : MonoBehaviour
{
    public Transform posToWarp;
    public int roomId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<NavMeshAgent>().Warp(posToWarp.position);
            other.transform.GetComponent<Character>().RoomID = roomId;
        }
    } 
}
