using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class channelbar : MonoBehaviour {
	public GameObject Toggled;
	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener (TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		//Toggle channel buttons when selected
		if (PhotonNetwork.inRoom) {
			if ("chat" + GetComponentInChildren<Text> ().text == PhotonNetwork.room.Name) {
				Toggled.SetActive (true);
			} else {
				Toggled.SetActive (false);
			}
		}
	}
	public void TaskOnClick()
	{
		StartCoroutine (CreateChatRoom ());
	}
	public IEnumerator CreateChatRoom()
	{
		//Quit if already in the room
		bool quit = false;
		if (PhotonNetwork.inRoom) {
			if (PhotonNetwork.room.Name == "chat" + GetComponentInChildren<Text> ().text) {
				quit = true;
			}
		}
		if (!quit) {
			PhotonNetwork.LeaveRoom ();
			//Wait until user left the room
			while (PhotonNetwork.insideLobby == false) {
				yield return null;
			}

			RoomOptions roomOptions = new RoomOptions ();
			roomOptions.maxPlayers = 20;
			PhotonNetwork.JoinOrCreateRoom ("chat" + GetComponentInChildren<Text> ().text, roomOptions, TypedLobby.Default);

			GameObject PhotonController = GameObject.FindGameObjectWithTag ("PhotonController");

			//Wait until user joined the new room
			while (PhotonNetwork.inRoom == false) {
				yield return null;
			}
			if (PhotonController != null) {
				PhotonController.GetComponent<photonConnect> ().CurrentRoom.SetActive (false);
				PhotonController.GetComponent<photonConnect> ().LastRoomName = PhotonNetwork.room.Name;
			}

		}
	}
		
}
