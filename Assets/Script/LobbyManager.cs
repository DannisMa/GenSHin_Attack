using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class LobbyManager : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 100, 100));
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            InitGameTypeButton();
        }
        else
        {
            ShowStatusLabels();
        }
        GUILayout.EndArea();
    }

    private void ShowStatusLabels()
    {
        string mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsClient ? "Client" : "Server";
        GUILayout.Label($"Transport : {NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name}");
        GUILayout.Label($"mode : {mode}");
        GUILayout.Label($"version : {GameConst.version}");
    }

    private void InitGameTypeButton()
    {
        if(GUILayout.Button("Host"))
        {
            NetworkManager.Singleton.StartHost();
        }

        if(GUILayout.Button("Client"))
        {
            NetworkManager.Singleton.StartClient();
        }

        if(GUILayout.Button("Server"))
        {
            NetworkManager.Singleton.StartServer();
        }
    }
}
