using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public uint ID;
    public uint IDPrefab;
    public uint RoomID;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += transform.forward * 10;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += transform.forward * -10;
        }
    }

}
