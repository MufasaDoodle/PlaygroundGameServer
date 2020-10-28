using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class ServerHandle
{
    public static void WelcomeReceived(int fromClient, Packet packet)
    {
        int clientIDCheck = packet.ReadInt();
        string username = packet.ReadString();

        Debug.Log($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}");
        if (fromClient != clientIDCheck)
        {
            Debug.Log($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientIDCheck})");
        }
        Server.clients[fromClient].SendIntoGame(username);
    }

    public static void PlayerMovement(int fromClient, Packet packet)
    {
        bool[] inputs = new bool[packet.ReadInt()];
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = packet.ReadBool();
        }
        Quaternion rotation = packet.ReadQuaternion();

        Server.clients[fromClient].player.SetInput(inputs, rotation);
    }
}

