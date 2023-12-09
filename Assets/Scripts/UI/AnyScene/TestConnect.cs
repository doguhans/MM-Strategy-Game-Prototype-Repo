using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallbacks
{
private void Start() {

    Debug.Log("Connecting to Photon Network server...", this);
    PhotonNetwork.SendRate= 20; // by default 20.
    PhotonNetwork.SerializationRate = 5; //10.
    PhotonNetwork.AutomaticallySyncScene = true;
    PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
    PhotonNetwork.GameVersion= MasterManager.GameSettings.GameVersion;
    PhotonNetwork.ConnectUsingSettings();
    
}
public override void OnConnectedToMaster()
{
    Debug.Log("Connected to Photon server.", this);
    Debug.Log("Nickname:" + PhotonNetwork.LocalPlayer.NickName, this);
    print(PhotonNetwork.LocalPlayer.NickName);
    if(!PhotonNetwork.InLobby)
        PhotonNetwork.JoinLobby();
}


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason"+ cause.ToString(), this);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");
    }
}
