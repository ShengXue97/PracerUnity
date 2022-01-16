using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using MoreMountains.Tools;
using MoreMountains.CorgiEngine;
public class CameraFollow : MonoBehaviour
{
	public Button UndoButton;
	public Button RedoButton;

	public Sprite audioenabled;
	public Sprite audiodisabled;

	public GameObject Sky;
	public GameObject Lightnings;
	public GameObject SimpleLightningBoltPrefab;

	public float LastFPS;
	public GameObject ParallaxParent;
	public GameObject FPS;
	public GameObject LargePencil;
	public GameObject SmallEraser;
	public GameObject LargeEraser;
	public GameObject SmallPencil;
	public GameObject DeleteButton;
	public GameObject PanningButton;

	public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;
	public GameObject Star4;
	public GameObject Star5;

	public Sprite SelectedDelete;
	public Sprite DeselectedDelete;
	public Sprite SelectedPanning;
	public Sprite DeselectedPanning;
	public Sprite CursorDefault;
	public Sprite CursorSelected;
	public Sprite CursorSelectedReleased;
	public Sprite emptyImage;
	public String CurrentMapName="";

	public GameObject AcceptPanel;
	public GameObject AcceptText;
	public GameObject RequestPanel;
	public GameObject RequestTitle;
	public GameObject RequestText;
	public GameObject smallgrid;
	public GameObject InfoPanel;
	public GameObject InfoText;
	public GameObject LoadingController;
	public GameObject PlayerSlider;
	public GameObject EditingMode;
	public float playerx;
	public float playery;
	public string CurrentRoom;
	public int camsizeInt=7;
	[Space]
	public GameObject MoveHorizontalCheck;
	public GameObject MoveVerticalCheck;
	public GameObject RaceCheckmark;
	public GameObject DeathmatchCheckmark;
	public Controllers controller;
	public GameObject chunk;
	public GameObject WaterEntryEffect;
	public GameObject FreezeEffect;
	public GameObject RobotExplosion;
	public GameObject CrateHurt;
	public GameObject CrateExplode;
	public GameObject brickeffect;
	public GameObject RocketExplosion;

	public GameObject cursor;
	public GameObject UICameraCanvas;
	public LayerMask whatIsUI;
	public LayerMask whatIsPlatform;
	public LayerMask whatIsGround;
	public LayerMask whatIsPlayer;
	public LayerMask whatIsSign;
	public LayerMask whatIsBlockCheck;

	public GameObject SpeedSlider;
	public GameObject SpeedNoText;
	public GameObject AccSlider;
	public GameObject AccNoText;
	public GameObject JumpSlider;
	public GameObject JumpNoText;

	public LevelEditorAnimations scenecontroller;
	public Level1Animations level1scenecontroller;
	public GameObject UICanvas;
	public GameObject loadercontroller;
	public GameObject LoadingImage;
	public GameObject CountdownImage;
	public GameObject GameTimePanel;
	public GameObject GameTime;
	public GameObject ChatObj;
	public GameObject signobj;
	public GameObject Master;
	public GameObject TopPanel;
	public GameObject BlockPanel;
	public GameObject BlockSidePanel;
	public GameObject LoadPanel;
	public GameObject ConfirmDeletePanel;
	public GameObject SavePanel;
	public GameObject FinishPanel;
	public GameObject player;
	public GameObject mapname;
	public GameObject mapbar;
	public GameObject content;
	public GameObject SignPanel;
	public InputField SignPanelInput;
	public GameObject MinimapPanel;
	public GameObject FlyingButtonImage;
	public GameObject PreviewButtonImage;
	public GameObject DeleteButtonImage;
	public Transform playerTransform;
	public GameObject DropdownText;
	public GameObject ChatCanvas;
	public GameObject ChatPanel;
	public GameObject ChatBottom;
	public GameObject ChatBackground;
	public GameObject SelectedChannelText;
	public GameObject ChatScrollBar;
	public GameObject LargePlayer;
	public GameObject GameObjectToPool;
	public float TimeScale = 1f;
	public Vector3 signpos;

	public Sprite Countdown3;
	public Sprite Countdown2;
	public Sprite Countdown1;
	public Sprite Countdown0;
	public Sprite Loading3;
	public Sprite Loading2;
	public Sprite Loading1;

	public bool AuthorisedToSaveMap = false;
	public int CurrentRequestID = 0;
	public string CurrentRequestUser = "";
	public string CurrentRequestNameOfMap = "";
	public bool HasRated=false;
	public Mosframe.RealTimeInsertItemExample DynamicScrollView;
	public bool CanMoveInEdit = true;
	public string MapToDelete="";
	public int FreezeTimeNum = 0;
	public Vector3 startpos=new Vector3(0f,0f,0f);
	public int uses;
	public bool UsedUpWeapon = false;
	public int SSpeed=50;
	public int SAcc=50;
	public int SJump=50;
	public int PointsRemaining=900;
	public string GameMode="";
	public int depth = -20;
	public int selectedblock = 1;
	public string selectedportal="00";
	public string selectedkeylock="00";
	public Dictionary<string, string> mapd = new Dictionary<string, string>() { };
	public Dictionary<string, GameObject> blockmapd = new Dictionary<string, GameObject>() { };

	public List<string> mapdKeyCache;
	public List<string> mapdValueCache;
	public List<string> mapdHistory;

	public List<string> mapdUndoKeyCache;
	public List<string> mapdUndoValueCache;
	public List<string> mapdUndoHistory;

	public PhotonView LocalPhotonView;
	public string maps;
	public List<string> maplist;
	public List<GameObject> disabledblocks;
	public bool DeleteMap = false;
	public bool PauseGame=true;
	public string activem;
	public bool GameStarted = false;
	public string user="anonymous";
	public bool issaving = false;
	public int endgame=0;
	public bool flying=false;
	public float rank;
	public float exp;
	public int editplay=0;
	public bool resetting=false;
	public bool previewing=false;
	public bool editallowed = true;
	public bool canedit=true;
	public bool deletingblocks=false;
	public bool panning=false;
	public bool blockpanelenabled=false;
	public bool chatpanelenabled=false;
	public Sprite checkpointsprite;
	public Sprite checkpointedsprite;
	public Sprite toggleleft;
	public Sprite toggleright;
	public Sprite uptrans;public Sprite downtrans;public Sprite lefttrans;public Sprite righttrans;
	public Sprite watertrans;
	public Sprite waternormal;
	public Sprite itemopen;
	public Sprite itemclosed;
	public Sprite bomb1;public Sprite bomb2;public Sprite bomb3;public Sprite bomb4;public Sprite bomb5;public Sprite bomb6;
	public Sprite bomb7;public Sprite bomb8;public Sprite bomb9;public Sprite bomb10;public Sprite bomb11;public Sprite bomb12;
	public Sprite bomb13;public Sprite bomb14;public Sprite bomb15;public Sprite bomb16;public Sprite bomb17;public Sprite bomb18;
	public Sprite bomb19;public Sprite bomb20;public Sprite bomb21;public Sprite bomb22;public Sprite bomb23;public Sprite bomb24;
	public Sprite bomb25;public Sprite bomb26;public Sprite bomb27;public Sprite bomb28;public Sprite bomb29;public Sprite bomb30;
	public Sprite bouncy1;public Sprite bouncy2;public Sprite bouncy3;public Sprite bouncy4;public Sprite bouncy5;
	protected string MyStorageBucket = "gs://composite-store-188023.appspot.com/";
	public string blockdir="";
	private string logText = "";
	public AWSControl AWS;


	// Use this for initialization
	void Start()
	{
		controller = GameObject.FindGameObjectWithTag ("controller").GetComponent<Controllers> ();
		//Application.targetFrameRate = 20;
		PhotonNetwork.Instantiate ("spine-space-cat", Vector3.zero, Quaternion.identity, 0);

		GameObject AWSControl = GameObject.FindGameObjectWithTag ("awscontrol");
		AWS = AWSControl.GetComponent<AWSControl> ();

		FPS = GameObject.FindGameObjectWithTag ("FPS");
		user = controller.user;

		//Ignore collisions between all projectiles and blocks with no collisions
		Physics2D.IgnoreLayerCollision (12,21);
		Physics2D.IgnoreLayerCollision (12,30);
		Physics2D.IgnoreLayerCollision (12,26);

		Physics2D.IgnoreLayerCollision (16,21);
		Physics2D.IgnoreLayerCollision (16,30);
		Physics2D.IgnoreLayerCollision (16,26);
		//Enable flying and disable preview at start
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			cursor=PhotonNetwork.Instantiate ("Cursor", Vector3.zero, Quaternion.identity, 0);
			toggleflying ();
			//Spawn lots of grids
			for (int i = -10; i < 10; i++) {
				for (int j =-10; j < 10; j++) {
					//Instantiate(smallgrid,new Vector3(i*100+0.5f,j*100+0.5f,0),Quaternion.identity);
				}
			}
		}

		rank = controller.rank;
		exp = controller.exp;

		//Fixed block panel bug
		if (BlockPanel != null) {
			BlockPanel.SetActive (false);
			BlockPanel.SetActive (true);
		}
		if (Application.isMobilePlatform) {
			//Hide custom cursor sprite renderer
			if (cursor != null) {
				cursor.GetComponent<SpriteRenderer> ().color = new Color (cursor.GetComponent<SpriteRenderer> ().color.r, cursor.GetComponent<SpriteRenderer> ().color.g, cursor.GetComponent<SpriteRenderer> ().color.b, 0f);
			}
			Cursor.visible = false;
		} else if (!Application.isMobilePlatform && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")){
			//Hide custom cursor sprite renderer
			cursor.GetComponent<SpriteRenderer> ().color = new Color(cursor.GetComponent<SpriteRenderer> ().color.r,cursor.GetComponent<SpriteRenderer> ().color.g,cursor.GetComponent<SpriteRenderer> ().color.b,0f);

			Cursor.visible = true;
		}
		if (!Application.isMobilePlatform) {
			UICameraCanvas.GetComponent<Canvas> ().enabled = false;
		}
		//Disable server list panel (bug)

		GameObject serverlistpanel = GameObject.FindGameObjectWithTag ("serverlistpanel");
		if (serverlistpanel != null) {
			serverlistpanel.SetActive (false);
		}

		GameObject MultiplayerTab = GameObject.FindGameObjectWithTag ("multiplayertab");
		if (MultiplayerTab != null) {
			MultiplayerTab.SetActive (false);
		}
		if (content != null) {
			DynamicScrollView = content.GetComponent<Mosframe.RealTimeInsertItemExample> ();
		}
	}


	// Update is called once per frame
	void Update()
	{ 
		if (LastFPS != null && Time.time - LastFPS > 0.5f) {
			LastFPS = Time.time;
			FPS.GetComponent<Text> ().text = (1.0f / Time.deltaTime).ToString ().Split ('.') [0] + " FPS";
		}

		if (Application.isMobilePlatform) {
			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor") && editplay == 0) {
				UICameraCanvas.GetComponent<Canvas> ().enabled = false;
			} else {
				UICameraCanvas.GetComponent<Canvas> ().enabled = true;
			}
		}
		/*
		var AllObj = GameObject.FindObjectsOfType<GameObject> ();
		print ("length" + AllObj.Length);
		if (AllObj.Length <= 4) {
			PhotonNetwork.LoadLevel ("NetworkLobby");
		}
		*/
		if (PhotonNetwork.inRoom) {
			CurrentRoom = PhotonNetwork.room.Name;
		} else {
			CurrentRoom = "none";
		}
		/*
		var AllObj2 = GameObject.FindObjectsOfType<GameObject> ();
		print (AllObj2.Length);
		if (AllObj2.Length <= 4) {
			PhotonNetwork.LoadLevel ("NetworkLobby");
		}
*/
		if (playerTransform != null) {
			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor") && editplay == 0 && panning) {
				//playerTransform.position = transform.position - new Vector3 (0, 0, depth);
			} else {
				transform.position = playerTransform.position + new Vector3 (0, 0, depth);
			}
		}

		if (cursor != null) {
			cursor.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x + 4f, Input.mousePosition.y - 11f, 20.0f));
		}
		var cursors = GameObject.FindGameObjectsWithTag ("cursor");
		foreach (GameObject thiscursor in cursors) {
			if (thiscursor.name == "Cursor(Clone)") {
				//Set names of each cursors
				thiscursor.name = thiscursor.GetComponent<PhotonView> ().owner.NickName + ";" + "cursor";
			}
		}

	}


	public Camera setTarget(Transform target)
	{
		playerTransform = target;
		LocalPhotonView = playerTransform.gameObject.GetComponent<PhotonView> ();
		//cursor.name = playerTransform.gameObject.name+";"+"cursor";
		return gameObject.GetComponent<Camera>();
	}
	public void setblock(int b)
	{
		selectedblock = b;
	}
	public void setblockdir(string dir)
	{
		blockdir = dir;
	}
	public void setportal(string digit)
	{
		selectedblock = 34;
		selectedportal = digit;
	}
	public void setkeylock(string digit)
	{
		selectedkeylock = digit;
	}




	public IEnumerator savemapCoroutine()
	{
		maps = GameMode + "#{";
		foreach (KeyValuePair<string, string> item in mapd)
		{
			maps += ("{" + '"' + item.Key + '"' + "," + '"' + item.Value + '"' + "},");
		}
		maps = maps.Substring(0, maps.Length - 1);
		maps += ("}");
		print(maps);
		mapname.GetComponent<InputField> ().text = mapname.GetComponent<InputField> ().text.Replace ("/", "");
		PlayerPrefs.SetString ("lastmap", maps);
		PlayerPrefs.SetString ("lastmapname", mapname.GetComponent<InputField> ().text.Replace ("/", ""));
		//Save map to public or private
		string fileName;
		fileName = "user/"+user+"/maps/" + mapname.GetComponent<InputField> ().text.Replace ("/", "") + ".txt";

		AWS.AWSUpload (fileName,maps);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "false") {
			AWS.result = "";
			InfoText.GetComponent<Text> ().text = "An error occured while saving your map. Please try again later.";
		} else {
			AWS.result = "";

			//Upload file to latest
			AWS.AWSUpload ("maps/latest/"+controller.user+";"+ mapname.GetComponent<InputField> ().text.Replace ("/", "") + ".txt","");
			//Upload file to popular
			AWS.AWSUpload ("maps/popular/"+controller.user+";"+ mapname.GetComponent<InputField> ().text.Replace ("/", "") + ".txt","");
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "false") {
				AWS.result = "";
				InfoText.GetComponent<Text> ().text = "An error occured while saving your map. Please try again later.";
			} else {
				AWS.result = "";
				StartCoroutine (updatemaplist (false,""));	
			}
		}




	}
	public void deletemap()
	{
		InfoText.GetComponent<Text> ().text = "Deleting map: " + '"' + MapToDelete + '"'+".";
		InfoPanel.SetActive (true);
		scenecontroller.MovePanels (12);
		StartCoroutine (deletemapCoroutine ());
	}
	public IEnumerator deletemapCoroutine()
	{
		string tempmapname = MapToDelete;
		//Save map to public or private
		string fileName;
		fileName = "user/"+controller.user+"/maps/" + tempmapname.Replace ("/", "") + ".txt";

		AWS.AWSDelete (fileName,true);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "DeleteFalse") {
			AWS.result = "";
			InfoText.GetComponent<Text> ().text = "An error occured while deleting your map. Please try again later.";
		} else {
			AWS.result = "";

			//Delete file from latest
			AWS.AWSDelete ("maps/latest/"+controller.user+";"+ tempmapname.Replace ("/", "") + ".txt",true);
			//Delete file from popular
			AWS.AWSDelete ("maps/popular/"+controller.user+";"+ tempmapname.Replace ("/", "") + ".txt",true);
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "DeleteFalse") {
				AWS.result = "";
				InfoText.GetComponent<Text> ().text = "An error occured while saving your map. Please try again later.";
			} else {
				AWS.result = "";
				StartCoroutine (updatemaplist (true,tempmapname.Replace ("/", "")));	
			}
		}




	}


	public IEnumerator updatemaplist(bool IsDelete,string MapToDelete)
	{
		//Download map list first
		string dir;
		string fileContents="";
		dir = "user/"+user+"/maps/" + "maplist.txt";
		AWS.result = "";
		AWS.AWSDownload ("11",dir);

		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			InfoText.GetComponent<Text> ().text = "An error occured while saving your map. Please try again later.";
			fileContents = mapname.GetComponent<InputField> ().text;
		} else if (AWS.result.Split('|')[0]=="11"){
			if (AWS.result != "true") {
				string output = AWS.result.Split ('|') [1];
				AWS.result = "";
				var split = output.Split ('/');
				string newmaplist = "";
				if (!IsDelete) {
					//check if name already exists
					bool containsmap = false;
					foreach (string level in split) {
						if (level == mapname.GetComponent<InputField> ().text) {
							containsmap = true;
						}
					}
					if (!containsmap) {
						fileContents = output + "/" + mapname.GetComponent<InputField> ().text;
					} else {
						//else do not add the map name
						fileContents = output;
					}
				} else {
					foreach (string level in split) {
						if (level != MapToDelete) {
							if (newmaplist == "") {
								newmaplist += level;
							} else {
								newmaplist += "/" + level;
							}
						}
					}
					fileContents = newmaplist;
				}
			}

		}
		InfoText.GetComponent<Text> ().text = "Almost there...";

		//Then upload updated map list
		if (fileContents != "") {
			AWS.AWSUpload (dir, fileContents);
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "false") {
				AWS.result = "";
				InfoText.GetComponent<Text> ().text = "An error occured while saving your map. Please try again later.";
			} else {
				AWS.result = "";
				fileContents = "";
				if (IsDelete) {
					InfoText.GetComponent<Text> ().text = "Your map: " + '"' + MapToDelete + '"' + " has successfully been deleted!";
					UICanvas.GetComponent<MapSaveGUI> ().callListmap ();
				} else {
					InfoText.GetComponent<Text> ().text = "Your map: " + '"' + mapname.GetComponent<InputField> ().text + '"' + " has successfully been uploaded!";
				}
			}
		}
	}
	//Loads new map
	public IEnumerator loadmap(string nameofmap,bool leveleditor,string mapowner)
	{

		if (editplay != 0) {
			toggleeditplay ();
		}

		//If loaded map exists in directory

		string fileContents;
		maplist.Clear ();

		//Sets lastmap to what is loaded.
		string data = null;
		string mydir;
		if (leveleditor) {
			if (nameofmap == "empty") {
				mydir = "maps/empty.txt";
			} else if (mapowner!="") {
				//Custom loading in multiplayer level editor
				mydir = "user/" + mapowner + "/maps/" + nameofmap + ".txt";
			}
			else {
				mydir = "user/" + controller.user + "/maps/" + nameofmap + ".txt";
			}
		} else {
			mydir = nameofmap;
		}
		//Downloads user map list
		AWS.AWSDownload ("12",mydir);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			print ("Error loading");
		} else if (AWS.result.Split('|')[0]=="12"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";
			string loaded = fileContents;
			//Saves map locally
			activem = loaded;
			//Contains the mapname
			DeleteMap = true;
			//playerTransform.GetComponent<Rigidbody2D> ().gravityScale = 0;
			playerTransform.GetComponent<Controls> ().loadmap (loaded, true, nameofmap);

			if (LoadPanel != null) {
				LoadPanel.SetActive (false);
			}
			if (leveleditor) {
				if (nameofmap != "empty") {
					mapname.GetComponent<InputField> ().text = nameofmap;
				}
			}
			//Delay before loading new map

			StartCoroutine (DelayBeforeLoadingMap (0.1f, loaded, mydir));
		}








	}

	//Loads new map
	public IEnumerator downloadmap(string nameofmap)
	{
		//If loaded map exists in directory
		string fileContents;
		maplist.Clear();
		//Sets lastmap to what is loaded.
		string data = null;

		//Downloads user map list
		AWS.AWSDownload ("13",nameofmap);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
		} else if (AWS.result.Split('|')[0]=="13"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";
			print("Contents=" + fileContents);
			string loaded=fileContents;

			player.GetComponent<Controls> ().loadmap (loaded, false, nameofmap);

		}
	}

	public void RateLevel(int num)
	{
		if (!HasRated) {
			HasRated = true;
			StartCoroutine (DownloadUserRating (num));
			Star1.GetComponent<Button> ().interactable = false;
			Star2.GetComponent<Button> ().interactable = false;
			Star3.GetComponent<Button> ().interactable = false;
			Star4.GetComponent<Button> ().interactable = false;
			Star5.GetComponent<Button> ().interactable = false;

			InfoPanel.SetActive (true);
			level1scenecontroller.MovePanels (2);
		} else {
			InfoPanel.SetActive (false);
		}
	}
	public IEnumerator DownloadUserRating(int num)
	{
		var mymap=CurrentMapName.Split('/');
		string mydest = "";
		if (mymap.Length >= 4) {
			mydest = mymap [0] + "/" + mymap [1] + "/" + mymap [2] + "/ratings/"+mymap[3].Substring(0,mymap[3].Length-4)+"/";
		}

		AWS.AWSDownload ("22","user/" + controller.user + "/rating.txt");

		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
			//User has not rated any levels yet,so upload a new rating history for user
			AWS.AWSUpload ("user/" + controller.user + "/rating.txt",mydest+"+");
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "false") {
				AWS.result = "";
				InfoText.GetComponent<Text> ().text = "There was a problem rating the map. Please try again later.";
			} else {
				AWS.result = "";
				StartCoroutine (downloadrating (num));
			}

		} else if (AWS.result.Split ('|') [0] == "22") {
			if (AWS.result != "true") {
				string output = AWS.result.Split ('|') [1];
				AWS.result = "";
				bool CanRate = true;

				var outputsplit = output.Split ('+');
				for (int i = 0; i < outputsplit.Length; i++) {
					if (outputsplit [i] == mydest) {
						CanRate = false;
						InfoText.GetComponent<Text> ().text = "You have already voted on this level.";
					}
				}

				if (CanRate) {
					//If never rated map before, update user's rating history
					string newratinglist=output+mydest+"+";
					AWS.AWSUpload ("user/" + controller.user + "/rating.txt",newratinglist);
					while (AWS.result == "") {
						yield return null;
					}
					if (AWS.result == "false") {
						AWS.result = "";
						InfoText.GetComponent<Text> ().text = "There was a problem rating the map. Please try again later.";
					} else {
						AWS.result = "";
						StartCoroutine (downloadrating (num));
					}
				}
			}
		}
	}
	public IEnumerator downloadrating(int num)
	{
		InfoText.GetComponent<Text> ().text = "Rating level...";
		//Downloads user map ratings
		var split=CurrentMapName.Split('/');
		string ratingdest = "";
		if (split.Length >= 4) {
			ratingdest = split [0] + "/" + split [1] + "/" + split [2] + "/ratings/"+split[3].Substring(0,split[3].Length-4)+"/";
		}
		AWS.AWSList (ratingdest, 0);
		while (AWS.result == "") {
			yield return null;
		}

		if (AWS.result == "none") {
			AWS.result = "";
			InfoText.GetComponent<Text> ().text = "There was a problem rating the map. Please try again later.";
		} else if (AWS.result == "empty"){
			string list = AWS.result;
			AWS.result = "";

			//No ratings for the level,so upload new rating
			AWS.AWSUpload (ratingdest+num.ToString()+";1","");
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "false") {
				AWS.result = "";
			} else if (AWS.result == "true") {
				AWS.result = "";
			}

		} else {
			string list = AWS.result;
			AWS.result = "";
			var split2=list.Split('+');
			var split3 = split2 [0].Split ('/');

			//Delete rating
			AWS.AWSDelete(ratingdest,false);
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "DeleteFalse") {
				AWS.result = "";
				InfoText.GetComponent<Text> ().text = "There was a problem rating the map. Please try again later.";
			} else if (AWS.result == "DeleteTrue") {
				AWS.result = "";
			}

			string ratingname = split3 [9];

			float rating = float.Parse(ratingname.Split (';') [0]);
			float ratingQuantity = float.Parse(ratingname.Split (';') [1]);

			float newrating = (rating * ratingQuantity + num) / (ratingQuantity + 1);
			string newratingname = newrating.ToString () + ";" + (ratingQuantity + 1).ToString ();
			//Finally upload the new ratings
			AWS.AWSUpload (ratingdest+newratingname,"");
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result == "false") {
				AWS.result = "";
				InfoText.GetComponent<Text> ().text = "There was a problem rating the map. Please try again later.";
			} else if (AWS.result == "true") {
				AWS.result = "";
			}
			InfoText.GetComponent<Text> ().text = "Your vote has changed the rating of the level from "+rating+" to "+newrating+".";
		}


	}

	//Unpause game
	public void unpausegame()
	{
		PauseGame = false;
	}

	//Unpause game
	public void pausegame()
	{
		PauseGame = true;
	}

	//Allow placing blocks
	public void caneditblock()
	{
		editallowed = true;
	}

	//Disallow placing blocks
	public void cannoteditblock()
	{
		editallowed = false;
	}


	//Clear maplist
	public void clearmaplist()
	{
		maplist.Clear();
	}

	IEnumerator DelayBeforeLoadingMap(float time, string loaded,string mydir)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		string[] blocka = { "b1", "b2", "b3", "b4", "brick", "finish", "ice", "itemonce", "iteminf", "leftarrow", "rightarrow", "uparrow", "downarrow", "bomb", "crumble", "vanish", "move", "rotateleft", "rotateright", "push", "happy", "sad", "net", "heart", "time", "water","start" ,"start","checkpoint"};

		/*
		foreach (string bstring in blocka)
		{
			var blocks = GameObject.FindGameObjectsWithTag (bstring);
			foreach (GameObject block in blocks) {
				Destroy (block);
			}
		}
		*/
		foreach (KeyValuePair<string, GameObject> item in blockmapd)
		{
			Destroy (blockmapd[item.Key].gameObject);
		}
		PlayerPrefs.SetString ("lastmapname", mydir);

		playerTransform.GetComponent<Controls>().loadmap(loaded,false,mydir);
		playerTransform.GetComponent<Controls> ().Initialise ();
		resetting = false;
		//unpausegame();


	}
	public void setplayer(GameObject obj)
	{
		player = obj;
	}

	//leveleditor/level1 to networklobby
	public void enterlobby()
	{
		if (PhotonNetwork.inRoom) {
			PhotonNetwork.LeaveRoom ();
		}

	}

	//Followed after enterlobby
	public void OnJoinedLobby()
	{
		/*
		foreach (GameObject obj in AllObj) {
			if (!obj.GetComponent<DDOL> () && obj!=gameObject) {
				Destroy (obj);
			}
		}
		*/
		GameObject photoncontroller = GameObject.FindGameObjectWithTag ("PhotonController");
		Destroy (photoncontroller);
		/*
		var AllObj = GameObject.FindObjectsOfType<GameObject> ();
		foreach (GameObject obj in AllObj) {
			if (obj != gameObject && obj.GetComponent<DDOL>()==null) {
				Destroy (obj);
			}
		}
		*/
		LoadingController.GetComponent<Loader> ().StartLoad ("NetworkLobby", true);
	}

	public void quitgame()
	{
		if (endgame == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			PauseGame = true;
			endgame = 2;


		}
	}



	public IEnumerator autosavemap(float time)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay

		//Autosaves map every 60 seconds
		print("Saving");
		if ((PhotonNetwork.room.PlayerCount == 1)) {
			StartCoroutine (savemapCoroutine ());
		} else {
			if (PhotonNetwork.isMasterClient) {
				//Individual saving
				StartCoroutine (savemapCoroutine ());

			} else if (!PhotonNetwork.isMasterClient) {
				//Cannot save
				if (AuthorisedToSaveMap) {
					StartCoroutine (savemapCoroutine ());
				}
			}
		}

		StartCoroutine(autosavemap(5f));
	}

	//Reply from master client
	public void CallMasterSendAccept()
	{
		if (PhotonNetwork.isMasterClient) {
			playerTransform.gameObject.GetComponent<Controls> ().MasterSend (CurrentRequestID, CurrentRequestUser, true,CurrentRequestNameOfMap);
		}
	}
	//Reply from master client
	public void CallMasterSendReject()
	{
		if (PhotonNetwork.isMasterClient) {
			playerTransform.gameObject.GetComponent<Controls> ().MasterSend (CurrentRequestID, CurrentRequestUser, false,CurrentRequestNameOfMap);
		}
	}

	public void Calltoggleeditplay()
	{
		if ((PhotonNetwork.room.PlayerCount == 1)) {
			toggleeditplay ();
		} else {
			if (PhotonNetwork.isMasterClient) {
				if (editplay == 0) {
					CurrentRequestID = 0;
					CurrentRequestUser = controller.user;
					CallMasterSendAccept ();
				} else {
					CurrentRequestID = 1;
					CurrentRequestUser = controller.user;
					CallMasterSendAccept ();
				}
			} else if (!PhotonNetwork.isMasterClient) {
				if (editplay == 0) {
					playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 0, controller.user,"");

					RequestPanel.SetActive (true);
					scenecontroller.MovePanels (16);

					string message = "Waiting for room owner to accept request to start playing mode";
					RequestText.GetComponent<Text> ().text = message;
					RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
				}
				else if (editplay != 0) {
					playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 1, controller.user,"");

					RequestPanel.SetActive (true);
					scenecontroller.MovePanels (16);

					string message = "Waiting for room owner to accept request to start editing mode";
					RequestText.GetComponent<Text> ().text = message;
					RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
				}
			}
		}
	}

	public void Callsetgamemode(int mode)
	{
		if ((PhotonNetwork.room.PlayerCount == 1)) {
			setgamemode (mode);
		} else {
			if (PhotonNetwork.isMasterClient) {
				if (mode == 1) {
					CurrentRequestID = 2;
					CurrentRequestUser = controller.user;
					CallMasterSendAccept ();
				} else {
					CurrentRequestID = 3;
					CurrentRequestUser = controller.user;
					CallMasterSendAccept ();
				}
			} else if (!PhotonNetwork.isMasterClient) {
				if (mode == 1) {
					playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 2, controller.user,"");

					RequestPanel.SetActive (true);
					scenecontroller.MovePanels (16);

					string message = "Waiting for room owner to accept request to change mode to Deathmatch";
					RequestText.GetComponent<Text> ().text = message;
					RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
				}
				else if (mode == 0) {
					playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 3, controller.user,"");

					RequestPanel.SetActive (true);
					scenecontroller.MovePanels (16);

					string message = "Waiting for room owner to accept request to change mode to Race";
					RequestText.GetComponent<Text> ().text = message;
					RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
				}
			}
		}
	}

	public void Callloadmap(string nameofmap,bool leveleditor)
	{
		if ((PhotonNetwork.room.PlayerCount == 1)) {
			StartCoroutine(loadmap(nameofmap,leveleditor,controller.user));
		} else {
			if (PhotonNetwork.isMasterClient) {
				//Individual saving
				CurrentRequestID = 4;
				CurrentRequestNameOfMap = nameofmap;
				CurrentRequestUser = controller.user;

				CallMasterSendAccept ();

			} else if (!PhotonNetwork.isMasterClient) {
				playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 4, controller.user,nameofmap);

				RequestPanel.SetActive (true);
				scenecontroller.MovePanels (16);

				string message = "Waiting for room owner to accept request to load "+nameofmap;
				RequestText.GetComponent<Text> ().text = message;
				RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
			}
		}

	}

	public void Callsavemap()
	{
		if ((PhotonNetwork.room.PlayerCount == 1)) {
			savemap ();
		} else {
			if (PhotonNetwork.isMasterClient) {
				//Individual saving
				savemap ();

			} else if (!PhotonNetwork.isMasterClient) {
				if (AuthorisedToSaveMap) {
					savemap ();
				}
				else{
					playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 5, controller.user, "");

					RequestPanel.SetActive (true);
					scenecontroller.MovePanels (16);

					string message = "Waiting for room owner to accept request to save this map to your own levels";
					RequestText.GetComponent<Text> ().text = message;
					RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
				}
			}
		}
	}

	public void CallResetMap()
	{
		if ((PhotonNetwork.room.PlayerCount == 1)) {
			ResetMap ();
		} else {
			if (PhotonNetwork.isMasterClient) {
				CurrentRequestID = 6;
				CallMasterSendAccept ();

			} else if (!PhotonNetwork.isMasterClient) {
				playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("MasterReceive", PhotonTargets.MasterClient, 6, controller.user,"");

				RequestPanel.SetActive (true);
				scenecontroller.MovePanels (16);

				string message = "Waiting for room owner to accept request to reset entire map";
				RequestText.GetComponent<Text> ().text = message;
				RequestTitle.GetComponent<Text> ().text = "Awaiting reply";
			}
		}
	}

	//Save map into textfile when pressing save button
	public void savemap()
	{
		InfoText.GetComponent<Text> ().text = "Saving map: " + '"' + mapname.GetComponent<InputField> ().text + '"'+".";
		InfoPanel.SetActive (true);
		scenecontroller.MovePanels (12);
		StartCoroutine (savemapCoroutine ());
	}

	public void ResetMap()
	{
		if (!resetting) {
			resetting = true;
			StartCoroutine (loadmap ("empty",true,""));
		}
	}

	public void setgamemode(int mode)
	{
		//Race mode
		if (mode == 0) {
			GameMode = "Race";
			RaceCheckmark.SetActive (true);
			DeathmatchCheckmark.SetActive (false);
		} else if (mode == 1) {
			GameMode = "Deathmatch";
			RaceCheckmark.SetActive (false);
			DeathmatchCheckmark.SetActive (true);
		}
	}
	public void toggleeditplay()
	{
		//Start edit play
		if (editplay == 0) {
			GameTime.GetComponent<GameTime> ().ResetStartTime ();
			editplay = 1;
			toggleflying ();
			togglepreview ();
			TopPanel.SetActive (false);
			BlockPanel.SetActive (false);
			BlockSidePanel.SetActive (false);
			GameTimePanel.SetActive (true);
			if (EditingMode != null) {
				EditingMode.GetComponent<Text> ().text = "IN PLAYING MODE";
			}
			//End edit play
		} else {
			//Reset gravity and rotation
			Camera.main.GetComponent<CameraFollow> ().UICanvas.GetComponent<RectTransform> ().rotation=Quaternion.Euler(new Vector3(0,0,0));
			Camera.main.GetComponent<Transform> ().rotation = Quaternion.Euler(new Vector3(0,0,0));
			TopPanel.SetActive (true);
			BlockPanel.SetActive (true);
			BlockSidePanel.SetActive (false);
			if (EditingMode != null) {
				EditingMode.GetComponent<Text> ().text = "IN EDITING MODE";
			}
			foreach (GameObject block in disabledblocks) {
				if (block == null) {
					return;
				}
				if (block.tag == "key") {
					block.GetComponent<SnapToGrid> ().canactivatekey = true;
					Color tempcolor = block.GetComponent<SpriteRenderer> ().color;
					tempcolor.a = 1f;
					block.GetComponent<SpriteRenderer> ().color = tempcolor;

				} else if (block.tag == "locked") {
					block.GetComponent<SnapToGrid> ().canactivatekey = true;
					block.GetComponent<BoxCollider2D> ().enabled = true;
					block.GetComponent<SnapToGrid> ().fading = "none";
					Color tempcolor = block.GetComponent<SpriteRenderer> ().color;
					tempcolor.a = 1f;
					block.GetComponent<SpriteRenderer> ().color = tempcolor;

				} else if (block.GetComponent<SpriteRenderer> () != null) {
					if (block.GetComponent<SpriteRenderer> ().color == new Color (0.4f, 0.4f, 0.4f)) {
						//Set used blocks back
						block.GetComponent<SnapToGrid> ().used = false;
						block.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f);

						//set itemonce back
						if (block.GetComponent<SpriteRenderer> ().sprite == itemopen) {
							block.GetComponent<SpriteRenderer> ().sprite = itemclosed;
						}

						//Destroylarge player
						GameObject largeplayer = GameObject.FindGameObjectWithTag ("LargePlayer");
						if (largeplayer != null) {
							Destroy (largeplayer);
						}
					}
				} else {
					//Set destroyed blocks back
					block.SetActive (true);
				}
			}
			disabledblocks.Clear ();
			editplay = 0;
			toggleflying ();
			togglepreview ();
			GameTimePanel.SetActive (false);
		}
	}


	public void toggleflying()
	{
		if (endgame == 0) {
			endgame = 4;
			PauseGame = true;
			flying = true;
			FlyingButtonImage.GetComponent<Image> ().sprite = toggleright;
		} else {
			endgame = 0;
			PauseGame = false;
			flying = false;
			FlyingButtonImage.GetComponent<Image> ().sprite = toggleleft;
		}
	}

	public void togglepreview()
	{
		if (previewing == true) {
			previewing = false;
			PreviewButtonImage.GetComponent<Image> ().sprite = toggleleft;
		} else {
			previewing = true;
			PreviewButtonImage.GetComponent<Image> ().sprite = toggleright;
		}
	}
	public void toggledelete()
	{
		if (panning == true) {
			panning = false;
			PanningButton.GetComponent<Image> ().sprite = DeselectedPanning;
			DeleteButton.GetComponent<Image> ().sprite = SelectedDelete;
		} else {
			if (deletingblocks) {
				deletingblocks = false;
				LargePencil.SetActive (true);
				SmallEraser.SetActive (true);
				LargeEraser.SetActive (false);
				SmallPencil.SetActive (false);
			} else {
				deletingblocks = true;
				LargePencil.SetActive (false);
				SmallEraser.SetActive (false);
				LargeEraser.SetActive (true);
				SmallPencil.SetActive (true);
			}
		}
	}
	public void togglepanning()
	{
		if (panning == true) {
			panning = false;
			PanningButton.GetComponent<Image> ().sprite = DeselectedPanning;
			DeleteButton.GetComponent<Image> ().sprite = SelectedDelete;
		} else {
			panning = true;
			PanningButton.GetComponent<Image> ().sprite = SelectedPanning;
			DeleteButton.GetComponent<Image> ().sprite = DeselectedDelete;
		}
	}


	public IEnumerator Upload(string filecontents,string user) {
		AWS.AWSUpload ("user/"+user+"/rank.txt",filecontents);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "false") {
			AWS.result = "";
		} else {
			AWS.result = "";
		}
	}

	//Handles customisation panel
	public void OnSpeedChange()
	{
		SpeedNoText.GetComponent<Text> ().text = SpeedSlider.GetComponent<Slider> ().value.ToString ();
		SSpeed = Mathf.RoundToInt (SpeedSlider.GetComponent<Slider> ().value);
	}

	public void OnAccChange()
	{
		AccNoText.GetComponent<Text> ().text = AccSlider.GetComponent<Slider> ().value.ToString ();
		SAcc = Mathf.RoundToInt (AccSlider.GetComponent<Slider> ().value);
	}

	public void OnJumpChange()
	{
		JumpNoText.GetComponent<Text> ().text = JumpSlider.GetComponent<Slider> ().value.ToString ();
		SJump = Mathf.RoundToInt (JumpSlider.GetComponent<Slider> ().value);
	}

	//Save sign information
	public void setsign(InputField input)
	{
		string signtext = input.GetComponent<InputField> ().text;
		string[] dialogue = { signtext };
		signobj.GetComponent<DialogueZone> ().Dialogue = dialogue;

		if (mapd.ContainsKey (signpos.x + "," + signpos.y)) {
			if (mapd [signpos.x + "," + signpos.y].Contains ("sign")) {
				mapd [signpos.x + "," + signpos.y] = "sign" + signtext;
			}
		}
	}

	public void SetCanMoveInEdit()
	{
		CanMoveInEdit = true;
		playerTransform.gameObject.GetComponent<Character> ().UnFreeze ();
	}
	public void SetCannotMoveInEdit()
	{
		CanMoveInEdit = false;
		playerTransform.gameObject.GetComponent<Character> ().Freeze ();
	}
	public void ToggleBlockPanel()
	{
		if (BlockPanel != null) {
			print (BlockPanel.activeSelf + "s");
			if (BlockPanel.activeSelf == true) {
				BlockPanel.SetActive (false);
				//scenecontroller.MovePanels (1);

				BlockSidePanel.SetActive (false);
			} else if (BlockPanel.activeSelf == false) {
				BlockPanel.SetActive (true);
				//scenecontroller.MovePanels (0);
			}
		}
	}




	public void CreateRobotExplosion(string playername)
	{
		var playerss = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in playerss) {
			if ("Slider" + player.name == playername) {
				Instantiate (RobotExplosion, player.transform.position + new Vector3 (0f, 0f, 0f), transform.rotation);
				if (player.name == PhotonNetwork.player.NickName) {
					controller.source.PlayOneShot (controller.a_win);
				}
			}
		}
	}

	public void SendChat()
	{
		if (!Application.isMobilePlatform) {
			//Deactivate chat panel if not sending message
			if (ChatPanel.GetComponentInChildren<InputField> ().text == "") {
				ChatBottom.SetActive (false);
				ChatBackground.SetActive (false);
				//camfollow.GetComponent<CameraFollow>().ChatCanvas.GetComponent<GraphicRaycaster> ().enabled = false;
			} else {
				//Send message instead of deactivating chat panel
				playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("SendChatMessage", PhotonTargets.OthersBuffered, user + "(" + rank + "): ",ChatPanel.GetComponentInChildren<InputField> ().text + "\n");

				string chatuser = user + "(" + rank + "): ";
				string chatmessage = ChatPanel.GetComponentInChildren<InputField> ().text + "\n";
				SelectedChannelText.GetComponent<Text> ().text += "<color=#FFA53F>"+chatuser+"</color>" +  "<color=#FFD8AF>"+chatmessage+"</color>";

				ChatPanel.GetComponentInChildren<InputField> ().text = "";
				ChatPanel.GetComponentInChildren<InputField> ().ActivateInputField ();
				ChatScrollBar.GetComponent<Scrollbar> ().value = 0;
			}
		} else {
			//Send message instead of deactivating chat panel
			if (ChatPanel.GetComponentInChildren<InputField> ().text != "") {
				playerTransform.gameObject.GetComponent<PhotonView> ().photonView.RPC ("SendChatMessage", PhotonTargets.OthersBuffered, user + "(" + rank + "): ",ChatPanel.GetComponentInChildren<InputField> ().text + "\n");

				string chatuser = user + "(" + rank + "): ";
				string chatmessage = ChatPanel.GetComponentInChildren<InputField> ().text + "\n";
				SelectedChannelText.GetComponent<Text> ().text += "<color=#FFA53F>"+chatuser+"</color>" +  "<color=#FFD8AF>"+chatmessage+"</color>";

				ChatPanel.GetComponentInChildren<InputField> ().text = "";
				ChatScrollBar.GetComponent<Scrollbar> ().value = 0;
			}
		}
	}

	public void LightningStrike(string player)
	{
		if (PhotonNetwork.player.NickName != player) {
			playerTransform.gameObject.GetComponent<CharacterDash> ().StunPlayer ();
		}

		Sky.GetComponent<Animator> ().SetBool ("dark", true);
		controller.PlayOnce (controller.a_thunder);
		for (int i = 0; i < 5; i++) {
			float randx = UnityEngine.Random.Range (-10f, 20f);
			float randy = UnityEngine.Random.Range (0f, 0f);
			//print (randx + ";" + randy);
			GameObject thislightning= Instantiate (SimpleLightningBoltPrefab, new Vector3 (randx, randy, 0f), Quaternion.identity, Lightnings.transform);
			thislightning.transform.GetChild (1).transform.position = thislightning.transform.GetChild (1).transform.position + new Vector3 (UnityEngine.Random.Range(-10f,10f), 0f, 0f);
		}
		StartCoroutine (DestroyLightning ());
	}

	public IEnumerator DestroyLightning()
	{
		yield return new WaitForSeconds(5f);
		Sky.GetComponent<Animator> ().SetBool ("dark", false);
		var lightninglist = GameObject.FindGameObjectsWithTag ("lightning");

		foreach (GameObject lightningobj in lightninglist) {
			Destroy (lightningobj);
		}
	}

	public void InvitePlayers(InputField userinput)
	{
		string mapnamewithTXT = CurrentMapName.Split ('/') [3];
		string mapname = mapnamewithTXT.Substring (0, mapnamewithTXT.Length - 4);
		StartCoroutine(InvitePlayersToLevelEditor("Main",controller.user,userinput.text,mapname));
	}
	//Invite players to level editor through lobby chat
	public IEnumerator InvitePlayersToLevelEditor(string room,string FromUsername,string ToUsername, string mapname) {

		if (FromUsername != ToUsername) {
			AWS.AWSUpload ("chatrooms/" + room + "/" + ("chatbot" + ": " + FromUsername + ":" + ToUsername + ":" + mapname).Replace ("/", "") + ".txt", "");
			while (AWS.result == "") {
				yield return null;
			}
			AWS.result = "";
		}
	}

}
