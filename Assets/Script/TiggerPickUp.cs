using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TiggerPickUp : MonoBehaviour
{
    Transform player;
    RoomManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.GetComponent<Animator>().SetTrigger("PickUp");
                player = other.transform;
                Invoke("TakePaint", 1.22f);
                manager.PaintIsStoled = true;
                //Mettere Modello Dietro il Ladro
            }
        }
    }

    private void TakePaint()
    {
        transform.position = player.position - (player.forward * 1.1f);
        gameObject.SetActive(false);
    } 
}
