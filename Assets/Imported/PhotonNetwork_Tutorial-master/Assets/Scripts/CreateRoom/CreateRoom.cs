using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
	private ExitGames.Client.Photon.Hashtable m_roomCustomProperties = new ExitGames.Client.Photon.Hashtable();

    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

	//Called from host editing or host level->Create room
	public IEnumerator OnClick_CreateRoom(string lastmapname,string IsEditor)
    {
		GameObject PhotonController = GameObject.FindGameObjectWithTag ("PhotonController");

		if (PhotonController != null) {
			PhotonController.GetComponent<photonConnect> ().PendingRoomToJoin = "True";
			if (PhotonNetwork.inRoom) {
				PhotonNetwork.LeaveRoom ();
				while (PhotonNetwork.insideLobby == false) {
					yield return null;
				}
			}
			m_roomCustomProperties ["IsLevelEditor"] = IsEditor;
			m_roomCustomProperties["started"] ="false" ;
			m_roomCustomProperties["lastmapname"] =lastmapname ;
			bool isvisible = true;
			bool isopen = true;
			if (IsEditor=="true") {
				isvisible = false;
			}

			RoomOptions roomOptions = new RoomOptions() { IsVisible = isvisible, IsOpen = isopen, MaxPlayers = 20, CustomRoomProperties=m_roomCustomProperties };

			PhotonNetwork.CreateRoom (lastmapname, roomOptions, TypedLobby.Default);
		}

    }


    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("create room failed: " + codeAndMessage[1]);
    }
}
