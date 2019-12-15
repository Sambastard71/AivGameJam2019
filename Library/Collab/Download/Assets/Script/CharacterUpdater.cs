using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpdater : MonoBehaviour
{
    public float counter = 0.3f;

    List<Transform> GOtoUpdate;
    float timer;
    Server server;

    private void OnEnable()
    {
        GOtoUpdate = new List<Transform>();
    }

    private void Start()
    {
        server = GameObject.FindGameObjectWithTag("Client").GetComponent<Server>();
    }

    void Update()
    {
        

        timer += Time.deltaTime;
        if (timer >= counter)
        {
            timer = 0.0f;

            foreach (Transform transform in GOtoUpdate)
            {
                Packet update = new Packet(Server.COMMAND_UPDATE, transform.GetComponent<Character>().ID, transform.position.x, transform.position.y, transform.position.z, transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                server.Send(update.GetData(), server.other.EndPoint);
                //Debug.Log("update: " + transform.GetComponent<Character>().ID);
            }
        }
    }

    public void AddToList(Transform go)
    {
        GOtoUpdate.Add(go);
    }
}
