using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCurrentMatch : MonoBehaviour
{

    public void OnClick_LeaveMatch()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("NetworkLobby");
    }
	
}
