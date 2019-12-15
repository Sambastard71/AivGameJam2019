using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TiggerPickUp : MonoBehaviour
{
    Transform player;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.GetComponent<Animator>().SetTrigger("PickUp");
                player = other.transform;
                Invoke("TakePaint", 1.22f);
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
