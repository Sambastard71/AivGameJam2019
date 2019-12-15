using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultyDisplay : MonoBehaviour
{
    public Camera Guard;
    public Camera Thief;

    // Start is called before the first frame update
    void Start()
    {
        int width, height, refreshRate;
        width = Screen.width;
        height = Screen.height;
        refreshRate = 120;
        Display.displays[0].Activate(width, height, refreshRate);
        Display.displays[1].Activate(width, height, refreshRate);

        Guard.targetDisplay = 0;
        Thief.targetDisplay = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
