using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPun, IPunOwnershipCallbacks
{   
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if(targetView != base.photonView)
            return;

        // Any request checks can be written in here.


        // based on need this line can be commented out so that character transfer by request can be accepted.
        base.photonView.TransferOwnership(requestingPlayer); 
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if(targetView != base.photonView)
            return;
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        if(targetView != base.photonView)
            return;
    }

    private void OnMouseDown()
    {
        base.photonView.RequestOwnership();
    }
}
