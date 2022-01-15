using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLayoutGroup : MonoBehaviour
{
	public GameObject RoomState;
	public GameObject CurrentRoom;
    [SerializeField]
    private GameObject _playerListingPrefab;
    private GameObject PlayerListingPrefab
    {
        get { return _playerListingPrefab; }
    }

    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings
    {
        get { return _playerListings; }
    }

	void Update(){
		if (RoomState != null && PhotonNetwork.inRoom) {
			string fullname = PhotonNetwork.room.Name;
			var split = fullname.Split ('/');
            if (split.Length >= 4){
                //not campaign
                string mapname = split [split.Length - 1];
			    string mapusername = split [split.Length - 3];
			    string mapnamewithoutTXT = mapname.Substring (0, mapname.Length - 4);
			    RoomState.GetComponent<Text> ().text = "Map name: " + mapnamewithoutTXT + "\n\nCreated by: " + mapusername + "\n\nPlayers: "+PhotonNetwork.room.PlayerCount + " out of "+PhotonNetwork.room.MaxPlayers;
            } else {
                //campaign
                string mapname = fullname;
			    string mapusername = "Campaign";
			    string mapnamewithoutTXT = fullname.Split('.')[0];
			    RoomState.GetComponent<Text> ().text = "Map name: " + mapnamewithoutTXT + "\n\nCreated by: " + mapusername + "\n\nPlayers: "+PhotonNetwork.room.PlayerCount + " out of "+PhotonNetwork.room.MaxPlayers;
            }
		}
	}

    //Called by photon whenever the master client is swithced.
    private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }


    public void JoinedRoom()
    {

		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		if (MainCanvasManager.Instance != null) {
			if (MainCanvasManager.Instance.CurrentRoomCanvas != null) {
				MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsLastSibling ();
			}
		}
		PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
		for (int i = 0; i < photonPlayers.Length; i++) {
			PlayerJoinedRoom (photonPlayers [i]);
		}

		PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
		PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;

    }

    //Called by photon when a player joins the room.
    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }

    //Called by photon when a player leaves the room.
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
    }


    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);

        GameObject playerListingObj = Instantiate(PlayerListingPrefab);
        playerListingObj.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        PlayerListings.Add(playerListing);
    }

    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = PlayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if (index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }
    }



    public void OnClickLeaveRoom()
	{
		if (PhotonNetwork.inRoom) {
			PhotonNetwork.LeaveRoom ();
		}
	}
}
