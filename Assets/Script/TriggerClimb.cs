using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClimb : MonoBehaviour
{
    public RoomManager roomManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && roomManager.PaintIsStoled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.GetComponent<Animator>().SetTrigger("Climb");
                Invoke("WinThief", 2.0f);

                //Fare Freeze per fine Gioco
                //Sconfitta Guardia

            }
        }
    }

    private void WinThief()
    {
        roomManager.isGameover = true;
        roomManager.WinThief = true;
    }
}
