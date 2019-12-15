using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    RoomManager roomManager;
    public AudioSource source;
    public AudioSource sourceDoor;

    public AudioClip[] clips;

   

    bool setPlay0 = false;
    bool setPlay1 = false;
    bool setPlay2 = false;
    bool setPlay3 = false;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = transform.GetComponent<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (roomManager.isStartedTimer)
        {
           
            if (!setPlay0)
            {
                source.clip = clips[0];
                source.Play();
                setPlay0 = true; 
            }

        }

        if (roomManager.IsAllarmOn)
        {
            if (!setPlay1)
            {
                source.clip = clips[1];
                source.Play();
                setPlay1 = true;
            }
        }

        if(roomManager.isGameover && roomManager.WinGuard)
        {
           
            
            if (!setPlay2)
            {
                source.clip = clips[2];
                source.Play();
                setPlay2 = true;
            }
            
        }

        if (roomManager.isGameover && roomManager.WinThief)
        {
            
            
            if (!setPlay3)
            {
                source.clip = clips[3];
                source.Play();
                setPlay3 = true;
            }

        }

        if(roomManager.IsClosed)
        {
            sourceDoor.gameObject.SetActive(true);
        }

        if (roomManager.IsOpen)
        {
            sourceDoor.gameObject.SetActive(false);
        }

    }
}
