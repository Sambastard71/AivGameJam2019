using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarMng : MonoBehaviour
{
    public RectTransform Image;
    public SpawnManager spawnManager;
    public RectTransform[] PositiionsOfWindows;
    public RoomManager roomManager;

    Character player;
    int[] WindowsIndex;
    int[] RoomsWindowsIndex;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        WindowsIndex = spawnManager.windowIndex;
        RoomsWindowsIndex = spawnManager.windowRoom;

    }

    // Update is called once per frame
    void Update()
    {
        switch (player.RoomID-1)
        {
            case 0:
                Image.anchoredPosition = new Vector2(-125, 75);
                break;
            case 1:
                Image.anchoredPosition = new Vector2(0, 75);
                break;
            case 2:
                Image.anchoredPosition = new Vector2(125, 75);
                break;
            case 3:
                Image.anchoredPosition = new Vector2(-125, 0);
                break;
            case 4:
                Image.anchoredPosition = new Vector2(0, 0);
                break;
            case 5:
                Image.anchoredPosition = new Vector2(125,0);
                break;
            case 6:
                Image.anchoredPosition = new Vector2(-125, -75);
                break;
            case 7:
                Image.anchoredPosition = new Vector2(0, -75);
                break;
            case 8:
                Image.anchoredPosition = new Vector2(125, -75);
                break;
            default:
                break;
        }

        if(roomManager.PaintIsStoled)
        {
            for (int i = 0; i < RoomsWindowsIndex.Length; i++)
            {
                PositiionsOfWindows[RoomsWindowsIndex[i]-1].gameObject.SetActive(true);
                PositiionsOfWindows[RoomsWindowsIndex[i]-1].transform.GetChild(WindowsIndex[i]).gameObject.SetActive(true);
            }
        }

    }
}
