using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.IO;
using System;
using System.Collections.Generic;

using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class ChatMessage
{
   public double lastModifiedSecond;
   public string chatMessage;

   public ChatMessage(double last, string chat)
   {
      lastModifiedSecond = last;
      chatMessage = chat;
   }

   // Remaining implementation of Person class.
}

public class networkcamera : MonoBehaviour {
	public float LastFPS;
	public InputField SearchInputField;
	public Text InfoPanelText;
	public string SearchingUsername="";
	public bool FirstTimeLoadingLevels=true;

	public GameObject LevelButton;
	public GameObject LoadingController;
	public GameObject FPS;
	public string CurrentRoom;
	public RoomInfo[] rooms;

	public InputField ChatInput;
	public Text SelectedChannelText;
	public string LastChatOutput="";
	public List<ChatMessage> lastchatlist = new List<ChatMessage>();
	public List<ChatMessage> chatlist = new List<ChatMessage>();
	List<string> ratinglist = new List<string>(new string[] {});
	List<string> ratingProcessedlist = new List<string>(new string[] {});
	public bool InitChat=false;

	public bool IsChatActivated=false;
	public string CurrentChatRoom="Main";

	public bool CanSendChatMessage = true;
	public int MaximumChatCount = 1000;
	public int RecentNumberOfChat=0;
	public int NumberOfTimesSpammed = 0;

	public string activem="";
	public int networkmode=0;
	public bool DeleteMap = false;
	public bool PauseGame;
	public int CampaignPageNo = 1;
	public int FriendPageNo = 1;
	public int MessagePageNo = 0;
	public string user="anonymous";
	public List<string> fileInfo;
	public List<string> campaignfileInfo;
	public List<string> friendfileInfo;
	public List<string> messagesfileinfo;
	public List<string> usermessagesfileinfo;
	public int count;
	public int stringlocation;
	public float rank=0;
	public float exp=0;

	public Sprite NormalPage;
	public Sprite SelectedPage;

	public GameObject InvitePanel;
	public GameObject InvitedUsernameInputField;
	public GameObject InvitedMapnameInputField;

	public GameObject CurrentRoomPanel;
	public GameObject HostButton;
	public GameObject TopPanel;
	public GameObject LeftPanel;

	public GameObject cursor;
	public GameObject chattab;
	public GameObject messagetab;
	public GameObject campaigntab;
	public GameObject multiplayertab;
	public GameObject customisetab;
	public GameObject friendtab;
	public GameObject SceneController;
	public Controllers controller;
	public GA_FREE_Demo08 animator;
	public GameObject InfoText;

	public GameObject SpeedSlider;
	public GameObject SpeedNoText;
	public GameObject AccSlider;
	public GameObject AccNoText;
	public GameObject JumpSlider;
	public GameObject JumpNoText;
	public GameObject ExpSlider;
	public GameObject ExpNoText;

	public int SSpeed=100;
	public int SAcc=100;
	public int SJump=100;
	public int SExpBonus=100;
	public int PointsRemaining=0;

	public GameObject LoadPanelNoMap;
	public GameObject ServerListPanel;
	public GameObject LoadPanel;
	public GameObject activemapnameobj;
	public GameObject mapinputfield;
	public GameObject ScrollBarVertical;
	public GameObject content;
	public GameObject mapbarprefab;
	public GameObject CreateButton;
	public GameObject UsernameText;
	public GameObject RankText;
	public GameObject ExpText;
	public GameObject PointNoText;

	public GameObject MultiplayerToggleImage;
	public GameObject CustomiseToggleImage;
	public GameObject CampaignToggleImage;
	public GameObject FriendToggleImage;
	public GameObject MessageToggleImage;
	public GameObject ChatToggleImage;

	public int CampaignCurrentPage;
	public GameObject CampaignMaxPage;
	public GameObject CampaignPageInputField;
	public GameObject CampaignLeftButton;
	public GameObject CampaignFirstButton;
	public GameObject CampaignSecondButton;
	public GameObject CampaignThirdButton;
	public GameObject CampaignRightButton;
	public GameObject CampaignLevelButton0;
	public GameObject CampaignLevelButton1;
	public GameObject CampaignLevelButton2;

	public int FriendCurrentPage;
	public GameObject FriendMaxPage;
	public GameObject FriendPageInputField;
	public GameObject FriendLeftButton;
	public GameObject FriendFirstButton;
	public GameObject FriendSecondButton;
	public GameObject FriendThirdButton;
	public GameObject FriendRightButton;
	public GameObject FriendLevelButton0;
	public GameObject FriendLevelButton1;
	public GameObject FriendLevelButton2;
	public GameObject FriendInfoPanel;
	public GameObject FriendInfoText;
	public GameObject FriendUserText;
	public GameObject FriendToInput;
	public GameObject FriendTitleInput;
	public GameObject FriendBodyInput;

	public GameObject FriendMessageButton;
	public GameObject FriendViewLevelButton;
	public GameObject FriendDropdown;
	public GameObject FriendRemove;
	public GameObject FriendAdd;

	public int MessageCurrentPage;
	public GameObject MessageMaxPage;
	public GameObject MessagePageInputField;
	public GameObject MessageLeftButton;
	public GameObject MessageFirstButton;
	public GameObject MessageSecondButton;
	public GameObject MessageThirdButton;
	public GameObject MessageRightButton;
	public GameObject MessageLevelButton0;
	public GameObject MessageLevelButton1;
	public GameObject MessageLevelButton2;
	public GameObject MessageDeleteButton0;
	public GameObject MessageDeleteButton1;
	public GameObject MessageDeleteButton2;
	public GameObject MessageInfoPanel;
	public GameObject MessageInfoText;

	public GameObject ToInput;
	public GameObject TitleInput;
	public GameObject BodyInput;

	public GameObject ReadPanel;
	public GameObject ReadFromInput;
	public GameObject ReadTitleInput;
	public GameObject ReadBodyInput;

	public GameObject LevelTitleText;
	public bool IsLoadingLevels=false;
	public string CurrentLevelTab = "latest";
	public string LastLevelTab = "latest";
	public string LastLevelTitleText="latest";

	public Sprite Remember0;
	public Sprite Remember1;
	public List<string> maplist;
	protected string MyStorageBucket = "gs://composite-store-188023.appspot.com/";
	public bool GoingLevelEditorAlone=false;
	private string logText = "";
	protected string fileContents;

	public Mosframe.RealTimeInsertItemExample DynamicScrollView;
	public AWSControl AWS;
	public bool IsLevelEditor=false;
	//Ensures buttons are pressed only once
	public bool UpdatingCampaign=false;
	public bool UpdatingMessage=false;
	public bool UpdatingFriend=false;

	public bool LoadingCampaignLevel=false;
	// Use this for initialization
	void Start () {
		//Application.targetFrameRate = 20;
		GameObject AWSControl = GameObject.FindGameObjectWithTag ("awscontrol");
		AWS = AWSControl.GetComponent<AWSControl> ();
		animator = SceneController.GetComponent<GA_FREE_Demo08> ();
		//Draws username,rank and exp text
		controller=GameObject.FindGameObjectWithTag ("controller").GetComponent<Controllers>();
		FPS = GameObject.FindGameObjectWithTag ("FPS");
		user = controller.user;
		rank=controller.rank;
		exp = controller.exp;

		PlayerPrefs.SetString ("lastmapname", "");
		StartCoroutine (RefreshCampaigns ());
		UpdatingCampaign = false;
		//StartCoroutine (DownloadFriendList ());
		//StartCoroutine (RefreshMessages ());
		StartCoroutine(DownloadChat(CurrentChatRoom));
		//Handles Customisation Panel
		float expneeded = (float)(30) * Mathf.Pow((float)(1.25) ,(controller.rank + 1));

		UsernameText.GetComponent<Text>().text=controller.user;
		RankText.GetComponent<Text> ().text = controller.rank.ToString();
		ExpText.GetComponent<Text>().text=controller.exp+" of "+expneeded;
		PointNoText.GetComponent<Text> ().text = (Mathf.RoundToInt (controller.rank) + 150).ToString();


		SpeedSlider.GetComponent<Slider> ().value = controller.SSpeed;
		SpeedNoText.GetComponent<Text> ().text = controller.SSpeed.ToString ();

		AccSlider.GetComponent<Slider> ().value = controller.SAcc;
		AccNoText.GetComponent<Text> ().text = controller.SAcc.ToString ();

		JumpSlider.GetComponent<Slider> ().value = controller.SJump;
		JumpNoText.GetComponent<Text> ().text = controller.SJump.ToString ();

		ExpSlider.GetComponent<Slider> ().value = controller.SExpBonus;
		ExpNoText.GetComponent<Text> ().text = controller.SExpBonus.ToString ();

		PointsRemaining= Mathf.RoundToInt((150f+ float.Parse(RankText.GetComponent<Text>().text))-SSpeed-SAcc-SJump-SExpBonus);
		PointNoText.GetComponent<Text>().text=PointsRemaining.ToString();

		DynamicScrollView = content.GetComponent<Mosframe.RealTimeInsertItemExample> ();
		StartCoroutine (ChatCounter ());

		MessageCurrentPage = 1;
		FriendCurrentPage = 1;
		CampaignCurrentPage = 1;

	}

	// Update is called once per frame
	void Update () {
		if (LastFPS != null && Time.time - LastFPS > 0.5f) {
			LastFPS = Time.time;
			FPS.GetComponent<Text> ().text = (1.0f / Time.deltaTime).ToString ().Split ('.') [0] + " FPS";
		}
		if (Camera.main.gameObject != gameObject) {
			Destroy (gameObject);
		}
		mapinputfield = GameObject.FindGameObjectWithTag ("mapinputfield");
		if (mapinputfield != null) {
			if (mapinputfield.GetComponent<InputField> ().text == "") {
				mapinputfield.GetComponent<InputField> ().text = "maps/123.txt";
			}
		}
		if (PhotonNetwork.inRoom) {
			CurrentRoom = PhotonNetwork.room.Name;
		} else if (PhotonNetwork.insideLobby) {
			CurrentRoom = "inLobby";
		} else if (PhotonNetwork.connected) {
			CurrentRoom = "none";
		} else {
			CurrentRoom = "not connected";
		}


		//Handles Points in customisation panel
		PointsRemaining= Mathf.RoundToInt((150f+ float.Parse(RankText.GetComponent<Text>().text))-SSpeed-SAcc-SJump-SExpBonus);
		PointNoText.GetComponent<Text>().text=PointsRemaining.ToString();

		//Create new link with main panel if it is destroyed
		if (ServerListPanel == null) {
			GameObject serverlistpanel = GameObject.FindGameObjectWithTag ("serverlistpanel");
			if (serverlistpanel != null) {
				ServerListPanel = serverlistpanel;
			}
		}

		//Chatbox
		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
			ChatInput.ActivateInputField ();
			SendChat ();

		}

	}

	public void SendChat()
	{
		if (ChatInput.text != "" && CanSendChatMessage && ChatInput.text.Replace("/","")!="" && InitChat) {
			RecentNumberOfChat += 1;

			//Disable for 5 seconds if found spamming within 3 second
			if (RecentNumberOfChat >= 6) {
				NumberOfTimesSpammed += 1;
				SelectedChannelText.text += "<color=#FF635B>Do not spam.</color>\n";
				CanSendChatMessage = false;
				StartCoroutine (ActivateChat ());
			} else {
				StartCoroutine (UploadChat (CurrentChatRoom, PhotonNetwork.player.NickName , ChatInput.text));
				ChatInput.text = "";
			}
		}

		if (!Application.isMobilePlatform) {
			ChatInput.ActivateInputField ();
		}
	}
	//click load button
	public void loadbuttondown(bool otheruser)
	{
		LoadPanelNoMap.SetActive (false);
		string Muser = "";
		if (!otheruser) {
			PauseGame = true;
			if (CurrentLevelTab == "latest") {
				LevelTitleText.GetComponent<Text> ().text = "latest";
				StartCoroutine (updatemaplist (true, "", "latest"));
			}
			else if (CurrentLevelTab == "popular") {
				LevelTitleText.GetComponent<Text>().text="popular";
				StartCoroutine (updatemaplist (true, "", "popular"));
			}
			else if (CurrentLevelTab == "mylevels") {
				LevelTitleText.GetComponent<Text>().text="my levels";
				StartCoroutine (updatemaplist (false,"",""));
			}
			else if (CurrentLevelTab == "search") {
				LevelTitleText.GetComponent<Text>().text="searching user: "+SearchingUsername;
				StartCoroutine (updatemaplist (true,SearchingUsername,""));
			}
			LoadPanel.SetActive (true);
		} else {
			Muser = FriendUserText.GetComponent<Text> ().text;
			PauseGame = true;
			CurrentLevelTab = "search";
			LevelTitleText.GetComponent<Text>().text="searching user: "+Muser;
			StartCoroutine (updatemaplist (otheruser,Muser,""));
		}
	}


	public IEnumerator updatemaplist(bool otheruser,string Muser,string extra)
	{
		//Get list of files in directory
		fileInfo.Clear ();
		maplist.Clear ();

		DynamicScrollView.clearList ();

		string dir;
		string fileContents;

		if (extra == "latest" || extra=="popular") {
			dir = "maps/" + extra + "/";

			//Different for latest vs popular, as latest contains lastmodified in seconds for first /
			if (extra == "latest") {
				AWS.AWSList (dir, 2);
			}
			else if (extra == "popular") {
				AWS.AWSList (dir, 0);
			}

			while (AWS.result == "") {
				yield return null;
			}
			string output = AWS.result;
			AWS.result = "";
			var split = output.Split ('+');

			ratinglist.Clear ();
			ratingProcessedlist.Clear ();

			if (output != "empty") {
				for (int i = 0; i < split.Length; i++) {
					var split2 = split [i].Split ('/');
					//Different for latest vs popular, as latest contains lastmodified in seconds in first /
					string rankandmapname="";
					if (extra == "latest") {
						string lastmodified = split2 [0];
						rankandmapname = split2 [7];
						fileInfo.Add (lastmodified + ";" + rankandmapname);
					}
					else if (extra == "popular") {
						rankandmapname = split2 [6];
						fileInfo.Add (rankandmapname);
					}

					string mapuser = rankandmapname.Split (';') [0];
					string mapname = rankandmapname.Split (';') [1];
					//Get rating list
					AWS.AWSList ("user/" + mapuser + "/maps/" + "ratings/", 0);
					while (AWS.result == "") {
						yield return null;
					}
					string output2 = AWS.result;
					AWS.result = "";
					var splitt = output2.Split ('+');


					if (output2 != "empty") {
						for (int j = 0; j < splitt.Length; j++) {
							var splitt2 = splitt [j].Split ('/');

							string mapname2 = splitt2 [8];
							string rating2 = splitt2 [9];
							if (mapname2+".txt" == mapname) {
								ratinglist.Add (rating2 + ";" + mapuser + ";" + mapname2);

								//Add the product of rating score and rating quantity to a list
								//Hence, sort the list to get most popular levels later
								string ratingscore=rating2.Split(';')[0];
								string ratingquantity=rating2.Split(';')[1];
								float product = float.Parse (ratingscore) * float.Parse (ratingquantity);
								ratingProcessedlist.Add (product.ToString() + ";"+ mapuser + ";" + mapname2+ ";" + ratingscore + ";"+ ratingquantity);
							}
						}
					}
				}
				loadmaps (otheruser, Muser,extra);	
			}

		}
		else if (extra == "") {
			//Get rating list
			if (!otheruser) {
				dir = "user/" + user + "/maps/" + "ratings/";
			} else {
				dir = "user/" + Muser + "/maps/" + "ratings/";
			}
			AWS.AWSList (dir, 0);
			while (AWS.result == "") {
				yield return null;
			}
			string output = AWS.result;
			AWS.result = "";
			var split = output.Split ('+');
			ratinglist.Clear ();
			ratingProcessedlist.Clear ();

			if (output != "empty") {
				for (int i = 0; i < split.Length; i++) {
					var split2 = split [i].Split ('/');
					string tempuser = "";
					if (!otheruser) {
						tempuser = user;
					} else {
						tempuser = Muser;
					}

					string mapname = split2 [8];
					string rating = split2 [9];

					ratinglist.Add (rating + ";" + tempuser + ";" + mapname);
				}
			}

			//Download map list first
			if (!otheruser) {
				dir = "user/" + user + "/maps/" + "maplist.txt";
			} else {
				dir = "user/" + Muser + "/maps/" + "maplist.txt";
			}
			AWS.AWSDownload ("1", dir);
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "none") {
				AWS.result = "";
				LoadPanelNoMap.SetActive (true);
			} else if (AWS.result.Split ('|') [0] == "1") {
				LoadPanelNoMap.SetActive (false);
				fileContents = AWS.result.Split ('|') [1];
				AWS.result = "";

				var arr = fileContents.Split ('/');
				foreach (string file in arr) {
					fileInfo.Add (file);
				}
				loadmaps (otheruser, Muser,"");	
			}
		}
	}



	public void loadmaps(bool otheruser,string Muser,string extra)
	{
		//fileInfo.Reverse ();
		foreach (var file in fileInfo)
		{
			string myfile = file.ToString().Replace(".txt", "");
			maplist.Add(myfile);
		}

		if (extra == "") {
			foreach (string file in maplist) {
				string tempuser = "";
				if (!otheruser) {
					tempuser = user;
				} else {
					tempuser = Muser;
				}

				string temprating = "-";
				string tempratingNum = "-";
				foreach (string rating in ratinglist) {
					if (rating.Split(';')[2]+";"+rating.Split(';')[3]== tempuser + ";" + file) {
						temprating = rating.Split (';') [0];
						tempratingNum = rating.Split (';') [1];

					}
				}
				DynamicScrollView.AddItem (0, file, "Made By: " + tempuser, temprating, tempratingNum);
			}
		}
		else if (extra == "latest") {
			//Sort by latest
			maplist.Sort ();

			foreach (string file in maplist) {
				
				string temprating = "-";
				string tempratingNum = "-";

				foreach (string rating in ratinglist) {
					if (rating.Split(';')[2]+";"+rating.Split(';')[3]== file.Split (';') [1]+ ";"+file.Split (';') [2]) {
						temprating = rating.Split (';') [0];
						tempratingNum = rating.Split (';') [1];

					}
				}
				DynamicScrollView.AddItem (0, file.Split (';') [2], "Made By: " + file.Split (';') [1], temprating, tempratingNum);


			}
		}
		else if (extra == "popular") {
			ratingProcessedlist.Sort (SortByRating);

			//We only want levels with at least one rating to be in popular
			for (int i=0;i<ratingProcessedlist.Count();i++) {
				string temprating = "-";
				string tempratingNum = "-";

				temprating = ratingProcessedlist [i].Split (';') [3];
				tempratingNum = ratingProcessedlist [i].Split (';') [4];
				DynamicScrollView.AddItem (0, ratingProcessedlist [i].Split (';') [2], "Made By: " + ratingProcessedlist [i].Split (';') [1], temprating, tempratingNum);
			}
		}
	}

	//Return sorted rating list
	static int SortByRating(string s1, string s2)
	{
		return float.Parse(s1.Split(';')[0]).CompareTo(float.Parse(s2.Split(';')[0]));
	}

	//Loads new map
	public IEnumerator loadmap(string mapuser,string nameofmap,bool iscampaign)
	{



		maplist.Clear();

		//Sets lastmap to what is loaded.
		string data = null;
		string mydir="";

		if (!iscampaign) {
			mydir = "user/" + mapuser + "/maps/" + nameofmap + ".txt";

		} else if (iscampaign) {
			mydir = "campaign/" + nameofmap + ".txt";
		}
		//Downloads user map list
		AWS.AWSDownload("2",mydir);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";

		} else if (AWS.result.Split('|')[0]=="2"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";
			string loaded=fileContents;
			PlayerPrefs.SetString ("lastmap", loaded);
			PlayerPrefs.SetString ("lastmapname", mydir);

			controller.lastmap = loaded;
			controller.lastmapname = mydir;

			//Saves map locally
			activem=loaded;
			LoadPanel.SetActive(false);

			//Control level editor or level1
			if (!IsLevelEditor) {
				StartCoroutine( HostButton.GetComponent<CreateRoom> ().OnClick_CreateRoom (mydir,"false"));
			} else {
				StartCoroutine( HostButton.GetComponent<CreateRoom> ().OnClick_CreateRoom (mydir,"true"));
				//StartCoroutine(EnterLevelEditor ());
				//StartCoroutine(HostButton.GetComponent<CreateRoom> ().OnClick_CreateRoom (mydir, loaded,"true"));
			}
			LoadingCampaignLevel = false;
			//mapinputfield.GetComponent<InputField>().text=mydir;
		}








	}
	public IEnumerator EnterLevelEditor()
	{
		if (PhotonNetwork.inRoom) {
			PhotonNetwork.LeaveRoom ();
		}
		while (!PhotonNetwork.insideLobby) {
			yield return null;
		}

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.maxPlayers = 20;
		PhotonNetwork.JoinOrCreateRoom ("LevelEditor"+UnityEngine.Random.Range(0,99999999999999).ToString(), roomOptions, TypedLobby.Default);
		while (!PhotonNetwork.inRoom) {
			yield return null;
		}

		LoadingController.GetComponent<Loader> ().StartLoad ("LevelEditor", true);
	}
	//Unpause game
	public void unpausegame()
	{
		PauseGame = false;
	}

	//Clear maplist
	public void clearmaplist()
	{
		maplist.Clear();
	}

	public void SetIsLevelEditor()
	{
		LevelButton.SetActive (false);
		IsLevelEditor = true;

		LastLevelTab = CurrentLevelTab;
		LastLevelTitleText = LevelTitleText.GetComponent<Text> ().text;

		CurrentLevelTab = "mylevels";
		LevelTitleText.GetComponent<Text>().text="my levels";
	}
	public void SetNotLevelEditor()
	{
		LevelButton.SetActive (true);
		if (IsLevelEditor) {
			IsLevelEditor = false;

			CurrentLevelTab = LastLevelTab;
			LevelTitleText.GetComponent<Text> ().text = LastLevelTitleText;
		}
	}

	public IEnumerator UploadChat(string room,string username,string chatmessage) {
		
		//Instantly add locally sent message
		//"<color=#FFFFFFFF> from </color>]"
		SelectedChannelText.text += "<color=#FFA53F>"+username+": " +"</color>" +  "<color=#FFD8AF>"+chatmessage+"</color>" +"\n";

		AWS.AWSUpload("chatrooms/"+room+"/"+(username + ": " + chatmessage).Replace("/","")+".txt","");
		while (AWS.result == "") {
			yield return null;
		}
		AWS.result = "";

	}

	public IEnumerator DownloadChat(string room) {
		yield return new WaitForSeconds(1f);
		if (!PhotonNetwork.connected) {
			yield break;
		}
		AWS.AWSList("chatrooms/"+room,1);
		while (AWS.result == "" && AWS.result.Substring(0,6)=="Delete") {
			print ("Loading" + AWS.result);
			yield return null;
		}
		if (SelectedChannelText.text == "Loading chat...") {
			SelectedChannelText.text = "";
		}
		string output = AWS.result;
		AWS.result = "";


		chatlist.Clear ();
		string tempfinaloutput = "";

		chatlist = ConvertChat (output, false);
		var fullchatlist = ConvertChat (output, true);

		chatlist.Sort((x, y) => x.lastModifiedSecond.CompareTo(y.lastModifiedSecond));
		fullchatlist.Sort((x, y) => x.lastModifiedSecond.CompareTo(y.lastModifiedSecond));

		InitChat = true;

		//Return the sorted chat list
		for (int j = 0; j < chatlist.Count; j++) {
			string fullmessage = chatlist [j].chatMessage.Split ('/') [1];
			string chatuser = fullmessage.Split (':') [0];

			//Messages from "chatbot" are level editor invitations
			if (chatuser != "chatbot") {
				string chatmessage = fullmessage.Substring (chatuser.Length + 1);
				tempfinaloutput += "<color=#FFA53F>" + chatuser + ": " + "</color>" + "<color=#FFD8AF>" + chatmessage + "</color>" + '\n';
			} else {
				string FromUsername = fullmessage.Split (':') [1].Substring(1);
				string ToUsername = fullmessage.Split (':') [2];
				string mapname = fullmessage.Split (':') [3];
				if (ToUsername == controller.user) {
					InvitePanel.SetActive (true);
					animator.MovePanels (14);
					InvitedUsernameInputField.GetComponent<InputField> ().text = FromUsername;
					InvitedMapnameInputField.GetComponent<InputField> ().text = mapname;
				}
			}
		}

		if (fullchatlist.Count > MaximumChatCount) {
			StartCoroutine (DeleteChat (fullchatlist,CurrentChatRoom));
		}
		SelectedChannelText.text += tempfinaloutput;
		StartCoroutine (DownloadChat (CurrentChatRoom));
	}

	public IEnumerator DeleteChat(List<ChatMessage> newlist,string chatroom )
	{
		string dest = "";
		for (int j = 0; j < newlist.Count-MaximumChatCount; j++) {
			dest = "chatrooms/"+chatroom+"/"+ newlist [j].chatMessage.Substring(newlist[j].chatMessage.Split('/')[0].Length+1)+".txt";
			//print (dest + "ori" + newlist [j]);
			AWS.AWSDelete (dest,true);
			while (AWS.result == "") {
				yield return null;
			}
			AWS.result = "";
		}
		yield break;
	}

	public List<ChatMessage> ConvertChat(string output,bool ReturnEverything)
	{
		List<ChatMessage> tempchatlist=new List<ChatMessage>{};

		var split = output.Split('+');
		for (int i = 0; i < split.Length; i++) {
			string message = split [i];

			//Aws lists return an empty string?
			if (message != "") {
				var split2 = message.Split ('/');
				double lastmodifiedSecond = double.Parse(split2 [0]);
				string chatmessagewithTXT=split2[1];
				string chatMessage = chatmessagewithTXT.Substring (CurrentChatRoom.Length, chatmessagewithTXT.Length - 8);

				string chatuser = chatMessage.Split (' ')[0];

				string finalchatmessage=  " " + chatMessage.Substring(chatuser.Length+1);

				string result = lastmodifiedSecond.ToString() + "/" + chatuser + finalchatmessage;
				//No longer retreive messages sent locally after first retrieval
				ChatMessage chatMessageObj = new ChatMessage(lastmodifiedSecond, result);
				if (!ReturnEverything) {

					if (InitChat == true && chatMessage.Split (':') [0].Contains (PhotonNetwork.player.NickName)) {

					} else if (!lastchatlist.Exists (x => x.lastModifiedSecond == lastmodifiedSecond) 
					&& !lastchatlist.Exists (x => x.chatMessage == chatMessage)) {
						tempchatlist.Add (chatMessageObj);
						lastchatlist.Add (chatMessageObj);
					}
				} else {
					tempchatlist.Add (chatMessageObj);
				}
			}
		}
		return tempchatlist;
	}
	public void MultiplayerTab()
	{
		multiplayertab.SetActive (true);
		campaigntab.SetActive (false);
		customisetab.SetActive (false);
		friendtab.SetActive (false);
		messagetab.SetActive (false);

		animator.MovePanels (3);
	}
	public void CampaignTab()
	{
		multiplayertab.SetActive (false);
		campaigntab.SetActive (true);
		customisetab.SetActive (false);
		friendtab.SetActive (false);
		messagetab.SetActive (false);

		animator.MovePanels (1);
		if (!UpdatingCampaign)
		{
			UpdatingCampaign = true;
			StartCoroutine (RefreshCampaigns ());
		}
	}
	public void CustomiseTab()
	{
		multiplayertab.SetActive (false);
		campaigntab.SetActive (false);
		customisetab.SetActive (true);
		friendtab.SetActive (false);
		messagetab.SetActive (false);

		animator.MovePanels (0);
	}

	public void FriendTab()
	{
		multiplayertab.SetActive (false);
		campaigntab.SetActive (false);
		customisetab.SetActive (false);
		friendtab.SetActive (true);
		messagetab.SetActive (false);

		animator.MovePanels (4);
		if (!UpdatingFriend) {
			UpdatingFriend = true;
			if (friendfileInfo.Count==0) {
				StartCoroutine (DownloadFriendList (true));
			}
		}
	}

	public void MessageTab()
	{
		multiplayertab.SetActive (false);
		campaigntab.SetActive (false);
		customisetab.SetActive (false);
		friendtab.SetActive (false);
		messagetab.SetActive (true);

		animator.MovePanels (5);
		if (!UpdatingMessage) {
			UpdatingMessage = true;
			StartCoroutine (RefreshMessages ());
		}
	}
	//Campaign Tab functions
	public void CampaignGoToPage()
	{
		CampaignPageNo = int.Parse(CampaignPageInputField.GetComponent<InputField>().text) -1;
		int maxpage = int.Parse(CampaignMaxPage.GetComponent<Text>().text);
		if (CampaignPageNo>=0 && CampaignPageNo+1<=maxpage)
		{
			if (CampaignPageNo+1 == 1) {
				//If on the first page
				CampaignFirstButton.GetComponent<Image>().sprite=SelectedPage;
				CampaignSecondButton.GetComponent<Image>().sprite=NormalPage;
				CampaignThirdButton.GetComponent<Image>().sprite=NormalPage;

				CampaignFirstButton.GetComponentInChildren<Text> ().text = "1";
				CampaignSecondButton.GetComponentInChildren<Text> ().text = "2";
				CampaignThirdButton.GetComponentInChildren<Text> ().text = "3";
			} else if (CampaignPageNo+1==maxpage) {
				//If on the last page
				if (maxpage == 2) {
					CampaignFirstButton.GetComponent<Image> ().sprite = NormalPage;
					CampaignSecondButton.GetComponent<Image> ().sprite = SelectedPage;
					CampaignThirdButton.GetComponent<Image> ().sprite = NormalPage;

					CampaignFirstButton.GetComponentInChildren<Text> ().text = "1";
					CampaignSecondButton.GetComponentInChildren<Text> ().text = "2";
					CampaignThirdButton.GetComponentInChildren<Text> ().text = "3";
				}
				else{
					CampaignFirstButton.GetComponent<Image> ().sprite = NormalPage;
					CampaignSecondButton.GetComponent<Image> ().sprite = NormalPage;
					CampaignThirdButton.GetComponent<Image> ().sprite = SelectedPage;

					CampaignFirstButton.GetComponentInChildren<Text> ().text = (CampaignPageNo - 1).ToString ();
					CampaignSecondButton.GetComponentInChildren<Text> ().text = (CampaignPageNo - 0).ToString ();
					CampaignThirdButton.GetComponentInChildren<Text> ().text = (CampaignPageNo + 1).ToString ();
				}
			} else {
				CampaignFirstButton.GetComponent<Image>().sprite=NormalPage;
				CampaignSecondButton.GetComponent<Image>().sprite=SelectedPage;
				CampaignThirdButton.GetComponent<Image>().sprite=NormalPage;

				CampaignFirstButton.GetComponentInChildren<Text> ().text = (CampaignPageNo -0).ToString ();
				CampaignSecondButton.GetComponentInChildren<Text> ().text = (CampaignPageNo + 1).ToString ();
				CampaignThirdButton.GetComponentInChildren<Text> ().text = (CampaignPageNo + 2).ToString ();
			}

			CampaignLevelButton0.name = campaignfileInfo [CampaignPageNo*3+ 0];
			CampaignLevelButton0.GetComponentInChildren<Text> ().text = campaignfileInfo [CampaignPageNo*3+ 0];
			CampaignLevelButton0.GetComponent<Button> ().interactable = true;

			if (campaignfileInfo.Count > CampaignPageNo * 3 + 1) {
				CampaignLevelButton1.name = campaignfileInfo [CampaignPageNo * 3 + 1];
				CampaignLevelButton1.GetComponentInChildren<Text> ().text = campaignfileInfo [CampaignPageNo*3+ 1];
				CampaignLevelButton1.GetComponent<Button> ().interactable = true;
			} else {
				CampaignLevelButton1.name = "disabledmessage";
				CampaignLevelButton1.GetComponentInChildren<Text> ().text = "";
				CampaignLevelButton1.GetComponent<Button> ().interactable = false;
			}

			if (campaignfileInfo.Count > CampaignPageNo * 3 + 2) {
				CampaignLevelButton2.name = campaignfileInfo [CampaignPageNo * 3 + 2];
				CampaignLevelButton2.GetComponentInChildren<Text> ().text = campaignfileInfo [CampaignPageNo*3+ 2];
				CampaignLevelButton2.GetComponent<Button> ().interactable = true;
			} else {
				CampaignLevelButton2.name = "disabledmessage";
				CampaignLevelButton2.GetComponentInChildren<Text> ().text = "";
				CampaignLevelButton2.GetComponent<Button> ().interactable = false;
			}
		}
	}
	public void CampaignLeftButtonDown()
	{
		if (CampaignCurrentPage > 0) {
			CampaignCurrentPage -= 1;
			CampaignPageInputField.GetComponent<InputField> ().text = CampaignCurrentPage.ToString();
			CampaignGoToPage ();
		}
	}

	public void CampaignRighButtonDown()
	{
		int maxpage = int.Parse(CampaignMaxPage.GetComponent<Text>().text);
		if (CampaignCurrentPage < maxpage) {
			CampaignCurrentPage += 1;
			CampaignPageInputField.GetComponent<InputField> ().text = CampaignCurrentPage.ToString();
			CampaignGoToPage ();
		}
	}

	public void CampaignPageButton(int pageno)
	{
		int firstpage = int.Parse(CampaignFirstButton.GetComponentInChildren<Text> ().text);
		CampaignPageNo = firstpage + pageno ;
		CampaignCurrentPage = CampaignPageNo;

		CampaignPageInputField.GetComponent<InputField> ().text = CampaignCurrentPage.ToString();
		CampaignGoToPage ();

	}

	public IEnumerator RefreshCampaigns()
	{
		AWS.AWSDownload ("3","campaign/maplist.txt");
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			UpdatingCampaign = false;
		} else if (AWS.result.Split('|')[0]=="3"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";

			campaignfileInfo.Clear ();
			var arr = fileContents.Split ('/');
			foreach (string file in arr) {
				print(file);
				campaignfileInfo.Add (file);
			}

			CampaignFirstButton.GetComponent<Button> ().interactable = false;
			CampaignSecondButton.GetComponent<Button> ().interactable = false;
			CampaignThirdButton.GetComponent<Button> ().interactable = false;

			if (fileContents == "") {
				CampaignFirstButton.GetComponent<Image> ().sprite = NormalPage;
				CampaignSecondButton.GetComponent<Image> ().sprite = NormalPage;
				CampaignThirdButton.GetComponent<Image> ().sprite = NormalPage;

				CampaignLevelButton0.name = "disabledmessage";
				CampaignLevelButton0.GetComponentInChildren<Text> ().text = "";
				CampaignLevelButton0.GetComponent<Button> ().interactable = false;

				CampaignLevelButton1.name = "disabledmessage";
				CampaignLevelButton1.GetComponentInChildren<Text> ().text = "";
				CampaignLevelButton1.GetComponent<Button> ().interactable = false;

				CampaignLevelButton2.name = "disabledmessage";
				CampaignLevelButton2.GetComponentInChildren<Text> ().text = "";
				CampaignLevelButton2.GetComponent<Button> ().interactable = false;
				yield break;
			} else {
				if (CampaignCurrentPage == 1) {
					CampaignFirstButton.GetComponent<Image> ().sprite = SelectedPage;
					CampaignSecondButton.GetComponent<Image> ().sprite = NormalPage;
					CampaignThirdButton.GetComponent<Image> ().sprite = NormalPage;
				}

				CampaignFirstButton.GetComponent<Button> ().interactable = true;
				if (campaignfileInfo.Count > 3) {
					CampaignSecondButton.GetComponent<Button> ().interactable = true;
				}
				if (campaignfileInfo.Count > 6) {
					CampaignThirdButton.GetComponent<Button> ().interactable = true;
				} 
				int maxpage = Mathf.FloorToInt ((campaignfileInfo.Count - 1) / 3) + 1;
				CampaignMaxPage.GetComponent<Text> ().text = maxpage.ToString ();
				if (CampaignCurrentPage == maxpage + 1) {
					if (maxpage == 1) {
						CampaignCurrentPage = 1;
					} else {
						CampaignCurrentPage = maxpage;
					}
				}
				CampaignPageInputField.GetComponent<InputField> ().text = CampaignCurrentPage.ToString();
				CampaignGoToPage ();

			}
			UpdatingCampaign = false;
		}
	}


	//Friend Tab functions
	public void FriendGoToPage()
	{
		FriendPageNo = int.Parse(FriendPageInputField.GetComponent<InputField>().text) -1;
		int maxpage = int.Parse(FriendMaxPage.GetComponent<Text>().text);
		if (FriendPageNo>=0 && FriendPageNo+1<=maxpage)
		{
			if (FriendPageNo+1 == 1) {
				//If on the first page
				FriendFirstButton.GetComponent<Image>().sprite=SelectedPage;
				FriendSecondButton.GetComponent<Image>().sprite=NormalPage;
				FriendThirdButton.GetComponent<Image>().sprite=NormalPage;

				FriendFirstButton.GetComponentInChildren<Text> ().text = "1";
				FriendSecondButton.GetComponentInChildren<Text> ().text = "2";
				FriendThirdButton.GetComponentInChildren<Text> ().text = "3";
			} else if (FriendPageNo+1==maxpage) {
				//If on the last page
				if (maxpage == 2) {
					FriendFirstButton.GetComponent<Image> ().sprite = NormalPage;
					FriendSecondButton.GetComponent<Image> ().sprite = SelectedPage;
					FriendThirdButton.GetComponent<Image> ().sprite = NormalPage;

					FriendFirstButton.GetComponentInChildren<Text> ().text = "1";
					FriendSecondButton.GetComponentInChildren<Text> ().text = "2";
					FriendThirdButton.GetComponentInChildren<Text> ().text = "3";
				}
				else{
					FriendFirstButton.GetComponent<Image> ().sprite = NormalPage;
					FriendSecondButton.GetComponent<Image> ().sprite = NormalPage;
					FriendThirdButton.GetComponent<Image> ().sprite = SelectedPage;

					FriendFirstButton.GetComponentInChildren<Text> ().text = (FriendPageNo - 1).ToString ();
					FriendSecondButton.GetComponentInChildren<Text> ().text = (FriendPageNo - 0).ToString ();
					FriendThirdButton.GetComponentInChildren<Text> ().text = (FriendPageNo + 1).ToString ();
				}
			} else {
				FriendFirstButton.GetComponent<Image>().sprite=NormalPage;
				FriendSecondButton.GetComponent<Image>().sprite=SelectedPage;
				FriendThirdButton.GetComponent<Image>().sprite=NormalPage;

				FriendFirstButton.GetComponentInChildren<Text> ().text = (FriendPageNo -0).ToString ();
				FriendSecondButton.GetComponentInChildren<Text> ().text = (FriendPageNo + 1).ToString ();
				FriendThirdButton.GetComponentInChildren<Text> ().text = (FriendPageNo + 2).ToString ();
			}

			FriendLevelButton0.name = friendfileInfo [FriendPageNo*3+ 0];
			FriendLevelButton0.GetComponentInChildren<Text> ().text = friendfileInfo [FriendPageNo*3+ 0].Split(';')[0] + "<color=#FFFFFFFF>("+ friendfileInfo [FriendPageNo*3+ 0].Split(';')[1] +")</color>]";
			FriendLevelButton0.GetComponent<Button> ().interactable = true;

			if (friendfileInfo.Count > FriendPageNo * 3 + 1) {
				FriendLevelButton1.name = friendfileInfo [FriendPageNo * 3 + 1];
				FriendLevelButton1.GetComponentInChildren<Text> ().text = friendfileInfo [FriendPageNo*3+ 1].Split(';')[0] + "<color=#FFFFFFFF>("+ friendfileInfo [FriendPageNo*3+ 1].Split(';')[1] +")</color>]";;
				FriendLevelButton1.GetComponent<Button> ().interactable = true;
			} else {
				FriendLevelButton1.name = "disabledmessage";
				FriendLevelButton1.GetComponentInChildren<Text> ().text = "";
				FriendLevelButton1.GetComponent<Button> ().interactable = false;
			}

			if (friendfileInfo.Count > FriendPageNo * 3 + 2) {
				FriendLevelButton2.name = friendfileInfo [FriendPageNo * 3 + 2];
				FriendLevelButton2.GetComponentInChildren<Text> ().text = friendfileInfo [FriendPageNo*3+ 2].Split(';')[0] + "<color=#FFFFFFFF>("+ friendfileInfo [FriendPageNo*3+ 2].Split(';')[1] +")</color>]";
				FriendLevelButton2.GetComponent<Button> ().interactable = true;
			} else {
				FriendLevelButton2.name = "disabledmessage";
				FriendLevelButton2.GetComponentInChildren<Text> ().text = "";
				FriendLevelButton2.GetComponent<Button> ().interactable = false;
			}
		}
	}
	public void FriendLeftButtonDown()
	{
		if (FriendCurrentPage > 0) {
			FriendCurrentPage -= 1;
			FriendPageInputField.GetComponent<InputField> ().text = FriendCurrentPage.ToString();
			FriendGoToPage ();
		}
	}

	public void FriendRighButtonDown()
	{
		int maxpage = int.Parse(FriendMaxPage.GetComponent<Text>().text);
		if (FriendCurrentPage < maxpage) {
			FriendCurrentPage += 1;
			FriendPageInputField.GetComponent<InputField> ().text = FriendCurrentPage.ToString();
			FriendGoToPage ();
		}
	}

	public void FriendPageButton(int pageno)
	{
		int firstpage = int.Parse(FriendFirstButton.GetComponentInChildren<Text> ().text);
		FriendPageNo = firstpage + pageno ;
		FriendCurrentPage = FriendPageNo;

		FriendPageInputField.GetComponent<InputField> ().text = FriendCurrentPage.ToString();
		FriendGoToPage ();

	}

	public void RefreshFriends()
	{
		friendfileInfo.Reverse ();

		FriendFirstButton.GetComponent<Button> ().interactable = false;
		FriendSecondButton.GetComponent<Button> ().interactable = false;
		FriendThirdButton.GetComponent<Button> ().interactable = false;

		if (friendfileInfo.Count==0) {
			FriendFirstButton.GetComponent<Image> ().sprite = NormalPage;
			FriendSecondButton.GetComponent<Image> ().sprite = NormalPage;
			FriendThirdButton.GetComponent<Image> ().sprite = NormalPage;

			FriendLevelButton0.name = "disabledmessage";
			FriendLevelButton0.GetComponentInChildren<Text> ().text = "";
			FriendLevelButton0.GetComponent<Button> ().interactable = false;

			FriendLevelButton1.name = "disabledmessage";
			FriendLevelButton1.GetComponentInChildren<Text> ().text = "";
			FriendLevelButton1.GetComponent<Button> ().interactable = false;

			FriendLevelButton2.name = "disabledmessage";
			FriendLevelButton2.GetComponentInChildren<Text> ().text = "";
			FriendLevelButton2.GetComponent<Button> ().interactable = false;
			return;
		} else {
			if (FriendCurrentPage == 1) {
				FriendFirstButton.GetComponent<Image> ().sprite = SelectedPage;
				FriendSecondButton.GetComponent<Image> ().sprite = NormalPage;
				FriendThirdButton.GetComponent<Image> ().sprite = NormalPage;
			}

			FriendFirstButton.GetComponent<Button> ().interactable = true;
			if (friendfileInfo.Count > 3) {
				FriendSecondButton.GetComponent<Button> ().interactable = true;
			}
			if (friendfileInfo.Count > 6) {
				FriendThirdButton.GetComponent<Button> ().interactable = true;
			} 
			int maxpage = Mathf.FloorToInt ((friendfileInfo.Count - 1) / 3) + 1;
			FriendMaxPage.GetComponent<Text> ().text = maxpage.ToString ();
			if (FriendCurrentPage == maxpage + 1) {
				if (maxpage == 1) {
					FriendCurrentPage = 1;
				} else {
					FriendCurrentPage = maxpage;
				}
			}
			FriendPageInputField.GetComponent<InputField> ().text = FriendCurrentPage.ToString();
			FriendGoToPage ();

		}
		UpdatingFriend = false;
	}

	public IEnumerator DownloadFriendList(bool IsFriend)
	{
		if (IsFriend) {
			AWS.AWSDownload ("4", "user/" + user + "/friends/friendslist.txt");
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "none" || AWS.result == "empty") {
				AWS.result = "";
				friendfileInfo.Clear ();
				RefreshFriends ();
				UpdatingFriend = false;
				yield break;
			} else if (AWS.result.Split ('|') [0] == "4") {
				fileContents = AWS.result.Split ('|') [1];
				AWS.result = "";
			}
		} else {
			AWS.AWSList ("user/",0);
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "none" || AWS.result == "empty") {
				AWS.result = "";
				friendfileInfo.Clear ();
				RefreshFriends ();
				UpdatingFriend = false;
				yield break;
			} else {
				string output = AWS.result;
				AWS.result = "";

				fileContents = "";
				List<string> tempuser = new List<string>{ };
				tempuser.Clear ();

				var split = output.Split ('+');
				for (int i = 0; i < split.Length; i++) {
					var split2 = split [i].Split ('/');
					if (split2.Length >= 6) {
						if (!tempuser.Contains (split2 [5]) && split2[5]!="") {
							tempuser.Add (split2 [5]);
							string searchuser = split2 [5];
							float rank = 0f;

							//Downloads rank
							AWS.AWSDownload ("22","user/" + searchuser + "/" + "rank.txt");
							while (AWS.result == "") {
								yield return null;
							}

							if (AWS.result == "none") {
								AWS.result = "";
							} else if (AWS.result.Split ('|') [0] == "22") {
								string fileContents = AWS.result.Split ('|') [1];
								AWS.result = "";
								var newarr = fileContents.Split (';');
								rank = float.Parse (newarr [0]);
							}



							if (fileContents == "") {
								fileContents += split2 [5] + ";" + rank.ToString();
							} else {
								fileContents += "/" + split2 [5] + ";" + rank.ToString();
							}
						}
					}
				}
			}
		}
		friendfileInfo.Clear ();
		var arr = fileContents.Split ('/');
		foreach (string file in arr) {
			friendfileInfo.Add (file);
		}
		if (!IsFriend) {
			friendfileInfo.Sort();
		} else {
			friendfileInfo.Reverse ();
		}
		RefreshFriends ();
	}



	public void SearchUser(InputField text)
	{
		FriendLevelButton0.GetComponent<Button> ().interactable = false;
		FriendLevelButton1.GetComponent<Button> ().interactable = false;
		FriendLevelButton2.GetComponent<Button> ().interactable = false;

		if (FriendDropdown.GetComponent<Dropdown> ().value == 0) {
			StartCoroutine (DownloadFriendList (true));
		} else {
			StartCoroutine (DownloadFriendList (false));
		}

		string searchuser = text.text;
		List<string> tempfriendfileInfo = new List<string>{ };
		foreach (string user in friendfileInfo) {
			if (user.Contains (searchuser)) {
				tempfriendfileInfo.Add (user);
			}
		}
		friendfileInfo = tempfriendfileInfo;

		if (friendfileInfo.Count != 0) {
			FriendFirstButton.GetComponentInChildren<Text> ().text = "1";
			FriendSecondButton.GetComponentInChildren<Text> ().text = "2";
			FriendThirdButton.GetComponentInChildren<Text> ().text = "3";
			FriendPageButton (0);
		}
		RefreshFriends ();
	}
	public void AddFriend(InputField UserInput)
	{
		FriendInfoText.GetComponent<Text> ().text = "Finding user...";
		if (UserInput.GetComponent<InputField> ().text!="")
		{
			string newuser = UserInput.GetComponent<InputField> ().text;
			UserInput.GetComponent<InputField> ().text = "";
			StartCoroutine (GetUserExist (newuser,false));
		}
	}

	public void RemoveFriend()
	{
		FriendInfoText.GetComponent<Text> ().text = "Finding user...";
		if (FriendUserText.GetComponent<Text> ().text != "") {
			string newuser = FriendUserText.GetComponent<Text> ().text;
			FriendUserText.GetComponent<Text> ().text = "";
			StartCoroutine (GetUserExist (newuser, true));
		}
	}
	//Check if user exists before sending
	public IEnumerator GetUserExist(string Muser,bool deletefriend)
	{
		AWS.AWSDownload ("5","user/" + Muser + "/rank.txt");
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			//if user does not exist
			if (!deletefriend) {
				FriendInfoText.GetComponent<Text> ().text = "User does not exist";
			} else {
				FriendInfoText.GetComponent<Text> ().text = "User is already deleted from your friend list";
			}
		} else if (AWS.result.Split('|')[0]=="5"){
			//if user exists
			fileContents=AWS.result.Split('|')[1];
			AWS.result="";
			var split = fileContents.Split (';');
			FriendInfoText.GetComponent<Text> ().text = "Found user, uploading information...";
			StartCoroutine (GetFriendList (Muser,split[0],deletefriend));
		}
	}

	//Download the existing friend list before changing it
	public IEnumerator GetFriendList(string Muser,string Mrank,bool deletefriend)
	{

		//Download map list first
		string dir;
		string fileContents;
		dir="user/"+user+"/friends/friendslist.txt";
		AWS.AWSDownload ("6",dir);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			if (!deletefriend) {
				string newfriendslist = Muser + ";" + Mrank;
				StartCoroutine (UploadFriendList (Muser, newfriendslist,deletefriend));
			} else {
				//No change if no friend is left
				RefreshFriends();
				StartCoroutine(DeleteFriendList());
			}
		} else if (AWS.result.Split('|')[0]=="6"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";
			if (fileContents.Contains (Muser)) {
				if (!deletefriend) {
					FriendInfoText.GetComponent<Text> ().text = "User already exists in your friend list";
				} else {
					FriendInfoText.GetComponent<Text> ().text = "Updating friend list...";
					friendfileInfo.Remove (Muser+";"+Mrank);
					var oldfriendslist = fileContents.Split ('/');
					string newfriendslist = "";
					foreach (string friend in oldfriendslist) {
						if (friend.Split (';') [0] != Muser) {
							newfriendslist+=friend+"/";
						}
					}
					if (newfriendslist != "") {
						newfriendslist = newfriendslist.Substring (0, newfriendslist.Length - 1);
						StartCoroutine (UploadFriendList (Muser, newfriendslist,deletefriend));
					} else {
						//Delete friend list if no friend is left
						StartCoroutine(DeleteFriendList());
					}
				}
			} else {
				if (!deletefriend) {
					FriendInfoText.GetComponent<Text> ().text = "Updating friend list...";
					string newfriendslist = fileContents + "/" + Muser + ";" + Mrank;
					StartCoroutine (UploadFriendList (Muser, newfriendslist,deletefriend));
				} else {
					FriendInfoText.GetComponent<Text> ().text = "User is already deleted from your friend list";
				}
			}
		}
	}

	//Update the new friend list
	public IEnumerator UploadFriendList(string Muser,string newfriendslist,bool deletefriend) {
		AWS.AWSUpload ("user/" + user + "/friends/friendslist.txt",newfriendslist);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "false") {
			AWS.result = "";
		} else {
			AWS.result = "";
			if (!deletefriend) {
				FriendInfoText.GetComponent<Text> ().text = "Successfully added friend";
			} else {
				FriendInfoText.GetComponent<Text> ().text = "Successfully deleted friend";
			}
			FriendInfoPanel.SetActive (false);
			StartCoroutine (DownloadFriendList (true));
		}
	}

	public IEnumerator DeleteFriendList() {
		AWS.AWSDelete ("user/" + user + "/friends/friendslist.txt",true);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "false") {
			AWS.result = "";
		} else {
			AWS.result = "";
			FriendInfoText.GetComponent<Text> ().text = "Successfully deleted friend";
			FriendInfoPanel.SetActive (false);
			friendfileInfo.Clear ();
			RefreshFriends ();
		}
	}
	public void SelectFriend(GameObject FriendButton)
	{
		FriendUserText.GetComponent<Text> ().text = FriendButton.GetComponentInChildren<Text> ().text.Split('(')[0].Split('<')[0];
		FriendToInput.GetComponent<InputField>().text=FriendButton.GetComponentInChildren<Text> ().text.Split('(')[0].Split('<')[0];

		//If listing friends only
		if (FriendDropdown.GetComponent<Dropdown> ().value == 0) {
			FriendRemove.GetComponent<Button> ().interactable = true;
		} else {
			//Else remove friend remove button
			FriendRemove.GetComponent<Button> ().interactable = false;
		}
	}

	public void FriendSendMessage()
	{
		if (FriendToInput.GetComponent<InputField> ().text != "") {
			string Muser = FriendToInput.GetComponent<InputField> ().text;
			string Mtitle = FriendTitleInput.GetComponent<InputField> ().text;
			string Mbody = FriendBodyInput.GetComponent<InputField> ().text;

			FriendToInput.GetComponent<InputField> ().text="";
			FriendTitleInput.GetComponent<InputField> ().text = "";
			FriendBodyInput.GetComponent<InputField> ().text="";

			FriendInfoText.GetComponent<Text> ().text = "Finding user...";
			StartCoroutine (GetUserExist (Muser, Mtitle, Mbody, true));
		}
	}

	public void ToggleFriend()
	{
		FriendUserText.GetComponent<Text> ().text = "";
		if (FriendDropdown.GetComponent<Dropdown> ().value == 0) {
			FriendRemove.GetComponent<Button> ().interactable = true;
			FriendAdd.GetComponent<Button> ().interactable = true;
			StartCoroutine (DownloadFriendList (true));
		}
		else if (FriendDropdown.GetComponent<Dropdown> ().value == 1) {
			FriendRemove.GetComponent<Button> ().interactable = false;
			FriendAdd.GetComponent<Button> ().interactable = false;
			StartCoroutine (DownloadFriendList (false));
		}
	}

	//Message Tab functions
	public void MessageGoToPage()
	{
		MessagePageNo = int.Parse(MessagePageInputField.GetComponent<InputField>().text) -1;
		int maxpage = int.Parse(MessageMaxPage.GetComponent<Text>().text);
		if (MessagePageNo>=0 && MessagePageNo+1<=maxpage)
		{
			if (MessagePageNo+1 == 1) {
				//If on the first page
				MessageFirstButton.GetComponent<Image>().sprite=SelectedPage;
				MessageSecondButton.GetComponent<Image>().sprite=NormalPage;
				MessageThirdButton.GetComponent<Image>().sprite=NormalPage;

				MessageFirstButton.GetComponentInChildren<Text> ().text = "1";
				MessageSecondButton.GetComponentInChildren<Text> ().text = "2";
				MessageThirdButton.GetComponentInChildren<Text> ().text = "3";
			} else if (MessagePageNo+1==maxpage) {
				//If on the last page
				if (maxpage == 2) {
					MessageFirstButton.GetComponent<Image> ().sprite = NormalPage;
					MessageSecondButton.GetComponent<Image> ().sprite = SelectedPage;
					MessageThirdButton.GetComponent<Image> ().sprite = NormalPage;

					MessageFirstButton.GetComponentInChildren<Text> ().text = "1";
					MessageSecondButton.GetComponentInChildren<Text> ().text = "2";
					MessageThirdButton.GetComponentInChildren<Text> ().text = "3";
				}
				else{
					MessageFirstButton.GetComponent<Image> ().sprite = NormalPage;
					MessageSecondButton.GetComponent<Image> ().sprite = NormalPage;
					MessageThirdButton.GetComponent<Image> ().sprite = SelectedPage;

					MessageFirstButton.GetComponentInChildren<Text> ().text = (MessagePageNo - 1).ToString ();
					MessageSecondButton.GetComponentInChildren<Text> ().text = (MessagePageNo - 0).ToString ();
					MessageThirdButton.GetComponentInChildren<Text> ().text = (MessagePageNo + 1).ToString ();
				}
			} else {
				MessageFirstButton.GetComponent<Image>().sprite=NormalPage;
				MessageSecondButton.GetComponent<Image>().sprite=SelectedPage;
				MessageThirdButton.GetComponent<Image>().sprite=NormalPage;

				MessageFirstButton.GetComponentInChildren<Text> ().text = (MessagePageNo -0).ToString ();
				MessageSecondButton.GetComponentInChildren<Text> ().text = (MessagePageNo + 1).ToString ();
				MessageThirdButton.GetComponentInChildren<Text> ().text = (MessagePageNo + 2).ToString ();
			}

			MessageLevelButton0.name = messagesfileinfo [MessagePageNo*3+ 0];
			MessageLevelButton0.GetComponentInChildren<Text> ().text = messagesfileinfo [MessagePageNo*3+ 0].Split(';')[2] + "<color=#FFFFFFFF> from </color>]"+messagesfileinfo [MessagePageNo*3+ 0].Split(';')[0] + "("+messagesfileinfo [MessagePageNo*3+ 0].Split(';')[1] +")";
			MessageLevelButton0.GetComponent<Button> ().interactable = true;
			MessageDeleteButton0.GetComponent<Button> ().interactable = true;

			if (messagesfileinfo.Count > MessagePageNo * 3 + 1) {
				MessageLevelButton1.name = messagesfileinfo [MessagePageNo * 3 + 1];
				MessageLevelButton1.GetComponentInChildren<Text> ().text = messagesfileinfo [MessagePageNo * 3 + 1].Split (';') [2] + "<color=#FFFFFFFF> from </color>]" + messagesfileinfo [MessagePageNo * 3 + 1].Split (';') [0] + "(" + messagesfileinfo [MessagePageNo * 3 + 1].Split (';') [1] + ")";
				MessageLevelButton1.GetComponent<Button> ().interactable = true;
				MessageDeleteButton1.GetComponent<Button> ().interactable = true;
			} else {
				MessageLevelButton1.name = "disabledmessage";
				MessageLevelButton1.GetComponentInChildren<Text> ().text = "";
				MessageLevelButton1.GetComponent<Button> ().interactable = false;
				MessageDeleteButton1.GetComponent<Button> ().interactable = false;
			}

			if (messagesfileinfo.Count > MessagePageNo * 3 + 2) {
				MessageLevelButton2.name = messagesfileinfo [MessagePageNo * 3 + 2];
				MessageLevelButton2.GetComponentInChildren<Text> ().text = messagesfileinfo [MessagePageNo * 3 + 2].Split (';') [2] + "<color=#FFFFFFFF> from </color>]" + messagesfileinfo [MessagePageNo * 3 + 2].Split (';') [0] + "(" + messagesfileinfo [MessagePageNo * 3 + 2].Split (';') [1] + ")";
				MessageLevelButton2.GetComponent<Button> ().interactable = true;
				MessageDeleteButton2.GetComponent<Button> ().interactable = true;
			} else {
				MessageLevelButton2.name = "disabledmessage";
				MessageLevelButton2.GetComponentInChildren<Text> ().text = "";
				MessageLevelButton2.GetComponent<Button> ().interactable = false;
				MessageDeleteButton2.GetComponent<Button> ().interactable = false;
			}
		}
	}
	public void MessageLeftButtonDown()
	{
		if (MessageCurrentPage > 0) {
			MessageCurrentPage -= 1;
			MessagePageInputField.GetComponent<InputField> ().text = MessageCurrentPage.ToString();
			MessageGoToPage ();
		}
	}

	public void MessageRighButtonDown()
	{
		int maxpage = int.Parse(MessageMaxPage.GetComponent<Text>().text);
		if (MessageCurrentPage < maxpage) {
			MessageCurrentPage += 1;
			MessagePageInputField.GetComponent<InputField> ().text = MessageCurrentPage.ToString();
			MessageGoToPage ();
		}
	}

	public void MessagePageButton(int pageno)
	{
		int firstpage = int.Parse(MessageFirstButton.GetComponentInChildren<Text> ().text);
		MessagePageNo = firstpage + pageno ;
		MessageCurrentPage = MessagePageNo;

		MessagePageInputField.GetComponent<InputField> ().text = MessageCurrentPage.ToString();
		MessageGoToPage ();

	}

	public IEnumerator RefreshMessages()
	{
		AWS.AWSDownload ("7","user/" + user + "/messages/messageslist.txt");
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			UpdatingMessage = false;
		} else if (AWS.result.Split('|')[0]=="7"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";

			messagesfileinfo.Clear ();
			var arr = fileContents.Split ('/');
			foreach (string file in arr) {
				messagesfileinfo.Add (file);
			}
			messagesfileinfo.Reverse ();

			MessageFirstButton.GetComponent<Button> ().interactable = false;
			MessageSecondButton.GetComponent<Button> ().interactable = false;
			MessageThirdButton.GetComponent<Button> ().interactable = false;

			if (fileContents == "") {
				MessageFirstButton.GetComponent<Image> ().sprite = NormalPage;
				MessageSecondButton.GetComponent<Image> ().sprite = NormalPage;
				MessageThirdButton.GetComponent<Image> ().sprite = NormalPage;

				MessageLevelButton0.name = "disabledmessage";
				MessageLevelButton0.GetComponentInChildren<Text> ().text = "";
				MessageLevelButton0.GetComponent<Button> ().interactable = false;
				MessageDeleteButton0.GetComponent<Button> ().interactable = false;

				MessageLevelButton1.name = "disabledmessage";
				MessageLevelButton1.GetComponentInChildren<Text> ().text = "";
				MessageLevelButton1.GetComponent<Button> ().interactable = false;
				MessageDeleteButton1.GetComponent<Button> ().interactable = false;

				MessageLevelButton2.name = "disabledmessage";
				MessageLevelButton2.GetComponentInChildren<Text> ().text = "";
				MessageLevelButton2.GetComponent<Button> ().interactable = false;
				MessageDeleteButton2.GetComponent<Button> ().interactable = false;
				yield break;
			} else {
				if (MessageCurrentPage == 1) {
					MessageFirstButton.GetComponent<Image> ().sprite = SelectedPage;
					MessageSecondButton.GetComponent<Image> ().sprite = NormalPage;
					MessageThirdButton.GetComponent<Image> ().sprite = NormalPage;
				}

				MessageFirstButton.GetComponent<Button> ().interactable = true;
				if (messagesfileinfo.Count > 3) {
					MessageSecondButton.GetComponent<Button> ().interactable = true;
				}
				if (messagesfileinfo.Count > 6) {
					MessageThirdButton.GetComponent<Button> ().interactable = true;
				} 
				int maxpage = Mathf.FloorToInt ((messagesfileinfo.Count - 1) / 3) + 1;
				MessageMaxPage.GetComponent<Text> ().text = maxpage.ToString ();
				if (MessageCurrentPage == maxpage + 1) {
					if (maxpage == 1) {
						MessageCurrentPage = 1;
					} else {
						MessageCurrentPage = maxpage;
					}
				}
				MessagePageInputField.GetComponent<InputField> ().text = MessageCurrentPage.ToString();
				MessageGoToPage ();

			}
			UpdatingMessage = false;
		}
	}

	public void LoadMessage(GameObject LevelButton)
	{
		string nameofmap = LevelButton.GetComponentInChildren<Text> ().text;
		StartCoroutine (GetMessage (LevelButton.name));
	}

	//Loads new map
	public IEnumerator GetMessage(string Mname)
	{

		//Sets lastmap to what is loaded.
		string data = null;
		string mydir="";
		mydir = ("user/" + user + "/messages/"+Mname+".txt");
		//Downloads user map list
		AWS.AWSDownload ("8",mydir);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
		} else if (AWS.result.Split('|')[0]=="8"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";

			ReadPanel.SetActive (true);
			ReadFromInput.GetComponent<InputField> ().text = Mname.Split (';') [0] + "("+Mname.Split (';') [1]+")";
			ReadTitleInput.GetComponent<InputField> ().text = Mname.Split (';') [2];
			ReadBodyInput.GetComponent<InputField> ().text = fileContents;
		}

	}

	public void SendMessage()
	{
		if (ToInput.GetComponent<InputField> ().text != "") {
			string Muser = ToInput.GetComponent<InputField> ().text.Replace("/","");
			string Mtitle = TitleInput.GetComponent<InputField> ().text.Replace("/","");
			string Mbody = BodyInput.GetComponent<InputField> ().text.Replace("/","");

			ToInput.GetComponent<InputField> ().text = "";
			TitleInput.GetComponent<InputField> ().text ="";
			BodyInput.GetComponent<InputField> ().text ="";

			MessageInfoText.GetComponent<Text> ().text = "Finding user...";

			StartCoroutine (GetUserExist (Muser, Mtitle, Mbody, false));
		}
	}

	public IEnumerator ChatCounter()
	{
		yield return new WaitForSeconds(2f);
		RecentNumberOfChat = 0;

		StartCoroutine (ChatCounter ());
	}

	public IEnumerator ActivateChat()
	{
		yield return new WaitForSeconds(NumberOfTimesSpammed*5f);
		RecentNumberOfChat = 0;
		CanSendChatMessage = true;
	}

	//Check if user exists before sending
	public IEnumerator GetUserExist(string Muser,string Mtitle,string Mbody,bool Tofriend)
	{
		AWS.AWSDownload ("9","user/" + Muser + "/rank.txt");
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			//if user does not exist
			if (Tofriend) {
				FriendInfoText.GetComponent<Text> ().text = "User does not exist";
			} else {
				MessageInfoText.GetComponent<Text> ().text = "User does not exist";
			}
		} else if (AWS.result.Split('|')[0]=="9"){
			AWS.result = "";
			//if user exists
			if (Tofriend) {
				FriendInfoText.GetComponent<Text> ().text = "Found user, uploading message...";
			} else {
				MessageInfoText.GetComponent<Text> ().text = "Found user, uploading message...";
			}
			StartCoroutine (UploadMessage (Muser, Mtitle, Mbody,Tofriend));
		}
	}
	//Uploads message to server
	public IEnumerator UploadMessage(string Muser,string Mtitle,string Mbody,bool Tofriend) {
		AWS.AWSUpload("user/"+Muser+"/messages/"+user+";"+rank+";"+ Mtitle +".txt",Mbody);

		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "false") {
			AWS.result = "";
			
		} else {
			AWS.result = "";
			if (Tofriend) {
				FriendInfoText.GetComponent<Text> ().text = "Uploaded message, sending message to user...";
			} else {
				MessageInfoText.GetComponent<Text> ().text = "Uploaded message, sending message to user...";
			}
			StartCoroutine (DownloadMessageList (Muser, Mtitle,Tofriend,false,null));
		}
	}
	public void CallDeleteMessage(GameObject MessageButton)
	{
		StartCoroutine (DeleteMessage (MessageButton));
	}
	//Deletes a message
	public IEnumerator DeleteMessage(GameObject MessageButton) {
		string thisname = MessageButton.name;
		var split = thisname.Split (';');
		string Muser = split[0];
		string Mtitle = split [2];
		AWS.AWSDelete("user/"+Muser+"/messages/"+thisname+".txt",true);

		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "DeleteFalse") {
			AWS.result = "";
			StartCoroutine (DownloadMessageList (Muser, Mtitle, false,true,thisname));

		} else {
			AWS.result = "";
			StartCoroutine (DownloadMessageList (Muser, Mtitle, false,true,thisname));
		}
	}

	//Download the existing message list before changing it
	public IEnumerator DownloadMessageList(string Muser,string Mtitle,bool Tofriend,bool DeleteMessage,string filename)
	{
		//filename is only needed for message deletion

		//Get list of files in directory
		usermessagesfileinfo.Clear ();

		//Download map list first
		string dir;
		string fileContents;
		dir="user/"+Muser+"/messages/messageslist.txt";
		AWS.AWSDownload ("10",dir);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			if (!DeleteMessage) {
				string newmessagelist = user + ";" + rank + ";" + Mtitle;
				StartCoroutine (UploadMessageList (Muser, newmessagelist, Tofriend,DeleteMessage));
			}
		} else if (AWS.result.Split ('|') [0] == "10") {
			fileContents = AWS.result.Split ('|') [1];
			AWS.result = "";
			if (!DeleteMessage) {
				if (Tofriend) {
					FriendInfoText.GetComponent<Text> ().text = "Updating message list...";
				} else {
					MessageInfoText.GetComponent<Text> ().text = "Updating message list...";
				}
				if (fileContents == "") {
					string newmessagelist =user + ";" + rank + ";" + Mtitle;
					StartCoroutine (UploadMessageList (Muser, newmessagelist, Tofriend, DeleteMessage));
				} else {
					string newmessagelist = fileContents + "/" + user + ";" + rank + ";" + Mtitle;
					StartCoroutine (UploadMessageList (Muser, newmessagelist, Tofriend, DeleteMessage));
				}
			} else {
				string newmessagelist = "";
				var split2 = fileContents.Split ('/');
				for (int i = 0; i < split2.Length; i++) {
					if (split2 [i] != filename) {
						if (newmessagelist == "") {
							newmessagelist += split2 [i];
						} else {
							newmessagelist += "/" + split2 [i];
						}
					}
				}
				StartCoroutine (UploadMessageList (Muser, newmessagelist, Tofriend,DeleteMessage));
			}
		}
	}

	//Update the new message list
	public IEnumerator UploadMessageList(string Muser,string newmessagelist,bool Tofriend,bool DeleteMessage) {
		AWS.AWSUpload ("user/" + Muser + "/messages/messageslist.txt", newmessagelist);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "false") {
			AWS.result = "";
		} else {
			AWS.result = "";
			if (!DeleteMessage) {
				if (Tofriend) {
					FriendInfoText.GetComponent<Text> ().text = "Successfully sent message";
				} else {
					MessageInfoText.GetComponent<Text> ().text = "Successfully sent message";
				}
			} else {
				MessageInfoText.GetComponent<Text> ().text = "Successfully deleted message";
			}
			MessageInfoPanel.SetActive (false);
			StartCoroutine (RefreshMessages ());
		}
	}


	/////Handles customisation panel
	public void OnSpeedChange()
	{
		if (Mathf.RoundToInt (SpeedSlider.GetComponent<Slider> ().value) > SSpeed) {
			if (Mathf.RoundToInt (SpeedSlider.GetComponent<Slider> ().value) - SSpeed < PointsRemaining) {
				SpeedNoText.GetComponent<Text> ().text = SpeedSlider.GetComponent<Slider> ().value.ToString ();
				SSpeed = Mathf.RoundToInt (SpeedSlider.GetComponent<Slider> ().value);
				controller.SSpeed = SSpeed;
			} else {
				SpeedNoText.GetComponent<Text> ().text = (SSpeed + PointsRemaining).ToString ();
				SSpeed = SSpeed + PointsRemaining;
				SpeedSlider.GetComponent<Slider> ().value = SSpeed;
				controller.SSpeed = SSpeed;
			}
		} else {
			SpeedNoText.GetComponent<Text> ().text = SpeedSlider.GetComponent<Slider> ().value.ToString ();
			SSpeed = Mathf.RoundToInt (SpeedSlider.GetComponent<Slider> ().value);
			controller.SSpeed = SSpeed;
		}
	}

	public void OnAccChange()
	{
		if (Mathf.RoundToInt (AccSlider.GetComponent<Slider> ().value) > SAcc) {
			if (Mathf.RoundToInt (AccSlider.GetComponent<Slider> ().value) - SAcc < PointsRemaining) {
				AccNoText.GetComponent<Text> ().text = AccSlider.GetComponent<Slider> ().value.ToString ();
				SAcc = Mathf.RoundToInt (AccSlider.GetComponent<Slider> ().value);
				controller.SAcc = SAcc;
			} else {
				AccNoText.GetComponent<Text> ().text = (SAcc + PointsRemaining).ToString ();
				SAcc = SAcc + PointsRemaining;
				AccSlider.GetComponent<Slider> ().value = SAcc;
				controller.SAcc = SAcc;
			}
		} else {
			AccNoText.GetComponent<Text> ().text = AccSlider.GetComponent<Slider> ().value.ToString ();
			SAcc = Mathf.RoundToInt (AccSlider.GetComponent<Slider> ().value);
			controller.SAcc = SAcc;
		}
	}

	public void OnJumpChange()
	{
		if (Mathf.RoundToInt (JumpSlider.GetComponent<Slider> ().value) > SJump) {
			if (Mathf.RoundToInt (JumpSlider.GetComponent<Slider> ().value) - SJump < PointsRemaining) {
				JumpNoText.GetComponent<Text> ().text = JumpSlider.GetComponent<Slider> ().value.ToString ();
				SJump = Mathf.RoundToInt (JumpSlider.GetComponent<Slider> ().value);
				controller.SJump = SJump;
			} else {
				JumpNoText.GetComponent<Text> ().text = (SJump + PointsRemaining).ToString ();
				SJump = SJump + PointsRemaining;
				JumpSlider.GetComponent<Slider> ().value = SJump;
				controller.SJump = SJump;
			}
		} else {
			JumpNoText.GetComponent<Text> ().text = JumpSlider.GetComponent<Slider> ().value.ToString ();
			SJump = Mathf.RoundToInt (JumpSlider.GetComponent<Slider> ().value);
			controller.SJump = SJump;
		}
	}

	public void OnExpChange()
	{
		if (Mathf.RoundToInt (ExpSlider.GetComponent<Slider> ().value) > SExpBonus) {
			if (Mathf.RoundToInt (ExpSlider.GetComponent<Slider> ().value) - SExpBonus < PointsRemaining) {
				ExpNoText.GetComponent<Text> ().text = ExpSlider.GetComponent<Slider> ().value.ToString ();
				SExpBonus = Mathf.RoundToInt (ExpSlider.GetComponent<Slider> ().value);
				controller.SExpBonus = SExpBonus;
			} else {
				ExpNoText.GetComponent<Text> ().text = (SExpBonus + PointsRemaining).ToString ();
				SExpBonus = SExpBonus + PointsRemaining;
				ExpSlider.GetComponent<Slider> ().value = SExpBonus;
				controller.SExpBonus = SExpBonus;
			}
		} else {
			ExpNoText.GetComponent<Text> ().text = ExpSlider.GetComponent<Slider> ().value.ToString ();
			SExpBonus = Mathf.RoundToInt (ExpSlider.GetComponent<Slider> ().value);
			controller.SExpBonus = SExpBonus;
		}
	}



	//Set Level Mode
	public void setmode(int c)
	{
		networkmode = c;
	}


	public void Logout()
	{
		print ("Logging out");
		GameObject Controller = GameObject.FindGameObjectWithTag ("controller");
		Destroy (Controller);

		GameObject AWSControl = GameObject.FindGameObjectWithTag ("awscontrol");
		Destroy (AWSControl);

		GameObject PhotonController = GameObject.FindGameObjectWithTag ("PhotonController");
		Destroy (PhotonController);

		PhotonNetwork.Disconnect ();

	}

	public void UploadRank()
	{
		AWS.AWSUpload ("user/" + user + "/rank.txt", controller.rank+";"+controller.exp+";"+controller.SSpeed+";"+controller.SAcc+";"+controller.SJump+";"+controller.SExpBonus);
	}

	public void ChangeLevelTab(int num)
	{
		if (num == 0) {
			CurrentLevelTab = "latest";
			LevelTitleText.GetComponent<Text> ().text = "latest";
			StartCoroutine (updatemaplist (true, "", "latest"));
		}
		else if (num == 1) {
			CurrentLevelTab = "popular";
			LevelTitleText.GetComponent<Text>().text="popular";
			StartCoroutine (updatemaplist (true, "", "popular"));
		}
		else if (num == 2) {
			CurrentLevelTab = "mylevels";
			LevelTitleText.GetComponent<Text>().text="my levels";
			StartCoroutine (updatemaplist (false,"",""));
		}
		else if (num == 3) {
			//Check if user exists
			StartCoroutine (getuserlistexist ());
		}
	}

	public IEnumerator getuserlistexist()
	{
		SearchingUsername = SearchInputField.text;
		AWS.AWSDownload ("21","user/" + SearchInputField.text + "/rank.txt");
		while (AWS.result == "") {
			yield return null;
		}

		if (AWS.result=="none") {
			AWS.result="";
			animator.MoveInfoIn ();
			InfoPanelText.text = "The current username does not exist. Please enter the username again.";
		} else if (AWS.result.Split('|')[0]=="21"){
			//if user exists
			AWS.result="";
			CurrentLevelTab = "search";
			LevelTitleText.GetComponent<Text>().text="searching user: "+SearchingUsername;
			StartCoroutine (updatemaplist (true,SearchingUsername,""));
		}
	}

	public void AcceptLeveleditorInvitation()
	{
		string username = InvitedUsernameInputField.GetComponent<InputField> ().text;
		string mapname = InvitedMapnameInputField.GetComponent<InputField> ().text;
		string dest = "user/" + username + "/maps/" + mapname + ".txt";
		PhotonNetwork.JoinRoom (dest);
		StartCoroutine (DeleteLevelEditorInvitation (username, mapname));
	}

	public void DeclineLeveleditorInvitation()
	{
		string username = InvitedUsernameInputField.GetComponent<InputField> ().text;
		string mapname = InvitedMapnameInputField.GetComponent<InputField> ().text;

		StartCoroutine (DeleteLevelEditorInvitation (username, mapname));
	}

	public IEnumerator DeleteLevelEditorInvitation(string FromUsername,string mapname)
	{
		string dest = "chatrooms/Main/"+ "chatbot: "+FromUsername+":"+controller.user+":"+mapname+".txt";
		AWS.AWSDelete (dest,true);
		while (AWS.result == "") {
			yield return null;
		}
		AWS.result = "";
	}
}
