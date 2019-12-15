using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform[] CameraStand;
    int lastPlayerRoom=0;
    Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();    
        transform.position = CameraStand[player.RoomID-1].position;
        //Debug.Log(player.RoomID-1 + " " + lastPlayerRoom);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPlayerRoom != player.RoomID-1)
        {
            transform.position = CameraStand[player.RoomID-1].position;
            lastPlayerRoom = player.RoomID-1;
            //Debug.Log(player.RoomID-1 + " " + lastPlayerRoom);

        }

    }
}
