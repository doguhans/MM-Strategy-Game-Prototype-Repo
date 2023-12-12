using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _roomName;

    private RoomsCanvases _roomsCanvases;
    public void FirstInitialize(RoomsCanvases canvases) 
    {
        _roomsCanvases = canvases;
    }
    public void OnClick_CreateRoom()
    {   
        if(!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        options.PublishUserId = true;                                                     // TODO...

                                                                                        //Server max player capability
        options.MaxPlayers= 2;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text , options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully", this);
        _roomsCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: "+ message, this);
    }
}
