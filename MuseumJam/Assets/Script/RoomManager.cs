using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Light allarmSpotLight;
    public GameObject[] SecurityDoor;
    public float DooryOffset = 5;
    public TMP_Text timerText;
    public float timer = 180f;
    public float IntensityAllarm = 200;
    public float IntensityAllarmIncreaseMultiply = 1.1f;
    public float IntensityAllarmDecreaseMultiply = 0.9f;
    public bool isStartedTimer;
    public bool isGameoverWin;
    public bool isGameoverLose;

    public bool IsOpen;
    public bool IsClosed;
    public float DoorTimer = 3.0f;
    float doorCounter;

    public bool IsAllarmOn;
    public bool IsAllarmActivatedOnce;
    float allarmIntensitiMultiply;

    uint actualRoomIndex;
    public int ActualGuardCamIndex=5;
    public Transform[] cameras;
    public Material CamOnMaterial;
    public Material CamOffMaterial;
    public Material CamChangeMaterial;
    bool isCamBling;
    float timerCam = 2.0f;
    float counterCam;

    public float lightIntensity = 35;

    public Transform PeopleManager;
    Transform player;
    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }
    bool ihaveSeenThief;
    public Slider slider;

    private void Start()
    {
        Invoke("SetTimer", 1.5f);
    }

    public void SetActiveGuardCam( Transform CamProp, int TypeOfmaterial)
    {
        if (TypeOfmaterial == 0)
        {
            CamProp.GetComponent<MeshRenderer>().sharedMaterial = CamOffMaterial;
            CamProp.GetComponent<Light>().intensity = 0;

        }
        if (TypeOfmaterial == 1)
        {
            CamProp.GetComponent<MeshRenderer>().sharedMaterial = CamChangeMaterial;
            CamProp.GetComponent<Light>().intensity = lightIntensity;

        }
        if (TypeOfmaterial == 2)
        {
            CamProp.GetComponent<MeshRenderer>().sharedMaterial = CamOnMaterial;
            CamProp.GetComponent<Light>().intensity = lightIntensity;

        }
     
    }
    
    public void SetAllarmOn()
    {

        allarmSpotLight.intensity = IntensityAllarm;
        IsAllarmOn = true;
        allarmIntensitiMultiply = IntensityAllarmDecreaseMultiply;
        timer = 60.0f;
        isStartedTimer = true;
    }

    public void CloseDoor()
    {
        if (!IsClosed)
        {
            IsOpen = false;
            gameObject.transform.position -= new Vector3(0, DooryOffset, 0);
            IsClosed = true;
        }
    }

    public void TimeClock()
    {
        string min = ((int)timer / 60).ToString();
        string sec = ((int)timer % 60).ToString();

        timerText.text = min + ":" + sec;

        timer -= Time.deltaTime;
    }

    private void Update()
    {
        if (isStartedTimer)
        {
            TimeClock();
        }

        if (IsClosed)
        {
            doorCounter += Time.deltaTime;
            if (doorCounter >= DoorTimer)
            {
                gameObject.transform.position += new Vector3(0, DooryOffset, 0);
                doorCounter = 0;
                IsClosed = false;
                IsOpen = true;
            }
        }

        if (IsAllarmOn)
        {
            allarmSpotLight.intensity *= allarmIntensitiMultiply;
            if (allarmSpotLight.intensity <= IntensityAllarm/2)
            {
                allarmIntensitiMultiply = IntensityAllarmIncreaseMultiply;
            }
            else if (allarmSpotLight.intensity >= IntensityAllarm)
            {
                allarmIntensitiMultiply = IntensityAllarmDecreaseMultiply;
            }
        }

        if (ActualGuardCamIndex == player.GetComponent<Character>().RoomID)
        {
            ihaveSeenThief = true;
            slider.enabled = true;

            while (slider.value <= 1)
            {
                slider.value *= 1.01f;

            }
        }
        if (ActualGuardCamIndex != player.GetComponent<Character>().RoomID)
        {
            ihaveSeenThief = false;
            slider.enabled = false;

            while (slider.value >= 0)
            {
                slider.value *= 0.99f;

            }
        }
    }

    void SetTimer()
    {
        isStartedTimer = true;
    }
}


