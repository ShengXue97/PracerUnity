using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using MoreMountains.Tools;
using MoreMountains.CorgiEngine;
public class Controls : MonoBehaviour
{
	//PHOTON NETWORKING
	public PhotonView photonView;
	public Animator CorgiAnimator;
	public List<float> SliderList;
	public float PlayerPosition=0f;
	public int PlayerRank = 0;

	public string horcontact="";
	//[SyncVar]
	public float LastFallingSpeed=0f;
	public bool CanReduceLastFallingSpeed = false;

	public string user="anonymous";
	public bool AtSafeSpot=false;
	public bool SelectingBlock=false;
	public bool UsedUpItem=false;
	public string CurrentItem="none";
	public bool initialised=false;
	//[SyncVar]
	public string mapstring;
	public bool PressSpace=false;
	public bool HoldingSpace=false;
	public int LoadedMapBefore;

	public float protation=0f;
	//[SyncVar]
	Vector3 realPosition = Vector3.zero;
	//[SyncVar]
	Quaternion realRotation;
	private float updateInterval;
	public string localmapstring="";
	public float rank;
	public float exp;
	public float score=0;

	public float CurrentGravity=-30f;
	public int playerwon=0;
	public string lobbymapname;
	public string GameMode="";

	public GameObject leftcheck;
	public GameObject rightcheck;
	public GameObject topcheck;
	public GameObject bottomcheck;
	public GameObject leftcrushcheck;
	public GameObject rightcrushcheck;
	public GameObject topcrushcheck;
	public GameObject bottomcrushcheck;

	public Sprite wonsprite;
	public Sprite loadingsprite;
	public Sprite sjrightsprite;
	public Sprite crouchrightsprite;
	public Sprite rightsprite;
	public Sprite idlerightsprite;
	public Sprite stunnedrightsprite;
	public Sprite watersprite;
	public Sprite emptysprite;

	public Weapon CorgiMachineGun;
	public Weapon CorgiSpineMachineGun;
	public Weapon CorgiSword;
	public Weapon RobotWeapon;
	public Weapon GrenadeLauncher;
	public Weapon RocketLauncher;
	public Weapon Shotgun;
	public Weapon MeleeAttack;

	public Camera LocalCamera;
	public Vector3 safespot=new Vector3(15f,15f,0f);

	public GameObject HurtEffect;
	public GameObject RocketFlamesRedEffect;
	public GameObject RocketFlamesEffect;
	public GameObject CoinBurstEffect;
	public GameObject TouchTheGroundEffect;

	public GameObject NameText;
	public GameObject ModelContainer;
	public GameObject dot;
	public GameObject pmap;
	public GameObject pfinish;
	public GameObject myground;
	public GameObject BulletLeft;
	public GameObject BulletRight;
	public GameObject SwordBoxLeft;
	public GameObject SwordBoxRight;
	public GameObject LightningBox;
	public GameObject BombItem;
	public GameObject BlockItem;
	public GameObject vanishprefab;
	public GameObject niceprefab;
	public GameObject dotobj;
	public GameObject lobbymanager;
	public GameObject loadercontroller;
	GameObject mainplayer;
	//setblock
	public Sprite spr_b1; public Sprite spr_b2; public Sprite spr_b3; public Sprite spr_b4; public Sprite spr_brick; public Sprite spr_finish; public Sprite spr_ice; public Sprite spr_itemonce; public Sprite spr_iteminf; public Sprite spr_leftarrow; public Sprite spr_rightarrow; public Sprite spr_uparrow; public Sprite spr_downarrow; public Sprite spr_bomb; public Sprite spr_crumble; public Sprite spr_vanish; public Sprite spr_move; public Sprite spr_rotateleft; public Sprite spr_rotateright; public Sprite spr_push; public Sprite spr_happy; public Sprite spr_sad; public Sprite spr_net; public Sprite spr_heart; public Sprite spr_time; public Sprite spr_water; public Sprite spr_start; public Sprite spr_stop; public Sprite spr_ladder; public Sprite spr_antigravity; public Sprite spr_bouncy; public Sprite spr_spike; public Sprite spr_checkpoint; public Sprite spr_portal; public Sprite spr_door; public Sprite spr_oneway; public Sprite spr_rocket; public Sprite spr_falling; public Sprite spr_giant; public Sprite spr_tiny; public Sprite spr_sticky; public Sprite spr_fan; public Sprite spr_key; public Sprite spr_lock; public Sprite spr_weapon; public Sprite spr_npc; public Sprite spr_sign; public Sprite spr_timefreeze;
	//setblock
	public GameObject b1; public GameObject b2; public GameObject b3; public GameObject b4; public GameObject brick; public GameObject finish; public GameObject ice; public GameObject itemonce; public GameObject iteminf; public GameObject leftarrow; public GameObject rightarrow; public GameObject uparrow; public GameObject downarrow; public GameObject bomb; public GameObject crumble; public GameObject vanish; public GameObject move; public GameObject rotateleft; public GameObject rotateright; public GameObject push; public GameObject happy; public GameObject sad; public GameObject net; public GameObject heart; public GameObject time; public GameObject water; public GameObject start; public GameObject stop; public GameObject ladder; public GameObject antigravity; public GameObject bouncy; public GameObject spike; public GameObject checkpoint; public GameObject portal; public GameObject door; public GameObject oneway; public GameObject rocket; public GameObject falling; public GameObject giant; public GameObject tiny; public GameObject sticky; public GameObject fan; public GameObject key; public GameObject locked; public GameObject weapon; public GameObject npc; public GameObject sign; public GameObject timefreeze;
	public GameObject portal0; public GameObject portal1; public GameObject portal2; public GameObject portal3; public GameObject portal4; public GameObject portal5; public GameObject portal6; public GameObject portal7; public GameObject portal8; public GameObject portal9; 
	public GameObject key0; public GameObject key1; public GameObject key2; public GameObject key3; public GameObject key4; public GameObject key5; public GameObject key6; public GameObject key7; public GameObject key8; public GameObject key9; 
	public GameObject locked0; public GameObject locked1; public GameObject locked2; public GameObject locked3; public GameObject locked4; public GameObject locked5; public GameObject locked6; public GameObject locked7; public GameObject locked8; public GameObject locked9; 

	private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
	private ExitGames.Client.Photon.Hashtable m_roomCustomProperties = new ExitGames.Client.Photon.Hashtable();
	//setblock
	public Vector3 tempsignpos;
	public string[] blocka = { "b1", "b2", "b3", "b4", "brick", "finish", "ice", "itemonce", "iteminf", "leftarrow", "rightarrow", "uparrow", "downarrow", "bomb", "crumble", "vanish", "move", "rotateleft", "rotateright", "push", "happy", "sad", "net", "heart", "time", "water","start","stop","ladder","antigravity","bouncy","spike","checkpoint","portal","door","oneway","rocket","falling","giant","tiny","sticky","fan","lock","lock","weapon","npc","sign","timefreeze"};
	public int selectedblock = 1;
	public GameObject block;
	public GameObject pmapobj;
	public GameObject pfinishobj;
	public Rigidbody2D rb;
	public GameObject BlockAbove;
	public float jumpheight;
	public bool jump;
	public bool UsingSuperJump = false;
	public bool useitem;
	public List<string> maplist;
	public int crystals;
	public bool editing = false;
	public LayerMask whatIsGround;
	public LayerMask UserLayer;
	public bool onGround;
	public int mousedown;
	public string maps;
	public bool canjump=false;
	public List<Teleporter> portals;
	public Dictionary<string, string> mapd = new Dictionary<string, string>() { };
	Dictionary<string, string> tempmapd = new Dictionary<string, string>() { };
	public bool countingdown=false;
	public int mapxmax;
	public int mapymax;
	public int mapxmin;
	public int mapymin;
	public Camera Camera;
	public bool canshoot = true;
	public bool canteleport = false;
	public bool OnLadder=false;
	public bool OnWater=false;
	public Vector3 pos;
	public Rect bigrect;
	public Rect smallrect;
	public string keypressed = "none";
	public bool OnIce = false;
	public bool stunned = false;
	public int crouching = 0;
	public int playercount = 0;
	public bool facingright = false;
	public string settingcrouch = "none";
	public bool canexit = false;
	public bool moveleft;
	public bool moveright;
	public bool movedown;
	public double sjtime = 0;
	public bool GameStarted=false;
	public bool CountingDownEnded=false;
	public double sjmax = 1;
	public bool savingmap = false;
	public string OnArrow = "";
	public float icefloat = 0f;
	public double jetfuel;
	private Vector3 v;
	//Max and min of camera
	float minFov = 15f;
	float maxFov = 90f;
	float sensitivity = 10f;
	public bool immune = false;
	public GameObject buttonclicked;
	public Ray ray;
	public RaycastHit rayHit;
	public Vector3 startpos=new Vector3(15f,15f,0f);
	public Vector3 checkpointpos=new Vector3(15f,15f,0f);
	public float buttontime=0;
	public Vector3 lastposition;
	public float lastdistance;
	public CameraFollow camfollow;
	public List<GameObject> mapb;
	public List<GameObject> DisabledBlocks;
	public Controllers controller;
	public Sprites sprites;
	public bool isjoiner;
	public bool initialisedPositions = false;
	public bool crushed=false;
	//[SyncVar]
	public string newsprite="none";

	private bool jumpedDuringSprint; //Used to see if we jumped during our sprint

	//Jump related variables

	//Normal:320,Lowest: 53.3f, 1 point: 5.33f
	public float initialJumpForce=53.3f;       //How much force to give to our initial jump

	public float Speed= 0;//Don't touch this

	//Normal:5f, Lowest: 0.83f, 1 point: 0.083f
	public float MaxSpeed= 0.83f;//This is the maximum speed that the object will achieve

	//Normal:20f, Lowest: 3.33f, 1 point: 0.333f
	public float MaxAcceleration= 3.33f;//How fast will object reach a maximum speed
	public float Acceleration= 3.33f;
	//Normal:20f, Lowest: 3.33f, 1 point: 0.333f
	public float MaxDeceleration= 3.33f;//How fast will object reach a speed of 0
	public float Deceleration=3.33f;

	public float ExpBonus = 0f;
	public static Vector2 GetGrid(Vector3 pos, bool returngrid)
	{
		double val1 = pos.x / 1.00;
		float val2 = (float)val1;

		double val3 = 1.00;
		float val4 = (float)val3;
		float val5 = Mathf.Round(val2) * val4;

		double val6 = pos.y / 1.00;
		float val7 = (float)val6;

		double val8 = 1.00;
		float val9 = (float)val8;
		float val10 = Mathf.Round(val7) * val9;

		if (!returngrid)
		{
			Vector2 mappos = new Vector2(val5, val10);
			return mappos;
		}
		else
		{
			Vector2 gridpos = new Vector2(Mathf.Round(val2), Mathf.Round(val7));
			return gridpos;
		}


	}



	// Use this for initialization
	void Start()
	{
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Lobby")) {
			Destroy (gameObject);
		}
		camfollow = Camera.main.GetComponent<CameraFollow> ();
		rb = GetComponent<Rigidbody2D>();
		GetComponent<Character> ().Freeze ();
		CorgiAnimator = GetComponent<Character>().CharacterAnimator;
		GetComponent<CharacterJetpack> ().AbilityPermitted = false;
		transform.localScale =new Vector3(0.43f,0.5f,0.45f);
		//Change start position to middle of entire map of 100x100 blocks grid
		transform.position=new Vector3(1.00f*50f,1.00f*50f,0f);
		Physics2D.IgnoreLayerCollision (9, 9, true);
		LoadedMapBefore = 0;
		ChangeItem ("none");
		/*
		string test = "24#{";
		for (int i=0;i<10000;i++)
		{
			test+="{"+'"'+i.ToString()+","+i.ToString()+'"'+','+'"'+"b1"+'"'+"},";
		}
		System.IO.File.WriteAllText("test.txt", test);
		*/
		GameObject mycontroller = GameObject.FindGameObjectWithTag ("controller");
		if (mycontroller!=null)
		{
			controller = mycontroller.GetComponent<Controllers> ();
		}
		GameObject TopPanel = GameObject.FindGameObjectWithTag ("TopPanel");
		GameObject LeftPanel = GameObject.FindGameObjectWithTag ("LeftPanel");
		if (TopPanel != null) {
			TopPanel.SetActive (false);
		}
		if (LeftPanel != null) {
			LeftPanel.SetActive (false);
		}

		//Pause game before level is loaded
		if (PhotonNetwork.inRoom) {
			if (!photonView.isMine) {
				NameText.GetComponent<Text> ().text = photonView.owner.name;
				name=photonView.owner.name;
				return;
			}
		}
		user = controller.user;
		rank = controller.rank;
		if (camfollow != null) {
			if (camfollow.GetComponent<CameraFollow> ().UndoButton != null) {
				camfollow.GetComponent<CameraFollow> ().UndoButton.onClick.AddListener (UndoAction);
				camfollow.GetComponent<CameraFollow> ().RedoButton.onClick.AddListener (RedoAction);
			}
		}
		if (NameText != null) {
			NameText.GetComponent<Text> ().text = PhotonNetwork.playerName;
		}
			name=PhotonNetwork.playerName;
		//Loads map

		string lastmap="";
		string lastmapname = "";

		if (PhotonNetwork.inRoom) {
			if (PhotonNetwork.room.CustomProperties != null) {
				m_roomCustomProperties = PhotonNetwork.room.CustomProperties;
			}
		}
		//lastmap = (string)m_roomCustomProperties ["lastmap"];
		lastmapname = (string)m_roomCustomProperties ["lastmapname"];
		if (photonView.isMine) {
			LocalCamera = camfollow.GetComponent<CameraFollow>().setTarget(transform);
			camfollow.name = name + camfollow.name;
			camfollow.GetComponent<CameraFollow>().setplayer (gameObject);

			StartCoroutine (camfollow.GetComponent<CameraFollow> ().loadmap(lastmapname,false,""));
		}


	


	}

	public void Initialise()
	{
		//Start loader bar
		loadercontroller = camfollow.GetComponent<CameraFollow>().loadercontroller;
		if (loadercontroller != null) {
			//loadercontroller.GetComponent<Loader> ().StartLoadLevel ();
		}
		//camfollow.GetComponent<CameraFollow>().ChatPanel.GetComponentInChildren<InputField> ().ActivateInputField ();
		sprites = GetComponent<Sprites> ();
		controller = GameObject.FindGameObjectWithTag ("controller").GetComponent<Controllers>();
		mapstring= controller.lastmapname;
		rank = controller.rank;
		exp = controller.exp;

		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			StartCoroutine (LoadingTimer (0f, "1"));
		} else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")){
			CountingDownEnded = true;
		}
		camfollow.GetComponent<CameraFollow>().GameTime.SetActive (false);
		//GameStarted = true;
		rb.gravityScale = 0f;
		//Instantiate(ice, transform.position, Quaternion.identity);




		mousedown = 0;
		//Camera.allCameras[1].orthographicSize = 5.0f;

		/*
        for (int i = 0; i < 100; i++){
            for (int j = 0; j < 10; j++){
                string cord = i.ToString() + "," + j.ToString();
                mapd.Add(cord, "b1");
            }
        }
        */
		lastposition = gameObject.transform.position;
		//Calculates score
		StartCoroutine(calscore(1f));

		lobbymanager = GameObject.FindGameObjectWithTag ("lobbymanager");

		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {

		}
		//Retrieves usere stats from customisation panel
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			//Normal:3.025,Lowest: 1.2f, 1 point: 0.0365f
			GetComponent<CharacterJump>().JumpHeight = controller.SJump * 0.0365f + 1.2f;

			//Normal:8f, Lowest: 1.328f, 1 point: 0.13344f
			GetComponent<CharacterHorizontalMovement>().WalkSpeed = controller.SSpeed * 0.13344f + 1.328f;
			GetComponent<CharacterHorizontalMovement>().MovementSpeed = controller.SSpeed * 0.13344f + 1.328f;

			//Normal:20f, Lowest: 3.33f, 1 point: 0.333f
			GetComponent<CorgiController>().Parameters.SpeedAccelerationOnGround= controller.SAcc * 0.333f + 3.33f;

			//Normal:5f, Lowest: 0.8325f, 1 point: 0.08325f
			GetComponent<CorgiController>().Parameters.SpeedAccelerationInAir= controller.SAcc * 0.08325f + 0.8325f;

			//Lowest: 0f, 1point: 1f
			ExpBonus += controller.SExpBonus;

		} else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			initialJumpForce = 53.3f+camfollow.GetComponent<CameraFollow>().SJump * 5.33f;
			MaxSpeed = 0.83f+camfollow.GetComponent<CameraFollow>().SSpeed * 0.083f;

			MaxAcceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;
			Acceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;

			MaxDeceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;
			Deceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;

			//toggle flying in level editor mode
			//camfollow.GetComponent<CameraFollow>().toggleflying();

		}

		//Creates a slider for each player
		if (photonView.isMine && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1"))
		{
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				GameObject slider = Instantiate (camfollow.GetComponent<CameraFollow>().PlayerSlider,camfollow.GetComponent<CameraFollow>().UICanvas.transform);
				slider.name = "Slider"+player.NickName;
				slider.GetComponent<playerSlider> ().PlayerName.GetComponent<Text> ().text = player.NickName;
			}
		}
		//This sets a routine check to set walking animations if left or right button are pressed
		//StartCoroutine (animate (0.05f, sprites.Lwalkright, 0, gameObject));

		//This sets a routine check to set crouching animations if left or right button are pressed
		//StartCoroutine (animate (0.05f, sprites.Lcrouchleft, 0, gameObject));
		//StartCoroutine (animate (0.05f, sprites.Lcrouchright, 0, gameObject));
	}



	void FixedUpdate()
	{
		//Get crushed state
		Collider2D[] crushleftcolliders = Physics2D.OverlapCircleAll (leftcrushcheck.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsPlatform);
		Collider2D[] crushrightcolliders = Physics2D.OverlapCircleAll (rightcrushcheck.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsPlatform);
		Collider2D[] crushtopcolliders = Physics2D.OverlapCircleAll (topcrushcheck.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsPlatform);
		Collider2D[] crushbottomcolliders = Physics2D.OverlapCircleAll (bottomcrushcheck.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsPlatform);

		if (camfollow.GetComponent<CameraFollow>().endgame == 0) {
			if (crushleftcolliders.Length >= 1 && crushrightcolliders.Length >= 1 && crushtopcolliders.Length >= 1 && crushbottomcolliders.Length >= 1) {
				crushed = true;
				Instantiate (HurtEffect, transform.position, transform.rotation);
				GetComponent<Character> ().Freeze ();
				transform.position = checkpointpos;
			} else {
			crushed = false;
				if (CountingDownEnded && !stunned) {
					GetComponent<Character> ().UnFreeze ();
				}
			}
		}

		Collider2D[] laddercolliders = Physics2D.OverlapCircleAll (topcrushcheck.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsGround);
		bool tempOnLadder = false;
		foreach (Collider2D collider in laddercolliders) {
			GameObject obj = collider.gameObject;
			if (obj.tag == "ladder") {
				tempOnLadder = true;
			}
		}

		Collider2D[] watercolliders = Physics2D.OverlapCircleAll (topcrushcheck.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsGround);
		bool tempOnWater = false;
		foreach (Collider2D collider in watercolliders) {
			GameObject obj = collider.gameObject;
			if (obj.tag == "water") {
				obj.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f,0.6f);
				tempOnWater = true;
			}
		}

		OnLadder = tempOnLadder;
		OnWater = tempOnWater;
		if (OnLadder) {
			GetComponent<CharacterJump> ().enabled = false;
		} else if (OnWater) {
			
			CurrentGravity = -1f;
			GetComponent<CharacterJump> ().enabled = false;

		}
		else {
			CurrentGravity = -30f;
			GetComponent<CharacterJump> ().enabled = true;
		}

	}



	// Update is called once per frame
	void Update()
	{
		//Player follows camera instead if panning
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor") && camfollow.GetComponent<CameraFollow>().editplay == 0 && camfollow.GetComponent<CameraFollow>().panning) {
			transform.position = camfollow.transform.position - new Vector3 (0, 0, -20.0f);
		}
		//Set game mode
		if (camfollow.GetComponent<CameraFollow> ().GameMode != "Deathmatch") {
			GetComponent<Health> ().enabled = false;
			GetComponent<HealthBar> ().enabled = false;
			transform.GetChild (0).transform.gameObject.SetActive (false);
		}
		else if (camfollow.GetComponent<CameraFollow> ().GameMode == "Deathmatch") {
			GetComponent<Health> ().enabled = true;
			GetComponent<HealthBar> ().enabled = true;
			transform.GetChild (0).transform.gameObject.SetActive (true);
		}
		CorgiController corgicontroller = GetComponent<CorgiController> ();
		//Updates player position
		//camfollow.GetComponent<CameraFollow>().pslider.GetComponent<Slider>().value=PlayerPosition;
		//GetComponentInChildren<PhotonAnimatorView> ().enabled = true;
		if (corgicontroller.IsCollingLeft()) {
			Collider2D[] leftcolliders = Physics2D.OverlapCircleAll (new Vector2 (leftcheck.transform.position.x, leftcheck.transform.position.y), 0.0001f);
			foreach (Collider2D hit in leftcolliders) {
				if (hit.GetComponent<SnapToGrid> () != null) {
					hit.GetComponent<SnapToGrid> ().colside = "right";
					hit.GetComponent<SnapToGrid> ().PlayerCollide (gameObject);
				}
				if (hit.GetComponent<HurtPlayer> () != null) {
					hit.GetComponent<HurtPlayer> ().PlayerCollide (gameObject);
				}
			}
		}
		if (corgicontroller.IsCollingRight()) {
			Collider2D[] rightcolliders = Physics2D.OverlapCircleAll (new Vector2 (rightcheck.transform.position.x, rightcheck.transform.position.y), 0.0001f);
			foreach (Collider2D hit in rightcolliders) {
				if (hit.GetComponent<SnapToGrid> () != null) {
					hit.GetComponent<SnapToGrid> ().colside = "left";
					hit.GetComponent<SnapToGrid> ().PlayerCollide (gameObject);
				}
				if (hit.GetComponent<HurtPlayer> () != null) {
					hit.GetComponent<HurtPlayer> ().PlayerCollide (gameObject);
				}
			}
		}

		Collider2D[] topcolliders = Physics2D.OverlapCircleAll (new Vector2 (topcheck.transform.position.x, topcheck.transform.position.y), 0.0001f);
		foreach (Collider2D hit in topcolliders) {
			if (GetComponent<CharacterCrouch> ().IsCrouchingOrCrawling ()) {
				//IF player if crouching, only collide with block above if player is pressing up
				if (GetComponent<CharacterHorizontalMovement> ().IsHoldingUp () == 1) {
					if (hit.GetComponent<SnapToGrid> () != null) {
						hit.GetComponent<SnapToGrid> ().colside = "bottom";
						hit.GetComponent<SnapToGrid> ().PlayerCollide (gameObject);

						if (hit.GetComponent<SnapToGrid> ().IsSlightlyUp == false) {
							//hit.GetComponent<SnapToGrid> ().IsSlightlyUp = true;
							//hit.GetComponent<SnapToGrid> ().MoveSlightlyUp();
							BlockAbove = hit.gameObject;
						}
					}
					if (hit.GetComponent<HurtPlayer> () != null) {
						hit.GetComponent<HurtPlayer> ().PlayerCollide (gameObject);
					}
				}
			} else {
				//Else, just collide with block on top
				if (hit.GetComponent<SnapToGrid> () != null) {
					hit.GetComponent<SnapToGrid> ().colside = "bottom";
					hit.GetComponent<SnapToGrid> ().PlayerCollide (gameObject);
				}
				if (hit.GetComponent<HurtPlayer> () != null) {
					hit.GetComponent<HurtPlayer> ().PlayerCollide (gameObject);
				}
			}
		}


				
		
		if (corgicontroller.IsCollingBottom()) {
			Collider2D[] bottomcolliders = Physics2D.OverlapCircleAll (new Vector2 (bottomcheck.transform.position.x, bottomcheck.transform.position.y), 0.3f);
			foreach (Collider2D hit in bottomcolliders) {
				if (hit.GetComponent<SnapToGrid> () != null) {
					hit.GetComponent<SnapToGrid> ().colside = "top";
					hit.GetComponent<SnapToGrid> ().PlayerCollide (gameObject);

					if (hit.GetComponent<SnapToGrid> ().direction == "_mr" || hit.GetComponent<SnapToGrid> ().direction == "_ml") {
						if (!hit.GetComponent<SnapToGrid> ().blocktag.Contains ("arrow")) {
							if (hit.GetComponent<SnapToGrid> ().MovingRight) {
								rb.velocity = new Vector2 (hit.GetComponent<Rigidbody2D> ().velocity.x, rb.velocity.y);
							} else {
								rb.velocity = new Vector2 (-hit.GetComponent<Rigidbody2D> ().velocity.x, rb.velocity.y);
							}
						}
					}
				}
				if (hit.GetComponent<HurtPlayer> () != null) {
					hit.GetComponent<HurtPlayer> ().PlayerCollide (gameObject);
				}
			}
		}

		if (BlockAbove != null) {
			if (GetComponent<CharacterHorizontalMovement> ().IsHoldingUp () != 1) {
				if (BlockAbove.GetComponent<SnapToGrid> ().IsSlightlyUp == true) {
					//BlockAbove.GetComponent<SnapToGrid> ().IsSlightlyUp = false;
					//BlockAbove.GetComponent<SnapToGrid> ().MoveSlightlyDown();
					BlockAbove = null;
				}
			}
		}
		if (camfollow == null) {
			return;
		}

		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			if (camfollow.GetComponent<CameraFollow> ().editplay == 0) {
				if (!photonView.isMine) {
					//Get rid of the bug where other players glitch out while inside blocks during editing
					corgicontroller.enabled = false;
				}
			} else {
				corgicontroller.enabled = true;
			}
		}

		//Set item of players
		CurrentItem = photonView.owner.CustomProperties ["Item"].ToString ();
		GetComponentInChildren<PlayerItem> ().CallChangeItem (photonView.owner.CustomProperties ["Item"].ToString (),GetComponent<Controls>());

		if (CurrentItem.Contains ("jetpack")) {
			if (GetComponent<CharacterJetpack> ().emissionModule.enabled) {
				transform.GetChild (2).transform.position = new Vector3 (transform.position.x, transform.position.y+0.7f, transform.position.z);
			} else {
				transform.GetChild (2).transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			}
		} else {
			if (GetComponent<Character> ().IsFacingRight) {
				gameObject.transform.GetChild (2).gameObject.transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (1f, 0f);
			} else {
				gameObject.transform.GetChild (2).gameObject.transform.position = new Vector2 (transform.position.x, transform.position.y) - new Vector2 (1f, 0f);
			}
		}

		if (photonView.owner.CustomProperties ["Won"] != null) {
			if (photonView.owner.CustomProperties ["Won"].ToString () == "true") {
				//NameText.gameObject.GetComponent<Text>().enabled=false;
				//ModelContainer.SetActive (false);
			}
		}

		if (photonView.owner.CustomProperties ["Quitted"] != null) {
			if (photonView.owner.CustomProperties ["Quitted"].ToString () == "true") {
				//NameText.gameObject.GetComponent<Text>().enabled=false;
				//ModelContainer.SetActive (false);
			}
		}

		//Quit if not current player in multiplayer game
		if (!photonView.isMine )
		{
			return;
		}

		if (corgicontroller.Speed.y != 0) {
			if (!CanReduceLastFallingSpeed) {
				if (corgicontroller.Speed.y < LastFallingSpeed && corgicontroller.Speed.y < 0f) {
					LastFallingSpeed = corgicontroller.Speed.y;
				}
			} else {
				if (corgicontroller.Speed.y < 0f) {
					CanReduceLastFallingSpeed = false;
					LastFallingSpeed = corgicontroller.Speed.y;
				}
			}
		}
		//ONly activate blocks if they are nearby
		//Spawn b1 
		Vector2 playergridpos2 = GetGrid(new Vector2(transform.position.x,transform.position.y), true);
		float playerx = playergridpos2.x;
		float playery = playergridpos2.y;
		int px2 = Mathf.RoundToInt (playerx);
		int py2 = Mathf.RoundToInt (playery);
		//for (int x = px2 - 14; x < px2 + 15; x++) {
			//for (int y = py2 - 9; y < py2 + 9; y++) {
		int camsize=camfollow.GetComponent<CameraFollow>().camsizeInt;
		for (int x = px2 - camsize*2; x < px2 + camsize*2; x++) {
			for (int y = py2 - (int)(camsize*1.4); y < py2 + camsize*1; y++) {
				string playercord = x + "," + y;
				if (camfollow.GetComponent<CameraFollow> ().blockmapd.ContainsKey (playercord)) {

					bool canactivate = true;
					if (Camera.main.GetComponent<CameraFollow> ().blockmapd [playercord].gameObject != null) {
						GameObject ThisBlock = Camera.main.GetComponent<CameraFollow> ().blockmapd [playercord].gameObject;
						if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
							if (camfollow.GetComponent<CameraFollow> ().editplay != 0) {
								if (camfollow.GetComponent<CameraFollow> ().disabledblocks.Contains (ThisBlock)) {
									canactivate = false;
								}
							}
						}
						if (canactivate) {
							ThisBlock.SetActive (true);
							if (ThisBlock.GetComponent<SpriteRenderer> ().enabled == false) {
								if (ThisBlock.tag != "stop") {
									ThisBlock.GetComponent<SpriteRenderer> ().enabled = true;
									if (ThisBlock.GetComponent<Rigidbody2D> () != null) {
										ThisBlock.GetComponent<Rigidbody2D> ().WakeUp ();
									}
									ThisBlock.GetComponent<SnapToGrid> ().enabled = true;
								}
							}
						}
					} else {
						Debug.LogError ("Xue: Accessing destroyed object");
					}
				}
			}
		}


		//remove weapon
		if (camfollow.UsedUpWeapon) {
			camfollow.UsedUpWeapon = false;
			ChangeItem ("none");
			GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
		}
		bool canstart = true;


		foreach (PhotonPlayer player in PhotonNetwork.playerList) {
			//Makes sure everyone starts game at same time
			if (player.CustomProperties["Loaded"].ToString() != "true") {
				canstart = false;
			}

		}

		if (canstart) {
			if (GameStarted==false) {
				if (countingdown == false && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) { 
					countingdown = true;
					GameStarted = true;
					controller.source.PlayOneShot (controller.a_countdown);
					rb.gravityScale = 1.5f;
					StartCoroutine (CountdownTimer (0f, "30"));
				} else if (countingdown == false && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
					countingdown = true;
					if (!stunned) {
						GetComponent<Character> ().UnFreeze ();
					}
					camfollow.GetComponent<CameraFollow>().unpausegame ();
					camfollow.GetComponent<CameraFollow>().caneditblock ();
					GameStarted = true;
					rb.gravityScale = 1.5f;

					camfollow.GetComponent<CameraFollow>().GameTime.SetActive (true);
				}
			}
		}
		//Reset character rotation when going back to edit mode
		if (camfollow.GetComponent<CameraFollow>().editplay == 0 && (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor"))) {
			//NameText.gameObject.GetComponent<Text>().enabled=false;
			//ModelContainer.SetActive (false);
			//transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<Rigidbody2D>().Sleep();
			camfollow.GetComponent<CameraFollow> ().FreezeEffect.SetActive (false);


			CurrentGravity = 0f;

			this.transform.localScale = new Vector3 (0.43f, 0.5f, 0.45f);
			GetComponent<CharacterHorizontalMovement> ().MovementSpeed = 8.0f;
			GetComponent<CharacterJump> ().JumpHeight = 3.025f;
			rb.velocity = Vector2.zero;
			if (camfollow.GetComponent<CameraFollow> ().GameMode == "Deathmatch") {
				GetComponent<Health> ().ResetHealthToMaxHealth ();
			}

			if (GetComponent<CharacterGravity> ().GravityAngle != 0f) {
				GetComponent<CharacterGravity> ().SetGravityAngle (0f);
				protation = 0f;

				camfollow.GetComponent<CameraFollow> ().unpausegame ();
				GetComponent<Character> ().UnFreeze ();
				GetComponent<CharacterPause> ().UnPauseCharacter ();

		
			}
		}
		//Start character at start position when playing in level editor
		if (camfollow.GetComponent<CameraFollow>().editplay == 1) {
			camfollow.GetComponent<CameraFollow>().editplay = 2;
			GetComponent<Rigidbody2D>().WakeUp();
			//NameText.gameObject.GetComponent<Text>().enabled=true;
			//ModelContainer.SetActive (true);
			//transform.GetChild (2).gameObject.GetComponent<SpriteRenderer> ().enabled = true;

			CurrentGravity = -30f;

			camfollow.GetComponent<Camera> ().orthographicSize = 7.0f;
			camfollow.GetComponent<CameraFollow> ().camsizeInt = 7;
			camfollow.GetComponent<CameraFollow> ().ParallaxParent.GetComponent<Transform> ().localScale = new Vector3 (204.6663f,26.56232f,0f);

			GameObject startblock = GameObject.Find ("start");
			if (startblock != null) {
				startpos = startblock.transform.position;
				transform.position = startblock.transform.position;
				safespot = startblock.transform.position;
				checkpointpos = startblock.transform.position;
			} else {
				startpos=new Vector3(15f,15f,0f);
				transform.position = startpos;
			}
			camfollow.GetComponent<CameraFollow>().startpos = startpos;
			transform.position = startpos;
		}


		//Controls items
		if (CurrentItem == "jetpack" || CurrentItem == "jetpackused") {
			GetComponent<CharacterJetpack> ().AbilityPermitted = true;
		} else {
			GetComponent<CharacterJetpack> ().AbilityPermitted = false;
		}

		//Allows stats customisation in leveleditor
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			initialJumpForce = 53.3f+camfollow.GetComponent<CameraFollow>().SJump * 5.33f;
			MaxSpeed = 0.83f+camfollow.GetComponent<CameraFollow>().SSpeed * 0.083f;

			MaxAcceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;
			Acceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;

			MaxDeceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;
			Deceleration = 3.33f+camfollow.GetComponent<CameraFollow>().SAcc * 0.333f;
		}

		//Increase number of player won
		if (camfollow.GetComponent<CameraFollow>().endgame==1) {
			camfollow.GetComponent<CameraFollow>().endgame = 3;

			m_playerCustomProperties ["Won"] = "true";
			PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);



		}
		else if (camfollow.GetComponent<CameraFollow>().endgame==2) {
			camfollow.GetComponent<CameraFollow>().endgame = 4;

			m_playerCustomProperties ["Quitted"] = "true";
			PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
		}

		var pmaplist = GameObject.FindGameObjectsWithTag("pmap");
		foreach (GameObject pmap in pmaplist) {
			Destroy (pmap);

		}
		var players = GameObject.FindGameObjectsWithTag("Player");

		//Loop through playerlist to find out how many of them has already won
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			playerwon = 0;
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				if (player.CustomProperties ["Won"].ToString () == "true") {
					playerwon += 1;
				}
			}
		}
		//End level if last player alive in deathmatch
		if (camfollow.GetComponent<CameraFollow>().GameMode == "Deathmatch" && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			if (PhotonNetwork.room.PlayerCount - playerwon<=1 && camfollow.GetComponent<CameraFollow>().endgame==0 && PhotonNetwork.room.PlayerCount!=1) {
				camfollow.GetComponent<CameraFollow>().PauseGame = true;
				camfollow.GetComponent<CameraFollow>().endgame = 3;
				CmdChangeSprite("Wonsprite");
				playerwon += 1;
			}
		}
		//Shows players won
		if (camfollow.GetComponent<CameraFollow>().endgame == 3) {
			camfollow.GetComponent<CameraFollow>().endgame = 4;
			print ("You have won!!!");
			var fp = camfollow.GetComponent<CameraFollow>().FinishPanel;


			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				return;
			}
			fp.SetActive (true);
			float finalscore=0f;;
			if (camfollow.GetComponent<CameraFollow>().GameMode == "Deathmatch") {
				//Score Text
				finalscore = Mathf.Pow ((float)2, (float)(PhotonNetwork.room.PlayerCount)) * Mathf.Pow ((float)0.5, (float)(PhotonNetwork.room.PlayerCount-playerwon+1)) * score * (1f+ExpBonus/100f) * 0.1f;
				fp.transform.GetChild (1).GetComponentInChildren<Text> ().text = finalscore.ToString ();
				//Rank Text
				fp.transform.GetChild (3).GetComponentInChildren<Text> ().text = (PhotonNetwork.room.PlayerCount-playerwon+1).ToString ();
			} else if (camfollow.GetComponent<CameraFollow>().GameMode == "Race") {
				//Score Text
				print("original"+Mathf.Pow ((float)2, (float)(PhotonNetwork.room.PlayerCount)) * Mathf.Pow ((float)0.5, (float)(playerwon )) * score);
				print("bonus"+Mathf.Pow ((float)2, (float)(PhotonNetwork.room.PlayerCount)) * Mathf.Pow ((float)0.5, (float)(playerwon )) * score * (1f+ExpBonus/100f));
				finalscore = Mathf.Pow ((float)2, (float)(PhotonNetwork.room.PlayerCount)) * Mathf.Pow ((float)0.5, (float)(playerwon )) * score * (1f+ExpBonus/100f) * 0.1f;
				fp.transform.GetChild (1).GetComponentInChildren<Text> ().text = finalscore.ToString ();
				//Rank Text
				fp.transform.GetChild (3).GetComponentInChildren<Text> ().text = (playerwon).ToString ();
			}


			//Time Text
			GameObject GameTime = GameObject.FindGameObjectWithTag ("GameTime");
			fp.transform.GetChild (2).GetComponentInChildren<Text> ().text = camfollow.GetComponent<CameraFollow>().GameTime.GetComponent<Text> ().text;



			CalculateRank (finalscore,rank,exp);


		}

		//Sets player names of each client
		if (PhotonNetwork.room.PlayerCount >= 1)
		{
			foreach (GameObject player in players)
			{
				if (1==1)
				{
					if (player.GetComponentInChildren<Text> () == null) {
						return;
					}
					var playerspr=player.GetComponent<SpriteRenderer>();
					var playerco = player.GetComponent<Controls> ();



					//Hide healthbar in race
					if (camfollow.GetComponent<CameraFollow>().GameMode != "Deathmatch") {
						//player.transform.GetChild (0).gameObject.GetComponent<Canvas> ().enabled = false;
					} else {
						if (playerco.newsprite != "Wonsprite") {
							//player.transform.GetChild (0).gameObject.GetComponent<Canvas> ().enabled = true;
						}
					}
					//Hides player that won or lost
					if (playerco.newsprite == "Wonsprite") {
						player.transform.GetChild (0).gameObject.GetComponent<Canvas> ().enabled = false;
						player.transform.GetChild (1).gameObject.GetComponent<Canvas> ().enabled = false;
					} else {
						player.transform.GetChild (1).gameObject.GetComponent<Canvas> ().enabled = true;
					}
					//Draws player position sliders
					var cam = camfollow.GetComponent<CameraFollow>();
					Vector2 playergridpos = GetGrid(new Vector2(playerco.transform.position.x,playerco.transform.position.y), true);
					float px = playergridpos.x;
					float py = playergridpos.y;
					float mapwidth = (float)(mapxmax - mapxmin);
					float mapheight = (float)(mapymax - mapymin);

					float pxratio = Mathf.Clamp01 ((px - mapxmin)/mapwidth);
					float pyratio = Mathf.Clamp01 ((py - mapymin)/mapheight);

					PlayerPosition =pxratio;


					var sliders=GameObject.FindGameObjectsWithTag("Slider");
					foreach (GameObject slider in sliders) {
						if (slider.name == "Slider" + player.name) {
							float originalvalue = slider.GetComponent<Slider> ().value;
							slider.GetComponent<Slider> ().value = Mathf.Lerp(originalvalue, PlayerPosition,0.02f);
							slider.GetComponent<playerSlider>().position=PlayerPosition;

							SliderList.Add (PlayerPosition);
						}
					}
				}
			}
		}




		//Scrolls in and out using mouse wheel button
		float fov = Camera.main.fieldOfView;
		fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;

		bool moveblocknearby=false;


		if (rb != null)
		{
			if (GameStarted)
			{
				//rb.gravityScale = 1.5f;
			}
		}





		//rb.AddForce(300.0f * Vector2.left);
		if (photonView.isMine)
		{
			if (LocalCamera != null) {
				pos = LocalCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 20.0f));
			}
		}

		int max;
		if (mapxmax > mapymax)
		{
			max = mapxmax;
		}
		else
		{
			max = mapymax;
		}


		if (!photonView.isMine)
		{
			return;
		}


		//Teleport to safe spot
		if (caster(transform.position, 30) == false && !UsingSuperJump)
		{
			//Only teleport to safe spot when not using super jump
			if (camfollow.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			} else {
				GetComponent<Character> ().Freeze ();
				gameObject.transform.position = safespot;
				rb.velocity = Vector3.zero;
				AtSafeSpot = true;
			}

		}

		if (AtSafeSpot && CountingDownEnded && !stunned) {
			AtSafeSpot = false;
			GetComponent<Character> ().UnFreeze ();
		}

		if (1==1) {
			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				{
					if (camfollow.GetComponent<CameraFollow> ().panning) {
						
					} else {
						//Place selected block
						if (SelectingBlock) {
							if (Input.GetMouseButtonUp (0)) {
								SelectingBlock = false;
								photonView.RPC ("SendCursorState", PhotonTargets.OthersBuffered, 2,name,"",camfollow.GetComponent<CameraFollow> ().cursor.transform.position);
								camfollow.GetComponent<CameraFollow> ().cursor.GetComponent<SpriteRenderer> ().sprite = camfollow.GetComponent<CameraFollow> ().CursorSelectedReleased;

								//Handles customisation of sign blocks
								Collider2D[] mousecolliders = Physics2D.OverlapCircleAll (camfollow.GetComponent<CameraFollow>().cursor.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsSign);
								if (mousecolliders.Length >= 1) {
									camfollow.GetComponent<CameraFollow> ().signobj = mousecolliders [0].gameObject;
									camfollow.GetComponent<CameraFollow> ().signpos = GetGrid (camfollow.GetComponent<CameraFollow> ().signobj.transform.position, true);
									 
									//Only set sign panel active if user is not moving the sign block
									if (tempsignpos == camfollow.GetComponent<CameraFollow> ().signpos) {
										camfollow.GetComponent<CameraFollow> ().editallowed = false;
										camfollow.GetComponent<CameraFollow> ().SignPanel.SetActive (true);
										camfollow.GetComponent<CameraFollow> ().SetCannotMoveInEdit ();
									}
								}
							}
						} else {
							//Add block for first touch
							bool delete = camfollow.GetComponent<CameraFollow> ().deletingblocks;
							if ((!Application.isMobilePlatform && Input.GetMouseButton (0)) || (Application.isMobilePlatform && Input.GetMouseButton (0) && !delete)) {
								if (camfollow.GetComponent<CameraFollow> ().editallowed == true && camfollow.GetComponent<CameraFollow> ().editplay == 0 && camfollow.GetComponent<CameraFollow> ().canedit == true) {
									if (!IsPointerOverUIObject ()) {
										if (Application.isMobilePlatform && Input.touchCount > 1) {
										} else {
											MouseLeft ();

											//Clear undo history if an action is performed(To prevent redoing of previous undo)
											camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Clear ();
											camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Clear ();
											camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Clear ();

											//Handles customisation of sign blocks
											Collider2D[] mousecolliders = Physics2D.OverlapCircleAll (camfollow.GetComponent<CameraFollow>().cursor.transform.position, 0.001f, camfollow.GetComponent<CameraFollow>().whatIsSign);
											if (mousecolliders.Length >= 1) {
												//Record temp sign pos
												tempsignpos = GetGrid (mousecolliders [0].gameObject.transform.position, true);
											}
										}
									}
								}
							}
							//Remove block for first touch
							if ((!Application.isMobilePlatform && Input.GetMouseButton (1)) || (Application.isMobilePlatform && Input.GetMouseButton (0) && delete)) {
								if (camfollow.GetComponent<CameraFollow> ().editplay == 0 && camfollow.GetComponent<CameraFollow> ().canedit == true) {
									if (!IsPointerOverUIObject ()) {
										if (Application.isMobilePlatform && Input.touchCount > 1) {
										} else {
											//Manages selected block to place
											Vector3 pos = LocalCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 20.0f));

											//Convert to grid before checking if block exists in the gridspace
											Vector2 gridpos = GetGrid (pos, true);
											int gridx = (int)gridpos.x;
											int gridy = (int)gridpos.y;
											string gridposString = gridx.ToString ().Substring (0, gridx.ToString ().Length) + "," + gridy.ToString ().Substring (0, gridy.ToString ().Length);

											camfollow.GetComponent<CameraFollow> ().ChatObj.GetComponent<BlockSpawner> ().RemoveBlock (gridposString);

											//Send to all
											if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
												photonView.RPC ("RemoveForAll", PhotonTargets.OthersBuffered, gridposString);

											}
											//Clear undo history if an action is performed(To prevent redoing of previous undo)
											camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Clear ();
											camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Clear ();
											camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Clear ();

											//camfollow.GetComponent<CameraFollow>().ChatObj.GetComponent<UNETChat> ().SendRemoveBlockMessage ("%" + gridposString);
										}
									}
								}
							}
						}
					}
				}
			}
		}


		//Using items
		if (Input.GetKeyDown(KeyCode.Space)||(useitem) || PressSpace)
		{
			//PressSpace = true;
			//photonView.RPC ("ServerUseItem", PhotonTargets.Others,1);
			if (stunned == false && camfollow.GetComponent<CameraFollow>().PauseGame == false && GameStarted) {
				//laser
				if (CurrentItem == "laser" || CurrentItem == "laserused") {

					if (canshoot) {
						if (CurrentItem == "laser") {
							camfollow.uses = 5;
							ChangeItem ("laserused");
						}
						if (camfollow.uses <= 0) {
							ChangeItem ("none");
							//GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
						}

						camfollow.uses -= 1;
						CmdFire (facingright);
						canshoot = false;
					}

				} else {

					if (canshoot) {
						if (CurrentItem == "grenadelauncher") {
							controller.PlayOnce (controller.a_grenadelauncher);


							if (GetComponent<Character>().IsFacingRight) {
								corgicontroller.AddHorizontalForce (-20f);
							} else {
								corgicontroller.AddHorizontalForce (20f);
							}
						}
						else if (CurrentItem == "rocketlauncher") {
							controller.PlayOnce (controller.a_rocketlauncher);


							if (GetComponent<Character>().IsFacingRight) {
								corgicontroller.AddHorizontalForce (-20f);
							} else {
								corgicontroller.AddHorizontalForce (20f);
							}
						}
						else if (CurrentItem == "shotgun") {
							controller.PlayOnce (controller.a_shotgun);


							if (GetComponent<Character>().IsFacingRight) {
								corgicontroller.AddHorizontalForce (-20f);
							} else {
								corgicontroller.AddHorizontalForce (20f);
							}
						}
						else if (CurrentItem == "machinegun") {
							controller.PlayOnce (controller.a_gun);


							if (GetComponent<Character>().IsFacingRight) {
								corgicontroller.AddHorizontalForce (-20f);
							} else {
								corgicontroller.AddHorizontalForce (20f);
							}
						}

						else if (CurrentItem == "meleeattack") {
							controller.PlayOnce (controller.a_sword);


							if (GetComponent<Character>().IsFacingRight) {
								corgicontroller.AddHorizontalForce (20f);
							} else {
								corgicontroller.AddHorizontalForce (-20f);
							}
						}

						canshoot = false;
					}
				}
				if (CurrentItem == "teleport") {
					Vector3 teleportpos = Vector3.zero;
					if (facingright) {
						teleportpos = gameObject.transform.GetChild (6).gameObject.transform.position;
					} else {
						teleportpos = gameObject.transform.GetChild (7).gameObject.transform.position;
					}
					Collider2D[] hitColliders4 = Physics2D.OverlapCircleAll (teleportpos, 0.0001f);
					if (hitColliders4.Length <= 1) {
						ChangeItem ("none");
						transform.position = teleportpos;
						GetComponent<CharacterCrouch> ().ProcessAbility ();
						Instantiate(CoinBurstEffect,transform.position,transform.rotation);
					}

				} else if (CurrentItem == "speedburst") {
					controller.PlayOnce (controller.a_speedburst);
					ChangeItem ("none");
					PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
					GetComponent<CharacterHorizontalMovement> ().MovementSpeed = 20f;


					photonView.RPC ("MasterSpawn", PhotonTargets.MasterClient, facingright,PhotonNetwork.player.NickName+ ";RocketFlamesEffect1",PhotonNetwork.player.NickName);
			
				} else if (CurrentItem == "superjump") {
					controller.PlayOnce (controller.a_superjump);
					ChangeItem ("none");
					PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);


					photonView.RPC ("MasterSpawn", PhotonTargets.MasterClient, facingright,PhotonNetwork.player.NickName+ ";RocketFlamesEffect2",PhotonNetwork.player.NickName);
					StartCoroutine(SuperJumpTimer(0.01f,0,20));
				}

				//sword
				if (CurrentItem == "sword" || CurrentItem == "swordused") {

					if (canshoot) {
						if (CurrentItem == "sword") {
							camfollow.uses = 5;
							ChangeItem ("swordused");
						}
						//CmdSword (facingright);
						if (camfollow.uses <= 0) {
							ChangeItem ("none");
							//GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
						}
						camfollow.uses -= 1;
						immune = true;
						canshoot = false;
					}
				} else if (CurrentItem == "bomb") {
					controller.PlayOnce (controller.a_timebomb);
					ChangeItem ("none");
					PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
					photonView.RPC ("MasterSpawn", PhotonTargets.MasterClient, facingright,"bombitem",PhotonNetwork.player.NickName);
				} else if (CurrentItem == "blockitem") {
					ChangeItem ("none");
					PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
					photonView.RPC ("MasterSpawn", PhotonTargets.MasterClient, facingright,"blockitem",PhotonNetwork.player.NickName);
				} else if (CurrentItem == "lightning") {
					ChangeItem ("none");
					PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

					photonView.RPC ("MasterSpawn", PhotonTargets.MasterClient, facingright,"lightning",PhotonNetwork.player.NickName);

				}

			}
		}

		//Chat
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1") || (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor") && PhotonNetwork.room.PlayerCount>1)) {
			if (camfollow.GetComponent<CameraFollow> ().ChatCanvas.GetComponent<Canvas> ().enabled == false) {
				camfollow.GetComponent<CameraFollow> ().ChatCanvas.GetComponent<Canvas> ().enabled = true;
			}

			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
				if (camfollow.GetComponent<CameraFollow> ().ChatBottom.activeSelf == true) {
					//Deactivate chat panel if not sending message
					if (camfollow.GetComponent<CameraFollow> ().ChatPanel.GetComponentInChildren<InputField> ().text == "") {
						camfollow.GetComponent<CameraFollow> ().ChatBottom.SetActive (false);
						camfollow.GetComponent<CameraFollow> ().ChatBackground.SetActive (false);
						//camfollow.GetComponent<CameraFollow>().ChatCanvas.GetComponent<GraphicRaycaster> ().enabled = false;
					} else {
						//Send message instead of deactivating chat panel
						photonView.RPC ("SendChatMessage", PhotonTargets.OthersBuffered, user + "(" + rank + "): ", camfollow.GetComponent<CameraFollow> ().ChatPanel.GetComponentInChildren<InputField> ().text + "\n");

						string chatuser = user + "(" + rank + "): ";
						string chatmessage = camfollow.GetComponent<CameraFollow> ().ChatPanel.GetComponentInChildren<InputField> ().text + "\n";
						camfollow.GetComponent<CameraFollow> ().SelectedChannelText.GetComponent<Text> ().text += "<color=#FFA53F>" + chatuser + "</color>" + "<color=#FFD8AF>" + chatmessage + "</color>";

						camfollow.GetComponent<CameraFollow> ().ChatPanel.GetComponentInChildren<InputField> ().text = "";
						camfollow.GetComponent<CameraFollow> ().ChatPanel.GetComponentInChildren<InputField> ().ActivateInputField ();
						camfollow.GetComponent<CameraFollow> ().ChatScrollBar.GetComponent<Scrollbar> ().value = 0;
					}
				} else {
					//Activate chat panel
					camfollow.GetComponent<CameraFollow> ().ChatBottom.SetActive (true);
					camfollow.GetComponent<CameraFollow> ().ChatBackground.SetActive (true);
					//camfollow.GetComponent<CameraFollow>().ChatCanvas.GetComponent<GraphicRaycaster> ().enabled = true;
					camfollow.GetComponent<CameraFollow> ().ChatPanel.GetComponentInChildren<InputField> ().ActivateInputField ();
				}


			}
		} else {
			if (camfollow.GetComponent<CameraFollow> ().ChatCanvas.GetComponent<Canvas> ().enabled == true) {
				camfollow.GetComponent<CameraFollow> ().ChatCanvas.GetComponent<Canvas> ().enabled = false;
			}
			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
				//
			}
		}


		if (Input.GetKeyUp(KeyCode.Space) || (useitem==false))
		{
			canshoot = true;

			//HoldingSpace = false;
			//PressSpace = false;
			//photonView.RPC ("ServerUseItem", PhotonTargets.Others,0);
		}
		if (GetComponent<CharacterHorizontalMovement>().IsHoldingUp()==2) {
			//Press down to drop faster
			if (camfollow.GetComponent<CameraFollow>().PauseGame == false && GameStarted) {
				if (!OnWater) {
					GetComponent<CorgiController> ().AddVerticalForce (-0.1f);
				} else {
					GetComponent<CorgiController> ().AddVerticalForce (-0.1f);
				}

			}
		}
		if (GetComponent<CharacterHorizontalMovement>().IsHoldingUp()==1) {
			//Press down to drop faster
			if (camfollow.GetComponent<CameraFollow>().PauseGame == false && GameStarted) {
				if (!OnWater) {
					//
				} else {
					GetComponent<CorgiController> ().AddVerticalForce (0.1f);
				}

			}
		}


		//View the level
		if (camfollow.GetComponent<CameraFollow>().endgame != 0 && GameStarted && camfollow.GetComponent<CameraFollow> ().CanMoveInEdit) {
			if (!camfollow.GetComponent<CameraFollow> ().panning) {
				GetComponent<CorgiController> ().GravityActive (false);
				GetComponent<CorgiController> ().DisableCollisionsNoTimer ();
				GetComponent<CharacterHorizontalMovement> ().AbilityPermitted = false;
				if (!Application.isMobilePlatform) {
					if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
						GetComponent<CorgiController> ().AddHorizontalForce (-2f);
					} else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
						GetComponent<CorgiController> ().AddHorizontalForce (2f);
					} else {
						GetComponent<CorgiController> ().StopHorizontalMovement ();
					}

					if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
						GetComponent<CorgiController> ().AddVerticalForce (2f);
					} else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
						GetComponent<CorgiController> ().AddVerticalForce (-2f);
					} else {
						GetComponent<CorgiController> ().StopVerticalMovement ();
					}

				}
			}

		} else {
			GetComponent<CorgiController> ().GravityActive (true);
			GetComponent<CorgiController> ().EnableeCollisionsNoTimer ();
			GetComponent<CharacterHorizontalMovement> ().AbilityPermitted = true;
		}


		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) || moveleft) {
			if (facingright) {
				if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				} else {
					photonView.RPC ("FlipSprite", PhotonTargets.Others, false);
				}
			}
			facingright = false;
			if (CurrentItem.Contains ("jetpack")) {
				if (GetComponent<CharacterJetpack> ().emissionModule.enabled) {
					transform.GetChild (2).transform.position = new Vector3 (transform.position.x, transform.position.y + 0.7f, transform.position.z);
				} else {
					transform.GetChild (2).transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				}
			} else {
				transform.GetChild (2).transform.position = new Vector3 (transform.position.x - 1f, transform.position.y, transform.position.z);
			}
		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) || moveright) {
			if (!facingright) {
				if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				} else {
					photonView.RPC ("FlipSprite", PhotonTargets.Others, true);
				}
			}
			facingright = true;
			if (CurrentItem.Contains ("jetpack")) {
				if (GetComponent<CharacterJetpack> ().emissionModule.enabled) {
					transform.GetChild (2).transform.position = new Vector3 (transform.position.x, transform.position.y+0.7f, transform.position.z);
				} else {
					transform.GetChild (2).transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				}
			} else {
				transform.GetChild (2).transform.position = new Vector3 (transform.position.x + 1f, transform.position.y, transform.position.z);
			}
		}
		if (stunned == false && camfollow.GetComponent<CameraFollow>().PauseGame == false && GameStarted && camfollow.GetComponent<CameraFollow>().endgame ==0 && 1==2)
		{
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || moveleft)
			{
				facingright = false;
				//Slides down block slowly if holding left
				if (horcontact == "right" && camfollow.GetComponent<CameraFollow>().endgame == 0 ) {
					//rb.gravityScale = 0.2f;
				}

				//Cannot move while superjumping
				if (sjtime == 0) {
					Speed -= Acceleration * Time.deltaTime;
				}
				//rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);
			}
			else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || moveright)
			{
				facingright = true;


				//Cannot move while superjumping
				if (sjtime == 0) {
					Speed += Acceleration * Time.deltaTime;
				}
				//rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
			}
			//When releasing left or right buttons
			else
			{
				if(Speed > Deceleration * Time.deltaTime)
				{
					Speed = Speed - Deceleration * Time.deltaTime;
				}
				else if(Speed < -Deceleration * Time.deltaTime)
				{
					Speed = Speed + Deceleration * Time.deltaTime;
				}
				else
				{
					Speed = 0;
				}

			}

			//Sets horizontal velocity + with horizontal arrows

			if (OnArrow == "" || !onGround) {
				Speed = Mathf.Clamp (Speed, -MaxSpeed, MaxSpeed);
			} else if (OnArrow == "left") {
				if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) || moveright) && crouching == 0 && sjtime==0) {
					Speed += 40f * Time.deltaTime;
					Speed = Mathf.Clamp (Speed, -MaxSpeed, 2.5f);
				} else if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) || moveleft)) {
					Speed -= 40f * Time.deltaTime;
					Speed = Mathf.Clamp (Speed, -8f, MaxSpeed);
				} else {
					Speed -= 40f * Time.deltaTime;
					Speed = Mathf.Clamp (Speed, -5, MaxSpeed);
				}
			} else if (OnArrow == "right") {
				if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) || moveleft) && crouching == 0 && sjtime==0) {
					Speed -= 40f * Time.deltaTime;
					Speed = Mathf.Clamp (Speed, -2.5f, MaxSpeed);
				} else if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) || moveright)) {
					Speed += 40f * Time.deltaTime;
					Speed = Mathf.Clamp (Speed, -MaxSpeed, 8f);
				} else {
					Speed += 40f * Time.deltaTime;
					Speed = Mathf.Clamp (Speed, -MaxSpeed, 5f);
				}
			} else {
				Speed = Mathf.Clamp (Speed, -MaxSpeed, MaxSpeed);
			}
			rb.velocity = new Vector2 (Speed, rb.velocity.y);






			//superjumping
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) )
			{
				if (camfollow.GetComponent<CameraFollow>().endgame == 0 && GameStarted) {
					/*
					if (onGround) {
						if (sjtime <= 0.015) {
							StartCoroutine (animate (0.04f, sprites.Lsuperjumpright, 0, gameObject));
						}
						rb.gravityScale = 4.0f;

						if (sjtime == 1) {
							sjtime = 0;
						}
						if (sjtime < 1.0) {
							if (Application.isMobilePlatform) {
								sjtime += 0.015;
							}
							else if (!Application.isMobilePlatform) {
								sjtime += 0.005;
							}
						}*/
				}


			}
			if ((!Application.isMobilePlatform && Input.GetKeyUp (KeyCode.S)) || (!Application.isMobilePlatform && Input.GetKeyUp (KeyCode.DownArrow))   || (Application.isMobilePlatform && !movedown)) {
				if (camfollow.GetComponent<CameraFollow>().endgame == 0 && GameStarted) {
					if (onGround) {
						StartCoroutine (animate (0.05f, sprites.Ljumpright, 0, gameObject));

						rb.gravityScale = 1.5f;

						if (onGround && sjtime > 0.2) {
							rb.velocity = new Vector2 (rb.velocity.x, jumpheight * 2 * (float)sjtime);
						}

						sjtime = 0;
						//Ladder and water mechanics when releasing down
					}

				}

			}





		}

	}
	//Calculates score
	IEnumerator calscore(float time)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay

		float xdiff = transform.position.x - lastposition.x;
		float ydiff = transform.position.y - lastposition.y;

		float sum = Mathf.Pow (xdiff, 2) + Mathf.Pow (ydiff, 2);

		//How fast should the score increase?
		float fraction=2f;
		lastdistance = Mathf.Sqrt (sum)/fraction;

		score += lastdistance;
		lastposition = transform.position;

		StartCoroutine (calscore (1f));
	}

	IEnumerator LoadingTimer(float time,string no)
	{

		yield return new WaitForSeconds(time);
		if (no == "1") {
			camfollow.GetComponent<CameraFollow>().LoadingImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Loading1;
			StartCoroutine (LoadingTimer (0.2f, "2"));
		} else if (no == "2") {
			camfollow.GetComponent<CameraFollow>().LoadingImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Loading2;
			StartCoroutine (LoadingTimer (0.2f, "3"));
		} else if (no == "3") {
			camfollow.GetComponent<CameraFollow>().LoadingImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Loading3;
			StartCoroutine (LoadingTimer (0.2f, "1"));
		}
	}


	IEnumerator CountdownTimer(float time,string no)
	{

		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		if (no=="30")
		{
			camfollow.GetComponent<CameraFollow>().CountdownImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Countdown3;
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (0);
			StartCoroutine (CountdownTimer (0.8f, "31"));
		}
		else if (no=="31")
		{
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (1);
			StartCoroutine (CountdownTimer (0.2f, "20"));
		}
		else if (no=="20")
		{
			camfollow.GetComponent<CameraFollow>().CountdownImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Countdown2;
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (0);
			StartCoroutine (CountdownTimer (0.8f, "21"));
		}
		else if (no=="21")
		{
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (1);
			StartCoroutine (CountdownTimer (0.2f, "10"));
		}
		else if (no=="10")
		{
			camfollow.GetComponent<CameraFollow>().CountdownImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Countdown1;
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (0);
			StartCoroutine (CountdownTimer (0.8f, "11"));
		}
		else if (no=="11")
		{
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (1);
			StartCoroutine (CountdownTimer (0.2f, "00"));
		}
		else if (no=="00")
		{
			camfollow.GetComponent<CameraFollow>().CountdownImage.GetComponent<Image> ().sprite = camfollow.GetComponent<CameraFollow>().Countdown0;
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (0);
			StartCoroutine (CountdownTimer (0.8f, "01"));
		}
		else if (no=="01")
		{
			camfollow.GetComponent<CameraFollow>().level1scenecontroller.MovePanels (1);
			StartCoroutine (CountdownTimer (0.2f, "start"));
		}
		else if (no=="start")
		{
			CountingDownEnded = true;
			GetComponent<Character> ().UnFreeze ();
			camfollow.GetComponent<CameraFollow>().unpausegame ();
			camfollow.GetComponent<CameraFollow>().caneditblock ();
			GameStarted = true;
			camfollow.GetComponent<CameraFollow>().GameStarted = true;
			rb.gravityScale = 1.5f;

			camfollow.GetComponent<CameraFollow>().GameTime.SetActive (true);
			camfollow.GetComponent<CameraFollow>().GameTime.GetComponent<GameTime> ().ResetStartTime ();
		}
	}

	IEnumerator SpeedBurstEffect(float time,GameObject flames,float starttime)
	{

		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		if (Time.time - starttime < 10f) {
			flames.transform.position = transform.position;
			StartCoroutine (SpeedBurstEffect (0.01f, flames, starttime));
		} else {
			Destroy (flames);
			GetComponent<CharacterHorizontalMovement> ().ResetHorizontalSpeed();
		}
	}
	IEnumerator SuperJumpEffect(float time, GameObject flames,float starttime)
	{

		yield return new WaitForSeconds(time);
		// Code to execute after the delay
		flames.transform.position=transform.position;
		if (Time.time - starttime > 2f) {
			Destroy (flames);
			UsingSuperJump = false;
		} else {
			StartCoroutine (SuperJumpEffect (time, flames, starttime));
		}

	}

	IEnumerator SuperJumpTimer(float time, float number,float force)
	{

		yield return new WaitForSeconds(time);
		// Code to execute after the delay

		number+=1;
		if (number == 2) {
			force = 30f;
		}		
		if (number<=3)
		{
			if (GetComponent<CorgiController> ().Speed.y < 10f) {
				GetComponent<CorgiController> ().SetVerticalForce (force);
			}
			StartCoroutine(SuperJumpTimer(time,number,force));
		}
	}




	private bool caster(Vector2 center, float radius)
	{
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius,camfollow.GetComponent<CameraFollow>().whatIsGround);

		if (hitColliders.Length <= 5)
		{
			return false;
		}
		else
		{
			return true;
		}
	}


	public bool islocal()
	{
		if (photonView.isMine)
		{
			return true;
		}
		else
		{
			return false;
		}
	}






	//[Command]
	void CmdFire(bool facingright)
	{
		//Create bullet from the bullet preffab
		//Add velocity to bulleta
		GameObject bullet;
		if (facingright)
		{
			bullet = Instantiate(BulletRight, transform.position + new Vector3(0.56f, -0.095f, 0), Quaternion.identity);
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
		}
		else
		{
			bullet = Instantiate(BulletLeft, transform.position + new Vector3(-0.56f, -0.095f, 0), Quaternion.identity);
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
		}
		//NetworkServer.Spawn(bullet);

		//Destroy bullet after 5 seconds
		Destroy(bullet, 10.0f);
	}

	//[Command]
	void CmdSword(bool facingright)
	{
		//Create sword hitbox from the bullet preffab
		GameObject swordbox;
		if (facingright)
		{
			swordbox = Instantiate(SwordBoxRight, transform.position + new Vector3(0.118f, -0.072f, 0), Quaternion.identity);
			swordbox.name = user+"Sword";
			//bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(4, 0);
		}
		else
		{
			swordbox = Instantiate(SwordBoxLeft, transform.position + new Vector3(-0.118f, -0.072f, 0), Quaternion.identity);
			swordbox.name = user+"Sword";
			//bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
		}
		////NetworkServer.Spawn(swordbox);

		//Destroy bullet after 5 seconds
		Destroy(swordbox, 0.1f);
	}

	//Called by master client to all other clients
	[PunRPC]
	public void MasterSpawn(bool facingright,string item,string player)
	{
		photonView.RPC ("ClientSpawn", PhotonTargets.All, facingright, item,player);
	}

	//Received by player clients
	[PunRPC]
	public void ClientSpawn(bool facingright,string item,string player)
	{
		//Lightning
		if (item == "lightning") {
			camfollow.GetComponent<CameraFollow> ().LightningStrike (player);
		}
		//BOMBS
		else if (item == "bombitem" || item == "blockitem") {
			//Create sword hitbox from the bullet preffab
			GameObject bomby;
			if (facingright) {
				bomby = PhotonNetwork.InstantiateSceneObject (item, transform.position + new Vector3 (1.30f, 0f, 0), Quaternion.identity, 0, null);
				Instantiate (CoinBurstEffect, transform.position + new Vector3 (1.30f, 0f, 0), transform.rotation);
				//bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(4, 0);
			} else {
				bomby = PhotonNetwork.InstantiateSceneObject (item, transform.position + new Vector3 (-1.30f, 0f, 0), Quaternion.identity, 0, null);
				Instantiate (CoinBurstEffect, transform.position + new Vector3 (-1.30f, 0f, 0), transform.rotation);
				//bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
			}
		}
		//EFFECTS
		else if (item.Split(';')[1]=="RocketFlamesEffect1") {
			GameObject flames= Instantiate (RocketFlamesEffect, transform.position + new Vector3 (1.30f, 0f, 0),Quaternion.identity);
			StartCoroutine (SpeedBurstEffect (0.01f,flames,Time.time));
		}
		else if (item.Split(';')[1]=="RocketFlamesEffect2") {

			GameObject flames= Instantiate (RocketFlamesEffect, transform.position + new Vector3 (1.30f, 0f, 0),Quaternion.identity);
			UsingSuperJump = true;
			StartCoroutine (SuperJumpEffect (0.01f, flames, Time.time));
			Instantiate(TouchTheGroundEffect,transform.position,transform.rotation);
		}

	}

	//Received by master client
	[PunRPC]
	public void MasterReceive(int RequestID,string tempuser,string nameofmap)
	{
		//nameofmap is only required for RequestID 4.(Loading of map)
		if (PhotonNetwork.isMasterClient) {
			//Master client receives request from user
			camfollow.GetComponent<CameraFollow> ().CurrentRequestID = RequestID;
			camfollow.GetComponent<CameraFollow> ().CurrentRequestUser = tempuser;
			camfollow.GetComponent<CameraFollow> ().CurrentRequestNameOfMap = nameofmap;

			camfollow.GetComponent<CameraFollow> ().AcceptPanel.SetActive (true);
			camfollow.GetComponent<CameraFollow> ().scenecontroller.MovePanels (14);

			if (RequestID == 0) {
				//0: Request to start playing mode
				string message="User "+'"'+tempuser+'"'+" has requested to enter playing mode. Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
			else if (RequestID == 1) {
				//1: Request to start editing mode
				string message="User "+'"'+tempuser+'"'+" has requested to enter editing mode. Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
			else if (RequestID == 2) {
				//2: Request to change mode to deathmatch
				string message="User "+'"'+tempuser+'"'+" has requested to change mode to Deathmatch. Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
			else if (RequestID == 3) {
				//3: Request to change mode to Race
				string message="User "+'"'+tempuser+'"'+" has requested to change mode to Race. Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
			else if (RequestID == 4) {
				//4: Request to load new map
				string message="User "+'"'+tempuser+'"'+" has requested to laod his map "+nameofmap+". Any unsaved changes will be lost. Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
			else if (RequestID == 5) {
				//3: Request to change mode to Race
				string message="User "+'"'+tempuser+'"'+" has requested to save this map to his own levels. Warning: This does not save your level. This user will have permanent access to this level Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
			else if (RequestID == 6) {
				//6: Request to reset map
				string message="User "+'"'+tempuser+'"'+" has requested to reset entire map. Do you accept?";
				camfollow.GetComponent<CameraFollow> ().AcceptText.GetComponent<Text>().text=message;
			}
		}
	}

	//Sent by master client to all players
	public void MasterSend(int RequestID,string RequestUser,bool accept,string nameofmap)
	{
		//nameofmap is only required for RequestID 4.(Loading of map)
		if (PhotonNetwork.isMasterClient) {
			photonView.RPC ("ClientReceive", PhotonTargets.All,RequestID,RequestUser, accept,nameofmap);
		}
	}

	//Received by player clients
	[PunRPC]
	public void ClientReceive(int RequestID,string RequestUser, bool accept, string nameofmap)
	{
		//nameofmap is only required for RequestID 4.(Loading of map)
		camfollow.GetComponent<CameraFollow> ().RequestTitle.GetComponent<Text> ().text = "Received reply";
		if (accept) {
			camfollow.GetComponent<CameraFollow> ().scenecontroller.MovePanels (15);
			camfollow.GetComponent<CameraFollow> ().scenecontroller.MovePanels (17);
			if (RequestID == 0) {
				camfollow.GetComponent<CameraFollow> ().toggleeditplay ();
			}
			else if (RequestID == 1) {
				camfollow.GetComponent<CameraFollow> ().toggleeditplay ();
			}
			else if (RequestID == 2) {
				camfollow.GetComponent<CameraFollow> ().setgamemode (1);
			}
			else if (RequestID == 3) {
				camfollow.GetComponent<CameraFollow> ().setgamemode (0);
			}
			else if (RequestID == 4) {
				if (controller.user == RequestUser) {
					//Enable autosave and future saving for player, if loading his map
					camfollow.GetComponent<CameraFollow> ().AuthorisedToSaveMap = true;
				} else {
					//Disable autosave and future saving for player, if not loading his map
					camfollow.GetComponent<CameraFollow> ().AuthorisedToSaveMap = false;
				}
				StartCoroutine(camfollow.GetComponent<CameraFollow> ().loadmap(nameofmap,true,RequestUser));
			}
			else if (RequestID == 5) {
				//Only save map for the user that requested it
				if (controller.user == RequestUser) {
					//Enable autosave and future saving for player
					camfollow.GetComponent<CameraFollow> ().AuthorisedToSaveMap = true;

					camfollow.GetComponent<CameraFollow> ().savemap ();
				}
			}
			else if (RequestID == 6) {
				camfollow.GetComponent<CameraFollow> ().ResetMap ();
			}
		} else if (!accept) {
			if (RequestID == 0) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to start playing mode";
			}
			else if (RequestID == 1) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to start editing mode";
			}
			else if (RequestID == 2) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to change mode to Deathmatch";
			}
			else if (RequestID == 3) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to change mode to Race";
			}
			else if (RequestID == 4) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to load "+nameofmap;
			}
			else if (RequestID == 5) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to save this map to your own levels";
			}
			else if (RequestID == 6) {
				camfollow.GetComponent<CameraFollow> ().RequestText.GetComponent<Text>().text = "Room owner has declined your request to reset entire map";
			}
		}
	}


	//[Command]
	void CmdBlockItem(bool facingright)
	{
		//Create sword hitbox from the bullet preffab
		GameObject blockitem;
		if (facingright)
		{
			blockitem = Instantiate(BlockItem, transform.position + new Vector3(1.00f, 0f, 0), Quaternion.identity);
			Instantiate(CoinBurstEffect,transform.position + new Vector3(1.00f, 0f, 0),transform.rotation);
			//bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(4, 0);
		}
		else
		{
			blockitem = Instantiate(BlockItem, transform.position + new Vector3(-1.00f, 0f, 0), Quaternion.identity);
			Instantiate(CoinBurstEffect,transform.position + new Vector3(-1.00f, 0f, 0),transform.rotation);
			//bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
		}
		//NetworkServer.Spawn(blockitem);


	}

	//[Command]
	void CmdSpawnGround()
	{
		var myblock = (GameObject)Instantiate(myground, transform.position, Quaternion.identity);
		//NetworkServer.SpawnWithClientAuthority(myblock, connectionToClient);
	}


	//[Command]
	void CmdSpawnBlock(GameObject block, float xcordf, float ycordf,string blockname)
	{
		if (block != null)
		{
			var myblock = Instantiate(block, new Vector3(xcordf, ycordf, 0), Quaternion.identity);
			myblock.name = blockname;
			mapb.Add (myblock);
			//NetworkServer.Spawn(myblock);
		}
	}



	void MouseLeft()
	{
		if (SceneManager.GetActiveScene () != SceneManager.GetSceneByName ("LevelEditor")) {
			//return;
		}



		///Below handles placing new blocks into level
		var Cameras = GameObject.FindGameObjectsWithTag ("MainCamera");
		foreach (GameObject cam in Cameras) {
			Camera camy = cam.GetComponent<Camera> ();
			if (camy.name == name + "Main Camera") {
				LocalCamera = camy;
			}
		}
		pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 20.0f));
		var constantpos = pos - transform.position;
		//Manages selected block to place
		//Convert to grid before checking if block exists in the gridspace
		Vector2 gridpos = GetGrid (pos, true);
		Vector2 mappos = GetGrid (pos, false);
		int gridx = (int)gridpos.x;
		int gridy = (int)gridpos.y;
		string gridposString = gridx.ToString ().Substring (0, gridx.ToString ().Length) + "," + gridy.ToString ().Substring (0, gridy.ToString ().Length);

		//Set boundaries 
		Vector4 boundaries = new Vector4 (-9999999999, -9999999999, 9999999999, 9999999999999);
		if (!camfollow.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposString) && gridx >= boundaries [0] && gridy >= boundaries [1] && gridx < boundaries [2] && gridy < boundaries [3]) {
			if (gridx > mapxmax) {
				mapxmax = gridx;

			}

			if (gridx < mapxmin) {
				mapxmin = gridx;
			}
			if (gridy > mapymax) {
				mapymax = gridy;
			}
			if (gridy < mapymin) {
				mapymin = gridy;
			}



			int selectedblock = camfollow.GetComponent<CameraFollow> ().selectedblock;
			//setblock
			if (selectedblock == 1) {
				block = b1;
			} else if (selectedblock == 2) {
				block = b2;
			} else if (selectedblock == 3) {
				block = b3;
			} else if (selectedblock == 4) {
				block = b4;
			} else if (selectedblock == 5) {
				block = brick;
			} else if (selectedblock == 6) {
				block = finish;
			} else if (selectedblock == 7) {
				block = ice;
			} else if (selectedblock == 8) {
				block = itemonce;
			} else if (selectedblock == 9) {
				block = iteminf;
			} else if (selectedblock == 10) {
				block = leftarrow;
			} else if (selectedblock == 11) {
				block = rightarrow;
			} else if (selectedblock == 12) {
				block = uparrow;
			} else if (selectedblock == 13) {
				block = downarrow;
			} else if (selectedblock == 14) {
				block = bomb;
			} else if (selectedblock == 15) {
				block = crumble;
			} else if (selectedblock == 16) {
				block = vanish;
			} else if (selectedblock == 17) {
				block = move;
			} else if (selectedblock == 18) {
				block = rotateleft;
			} else if (selectedblock == 19) {
				block = rotateright;
			} else if (selectedblock == 20) {
				block = push;
			} else if (selectedblock == 21) {
				block = happy;
			} else if (selectedblock == 22) {
				block = sad;
			} else if (selectedblock == 23) {
				block = net;
			} else if (selectedblock == 24) {
				block = heart;
			} else if (selectedblock == 25) {
				block = time;
			} else if (selectedblock == 26) {
				block = water;
			} else if (selectedblock == 27) {
				block = start;
			} else if (selectedblock == 28) {
				block = stop;
			} else if (selectedblock == 29) {
				block = ladder;
			} else if (selectedblock == 30) {
				block = antigravity;
			} else if (selectedblock == 31) {
				block = bouncy;
			} else if (selectedblock == 32) {
				block = spike;
			} else if (selectedblock == 33) {
				block = checkpoint;
			} else if (selectedblock == 34) {
				block = portal;
			} else if (selectedblock == 35) {
				block = door;
			} else if (selectedblock == 36) {
				block = oneway;
			} else if (selectedblock == 37) {
				block = rocket;
			} else if (selectedblock == 38) {
				block = falling;
			} else if (selectedblock == 39) {
				block = giant;
			} else if (selectedblock == 40) {
				block = tiny;
			} else if (selectedblock == 41) {
				block = sticky;
			} else if (selectedblock == 42) {
				block = fan;
			} else if (selectedblock == 43) {
				block = key0;
			} else if (selectedblock == 44) {
				block = locked0;
			} else if (selectedblock == 45) {
				block = weapon;
			} else if (selectedblock == 46) {
				block = npc;
			} else if (selectedblock == 47) {
				block = sign;
			} else if (selectedblock == 48) {
				block = timefreeze;
			}
			//Spawn a specific portal color
			string selectedportal = camfollow.GetComponent<CameraFollow> ().selectedportal;
			if (block == portal) {
				if (selectedportal == "01") {
					block = portal0;
				} else if (selectedportal == "11") {
					block = portal1;
				} else if (selectedportal == "21") {
					block = portal2;
				} else if (selectedportal == "31") {
					block = portal3;
				} else if (selectedportal == "41") {
					block = portal4;
				} else if (selectedportal == "51") {
					block = portal5;
				} else if (selectedportal == "61") {
					block = portal6;
				} else if (selectedportal == "71") {
					block = portal7;
				} else if (selectedportal == "81") {
					block = portal8;
				} else if (selectedportal == "91") {
					block = portal9;
				}
			}

			string selectedkeylock = camfollow.GetComponent<CameraFollow> ().selectedkeylock;
			//Spawn a specific color of keylock
			if (block == key0 || block == locked0) {
				//if selected block is key
				if (selectedkeylock == "01") {
					block = key0;
				} else if (selectedkeylock == "11") {
					block = key1;
				} else if (selectedkeylock == "21") {
					block = key2;
				} else if (selectedkeylock == "31") {
					block = key3;
				} else if (selectedkeylock == "41") {
					block = key4;
				} else if (selectedkeylock == "51") {
					block = key5;
				} else if (selectedkeylock == "61") {
					block = key6;
				} else if (selectedkeylock == "71") {
					block = key7;
				} else if (selectedkeylock == "81") {
					block = key8;
				} else if (selectedkeylock == "91") {
					block = key9;
				}
				//if selected block is lock
				if (selectedkeylock == "02") {
					block = locked0;
				} else if (selectedkeylock == "12") {
					block = locked1;
				} else if (selectedkeylock == "22") {
					block = locked2;
				} else if (selectedkeylock == "32") {
					block = locked3;
				} else if (selectedkeylock == "42") {
					block = locked4;
				} else if (selectedkeylock == "52") {
					block = locked5;
				} else if (selectedkeylock == "62") {
					block = locked6;
				} else if (selectedkeylock == "72") {
					block = locked7;
				} else if (selectedkeylock == "82") {
					block = locked8;
				} else if (selectedkeylock == "92") {
					block = locked9;
				}
			}

			//Ensures there is only one start block
			if (selectedblock == 27) {
				var startblock = GameObject.FindGameObjectWithTag ("start");
				Destroy (startblock);

				tempmapd.Clear ();
				foreach (KeyValuePair<string, string> item in camfollow.GetComponent<CameraFollow>().mapd) {
					if (item.Value == "start") {
						tempmapd.Add (item.Key, item.Value);
					}
				}

				foreach (KeyValuePair<string, string> item2 in tempmapd) {
					camfollow.GetComponent<CameraFollow> ().mapd.Remove (item2.Key);
					camfollow.GetComponent<CameraFollow> ().blockmapd.Remove (item2.Key);
				}

			}
			//Ensures there is only one of each unique portal block
			if (selectedblock == 34) {
				var portalblock = GameObject.Find ("portal" + camfollow.GetComponent<CameraFollow> ().selectedportal);
				Destroy (portalblock);

				tempmapd.Clear ();
				foreach (KeyValuePair<string, string> item in camfollow.GetComponent<CameraFollow>().mapd) {
					if (item.Value.Contains ("portal" + camfollow.GetComponent<CameraFollow> ().selectedportal)) {
						tempmapd.Add (item.Key, item.Value);
					}
				}

				foreach (KeyValuePair<string, string> item2 in tempmapd) {
					camfollow.GetComponent<CameraFollow> ().mapd.Remove (item2.Key);
					camfollow.GetComponent<CameraFollow> ().blockmapd.Remove (item2.Key);
				}

			}
			//Add extra portal string if portal is selected.
			string ExtraPortalID = "";
			if (camfollow.GetComponent<CameraFollow> ().selectedblock == 34) {
				ExtraPortalID = camfollow.GetComponent<CameraFollow> ().selectedportal;
			}

			//Add extra keylock string if keylock is selected.
			string ExtraKeyLockID = "";
			if (camfollow.GetComponent<CameraFollow> ().selectedblock == 44 || camfollow.GetComponent<CameraFollow> ().selectedblock == 43) {
				ExtraKeyLockID = camfollow.GetComponent<CameraFollow> ().selectedkeylock;
			}
			string blockmessage = gridposString + "#" + pos [0].ToString () + "#" + pos [1].ToString () + "#" + pos [2].ToString () + "#" + ExtraPortalID + "#" + ExtraKeyLockID + "#" + selectedblock.ToString () + "#" + selectedportal + "#" + selectedkeylock + "#" + camfollow.GetComponent<CameraFollow> ().blockdir;
			camfollow.GetComponent<CameraFollow> ().ChatObj.GetComponent<BlockSpawner> ().SpawnBlock (blockmessage);


			//Send to all

			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				photonView.RPC ("SpawnForAll", PhotonTargets.OthersBuffered, blockmessage);
			}

			//camfollow.GetComponent<CameraFollow>().ChatObj.GetComponent<UNETChat> ().SendBlockMessage (blockmessage);
		} else {
			if (Input.GetMouseButtonDown (0)) {
				if (SelectingBlock == false) {
					SelectingBlock = true;
					camfollow.GetComponent<CameraFollow> ().cursor.GetComponent<SpriteRenderer> ().sprite = camfollow.GetComponent<CameraFollow> ().CursorSelected;
					camfollow.GetComponent<CameraFollow> ().mapd [gridposString] += "IsSelected";

					if (camfollow.GetComponent<CameraFollow> ().blockmapd.ContainsKey (gridposString)) {
						GameObject selectedblock = camfollow.GetComponent<CameraFollow> ().blockmapd [gridposString];
						camfollow.GetComponent<CameraFollow> ().cursor.GetComponent<CursorScript> ().SetSelectedBlock (selectedblock);
					} else {
						camfollow.GetComponent<CameraFollow> ().cursor.GetComponent<CursorScript> ().SetSelectedBlock (null);
					}
					photonView.RPC ("SendCursorState", PhotonTargets.OthersBuffered, 1,name,gridposString,camfollow.GetComponent<CameraFollow> ().cursor.transform.position);
				}
			}
		}
	}

	[PunRPC]
	public void SendCursorState(int state,string user,string gridposString,Vector3 cursorpos)
	{
		var cursors = GameObject.FindGameObjectsWithTag ("cursor");
		foreach (GameObject thiscursor in cursors) {
			if (thiscursor.name.Split (';') [0] == user) {
				thiscursor.transform.position = cursorpos;

				if (thiscursor.GetComponent<CursorScript> ().SelectedBlock != null) {
					thiscursor.GetComponent<CursorScript> ().SelectedBlock.transform.position = cursorpos;
				}
				if (state == 0) {
					//CursorDefault:0
					thiscursor.GetComponent<SpriteRenderer> ().sprite = camfollow.GetComponent<CameraFollow> ().CursorDefault;

					if (thiscursor.GetComponent<CursorScript>().SelectedBlock != null) {
						thiscursor.GetComponent<CursorScript>().SelectedBlock.GetComponent<SnapToGrid> ().CanRelease = true;
						thiscursor.GetComponent<CursorScript>().SelectedBlock = null;
					}
				} else if (state == 1) {
					//CursorSelected:1
					thiscursor.GetComponent<SpriteRenderer> ().sprite = camfollow.GetComponent<CameraFollow> ().CursorSelected;
					camfollow.GetComponent<CameraFollow> ().mapd [gridposString] += "IsSelected";

					if (camfollow.GetComponent<CameraFollow> ().blockmapd.ContainsKey (gridposString)) {
						GameObject selectedblock = camfollow.GetComponent<CameraFollow> ().blockmapd [gridposString];
						thiscursor.GetComponent<CursorScript> ().SetSelectedBlock (selectedblock);
					} else {
						thiscursor.GetComponent<CursorScript> ().SetSelectedBlock (null);
					}
				} else if (state == 2) {
					//CursorSelectedReleased:1
					thiscursor.GetComponent<SpriteRenderer> ().sprite = camfollow.GetComponent<CameraFollow> ().CursorSelectedReleased;
				}
			}
		}
	}

	[PunRPC]
	public void SendChatMessage(string chatuser,string chatmessage)
	{
		camfollow.GetComponent<CameraFollow>().SelectedChannelText.GetComponent<Text>().text+= "<color=#FFA53F>"+chatuser+"</color>" +  "<color=#FFD8AF>"+chatmessage+"</color>";
		camfollow.GetComponent<CameraFollow> ().ChatScrollBar.GetComponent<Scrollbar> ().value = 0;
	}
	//Spawn block for every player
	[PunRPC]
	public void SpawnForAll(string blockmessage)
	{
		camfollow.GetComponent<CameraFollow>().ChatObj.GetComponent<BlockSpawner>().SpawnBlock (blockmessage);

		//Clear undo history if an action is performed(To prevent redoing of previous undo)
		camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Clear ();
	}

	[PunRPC]
	public void RemoveForAll(string gridposString)
	{
		camfollow.GetComponent<CameraFollow>().ChatObj.GetComponent<BlockSpawner> ().RemoveBlock (gridposString);

		//Clear undo history if an action is performed(To prevent redoing of previous undo)
		camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Clear ();
	}

	public Camera returnlocalcamera()
	{
		return LocalCamera;
	}

	//[Command]
	public void CmdMovePlayerToStart()
	{
		if (photonView.isMine)
		{
			transform.position = new Vector2(0f, 0f);
		}
	}
	//Loads new map
	public void loadmap(string loaded, bool clearmapd,string nameofmap)
	{
		camfollow.GetComponent<CameraFollow> ().CurrentMapName = nameofmap;
		//Change start position to middle of entire map of 100x100 blocks grid
		transform.position = new Vector3 (1.00f * 50f, 1.00f * 50f, 0f);
		camfollow.GetComponent<CameraFollow> ().mapd.Clear ();
		camfollow.GetComponent<CameraFollow> ().blockmapd.Clear ();

		camfollow.GetComponent<CameraFollow> ().mapdHistory.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdValueCache.Clear ();

		camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Clear ();
		camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Clear ();

		//Resets map dimensions
		mapxmax = 0;
		mapxmin = 999999;
		mapymax = 0;
		mapymin = 999999;
		//Load map->Convert string to dictionary

		PlayerPrefs.SetString ("lastmap", loaded);
		PlayerPrefs.SetString ("lastmapname", nameofmap);

		if (nameofmap != "empty" && camfollow.GetComponent<CameraFollow> ().mapname != null) {
			var split = nameofmap.Split ('/');
			string reduced = split [split.Length - 1];
			camfollow.GetComponent<CameraFollow> ().mapname.GetComponent<InputField> ().text = reduced.Split ('.') [0];
		}

		string loadedmode = loaded.Split ('#') [0];
		if (loadedmode == "Deathmatch") {
			camfollow.GetComponent<CameraFollow> ().GameMode = "Deathmatch";

			if (camfollow.GetComponent<CameraFollow> ().RaceCheckmark != null) {
				camfollow.GetComponent<CameraFollow> ().RaceCheckmark.SetActive (false);
				camfollow.GetComponent<CameraFollow> ().DeathmatchCheckmark.SetActive (true);
			}
		} else {
			camfollow.GetComponent<CameraFollow> ().GameMode = "Race";

			if (camfollow.GetComponent<CameraFollow> ().RaceCheckmark != null) {
				camfollow.GetComponent<CameraFollow> ().RaceCheckmark.SetActive (true);
				camfollow.GetComponent<CameraFollow> ().DeathmatchCheckmark.SetActive (false);
			}
		}

		bool test = false;
		int size = 100;
		if (test) {
			loaded = loaded.Substring (0, loaded.Length - 1);
			for (int i = 100; i < 100 + size; i++) {
				for (int j = 100; j < 100 + size; j++) {
					loaded += ",{" + '"' + i + "," + j + "," + '"' + "," + '"' + "b1" + '"' + "}";
				}
			}
		}

		string LoadedMap = loaded.Split ('#') [1];
		//Camera.allCameras[1].orthographicSize = float.Parse(camerasize);

		int count = LoadedMap.Split ('{').Length - 1;
		string[] LoadedMapSplit = LoadedMap.Split ('{');

		for (int i = 2; i <= count; i++) {

			string[] LoadedMapSplit2 = LoadedMapSplit [i].Split ('"');
			string mykey = LoadedMapSplit2 [1];
			string myvalue = LoadedMapSplit2 [3];
			string xcordString = mykey.Split (',') [0];
			string ycordString = mykey.Split (',') [1];

			int xcordInt = int.Parse (xcordString);
			int ycordInt = int.Parse (ycordString);

			double multiplier = 1.00;
			float multiplierf = (float)multiplier;
			float xcordf = (float)xcordInt * multiplierf;
			float ycordf = (float)ycordInt * multiplierf;

			camfollow.GetComponent<CameraFollow> ().mapd.Add (mykey, myvalue);

		}
		//Draw level from mapd
		foreach (KeyValuePair<string, string> item in camfollow.GetComponent<CameraFollow>().mapd) {
			string mcord = item.Key;
			string myvalue = item.Value;
			string[] tokens = mcord.Split (',');

			int xcord = int.Parse (tokens [0]);
			int ycord = int.Parse (tokens [1]);

			//string bcord = item.Value;

			//xcord,ycord,block
			double multiplier = 1.00;
			float multiplierf = (float)multiplier;
			float xcordf = (float)xcord * multiplierf;
			float ycordf = (float)ycord * multiplierf;

			if (xcord > mapxmax) {
				mapxmax = xcord;
			}

			if (ycord > mapymax) {
				mapymax = ycord;
			}

			if (xcord < mapxmin) {
				mapxmin = xcord;
			}

			if (ycord < mapymin) {
				mapymin = ycord;
			}

			//setblock=
			if (myvalue.Contains ("b1")) {
				block = b1;
			} else if (myvalue.Contains ("b2")) {
				block = b2;
			} else if (myvalue.Contains ("b3")) {
				block = b3;
			} else if (myvalue.Contains ("b4")) {
				block = b4;
			} else if (myvalue.Contains ("brick")) {
				block = brick;
			} else if (myvalue.Contains ("finish")) {
				block = finish;
			} else if (myvalue.Contains ("ice")) {
				block = ice;
			} else if (myvalue.Contains ("itemonce")) {
				block = itemonce;
			} else if (myvalue.Contains ("iteminf")) {
				block = iteminf;
			} else if (myvalue.Contains ("leftarrow")) {
				block = leftarrow;
			} else if (myvalue.Contains ("rightarrow")) {
				block = rightarrow;
			} else if (myvalue.Contains ("uparrow")) {
				block = uparrow;
			} else if (myvalue.Contains ("downarrow")) {
				block = downarrow;
			} else if (myvalue.Contains ("bomb")) {
				block = bomb;
			} else if (myvalue.Contains ("crumble")) {
				block = crumble;
			} else if (myvalue.Contains ("vanish")) {
				block = vanish;
			} else if (myvalue.Contains ("move")) {
				block = move;
			} else if (myvalue.Contains ("rotateright")) {
				block = rotateright;
			} else if (myvalue.Contains ("rotateleft")) {
				block = rotateleft;
			} else if (myvalue.Contains ("push")) {
				block = push;
			} else if (myvalue.Contains ("happy")) {
				block = happy;
			} else if (myvalue.Contains ("sad")) {
				block = sad;
			} else if (myvalue.Contains ("net")) {
				block = net;
			} else if (myvalue.Contains ("heart")) {
				block = heart;
			} else if (myvalue.Contains ("time") && !myvalue.Contains ("timefreeze")) {
				block = time;
			} else if (myvalue.Contains ("water")) {
				block = water;
			} else if (myvalue.Contains ("start")) {
				block = start;
			} else if (myvalue.Contains ("stop")) {
				block = stop;
			} else if (myvalue.Contains ("ladder")) {
				block = ladder;
			} else if (myvalue.Contains ("antigravity")) {
				block = antigravity;
			} else if (myvalue.Contains ("bouncy")) {
				block = bouncy;
			} else if (myvalue.Contains ("spike")) {
				block = spike;
			} else if (myvalue.Contains ("checkpoint")) {
				block = checkpoint;
			} else if (myvalue.Contains ("portal")) {
				block = portal;
			} else if (myvalue.Contains ("door")) {
				block = door;
			} else if (myvalue.Contains ("oneway")) {
				block = oneway;
			} else if (myvalue.Contains ("rocket")) {
				block = rocket;
			} else if (myvalue.Contains ("falling")) {
				block = falling;
			} else if (myvalue.Contains ("giant")) {
				block = giant;
			} else if (myvalue.Contains ("tiny")) {
				block = tiny;
			} else if (myvalue.Contains ("sticky")) {
				block = sticky;
			} else if (myvalue.Contains ("fan")) {
				block = fan;
			} else if (myvalue.Contains ("key")) {
				block = key;
			} else if (myvalue.Contains ("lock")) {
				block = locked;
			} else if (myvalue.Contains ("weapon")) {
				block = weapon;
			} else if (myvalue.Contains ("npc")) {
				block = npc;
			} else if (myvalue.Contains ("sign")) {
				block = sign;
			} else if (myvalue.Contains ("timefreeze")) {
				block = timefreeze;
			}

			//Spawn a specific color of portal
			if (block == portal) {
				if (myvalue.Length >= 7) {
					int selectedportal = int.Parse (myvalue.Substring (6, 1));
					if (selectedportal == 0) {
						block = portal0;
					} else if (selectedportal == 1) {
						block = portal1;
					} else if (selectedportal == 2) {
						block = portal2;
					} else if (selectedportal == 3) {
						block = portal3;
					} else if (selectedportal == 4) {
						block = portal4;
					} else if (selectedportal == 5) {
						block = portal5;
					} else if (selectedportal == 6) {
						block = portal6;
					} else if (selectedportal == 7) {
						block = portal7;
					} else if (selectedportal == 8) {
						block = portal8;
					} else if (selectedportal == 9) {
						block = portal9;
					}
				}
			}
			//Spawn a specific color of keylock
			if (block == key || block == locked) {
				string selectedkeylock = myvalue.Substring (4, 2);
				if (selectedkeylock == "01") {
					block = key0;
				} else if (selectedkeylock == "11") {
					block = key1;
				} else if (selectedkeylock == "21") {
					block = key2;
				} else if (selectedkeylock == "31") {
					block = key3;
				} else if (selectedkeylock == "41") {
					block = key4;
				} else if (selectedkeylock == "51") {
					block = key5;
				} else if (selectedkeylock == "61") {
					block = key6;
				} else if (selectedkeylock == "71") {
					block = key7;
				} else if (selectedkeylock == "81") {
					block = key8;
				} else if (selectedkeylock == "91") {
					block = key9;
				}
				if (selectedkeylock == "02") {
					block = locked0;
				} else if (selectedkeylock == "12") {
					block = locked1;
				} else if (selectedkeylock == "22") {
					block = locked2;
				} else if (selectedkeylock == "32") {
					block = locked3;
				} else if (selectedkeylock == "42") {
					block = locked4;
				} else if (selectedkeylock == "52") {
					block = locked5;
				} else if (selectedkeylock == "62") {
					block = locked6;
				} else if (selectedkeylock == "72") {
					block = locked7;
				} else if (selectedkeylock == "82") {
					block = locked8;
				} else if (selectedkeylock == "92") {
					block = locked9;
				}
			}
			//CmdSpawnBlock(block, xcordf, ycordf,myvalue);
			var myblock = Instantiate (block, new Vector3 (xcordf, ycordf, 0), Quaternion.identity);
			myblock.name = myvalue;
			mapb.Add (myblock);
			camfollow.GetComponent<CameraFollow> ().blockmapd.Add (item.Key, myblock);


			//Set sign values from data
			if (myvalue.Contains ("sign")) {
				string signtext = myvalue.Substring (4);
				string[] dialogue = { signtext };
				if (myblock.GetComponent < DialogueZone> () != null) {
					myblock.GetComponent<DialogueZone> ().Dialogue = dialogue;
				}
			}


		}
		int max;
		if (mapxmax > mapymax) {
			max = mapxmax;
		} else {
			max = mapymax;
		}
		float mapwidth = (float)(mapxmax - mapxmin);
		float mapheight = (float)(mapymax - mapymin);

		GameObject startblock = GameObject.Find ("start");
		if (startblock != null) {
			startpos = startblock.transform.position - new Vector3 (0f, 1f, 0f);
			transform.position = startblock.transform.position;
			safespot = startblock.transform.position;
			checkpointpos = startblock.transform.position;
		} else {
			startpos = new Vector3 (15f, 15f, 0f);
			transform.position = startpos;
		}
		m_playerCustomProperties ["Loaded"] = "true";
		PhotonNetwork.player.SetCustomProperties (m_playerCustomProperties);

		//StartCoroutine(camfollow.GetComponent<CameraFollow>().autosavemap(5f));
	
	}

	public void ShutdownServer()
	{
		//NetworkServer.Shutdown();
	}
	/*
	void OnMyName(string newname)
	{
		user = newname;
	}
	*/


	//[Command]
	public void CmdChangeSprite(string myspr)
	{
		newsprite=myspr;

	}
	public void ChangeItem(string item)
	{
		if (photonView.isMine) {
			m_playerCustomProperties ["Item"] = item;
			PhotonNetwork.player.SetCustomProperties (m_playerCustomProperties);
		}
	}
	[PunRPC]
	public void ServerUseItem(int num)
	{
		if (num == 0) {
			PressSpace = false;
			HoldingSpace = false;
		} else if (num == 1) {
			PressSpace = true;
			HoldingSpace = false;
		} else if (num == 2) {
			PressSpace = false;
			HoldingSpace = true;
		}
	}
	//Calculates rank
	public void CalculateRank(float finalscore,float newrank,float newexp)
	{
		newexp += finalscore;
		float expneeded = (float)(30) * Mathf.Pow((float)(1.25) ,(newrank + 1));
		//If promoted to next rank
		if (newexp > expneeded) {
			CalculateRank (0, newrank + 1, newexp - expneeded);

		} else {

			var fp = camfollow.GetComponent<CameraFollow>().FinishPanel;
			//RankUp Text
			fp.transform.GetChild (5).GetComponent<Text> ().text = "User Rank: "+newrank;

			//Exp Text
			fp.transform.GetChild (6).GetComponent<Text> ().text = "EXP: "+newexp+"/"+expneeded;

			//Exp Bar
			fp.transform.GetChild (4).GetComponent<Slider>().value = Mathf.Clamp01(newexp/expneeded);

			StartCoroutine(camfollow.GetComponent<CameraFollow>().Upload (newrank.ToString()+";"+newexp.ToString()+";"+controller.SSpeed.ToString()+";"+controller.SAcc.ToString()+";"+controller.SJump.ToString()+";"+controller.SExpBonus.ToString(),user));

			controller.rank = newrank;
			controller.exp = newexp;
			PhotonNetwork.playerName = controller.user + "(" + controller.rank + ")";
		}
	}

	//Set rank from camera
	public void setrank()
	{
		rank = camfollow.GetComponent<CameraFollow>().rank;
		exp = camfollow.GetComponent<CameraFollow>().exp;
	}


	public void Splash()
	{

		Instantiate(camfollow.GetComponent<CameraFollow>().WaterEntryEffect,GetComponent<CharacterJump>().transform.position,Quaternion.identity);		
	}

	//Animates player
	public IEnumerator animate(float time,List<Sprite>list,int index,GameObject obj)
	{
		yield return new WaitForSeconds(time);
		string str="";
		// Code to execute after the delay
		if (list == sprites.Lwalkright) {
			if (sjtime == 0) {
				if (crouching == 0 && stunned == false) {
					str = "Walkrightsprite;" + index;
					if ((!Application.isMobilePlatform && Input.GetKey (KeyCode.D)) || (!Application.isMobilePlatform && Input.GetKey (KeyCode.RightArrow)) || (Application.isMobilePlatform && moveright)) {
						CmdChangeSprite (str);
						if (index < list.Count - 1) {	
							StartCoroutine (animate (time, list, index + 1, obj));
						} else {
							StartCoroutine (animate (time, list, 0, obj));
						}
					} else if ((!Application.isMobilePlatform && Input.GetKey (KeyCode.A)) || (!Application.isMobilePlatform && Input.GetKey (KeyCode.LeftArrow)) || (Application.isMobilePlatform && moveleft)) {
						CmdChangeSprite (str);
						if (index < list.Count - 1) {	
							StartCoroutine (animate (time, list, index + 1, obj));
						} else {
							StartCoroutine (animate (time, list, 0, obj));
						}
					} else {
						StartCoroutine (animate (time, list, 0, obj));
					}
				} else {
					StartCoroutine (animate (time, list, 0, obj));
				}
			} else {
				StartCoroutine (animate (time, list, 0, obj));
			}
		}

		else if (list == sprites.Lsuperjumpright) {
			str = "Sjrightsprite;" + index;
			if ((!Application.isMobilePlatform && Input.GetKey (KeyCode.S)) || (!Application.isMobilePlatform && Input.GetKey (KeyCode.DownArrow)) || (Application.isMobilePlatform && movedown)) {
				CmdChangeSprite (str);
				if (index < list.Count - 1) {	
					StartCoroutine (animate (time, list, index + 1, obj));
				}
			}
		}

		else if (list == sprites.Lcrouchright) {
			str = "Crouchrightsprite;" + index;
			if ((!Application.isMobilePlatform && Input.GetKey (KeyCode.D)) || (!Application.isMobilePlatform && Input.GetKey (KeyCode.RightArrow)) || (Application.isMobilePlatform && moveright)) {
				CmdChangeSprite (str);
				if (index < list.Count - 1) {	
					StartCoroutine (animate (time,list,index+1,obj));
				}
			}
			else if ((!Application.isMobilePlatform && Input.GetKey (KeyCode.A)) || (!Application.isMobilePlatform && Input.GetKey (KeyCode.LeftArrow)) || (Application.isMobilePlatform && moveleft)) {
				CmdChangeSprite (str);
				if (index < list.Count - 1) {	
					StartCoroutine (animate (time,list,index+1,obj));
				}
			}
		}
		else if (list == sprites.Ljumpright) {
			str = "Jumprightsprite;" + index;
			CmdChangeSprite (str);
			if (index < list.Count - 1) {	
				StartCoroutine (animate (time,list,index+1,obj));
			}
		}
	}


	//PHOTON NETWORKING

	//PHOTON NETWORKING
	/*
	private void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext(CorgiAnimator.GetBool("Crouching"));
			stream.SendNext(CorgiAnimator.GetBool("Crawling"));
		} else {
			MMAnimator.UpdateAnimatorBool(CorgiAnimator, "Crouching", (bool)stream.ReceiveNext ());
			MMAnimator.UpdateAnimatorBool(CorgiAnimator, "Crawling", (bool)stream.ReceiveNext ());
		}
	}
	*/

	[PunRPC]
	public void FlipSprite(bool newfacingright)
	{
		if ((GetComponent<Character>().IsFacingRight && !newfacingright) || (!GetComponent<Character>().IsFacingRight && newfacingright)) {
			GetComponent<Character>().Flip();
		}
		newfacingright = facingright;
	}

	[PunRPC]
	public void ServerRemoveBlock(string gridposString)
	{
		if (camfollow.GetComponent<CameraFollow> ().blockmapd.ContainsKey (gridposString)) {
			string blocktag = camfollow.GetComponent<CameraFollow> ().blockmapd [gridposString].GetComponent<SnapToGrid> ().blocktag;
			Transform blocktransform = camfollow.GetComponent<CameraFollow> ().blockmapd [gridposString].transform;

			camfollow.GetComponent<CameraFollow> ().blockmapd.Remove (gridposString);
			camfollow.GetComponent<CameraFollow> ().mapd.Remove (gridposString);

			//Spawn individual effects
			if (blocktag == "brick") {
				Instantiate (camfollow.GetComponent<CameraFollow> ().brickeffect, blocktransform.position, blocktransform.rotation);
			} else if (blocktag == "bomb") {
				Instantiate (camfollow.CrateExplode,blocktransform.position, Quaternion.identity);
			}
		}
	}

	public static void AddExplosionForce(GameObject body, float expForce, Vector3 expPosition, float expRadius)
	{

		var dir = (body.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0)
		{
			calc = 0;
		}
		CorgiController corgicontroller = body.GetComponent<CorgiController> ();
		corgicontroller.AddForce (dir.normalized * expForce * calc);
	}


	public GameObject SpawnBlock(string mcord,string myvalue)
	{
		string[] tokens = mcord.Split(',');

		int xcord = int.Parse(tokens[0]);
		int ycord = int.Parse(tokens[1]);

		//string bcord = item.Value;

		//xcord,ycord,block
		double multiplier = 1.00;
		float multiplierf = (float)multiplier;
		float xcordf = (float)xcord * multiplierf;
		float ycordf = (float)ycord * multiplierf;

		if (xcord > mapxmax)
		{
			mapxmax = xcord;
		}

		if (ycord > mapymax)
		{
			mapymax = ycord;
		}

		if (xcord < mapxmin)
		{
			mapxmin = xcord;
		}

		if (ycord < mapymin)
		{
			mapymin = ycord;
		}

		//setblock=
		if (myvalue.Contains( "b1")) { block = b1; } else if (myvalue.Contains( "b2")) { block = b2; } else if (myvalue.Contains( "b3")) { block = b3; } else if (myvalue.Contains( "b4")) { block = b4; } else if (myvalue.Contains( "brick")) { block = brick; } else if (myvalue.Contains( "finish")) { block = finish; } else if (myvalue.Contains( "ice")) { block = ice; } else if (myvalue.Contains( "itemonce")) { block = itemonce; } else if (myvalue.Contains( "iteminf")) { block = iteminf; } else if (myvalue.Contains( "leftarrow")) { block = leftarrow; } else if (myvalue.Contains( "rightarrow")) { block = rightarrow; } else if (myvalue.Contains( "uparrow")) { block = uparrow; } else if (myvalue.Contains( "downarrow")) { block = downarrow; } else if (myvalue.Contains( "bomb")) { block = bomb; } else if (myvalue.Contains( "crumble")) { block = crumble; } else if (myvalue.Contains( "vanish")) { block = vanish; } else if (myvalue.Contains( "move")) { block = move; } else if (myvalue.Contains( "rotateright")) { block = rotateright; } else if (myvalue.Contains( "rotateleft")) { block = rotateleft; } else if (myvalue.Contains( "push")) { block = push; } else if (myvalue.Contains( "happy")) { block = happy; } else if (myvalue.Contains( "sad")) { block = sad; } else if (myvalue.Contains( "net")) { block = net; } else if (myvalue.Contains( "heart")) { block = heart; } else if (myvalue.Contains( "time")) { block = time; } else if (myvalue.Contains( "water")) { block = water; } else if (myvalue.Contains( "start")) { block = start; } else if (myvalue.Contains( "stop")) { block = stop; } else if (myvalue.Contains( "ladder")) { block = ladder; } else if (myvalue.Contains( "antigravity")) { block = antigravity; } else if (myvalue.Contains( "bouncy")) { block = bouncy; } else if (myvalue.Contains( "spike")) { block = spike; }  else if (myvalue.Contains( "checkpoint")) { block = checkpoint; }  else if (myvalue.Contains( "portal")) { block = portal; }  else if (myvalue.Contains( "door")) { block = door; }  else if (myvalue.Contains( "oneway")) { block = oneway; }  else if (myvalue.Contains( "rocket")) { block = rocket; }  else if (myvalue.Contains( "falling")) { block = falling; }  else if (myvalue.Contains( "giant")) { block = giant; }  else if (myvalue.Contains( "tiny")) { block = tiny; }  else if (myvalue.Contains( "sticky")) { block = sticky; }  else if (myvalue.Contains( "fan")) { block = fan; }  else if (myvalue.Contains( "key")) { block = key; }  else if (myvalue.Contains( "lock")) { block = locked; }  else if (myvalue.Contains( "weapon")) { block = weapon; }  else if (myvalue.Contains( "npc")) { block = npc; }  else if (myvalue.Contains( "sign")) { block = sign; }  else if (myvalue.Contains( "timefreeze")) { block = timefreeze; }

		//Spawn a specific color of portal
		if (block == portal) {
			if (myvalue.Length>=7)
			{
				int selectedportal = int.Parse(myvalue.Substring(6,1));
				if (selectedportal == 0) {block = portal0;} else if (selectedportal == 1) {block = portal1;} else if (selectedportal == 2) {block = portal2;} else if (selectedportal == 3) {block = portal3;} else if (selectedportal == 4) {block = portal4;} else if (selectedportal == 5) {block = portal5;} else if (selectedportal == 6) {block = portal6;} else if (selectedportal == 7) {block = portal7;} else if (selectedportal == 8) {block = portal8;} else if (selectedportal == 9) {block = portal9;}
			}
		}
		//Spawn a specific color of keylock
		if (block == key || block==locked) {
			string selectedkeylock = myvalue.Substring(4,2);
			if (selectedkeylock == "01") {block = key0;} else if (selectedkeylock == "11") {block = key1;} else if (selectedkeylock == "21") {block = key2;} else if (selectedkeylock == "31") {block = key3;} else if (selectedkeylock == "41") {block = key4;} else if (selectedkeylock == "51") {block = key5;} else if (selectedkeylock == "61") {block = key6;} else if (selectedkeylock == "71") {block = key7;} else if (selectedkeylock == "81") {block = key8;} else if (selectedkeylock == "91") {block = key9;}
			if (selectedkeylock == "02") {block = locked0;} else if (selectedkeylock == "12") {block = locked1;} else if (selectedkeylock == "22") {block = locked2;} else if (selectedkeylock == "32") {block = locked3;} else if (selectedkeylock == "42") {block = locked4;} else if (selectedkeylock == "52") {block = locked5;} else if (selectedkeylock == "62") {block = locked6;} else if (selectedkeylock == "72") {block = locked7;} else if (selectedkeylock == "82") {block = locked8;} else if (selectedkeylock == "92") {block = locked9;}
		}
		//CmdSpawnBlock(block, xcordf, ycordf,myvalue);
		var myblock = Instantiate(block, new Vector3(xcordf, ycordf, 0), Quaternion.identity);
		myblock.name = myvalue;
		mapb.Add (myblock);

		//Set sign values from data
		if (myvalue.Contains ("sign")) {
			string signtext = myvalue.Substring (4);
			string[] dialogue = { signtext };
			if (myblock.GetComponent < DialogueZone>() != null) {
				myblock.GetComponent<DialogueZone> ().Dialogue = dialogue;
			}
		}
		return myblock;
	}

	void RedoAction()
	{
		photonView.RPC ("RedoActionForAll", PhotonTargets.All);
	}
	void UndoAction()
	{
		photonView.RPC ("UndoActionForAll", PhotonTargets.All);
	}

	[PunRPC]
	void RedoActionForAll()
	{
		//Redo
		if (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Count >= 1) {
			if (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Last () == "add") {
				//Remove addition of block
				string gridposstring = camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Last ();
				string blockstring = camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Last ();
				//Add last redo action to a cache
				camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Add (camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdValueCache.Add (camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdHistory.Add (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Last ());

				camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Count - 1);

				camfollow.GetComponent<CameraFollow> ().mapd.Add (gridposstring, blockstring);
				GameObject block= SpawnBlock (gridposstring, blockstring);
				camfollow.GetComponent<CameraFollow> ().blockmapd.Add (gridposstring, block);

				camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Count - 1);
				camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Count - 1);


			} else if (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Last () == "remove") {
				//Add back removal of block
				string gridposstring2 = camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Last ();
				string blockstring2 = camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Last ();

				//Add last undo action to a cache
				camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Add (camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdValueCache.Add (camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdHistory.Add (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Last ());


				if (camfollow.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposstring2)) {
					camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Count - 1);

					camfollow.GetComponent<CameraFollow> ().mapd.Remove (gridposstring2);
					camfollow.GetComponent<CameraFollow> ().blockmapd.Remove (gridposstring2);

					camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Count - 1);
					camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Count - 1);



				}
			}
		}
	}

	[PunRPC]
	void UndoActionForAll()
	{
		//Undo
		if (camfollow.GetComponent<CameraFollow> ().mapdHistory.Count >= 1) {
			if (camfollow.GetComponent<CameraFollow> ().mapdHistory.Last () == "add") {
				//Remove addition of block
				string gridposstring = camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Last ();
				string blockstring = camfollow.GetComponent<CameraFollow> ().mapdValueCache.Last ();

				//Add last undo action to a cache
				camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Add (camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Add (camfollow.GetComponent<CameraFollow> ().mapdValueCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Add (camfollow.GetComponent<CameraFollow> ().mapdHistory.Last ());


				if (camfollow.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposstring)) {
					camfollow.GetComponent<CameraFollow> ().mapdHistory.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdHistory.Count - 1);

					camfollow.GetComponent<CameraFollow> ().mapd.Remove (gridposstring);
					camfollow.GetComponent<CameraFollow> ().blockmapd.Remove (gridposstring);

					camfollow.GetComponent<CameraFollow> ().mapdKeyCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Count - 1);
					camfollow.GetComponent<CameraFollow> ().mapdValueCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdValueCache.Count - 1);



				}
			} else if (camfollow.GetComponent<CameraFollow> ().mapdHistory.Last () == "remove") {
				//Add back removal of block
				string gridposstring2 = camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Last ();
				string blockstring2 = camfollow.GetComponent<CameraFollow> ().mapdValueCache.Last ();

				//Add last undo action to a cache
				camfollow.GetComponent<CameraFollow> ().mapdUndoKeyCache.Add (camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdUndoValueCache.Add (camfollow.GetComponent<CameraFollow> ().mapdValueCache.Last ());
				camfollow.GetComponent<CameraFollow> ().mapdUndoHistory.Add (camfollow.GetComponent<CameraFollow> ().mapdHistory.Last ());

				camfollow.GetComponent<CameraFollow> ().mapdHistory.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdHistory.Count - 1);

				camfollow.GetComponent<CameraFollow> ().mapd.Add (gridposstring2, blockstring2);
				GameObject block= SpawnBlock (gridposstring2, blockstring2);
				camfollow.GetComponent<CameraFollow> ().blockmapd.Add (gridposstring2, block);

				camfollow.GetComponent<CameraFollow> ().mapdKeyCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdKeyCache.Count - 1);
				camfollow.GetComponent<CameraFollow> ().mapdValueCache.RemoveAt (camfollow.GetComponent<CameraFollow> ().mapdValueCache.Count - 1);

			}
		}
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		List<RaycastResult> tempresults = new List<RaycastResult>();

		foreach (RaycastResult obj in results) {
			if (obj.gameObject.tag != "chatUI") {
				//Ignore chat top panel
				tempresults.Add(obj);
			}
		}
		return tempresults.Count > 0;
	}



}

