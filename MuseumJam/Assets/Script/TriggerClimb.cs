using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClimb : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.GetComponent<Animator>().SetTrigger("Climb");
                //Fare Freeze per fine Gioco
                //Sconfitta Guardia
                
            }
        }
    }
}
