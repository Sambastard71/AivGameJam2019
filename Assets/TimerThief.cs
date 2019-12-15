using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerThief : MonoBehaviour
{
    public RoomManager roomManager;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string min = ((int)roomManager.timer / 60).ToString();
        string sec = ((int)roomManager.timer % 60).ToString();

        text.text = min + ":" + sec;
    }
}
