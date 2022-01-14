using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class photonConnect : MonoBehaviour {
	public string versionName="0.1";
	public InputField RoomInput;
	public bool InRoom = false;
	public bool GameStarted=false;
	public GameObject CurrentRoom;
	public GameObject PlayerLayoutGroup;
	public RoomInfo[] roomscached;
	public List<string> RoomList;
	public GameObject LobbyChatCanvas;
	public GameObject ChannelBar;
	public GameObject ChannelBarParent;
	public GameObject ChatTitleText;
	public GameObject SelectedChannelText;
	public GameObject RoomStateText;
	public bool InitiatedPlayerName = false;
	public string LastRoomName="";
	public string PendingRoomToJoin = "";
	private ExitGames.Client.Photon.Hashtable m_roomCustomProperties = new ExitGames.Client.Photon.Hashtable();

	public GameObject SceneController;
	public GA_FREE_Demo08 animator;

	private PhotonView PhotonView;
	private int PlayersInGame = 0;
	private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
	private Coroutine m_pingCoroutine;
	public GameObject playerprefab;
	// Use this for initialization
	void Awake () {
		//Avoid repeat
		GameObject[] list=GameObject.FindGameObjectsWithTag("PhotonController");
		if (list.Length >= 2) {
			//Destroy (list [0]);
		}
		PhotonNetwork.autoCleanUpPlayerObjects = true;

		DontDestroyOnLoad (this.transform);
		connectToPhoton ();
		animator = SceneController.GetComponent<GA_FREE_Demo08> ();
		PhotonNetwork.sendRate = 50;
		PhotonNetwork.sendRateOnSerialize = 40;

		PhotonView = GetComponent<PhotonView>();

		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.inRoom && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("NetworkLobby")) {

			m_roomCustomProperties = PhotonNetwork.room.CustomProperties;
			string IsLE = m_roomCustomProperties ["IsLevelEditor"].ToString ();
			if (m_roomCustomProperties["started"] != null) {
				if (m_roomCustomProperties ["started"].ToString () == "true") {
					if (PhotonNetwork.isMasterClient) {
						PhotonNetwork.room.IsVisible = false;
					}
					if (IsLE == "true") {
						if (PhotonNetwork.isMasterClient) {
							PhotonNetwork.room.IsOpen = true;
						}
						PhotonNetwork.LoadLevel ("LevelEditor");
						//Camera.main.GetComponent<networkcamera> ().LoadingController.GetComponent<Loader> ().StartLoad ("LevelEditor", true);
					} else {
						if (PhotonNetwork.isMasterClient) {
							PhotonNetwork.room.IsOpen = false;
						}
						PhotonNetwork.LoadLevel ("Level1");
						//Camera.main.GetComponent<networkcamera> ().LoadingController.GetComponent<Loader> ().StartLoad ("Level1", true);
					}
				} else {
					if (PhotonNetwork.isMasterClient) {
						PhotonNetwork.room.IsOpen = true;
						if (IsLE == "true") {
							PhotonNetwork.room.IsVisible = false;
						} else {
							PhotonNetwork.room.IsVisible = true;
						}
					}
				}
			}
		}
	}

	public void connectToPhoton(){
		if (!PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings (versionName);
		}
	}

	private void OnConnectedToMaster(){
		PhotonNetwork.automaticallySyncScene = true;
		if (!InitiatedPlayerName) {
			InitiatedPlayerName = true;
			try{
				PhotonNetwork.playerName = Camera.main.GetComponent<networkcamera> ().controller.user + "(" + Camera.main.GetComponent<networkcamera> ().controller.rank + ")";
			}
			catch{
			}
		}
		PhotonNetwork.JoinLobby(TypedLobby.Default);

		if (m_pingCoroutine != null)
			StopCoroutine(m_pingCoroutine);
		m_pingCoroutine = StartCoroutine(C_SetPing());
	}

	private void OnJoinedLobby(){

	}


	private void OnDisconnectedFromPhoton(){
		print ("Disconnected from photon");
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("NetworkLobby")) {
			Application.LoadLevel ("Lobby");	
		}
		else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			Application.LoadLevel ("Lobby");	
		}

	}


	public void quickJoin()
	{
		StartCoroutine (quickJoinLoop ());
	}

	public IEnumerator quickJoinLoop()
	{
		if (PhotonNetwork.inRoom) {
			PhotonNetwork.LeaveRoom ();
		}
		print ("leaving room");
		while (PhotonNetwork.insideLobby == false) {
			yield return null;
		}
		print ("joining room");
		PhotonNetwork.JoinRandomRoom ();
		while (PhotonNetwork.inRoom == false) {
			yield return null;
		}
		print ("Joined" + PhotonNetwork.room.Name);
		if (PhotonNetwork.room.Name.Contains ("LevelEditorlalala")) {
			StartCoroutine (quickJoinLoop ());
		} else {
			CurrentRoom.SetActive (true);
		}
	}

	public void OnJoinedRoom()
	{
		InRoom = true;
		m_roomCustomProperties = PhotonNetwork.room.CustomProperties;
		string IsLE = m_roomCustomProperties ["IsLevelEditor"].ToString ();

		//Check if room created is for level editor
		if (IsLE == "false") {
			if (CurrentRoom != null) {
				CurrentRoom.SetActive (true);
				if (Camera.main.GetComponent<networkcamera> () != null) {
					if (Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf == true) {
						Camera.main.GetComponent<networkcamera> ().multiplayertab.SetActive (true);
						Camera.main.GetComponent<networkcamera> ().campaigntab.SetActive (false);
					}
				}
			}
			PlayerLayoutGroup.GetComponent<PlayerLayoutGroup> ().JoinedRoom ();
		} else {
			PhotonNetwork.room.IsVisible = false;
			PhotonNetwork.room.IsOpen = true;
			PhotonNetwork.LoadLevel ("LevelEditor");
		}
	}


	public void OnReceivedRoomListUpdate()
	{
		RoomInfo[] roomie = PhotonNetwork.GetRoomList ();
		foreach (RoomInfo room in roomie) {
			RoomList.Add (room.Name);


		}
	}

	//Called from server list button-> Join rooms
	public IEnumerator OnClickJoinRoom(string roomName)
	{
		while (PhotonNetwork.insideLobby == false) {
			yield return null;
		}
		PhotonNetwork.JoinRoom (roomName);
	}

	public IEnumerator SetLastRoom()
	{
		//Wait until user joined the new room
		while (PhotonNetwork.inRoom == false) {
			yield return null;
		}

		LastRoomName = PhotonNetwork.room.Name;
	}

	public void LeaveRoom()
	{
		if (PhotonNetwork.inRoom) {
			PendingRoomToJoin = "true";
			if (PhotonNetwork.LeaveRoom ()) {
			} else {
			}
		}

	}



	///////PLAYERNETWORK SCRIPTS BELOW
	/// 
	private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//PhotonNetwork.Instantiate ("spine-space-cat", Vector3.zero, Quaternion.identity, 0);
		/*
		if (scene.name == "level1") {
			print ("loaded level");
			if (PhotonNetwork.isMasterClient) {
				print ("master");
				MasterLoadedGame ();
			} else {
				print ("nonmaster");
				NonMasterLoadedGame ();
			}
		} else if (scene.name=="LevelEditor"){
			//Immediaely create player if in level editor
			GameObject player=GameObject.FindGameObjectWithTag("Player");
			RoomOptions roomOptions = new RoomOptions ();
			roomOptions.maxPlayers = 1;
			PhotonNetwork.JoinOrCreateRoom ("leveleditor",roomOptions,TypedLobby.Default);
			//Do not create more than one player
			if (player == null) {
				Instantiate (playerprefab);
			}
		}
		*/

	}

	private void MasterLoadedGame()
	{
		PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
		PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
	}

	private void NonMasterLoadedGame()
	{
		PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
	}

	[PunRPC]
	private void RPC_LoadGameOthers()
	{
		Camera.main.GetComponent<networkcamera> ().LoadingController.GetComponent<Loader> ().StartLoad ("Level1", true);
	}

	[PunRPC]
	private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
	{
		print ("players" + PlayersInGame);
		PlayersInGame++;
		if (PlayersInGame == PhotonNetwork.playerList.Length)
		{
			print("All players are in the game scene.");
			PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
		}

	}


	[PunRPC]
	private void RPC_CreatePlayer()
	{
		PhotonNetwork.Instantiate ("spine-space-cat", Vector3.zero, Quaternion.identity, 0);
	}

	private IEnumerator C_SetPing()
	{
		while (PhotonNetwork.connected)
		{
			m_playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
			PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

			yield return new WaitForSeconds(5f);
		}

		yield break;
	}




}
