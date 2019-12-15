using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public delegate void command(byte[] packet, EndPoint sender);

public class Server : MonoBehaviour
{
    //Address and port where the socket listen the packet
    public string MineAddress;
    public int MinePort;
    public Button StartButt;
    public Text TextButton;
    public Client other;
    

    command[] commands;
    TransportIPV4 transport;

    public const byte COMMAND_JOIN = 0;
    public const byte COMMAND_WELCOME = 1;
    public const byte COMMAND_SPAWN = 3;
    public const byte COMMAND_UPDATE = 4;
    public const byte COMMAND_CAMERAGUARD = 5;
    public const byte COMMAND_ALLARM = 6;
    public const byte COMMAND_DOORS = 7;
    public const byte COMMAND_GAMEOVER = 8;
    public const byte COMMAND_STARTTIMER = 9;
    public const byte COMMAND_ISEEU = 10;
    public const byte COMMAND_ICANTSEEU = 11;


    public RoomManager roomManager;

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "0";
    }


    public void SetRoomManager()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        transport = new TransportIPV4();

       MineAddress = GetLocalIPAddress();

        transport.Bind(MineAddress, MinePort);
        commands = new command[20];
        commands[COMMAND_JOIN] = Join;
        commands[COMMAND_ALLARM] = Allarm;
        commands[COMMAND_DOORS] = DoorClosed;
        commands[COMMAND_CAMERAGUARD] = CameraGuard;

        //IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.43.130"), 9995);
        //other = new Client(ip); 
    }

    // Update is called once per frame
    void Update()
    {
        Dispatch();
    }

    void Dispatch()
    {
        EndPoint sender = transport.CreateEndPoint();
        byte[] data = transport.Recv(256, ref sender);

        if (data != null)
        {
            commands[data[0]](data, sender);
        }

    }

    public bool Send(byte[] packet, EndPoint endPoint)
    {
        return transport.Send(packet, endPoint);
    }

    public bool Send(byte[] packet, EndPoint endPoint, ref int sentLen)
    {
        int sentlen = 0;
        bool succes = transport.Send(packet, endPoint, ref sentlen);
        sentLen = sentlen;
        return succes;
    }

    public bool Send(byte[] packet, EndPoint endPoint, int total, int dataleft, ref int sentLen)
    {
        if (total >= dataleft)
        {
            return true;
        }
        int sentlen = 0;
        bool succes = transport.Send(packet, endPoint, ref sentlen);
        sentLen = sentlen;
        return succes;
    }

    private void Join(byte[] packet, EndPoint endPoint)
    {
        Client c = new Client(endPoint);
        
        if (packet.Length != 1)
        {
            return;
        }

        if (other != null)
        {
            if (other.EndPoint.Equals(c.EndPoint))
            {
                return;
            }
        }

        other = c;

        Packet welcome = new Packet(COMMAND_WELCOME);

        Debug.Log("welcome creato");

        Send(welcome.GetData(), other.EndPoint);

        Debug.Log("welcome Inviato");

        StartButt.interactable = true;
        TextButton.text = "Ready, Click To Start";
    }

    private void Allarm(byte[] packet, EndPoint endPoint)
    {
        
        if (packet.Length != 1)
        {
            return;
        }

        roomManager.SetAllarmOn();
    }

    private void DoorClosed(byte[] packet, EndPoint endPoint)
    {

        if (packet.Length != 1)
        {
            return;
        }

        roomManager.CloseDoor();
    }

    private void CameraGuard(byte[] packet, EndPoint endPoint)
    {

        if (packet.Length != 9)
        {
            return;
        }

        uint noise = BitConverter.ToUInt32(packet, 1);
        uint camID = BitConverter.ToUInt32(packet, 5);

        roomManager.SetActiveGuardCam(noise, camID);
    }
    
}
