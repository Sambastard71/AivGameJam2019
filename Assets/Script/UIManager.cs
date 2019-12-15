using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public RoomManager roomMng;
    public Transform Camera;
    public GameObject CamerasPositions;
    public RectTransform PanelMap;
    public Camera Noise;
    public GameObject text;
    public GameObject CamPropParent;
    


    public float MaxNoiseCounter;
    bool Movemap;

    float Noisecounter;
    int IndexCurrCamera;
    bool InNoise;

    GameObject[] ButtonsSwitchCamera;
    GameObject[] CampropsLights;
    Vector3[] PositionsOfCameras;


    // Start is called before the first frame update
    void Start()
    {
        ButtonsSwitchCamera = new GameObject[PanelMap.childCount];
        for (int i = 0; i < PanelMap.transform.childCount; i++)
        {
            ButtonsSwitchCamera[i] = PanelMap.transform.GetChild(i).gameObject;
        }

        CampropsLights = new GameObject[CamPropParent.transform.childCount];

        for (int i = 0; i < CamPropParent.transform.childCount; i++)
        {
            CampropsLights[i] = CamPropParent.transform.GetChild(i).GetChild(0).GetChild(0).gameObject;
        }

        PositionsOfCameras = new Vector3[CamerasPositions.transform.childCount];

        for (int i = 0; i < CamerasPositions.transform.childCount; i++)
        {
            PositionsOfCameras[i] = CamerasPositions.transform.GetChild(i).position;
        }

        IndexCurrCamera = 4;
    }

    private void Update()
    {
        Camera.position = PositionsOfCameras[IndexCurrCamera];

        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 1);

        if(InNoise)
        {
            PanelMap.gameObject.SetActive(false);
            roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 1);
            text.gameObject.SetActive(false);
            Noisecounter += Time.deltaTime;
            Noise.depth = 5;
            roomMng.watchingTime = 0;
        }
        if(Noisecounter>=MaxNoiseCounter)
        {
            PanelMap.gameObject.SetActive(true);
            roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 2);
            text.gameObject.SetActive(true);
            Noise.depth = -5;
            Noisecounter = 0;
            InNoise = false;
        }


        if (Input.GetMouseButtonDown(1))
        {
            PressAlarmButton();
           
        }

        roomMng.ActualGuardCamIndex = IndexCurrCamera;
    }

    public void PressAlarmButton()
    {
        if (!InNoise)
        {
            if (!roomMng.IsAllarmOn)
            {
                roomMng.SetAllarmOn();
            }
            else
            {
                if (!roomMng.IsClosed)
                {
                    roomMng.CloseDoor();
                }
            }

            if(roomMng.IsOpen)
            {
                roomMng.CloseDoor();
            }
        }
    }


    public void SwitchToRoom1()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 0;
        InNoise = true;
    }

    public void SwitchToRoom2()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 1;
        InNoise = true;

    }
    public void SwitchToRoom3()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 2;
        InNoise = true;

    }
    public void SwitchToRoom4()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 3;
        InNoise = true;

    }
    public void SwitchToRoom5()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 4;
        InNoise = true;

    }
    public void SwitchToRoom6()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 5;
        InNoise = true;

    }
    public void SwitchToRoom7()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 6;
        InNoise = true;

    }
    public void SwitchToRoom8()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 7;
        InNoise = true;

    }
    public void SwitchToRoom9()
    {
        ButtonsSwitchCamera[IndexCurrCamera].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        roomMng.SetActiveGuardCam(CampropsLights[IndexCurrCamera].transform, 0);
        IndexCurrCamera = 8;
        InNoise = true;

    }



}
