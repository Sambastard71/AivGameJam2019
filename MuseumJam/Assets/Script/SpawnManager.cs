using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WindowRoom
{
    public int RoomIndex;
    public Transform[] Window;
}

public class SpawnManager : MonoBehaviour
{
    public int MinNumOfPeopleInRoom;
    public int MaxNumOfPeopleInRoom;
    public Transform[] Spawners;
    public GameObject[] PrefabToSpawn;
    public GameObject[] PrefabPlayerToSpawn;
    public GameObject[] PaintPrefab;
    public RoomManager RoomManager;
    
    Transform northWest, northEst, southWest;
    
    int paintRoom;
    int[] windowRoom;
    int paintIndex;
    int[] windowIndex;
    public WindowRoom[] WindowRooms;

    int prefabIndex;
    GameObject go;
    int thiefRoom;
    Transform spawner;


    void Awake()
    {
        SpawnThief();

        paintRoom = SelectPaintRoom(thiefRoom + 1);
        paintIndex = Random.Range(0, 2);
        windowRoom = SelectWindowRoom(paintRoom);
        windowIndex = new int[windowRoom.Length];
        for (int i = 0; i < windowRoom.Length; i++)
        {
            if (windowRoom[i] % 2 == 1)
            {
                windowIndex[i] = Random.Range(0, 2);
            }
            else
            {
                windowIndex[i] = 0;
            }
        }

        for (int j = 0; j < windowRoom.Length; j++)
        {
            for (int i = 0; i < WindowRooms.Length; i++)
            {
                if (WindowRooms[i].RoomIndex == windowRoom[j])
                {
                    WindowRooms[i].Window[windowIndex[j]].gameObject.AddComponent<TriggerClimb>();
                }
            }
        }

       
        prefabIndex = Random.Range(0, PrefabPlayerToSpawn.Length);

        Transform spawner = GetSpawnerByIndex(thiefRoom);
        
        for (int i = 0; i < 9; i++)
        {
            int randomChosen = Random.Range(MinNumOfPeopleInRoom, MaxNumOfPeopleInRoom + 1);

            SpawnPeople(randomChosen, i);

            SpawnPaint(i);
        }

        //SetTimer
        
        //Packet startTimer = new Packet(Server.COMMAND_STARTTIMER);
        //server.Send(startTimer.GetData(), server.other.EndPoint);
        
    }

    public Transform GetSpawnerByIndex(int roomIndex)
    {
        //Debug.Log(roomIndex);
        return Spawners[roomIndex];
    }

    private int SelectPaintRoom(int thiefRoom)
    {
        float paintRoomPerc = Random.Range(0.0f, 10.0f);
        int paintRoom = 0;

        switch (thiefRoom)
        {
            case 1:
                paintRoom = paintRoomPerc >= 5.0f ? 6 : 8;
                break;
            case 2:
                paintRoom = paintRoomPerc >= 5.0f ? 7 : 9;
                break;
            case 3:
                paintRoom = paintRoomPerc >= 5.0f ? 4 : 8;
                break;
            case 4:
                paintRoom = paintRoomPerc >= 5.0f ? 3 : 9;
                break;
            case 6:
                paintRoom = paintRoomPerc >= 5.0f ? 1 : 7;
                break;
            case 7:
                paintRoom = paintRoomPerc >= 5.0f ? 2 : 6;
                break;
            case 8:
                paintRoom = paintRoomPerc >= 5.0f ? 1 : 3;
                break;
            case 9:
                paintRoom = paintRoomPerc >= 5.0f ? 2 : 4;
                break;
            default:
                break;
        }

        return paintRoom;
    }

    private int[] SelectWindowRoom(int paintRoom)
    {
        int[] windowRoom = new int[2];

        switch (paintRoom)
        {
            case 1:
                windowRoom[0] = 6;
                windowRoom[1] = 8;
                break;
            case 2:
                windowRoom[0] = 7;
                windowRoom[1] = 9;
                break;
            case 3:
                windowRoom[0] = 4;
                windowRoom[1] = 8;
                break;
            case 4:
                windowRoom[0] = 3;
                windowRoom[1] = 9;
                break;
            case 6:
                windowRoom[0] = 1;
                windowRoom[1] = 7;
                break;
            case 7:
                windowRoom[0] = 2;
                windowRoom[1] = 6;
                break;
            case 8:
                windowRoom[0] = 1;
                windowRoom[1] = 3;
                break;
            case 9:
                windowRoom[0] = 2;
                windowRoom[1] = 4;
                break;
            default:
                break;
        }

        return windowRoom;
    }

    private void SpawnThief()
    {
         thiefRoom = 4;
        while (thiefRoom == 4)
        {
            thiefRoom = Random.Range(0, 9);
        }
        
        prefabIndex = Random.Range(0, PrefabPlayerToSpawn.Length);

        spawner = GetSpawnerByIndex(thiefRoom);

        go = Instantiate<GameObject>(PrefabPlayerToSpawn[prefabIndex], spawner);
        
        go.transform.position = spawner.position;

        Character character = go.AddComponent<Character>();
        character.owner = go;
        character.RoomID = thiefRoom + 1;
        
        RoomManager.Player = go.transform;
    }

    private void SpawnPeople(int randomChosen, int room)
    {
        for (int j = 0; j < randomChosen; j++)
        {
            prefabIndex = Random.Range(0, PrefabToSpawn.Length);

            spawner = GetSpawnerByIndex(room);

            go = Instantiate<GameObject>(PrefabToSpawn[(int)prefabIndex], spawner);

            northWest = spawner.GetChild(0);
            southWest = spawner.GetChild(1);
            northEst = spawner.GetChild(2);

            float posX = Random.Range(northWest.position.x, northEst.position.x);
            float posY = -0.5f;
            float posZ = Random.Range(northWest.position.z, southWest.position.z);

            go.transform.position = new Vector3(posX, posY, posZ);

            Character character = go.AddComponent<Character>();
            character.IDPrefab = prefabIndex + 1;
            character.RoomID = room + 1;
        }
    }

    private void SpawnPaint(int room)
    {
        for (int j = 0; j < 2; j++)
        {
            prefabIndex = Random.Range(0, PaintPrefab.Length);

            spawner = GetSpawnerByIndex(room);

            go = Instantiate<GameObject>(PaintPrefab[prefabIndex], spawner);

            Transform paintPos = spawner.GetChild(3 + j);

            go.transform.position = paintPos.position;

            Character character = go.AddComponent<Character>();
            character.IDPrefab = prefabIndex + 1;
            character.RoomID = room + 1;

            if (paintRoom == room + 1 && paintIndex == j)
            {
                go.AddComponent<TiggerPickUp>();
                go.tag = "PaintWin";
            }
        }
    }
}
