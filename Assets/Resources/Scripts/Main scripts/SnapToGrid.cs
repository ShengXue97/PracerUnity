using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
public class SnapToGrid : MonoBehaviour
{
	public bool isSelected=false;
	public bool CanRelease = false;
	private Camera mcam;
	public bool used = false;
	public string fading = "none";
	public Vector2 pos;
	private Controls player;
	public string colside = "top";
	public Transform brickparticle;
	public float startTime;
	public float maximum;
	public float minimum;
	public float time;
	public int crumblehealth = 100;
	public string[] blocka = { "b1", "b2", "b3", "b4", "brick", "finish", "ice", "itemonce", "iteminf", "leftarrow", "rightarrow", "uparrow", "downarrow", "bomb", "crumble", "vanish", "move", "rotateleft", "rotateright", "push", "happy", "sad", "net", "heart", "time", "water","start","stop","ladder","antigravity","bouncy","spike"};
	public string[] newitems7 = {"jetpack","speedburst","superjump","bomb","machinegun","grenadelauncher", "rocketlauncher", "shotgun" ,"meleeattack","lightning"};
	//public string[] itemarray2 = {"laser","jetpack","lightning","teleport","speedburst","superjump","sword","bomb","blockitem"};
	public string[] weaponarray5 = {"machinegun","grenadelauncher", "rocketlauncher", "shotgun" ,"meleeattack"};
	public float xspeedconst = -10.5f;
	public float xspeed=-10.5f;
	public float yspeedconst = 10.5f;
	public float yspeed=10.5f;
	public GameObject leftchild;
	public GameObject rightchild;
	public GameObject topchild;
	public GameObject bottomchild;
	public Vector3 startpos;
	public Camera LocalCamera;
	public bool canexit = false;
	public float Power;
	public float Radius;
	public bool exploding=false;
	public List<Sprite> frames;
	public List<Sprite> bouncyframes;
	public bool previewing=false;
	public Rigidbody2D rb;
	public string direction="";
	public string blocktag;
	private Controls controls;
	public GameObject lastplayer;
	public string arrowblocktag="";
	public bool canactivatekey = true;
	public string gridposString;
	public bool gettingitem=false;
	public bool isColliding=false;
	public string BlockValue="";
	public bool WaitingForUpArrow=false;
	public bool IsSlightlyUp=false;
	public bool PlayerOnTop=false;
	public float FallingStartTime;
	public bool WaitingToBeDestroyed=false;
	public bool alwaysActiveHorizontal = false;
	public bool alwaysActiveVertical=false;
	public bool CheckedalwaysActiveHorizontal = false;
	public bool CheckedalwaysActiveVertical = false;
	public float CreationTime;
	public bool MovingRight;
	// Use this for initialization

	void Start()
	{
		CreationTime = Time.time;
		mcam = Camera.main;
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine (EnablePreview (5f));
		//var r = GetComponent<RectTransform> ().sizeDelta;
		//print ("x" + r.x + ",y" + r.y);
		//gameObject.transform.SetParent (mcam.GetComponent<CameraFollow>().Master.transform);
		//Auto Rotates according to camera=
		//GetComponent<Transform>().rotation=mcam.GetComponent<CameraFollow> ().UICanvas.GetComponent<RectTransform> ().rotation;

		//Snap to grid
		pos = transform.position;

		float val5 = (float)Mathf.Round (pos.x);

		float val10 = (float)Mathf.Round (pos.y);

		//print(pos);
		Vector2 temp = new Vector2 (val5, val10);
		if (name != "bombitem") {
			transform.position = temp;
		}
		blocktag = gameObject.name;
		//Set all move checks to the proper positions
		if (blocktag!="push")
		{
			//push causing unknown errors
			transform.GetChild(0).transform.position=transform.position-new Vector3(0.5f,0f,0f);
			transform.GetChild(1).transform.position=transform.position+new Vector3(0.5f,0f,0f);
			transform.GetChild(2).transform.position=transform.position-new Vector3(0f,0.5f,0f);
			transform.GetChild(3).transform.position=transform.position+new Vector3(0f,0.5f,0f);
		}

		Vector2 gridpos = GetGrid (pos, true);
		int gridx = (int)gridpos.x;
		int gridy = (int)gridpos.y;
		gridposString = gridx.ToString ().Substring (0, gridx.ToString ().Length) + "," + gridy.ToString ().Substring (0, gridy.ToString ().Length);

		if (mcam.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposString)) {
			BlockValue = mcam.GetComponent<CameraFollow> ().mapd [gridposString];
		}
		//Hide arrows
		if (gameObject.transform.childCount ==6) {
			gameObject.transform.GetChild(5).gameObject.SetActive(false);  
		}
		else if (gameObject.transform.childCount ==7) {
			gameObject.transform.GetChild(6).gameObject.SetActive(false);  
		}

		if (blocktag == "bouncy") {

			bouncyframes.Add (mcam.GetComponent<CameraFollow> ().bouncy1);
			bouncyframes.Add (mcam.GetComponent<CameraFollow> ().bouncy2);
			bouncyframes.Add (mcam.GetComponent<CameraFollow> ().bouncy3);
			bouncyframes.Add (mcam.GetComponent<CameraFollow> ().bouncy4);
			bouncyframes.Add (mcam.GetComponent<CameraFollow> ().bouncy5);
		}
		startpos = transform.position;

		///Gets type of block
		Physics.IgnoreLayerCollision(gameObject.layer, 30);
		if (name.Contains ("_mr") || name.Contains ("_ml") || name.Contains ("_mu") || name.Contains ("_md")) {
			blocktag = blocktag.Substring (0, blocktag.Length - 3);

			if (name.Contains ("_mr")) {
				direction = "_mr";
				//Send out "MovingCheck" in a horizontal direction, so that those blocks that come into contact
				GameObject movecheck= Instantiate(mcam.GetComponent<CameraFollow>().MoveHorizontalCheck,transform.position,Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "horizontal";
			} else if (name.Contains ("_ml")) {
				direction = "_ml";
				GameObject movecheck= Instantiate(mcam.GetComponent<CameraFollow>().MoveHorizontalCheck,transform.position,Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "horizontal";
			}
			else if (name.Contains ("_mu")) {
				direction = "_mu";
				GameObject movecheck= Instantiate(mcam.GetComponent<CameraFollow>().MoveVerticalCheck,transform.position,Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "vertical";
			}
			else if (name.Contains ("_md")) {
				direction = "_md";
				GameObject movecheck= Instantiate(mcam.GetComponent<CameraFollow>().MoveVerticalCheck,transform.position,Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "vertical";
			}
		}


		StartCoroutine (SpawnMoveCheck ());

		//Draws direction sprite
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			if (direction== ("_mr") || direction== ("_ml") || direction== ("_mu") || direction== ("_md")) {
				var dirsprite = gameObject.transform.GetChild (4).GetComponent<SpriteRenderer> ();
				if (direction== ("_mr")) {
					dirsprite.sprite = mcam.GetComponent<CameraFollow> ().righttrans;
				} else if (direction== ("_ml")) {
					dirsprite.sprite = mcam.GetComponent<CameraFollow> ().lefttrans;
				} else if (direction== ("_mu")) {
					dirsprite.sprite = mcam.GetComponent<CameraFollow> ().uptrans;
				} else if (direction== ("_md")) {
					dirsprite.sprite = mcam.GetComponent<CameraFollow> ().downtrans;
				}
			}
		}

		//Disable rigibody if its not a moving object or block to be moved
		if (direction == "" && blocktag != "crumble" && blocktag != "move" && blocktag != "push" && blocktag!="rotateleft" && blocktag!="rotateright" && blocktag!="falling") {
			Destroy (rb);
		}

		//Hide stop blocks in level1
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			if (blocktag == "stop") {
				GetComponent<SpriteRenderer>().enabled=false;
			}
		}



	}


	// Update is called once per frame
	void Update()
	{
		/*
		//Disable objects that are not visible
		Vector2 playergridpos = GetGrid(mcam.GetComponent<CameraFollow>().player.gameObject.transform.position, true);
		Vector2 objgridpos = GetGrid (transform.position, true);
		float px = playergridpos.x;
		float py = playergridpos.y;
		float ox = objgridpos.x;
		float oy = objgridpos.y;

		if (Mathf.Abs(px-ox)>32f && Mathf.Abs(py-oy)>32f) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
		*/
		if (alwaysActiveHorizontal && !CheckedalwaysActiveHorizontal && !alwaysActiveVertical) {
			bool NoMovingBlockLeft = false;
			bool NoMovingBlockRight = false;

			CheckedalwaysActiveHorizontal = true;

			Collider2D[] blockcollidersRight = Physics2D.OverlapCircleAll (transform.position+ Vector3.right, 0.01f,mcam.GetComponent<CameraFollow>().whatIsGround);

			if (blockcollidersRight.Length > 0) {
				if (blockcollidersRight [0].GetComponent<SnapToGrid> () != null && blockcollidersRight [0].gameObject!=gameObject) {
					if (blockcollidersRight [0].GetComponent<SnapToGrid> ().direction == "") {
						NoMovingBlockRight = true;
					}
				}
			}

			Collider2D[] blockcollidersLeft = Physics2D.OverlapCircleAll (transform.position-Vector3.right, 0.01f,mcam.GetComponent<CameraFollow>().whatIsGround);
			if (blockcollidersLeft.Length > 0) {
				if (blockcollidersLeft [0].GetComponent<SnapToGrid> () != null && blockcollidersLeft [0].gameObject!=gameObject) {
					if (blockcollidersLeft [0].GetComponent<SnapToGrid> ().direction == "") {
						NoMovingBlockLeft = true;
					}
				}
			}
			if (NoMovingBlockLeft && NoMovingBlockRight) {
				alwaysActiveHorizontal = false;
				GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
			}
		}

		if (alwaysActiveVertical && !CheckedalwaysActiveVertical && !alwaysActiveHorizontal) {
			bool NoMovingBlockTop = false;
			bool NoMovingBlockBottom = false;

			CheckedalwaysActiveVertical = true;

			Collider2D[] blockcollidersTop = Physics2D.OverlapCircleAll (transform.position+ Vector3.up, 0.01f,mcam.GetComponent<CameraFollow>().whatIsGround);

			if (blockcollidersTop.Length > 0) {
				if (blockcollidersTop [0].GetComponent<SnapToGrid> () != null && blockcollidersTop [0].gameObject!=gameObject) {
					if (blockcollidersTop [0].GetComponent<SnapToGrid> ().direction == "") {
						NoMovingBlockTop = true;
					}
				}
			}

			Collider2D[] blockcollidersBottom = Physics2D.OverlapCircleAll (transform.position-Vector3.up, 0.01f,mcam.GetComponent<CameraFollow>().whatIsGround);
			if (blockcollidersBottom.Length > 0) {
				if (blockcollidersBottom [0].GetComponent<SnapToGrid> () != null && blockcollidersBottom [0].gameObject!=gameObject) {
					if (blockcollidersBottom [0].GetComponent<SnapToGrid> ().direction == "") {
						NoMovingBlockBottom = true;
					}
				}
			}

			if (NoMovingBlockTop && NoMovingBlockBottom) {
				alwaysActiveVertical = false;
				//GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
			}
		}



		if (!GetComponent<Renderer>().isVisible)
		{
			if (gameObject.tag != "start" && Time.time-CreationTime>3f) {
				if (direction == "" && !alwaysActiveVertical && !alwaysActiveHorizontal) {
					gameObject.SetActive (false);
					return;
				} else if (alwaysActiveVertical || alwaysActiveHorizontal) {
					GetComponent<SpriteRenderer> ().enabled = false;
					if (GetComponent<Rigidbody2D> () != null) {
						GetComponent<Rigidbody2D> ().Sleep ();
					}
					GetComponent<SnapToGrid> ().enabled = false;
				}
			}

		}
		if (mcam != null) {
			if (mcam.GetComponent<CameraFollow> () != null) {
			}
		}


		//Remove blocks
		if (!mcam.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposString) && !name.Contains ("bombitem") && !name.Contains ("blockitem")) {
			mcam.GetComponent<CameraFollow> ().blockmapd.Remove (gridposString);
			if ((direction == "_ml" || direction == "_mr")) {
				WaitingToBeDestroyed = true;

				if (!blocktag.Contains ("arrow")) {
					//If the object has a moving child
					if (transform.childCount > 5) {

						transform.GetChild (5).transform.GetChild (0).gameObject.transform.position = transform.GetChild (5).transform.position - new Vector3 (0.5f, 0f, 0f);
						transform.GetChild (5).transform.SetParent (null);
					}
				}
				else if (blocktag.Contains ("arrow")) {
					//If the object has a moving child
					if (transform.childCount > 6) {

						transform.GetChild (6).transform.GetChild (0).gameObject.transform.position = transform.GetChild (6).transform.position-new Vector3(0.5f,0f,0f);
						transform.GetChild (6).transform.SetParent (null);
					}
				}

				//If the object has a parent
				if (transform.parent != null)
				{
					transform.parent.GetChild (1).transform.position = transform.parent.transform.position + new Vector3 (0.5f, 0f, 0f);

					transform.parent.gameObject.GetComponent<SnapToGrid> ().rightchild = transform.parent.GetChild (1).gameObject;
				}
			}
			else if ((direction == "_mu" || direction == "_md")) {
				WaitingToBeDestroyed = true;

				if (!blocktag.Contains ("arrow")) {
					//If the object has a moving child
					if (transform.childCount > 5) {

						transform.GetChild (5).transform.GetChild (2).gameObject.transform.position = transform.GetChild (5).transform.position - new Vector3 (0f, 0.5f, 0f);
						transform.GetChild (5).transform.SetParent (null);
					}
				}
				else if (blocktag.Contains ("arrow")) {
					//If the object has a moving child
					if (transform.childCount > 6) {

						transform.GetChild (6).transform.GetChild (2).gameObject.transform.position = transform.GetChild (6).transform.position-new Vector3(0f,0.5f,0f);
						transform.GetChild (6).transform.SetParent (null);
					}
				}

				//If the object has a parent
				if (transform.parent != null)
				{
					transform.parent.GetChild (3).transform.position = transform.parent.transform.position + new Vector3 (0f, 0.5f, 0f);

					transform.parent.gameObject.GetComponent<SnapToGrid> ().topchild = transform.parent.GetChild (3).gameObject;
				}
			}

			Destroy (gameObject);
		} else {
			//Select a block and move it
			if (mcam.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposString)) {
				if (mcam.GetComponent<CameraFollow> ().mapd [gridposString].Length >= 11) {
					if (mcam.GetComponent<CameraFollow> ().mapd [gridposString].Substring (mcam.GetComponent<CameraFollow> ().mapd [gridposString].Length - 10, 10) == "IsSelected" && blocktag!="start") {
						isSelected = true;
						//transform.position = mcam.GetComponent<CameraFollow> ().cursor.transform.position;

						if ((direction == "_ml" || direction == "_mr")) {
							WaitingToBeDestroyed = true;

							if (!blocktag.Contains ("arrow")) {
								//If the object has a moving child
								if (transform.childCount > 5) {

									transform.GetChild (5).transform.GetChild (0).gameObject.transform.position = transform.GetChild (5).transform.position - new Vector3 (0.5f, 0f, 0f);
									transform.GetChild (5).transform.SetParent (null);
								}
							}
							else if (blocktag.Contains ("arrow")) {
								//If the object has a moving child
								if (transform.childCount > 6) {

									transform.GetChild (6).transform.GetChild (0).gameObject.transform.position = transform.GetChild (6).transform.position-new Vector3(0.5f,0f,0f);
									transform.GetChild (6).transform.SetParent (null);
								}
							}

							//If the object has a parent
							if (transform.parent != null)
							{
								transform.parent.GetChild (1).transform.position = transform.parent.transform.position + new Vector3 (0.5f, 0f, 0f);

								transform.parent.gameObject.GetComponent<SnapToGrid> ().rightchild = transform.parent.GetChild (1).gameObject;
								transform.SetParent (null);
							}

							//Reset left and right check
							transform.GetChild(0).transform.position=transform.position-new Vector3(0.5f,0f,0f);
							transform.GetChild(1).transform.position=transform.position+new Vector3(0.5f,0f,0f);
							rightchild = transform.GetChild (1).gameObject;
						}

						else if ((direction == "_mu" || direction == "_md")) {
							WaitingToBeDestroyed = true;

							if (!blocktag.Contains ("arrow")) {
								//If the object has a moving child
								if (transform.childCount > 5) {

									transform.GetChild (5).transform.GetChild (2).gameObject.transform.position = transform.GetChild (5).transform.position - new Vector3 (0f, 0.5f, 0f);
									transform.GetChild (5).transform.SetParent (null);
								}
							}
							else if (blocktag.Contains ("arrow")) {
								//If the object has a moving child
								if (transform.childCount > 6) {

									transform.GetChild (6).transform.GetChild (2).gameObject.transform.position = transform.GetChild (6).transform.position -new Vector3(0f,0.5f,0f);
									transform.GetChild (6).transform.SetParent (null);
								}
							}

							//If the object has a parent
							if (transform.parent != null)
							{
								transform.parent.GetChild (3).transform.position = transform.parent.transform.position + new Vector3 (0f, 0.5f, 0f);

								transform.parent.gameObject.GetComponent<SnapToGrid> ().topchild = transform.parent.GetChild (3).gameObject;
								transform.SetParent (null);
							}

							//Reset top and bottom check
							transform.GetChild(2).transform.position=transform.position-new Vector3(0f,0.5f,0f);
							transform.GetChild(3).transform.position=transform.position+new Vector3(0f,0.5f,0f);
							topchild = transform.GetChild (3).gameObject;
						}
					}
				}
			}
		}
		if (CanRelease) {
			//End selected block
			if (isSelected && mcam.GetComponent<CameraFollow> ().mapd.ContainsKey(gridposString)) {
				CanRelease=false;
				mcam.GetComponent<CameraFollow> ().cursor.GetComponent<SpriteRenderer> ().sprite = mcam.GetComponent<CameraFollow> ().CursorDefault;

				mcam.GetComponent<CameraFollow> ().mapd [gridposString] = mcam.GetComponent<CameraFollow> ().mapd [gridposString].Substring (0, mcam.GetComponent<CameraFollow> ().mapd [gridposString].Length - 10);
				isSelected = false;
				WaitingToBeDestroyed = false;
				//Add to undo list
				if (mcam.GetComponent<CameraFollow>().mapd [gridposString] != "start") {
					mcam.GetComponent<CameraFollow>().mapdKeyCache.Add (gridposString);
					mcam.GetComponent<CameraFollow>().mapdValueCache.Add (mcam.GetComponent<CameraFollow>().mapd [gridposString]);
					mcam.GetComponent<CameraFollow>().mapdHistory.Add ("remove");
					mcam.GetComponent<CameraFollow>().mapd.Remove (gridposString);
					mcam.GetComponent<CameraFollow>().blockmapd.Remove (gridposString);

				}


			

				pos = transform.position;
				blocktag = gameObject.name;
				Vector2 gridpos = GetGrid (pos, true);
				int gridx = (int)gridpos.x;
				int gridy = (int)gridpos.y;
				gridposString = gridx.ToString ().Substring (0, gridx.ToString ().Length) + "," + gridy.ToString ().Substring (0, gridy.ToString ().Length);


				double val1 = pos.x / 1.00;
				float val2 = (float)val1;

				double val3 = 1.00;
				float val4 = (float)val3;
				float val5 = (float)Mathf.Round (val2) * val4;

				double val6 = pos.y / 1.00;
				float val7 = (float)val6;

				double val8 = 1.00;
				float val9 = (float)val8;
				float val10 = (float)Mathf.Round (val7) * val9;

				Vector2 temp = new Vector2 (val5, val10);
				if (name != "bombitem") {
					transform.position = temp;
				}
				startpos = transform.position;

				//Remove existing block at new location
				if (mcam.GetComponent<CameraFollow> ().mapd.ContainsKey (gridposString)) {
					//Add to undo list
					if (mcam.GetComponent<CameraFollow>().mapd [gridposString] != "start") {
						mcam.GetComponent<CameraFollow>().mapdKeyCache.Add (gridposString);
						mcam.GetComponent<CameraFollow>().mapdValueCache.Add (mcam.GetComponent<CameraFollow>().mapd [gridposString]);
						mcam.GetComponent<CameraFollow>().mapdHistory.Add ("remove");
						mcam.GetComponent<CameraFollow>().mapd.Remove (gridposString);
						mcam.GetComponent<CameraFollow>().blockmapd.Remove (gridposString);
					}
				}
				mcam.GetComponent<CameraFollow> ().mapd.Add (gridposString,BlockValue);
				mcam.GetComponent<CameraFollow> ().blockmapd.Add (gridposString,gameObject);
				//Add to undo list
				if (mcam.GetComponent<CameraFollow>().mapd [gridposString] != "start") {
					mcam.GetComponent<CameraFollow>().mapdKeyCache.Add (gridposString);
					mcam.GetComponent<CameraFollow>().mapdValueCache.Add (BlockValue);
					mcam.GetComponent<CameraFollow>().mapdHistory.Add ("add");
				}

				//Make new always active blocks
				if (name.Contains ("_mr")) {
					direction = "_mr";
					//Send out "MovingCheck" in a horizontal direction, so that those blocks that come into contact
					Instantiate(mcam.GetComponent<CameraFollow>().MoveHorizontalCheck,transform.position,Quaternion.identity);
				}
				else if (name.Contains ("_ml")) {
					direction = "_ml";
					Instantiate(mcam.GetComponent<CameraFollow>().MoveHorizontalCheck,transform.position,Quaternion.identity);
				}
				else if (name.Contains ("_mu")) {
					direction = "_mu";
					Instantiate(mcam.GetComponent<CameraFollow>().MoveVerticalCheck,transform.position,Quaternion.identity);
				}
				else if (name.Contains ("_md")) {
					direction = "_md";
					Instantiate(mcam.GetComponent<CameraFollow>().MoveVerticalCheck,transform.position,Quaternion.identity);
				}

				Collider2D[] blockcolliders = Physics2D.OverlapCircleAll (transform.position, 0.3f,mcam.GetComponent<CameraFollow>().whatIsGround);
				foreach (Collider2D other in blockcolliders) {
					if (other.gameObject.tag != "Player" && other.gameObject!=gameObject && other.gameObject.tag!="start") {
						if (other.gameObject.GetComponent<SnapToGrid> () != null) {
							GameObject TargetBlock = other.gameObject;
							Transform TargetTransform = TargetBlock.transform;
							SnapToGrid TargetSnap = TargetBlock.GetComponent<SnapToGrid> ();

							if ((TargetSnap.direction == "_ml" || TargetSnap.direction == "_mr")) {
								TargetSnap.WaitingToBeDestroyed = true;

								if (!TargetSnap.blocktag.Contains ("arrow")) {
									//If the object has a moving child
									if (TargetTransform.childCount > 5) {

										TargetTransform.GetChild (5).transform.GetChild (0).gameObject.transform.position = TargetTransform.GetChild (5).transform.position - new Vector3 (0.5f, 0f, 0f);
										TargetTransform.GetChild (5).transform.SetParent (null);
									}
								} else if (TargetSnap.blocktag.Contains ("arrow")) {
									//If the object has a moving child
									if (TargetTransform.childCount > 6) {

										TargetTransform.GetChild (6).transform.GetChild (0).gameObject.transform.position = TargetTransform.GetChild (6).transform.position - new Vector3 (0.5f, 0f, 0f);
										TargetTransform.GetChild (6).transform.SetParent (null);
									}
								}

								//If the object has a parent
								if (TargetTransform.parent != null) {
									TargetTransform.parent.GetChild (1).transform.position = TargetTransform.parent.transform.position + new Vector3 (0.5f, 0f, 0f);

									TargetTransform.parent.gameObject.GetComponent<SnapToGrid> ().rightchild = TargetTransform.parent.GetChild (1).gameObject;
								}
							}

							else if ((TargetSnap.direction == "_mu" || TargetSnap.direction == "_md")) {
								TargetSnap.WaitingToBeDestroyed = true;

								if (!TargetSnap.blocktag.Contains ("arrow")) {
									//If the object has a moving child
									if (TargetTransform.childCount > 5) {

										TargetTransform.GetChild (5).transform.GetChild (2).gameObject.transform.position = TargetTransform.GetChild (5).transform.position - new Vector3 (0f, 0.5f, 0f);
										TargetTransform.GetChild (5).transform.SetParent (null);
									}
								} else if (TargetSnap.blocktag.Contains ("arrow")) {
									//If the object has a moving child
									if (TargetTransform.childCount > 6) {

										TargetTransform.GetChild (6).transform.GetChild (2).gameObject.transform.position = TargetTransform.GetChild (6).transform.position - new Vector3 (0f, 0.5f, 0f);
										TargetTransform.GetChild (6).transform.SetParent (null);
									}
								}

								//If the object has a parent
								if (TargetTransform.parent != null) {
									TargetTransform.parent.GetChild (3).transform.position = TargetTransform.parent.transform.position + new Vector3 (0f, 0.5f, 0f);

									TargetTransform.parent.gameObject.GetComponent<SnapToGrid> ().topchild = TargetTransform.parent.GetChild (3).gameObject;
								}
							}
						}

						Destroy (other.gameObject);
					}
				}

			}
		}
		//Set water back to normal transparency
		if (blocktag == "water") {
			if (GetComponent<SpriteRenderer> ().color.a != 1f) {
				Collider2D[] watercolliders = Physics2D.OverlapCircleAll (transform.position, 1f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
				if (watercolliders.Length == 0) {
					GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
				}
			}
		}
		//Link Portals
		if (blocktag.Contains( "portal")) {
			for (int i = 0; i <= 9; i++) {
				GameObject tportal1 = null;
				GameObject tportal2 = null;
				if (name .Contains( "portal" + i.ToString () + "1")) {
					tportal2 = GameObject.Find ("portal" + i.ToString () + "2");
					if (tportal2 == null) {
						tportal2 = GameObject.Find ("portal" + i.ToString () + "2" + "_mr");
					}
					if (tportal2 == null) {
						tportal2 = GameObject.Find ("portal" + i.ToString () + "2" + "_ml");
					}
					if (tportal2 == null) {
						tportal2 = GameObject.Find ("portal" + i.ToString () + "2" + "_mu");
					}
					if (tportal2 == null) {
						tportal2 = GameObject.Find ("portal" + i.ToString () + "2" + "_md");
					}

					if (tportal2 != null) {
						GetComponent<Teleporter> ().Destination = tportal2.GetComponent<Teleporter> ();
					}
				} else if (name .Contains( "portal" + i.ToString () + "2")) {
					tportal1 = GameObject.Find ("portal" + i.ToString () + "1");

					if (tportal1 == null) {
						tportal1 = GameObject.Find ("portal" + i.ToString () + "1" + "_mr");
					}
					if (tportal1 == null) {
						tportal1 = GameObject.Find ("portal" + i.ToString () + "1" + "_ml");
					}
					if (tportal1 == null) {
						tportal1 = GameObject.Find ("portal" + i.ToString () + "1" + "_mu");
					}
					if (tportal1 == null) {
						tportal1 = GameObject.Find ("portal" + i.ToString () + "1" + "_md");
					}

					if (tportal1 != null) {
						GetComponent<Teleporter> ().Destination = tportal1.GetComponent<Teleporter> ();
					}
				}
			}
		}
		//Remove fallingplatform when it coolides with other blocks
		if (blocktag == "fallingnow") {

			if (gameObject.transform.GetChild (2).name.Contains ("check") && gameObject.transform.GetChild (3).name.Contains ("check")) {
				Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (2).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);
				for (var i = 0; i < hitColliders2.Length; i++) {
					if (hitColliders2 [i].gameObject != gameObject) {
						Instantiate (mcam.GetComponent<CameraFollow> ().CrateExplode, transform.position, transform.rotation);

						if (mcam.GetComponent<CameraFollow> ().editplay == 0) {
							mcam.GetComponent<CameraFollow> ().blockmapd.Remove (gridposString);
							Destroy (gameObject);
						} else {
							blocktag = "falling";
							mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
							gameObject.SetActive (false);
						}
					}
				}
			}
		}

		///PALTFORMS STICKING MOVEMENT CODE
		//Stick platforms together

		if ((direction=="_ml" || direction=="_mr" || direction=="_mu" || direction=="_md")) {
			//platforms sticking
			//horizontal sticking
			if (!WaitingToBeDestroyed) {
				if (mcam.GetComponent<CameraFollow> ().editplay != 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
					//do not allow sticking of platforms in level editor while edit playing
				} else if (transform.position==startpos){
					if (direction == "_ml" || direction == "_mr") {
						Collider2D[] horhitcolliders = Physics2D.OverlapCircleAll (gameObject.transform.position + new Vector3 (0.5f, 0f, 0f), 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);
						for (var i = 0; i < horhitcolliders.Length; i++) {
							if (horhitcolliders [i].gameObject != gameObject) {
								if (horhitcolliders [i].GetComponent<SnapToGrid> ().direction == ("_mr") && direction == ("_mr")) {
									horhitcolliders [i].transform.SetParent (transform);
								}
								if (horhitcolliders [i].GetComponent<SnapToGrid> ().direction == ("_ml") && direction == ("_ml")) {
									horhitcolliders [i].transform.SetParent (transform);
								}
							}
						}
					}

					if (!blocktag.Contains ("arrow")) {
						if (transform.childCount <= 5 && transform.parent != null) {
							rightchild = gameObject;
							setrightcheck (gameObject.transform.parent.gameObject);
						}
					} else if (blocktag.Contains ("arrow")) {
						if (transform.childCount <= 6 && transform.parent != null) {
							rightchild = gameObject;
							setrightcheck (gameObject.transform.parent.gameObject);
						}
					}

					if (transform.parent != null) {
						setleftcheck (gameObject);
					}


					//vertical sticking

					if (direction == "_mu" || direction == "_md") {
						Collider2D[] verthitcolliders = Physics2D.OverlapCircleAll (gameObject.transform.position + new Vector3 (0f, 0.5f, 0f), 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);
						for (var i = 0; i < verthitcolliders.Length; i++) {
							if (verthitcolliders [i].gameObject != gameObject) {
								if (verthitcolliders [i].GetComponent<SnapToGrid> ().direction == ("_mu") && direction == ("_mu")) {
									verthitcolliders [i].transform.SetParent (transform);
								}
								if (verthitcolliders [i].GetComponent<SnapToGrid> ().direction == ("_md") && direction == ("_md")) {
									verthitcolliders [i].transform.SetParent (transform);
								}
							}
						}
					}

					if (!blocktag.Contains ("arrow")) {
						if (transform.childCount <= 5 && transform.parent != null) {
							topchild = gameObject;
							settopcheck (gameObject.transform.parent.gameObject);
						}
					} else if (blocktag.Contains ("arrow")) {
						if (transform.childCount <= 6 && transform.parent != null) {
							topchild = gameObject;
							settopcheck (gameObject.transform.parent.gameObject);
						}
					}

					if (transform.parent != null) {
						setbottomcheck (gameObject);
					}

				}
			}
			//Move platforms if preview is true
			if (previewing == true || SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
				//Move block horizontally
				if (direction == ("_mr") || direction == ("_ml")) {
					//Get collision side
					int blockfound = 0;
					bool canchangedir = false;
					if (gameObject.transform.GetChild (0).name.Contains ("check") && gameObject.transform.GetChild (1).name.Contains ("check")) {
						if (xspeed < 0) {
							Collider2D[] playercolliders = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (0).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
							if (playercolliders.Length >= 1) {
								//Push player at side of horizontal moving platform
								if (playercolliders [0].gameObject.GetComponent<CorgiController> () != null) {
									playercolliders [0].gameObject.GetComponent<CorgiController> ().AddHorizontalForce (-1f);
									playercolliders [0].gameObject.GetComponent<CharacterCrouch> ().ProcessAbility ();
								}
							}


							Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (0).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);

							for (var i = 0; i < hitColliders2.Length; i++) {
								if (hitColliders2 [i].gameObject != gameObject) {
									if (transform.childCount > 5) {
										if (hitColliders2 [i].gameObject != rightchild) {
											xspeed = xspeedconst;
											canchangedir = true;
										}
									} else {
										xspeed = xspeedconst;
										canchangedir = true;
									}
									if (xspeed < 0) {
										MovingRight = false;
										setChildrenDirection (gameObject);
									} else if (xspeed > 0) {
										MovingRight = true;
										setChildrenDirection (gameObject);
									}
								}


								//Prevent collision with other move blocks
								if ((hitColliders2 [i].gameObject.GetComponent<SnapToGrid> ().direction == "_mr" && direction == "_ml") || (hitColliders2 [i].gameObject.GetComponent<SnapToGrid> ().direction == "_ml" && direction == "_mr")) {
									rb.MovePosition (new Vector2 (transform.position.x + 0.1f, transform.position.y));
								}
							}
						} else if (xspeed >= 0) {
							Collider2D[] playercolliders3 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (1).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
							if (playercolliders3.Length >= 1) {
								//Push player at side of horizontal moving platform
								if (playercolliders3 [0].gameObject.GetComponent<CorgiController> () != null) {
									playercolliders3 [0].gameObject.GetComponent<CorgiController> ().AddHorizontalForce (1f);
									playercolliders3 [0].gameObject.GetComponent<CharacterCrouch> ().ProcessAbility ();
								}
							}


							Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (1).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);

							for (var i = 0; i < hitColliders2.Length; i++) {
								if (hitColliders2 [i].gameObject != gameObject) {
									if (transform.parent == null) {
										if (transform.childCount > 5) {
											if (hitColliders2 [i].gameObject != rightchild) {
												xspeed = -xspeedconst;
												canchangedir = true;

											}
										} else {
											xspeed = -xspeedconst;
											canchangedir = true;
										}
										if (xspeed < 0) {
											MovingRight = false;
											setChildrenDirection (gameObject);
										} else if (xspeed > 0) {
											MovingRight = true;
											setChildrenDirection (gameObject);
										}
									}


									//Prevent collision with other move blocks
									if ((hitColliders2 [i].gameObject.GetComponent<SnapToGrid> ().direction == "_mr" && direction == "_ml") || (hitColliders2 [i].gameObject.GetComponent<SnapToGrid> ().direction == "_ml" && direction == "_mr")) {
										rb.MovePosition (new Vector2 (transform.position.x - 0.1f, transform.position.y));
									}
								}
							}
						}
						if (Time.time - CreationTime < 3f && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
						}
						else {
							rb.velocity = new Vector2 (xspeed * mcam.GetComponent<CameraFollow> ().TimeScale, rb.velocity.y);
						}
					}
				}

				//Move block vertically
				if (direction == ("_mu") || direction == ("_md")) {
					//Get collision side
					int blockfound = 0;

					if (gameObject.transform.GetChild (2).name.Contains ("check") && gameObject.transform.GetChild (3).name.Contains ("check")) {
						if (yspeed < 0) {
							Collider2D[] playercolliders2 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (2).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
							if (playercolliders2.Length >= 1) {
								//Push player at bottom of vertical moving platform
								if (playercolliders2 [0].gameObject.GetComponent<CorgiController> () != null) {
									playercolliders2 [0].gameObject.GetComponent<CorgiController> ().AddVerticalForce (-1f);
									playercolliders2 [0].gameObject.GetComponent<CharacterCrouch> ().ProcessAbility ();
								}
							}

							Collider2D[] playercolliders3 = Physics2D.OverlapCircleAll (transform.position+Vector3.up, 0.3f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
							if (playercolliders3.Length >= 1) {
								//Allow player to jump on top of the moving block
								if (playercolliders3 [0].gameObject.GetComponent<CorgiController> () != null) {
									playercolliders3 [0].gameObject.GetComponent<CharacterJump> ().SetJumpAnywhereTwice();
									//playercolliders3 [0].gameObject.GetComponent<CorgiController> ().SetCollidingBelow ();
								}
							}

							Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (2).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);
							for (var i = 0; i < hitColliders2.Length; i++) {
								if (hitColliders2 [i].gameObject != gameObject) {
									if (transform.parent == null) {
										yspeed = yspeedconst;
									}
								}
							}
						} else if (yspeed >= 0) {
							Collider2D[] hitColliders2 = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (3).gameObject.transform.position, 0.001f, mcam.GetComponent<CameraFollow> ().whatIsGround);

							Collider2D[] playercolliders3 = Physics2D.OverlapCircleAll (transform.position+Vector3.up, 0.3f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
							if (playercolliders3.Length >= 1) {
								//Allow player to jump on top of the moving block
								if (playercolliders3 [0].gameObject.GetComponent<CorgiController> () != null) {
									print ("grounded haha");
									playercolliders3 [0].gameObject.GetComponent<CharacterJump> ().SetJumpAnywhereTwice();
									//playercolliders3 [0].gameObject.GetComponent<CorgiController> ().SetCollidingBelow ();
								}
							}

							for (var i = 0; i < hitColliders2.Length; i++) {
								if (hitColliders2 [i].gameObject != gameObject) {
									if (transform.parent == null) {
										if (transform.childCount > 5) {
											if (hitColliders2 [i].gameObject != topchild) {
												yspeed = -yspeedconst;
											}
										} else {
											yspeed = -yspeedconst;
										}
									}
								}
							}
						}
						if (Time.time - CreationTime < 3f && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
						}
						else {
							rb.velocity = new Vector2 (rb.velocity.x, yspeed * mcam.GetComponent<CameraFollow> ().TimeScale);
						}
					}
				}

			} else {
				rb.velocity = new Vector2 (0f, 0f);
			}
		}

		//Toggle preview
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			if (mcam.GetComponent<CameraFollow> ().previewing == true && previewing == false) {
				previewing = true;
			} else if (mcam.GetComponent<CameraFollow> ().previewing == false && previewing == true) {
				previewing = false;
				if (!isSelected) {
					transform.position = startpos;
				}
			}
			if (mcam.GetComponent<CameraFollow> ().previewing == false || previewing == false) {
				if (!isSelected) {
					transform.position = startpos;
				}
			}
		}
		//GetComponent<NetworkIdentity> ().enabled=false;
		if (!GetComponent<Renderer>().isVisible)
		{
			return;
		}


		canexit = true;



		bool playerfound = false;
		//Removing watertrans sprite
		if (blocktag==( "water")) {
			for(int j=0;j<4;j++)
			{
				Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.GetChild (j).transform.position, 0.001f);
				for (var i = 0; i < hitColliders.Length; i++)
				{
					if (hitColliders [i].tag == "Player") {
						playerfound = true;
					}

				}
			}

			if (playerfound == false && GetComponent<SpriteRenderer> ().sprite == mcam.GetComponent<CameraFollow> ().watertrans) {
				GetComponent<SpriteRenderer> ().sprite = mcam.GetComponent<CameraFollow> ().waternormal;
			}
		}




		else if (blocktag=="move")
		{
			int direction = Random.Range(1,5);
			float force = Random.Range(100f,200f);
			Vector2 velo=Vector2.up;
			if (direction==1)
			{
				velo = Vector2.up;
			}
			else if (direction == 2)
			{
				velo = Vector2.down;
			}
			else if (direction == 3)
			{
				velo = Vector2.left;
			}
			else if (direction == 4)
			{
				velo = Vector2.right;
			}
			gameObject.GetComponent<Rigidbody2D>().AddForce(force * velo);
		}
		//Fade out vanish block
		if (fading == "fadingin")
		{
			minimum = 0.03f;
			maximum = 1.0f;
			time = 1.0f;
			float t = ((Time.time - startTime) / time)*mcam.GetComponent<CameraFollow> ().TimeScale;
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, Mathf.SmoothStep(minimum, maximum, t));
			if (GetComponent<SpriteRenderer>().color == new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1.0f))
			{
				fading = "none";
			}
		}
		else if (fading == "fadingout")
		{
			minimum = 0.01f;
			maximum = 1.0f;
			time = 1.0f;
			float t = ((Time.time - startTime) / time)*mcam.GetComponent<CameraFollow> ().TimeScale;
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, Mathf.SmoothStep(maximum, minimum, t));
			if (GetComponent<SpriteRenderer>().color == new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.01f))
			{
				if (blocktag==( "vanish") || blocktag=="locked")
				{
					GetComponent<BoxCollider2D>().enabled = false;
				}
				fading = "fadingrest";
				startTime = Time.time;
			}

		}
		else if (fading == "fadingrest")
		{
			minimum = 0.02f;
			maximum = 0.03f;
			time = 3.0f;
			float t = ((Time.time - startTime) / time)*mcam.GetComponent<CameraFollow> ().TimeScale;
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, Mathf.SmoothStep(minimum, maximum, t));
			if (GetComponent<SpriteRenderer>().color == new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.03f))
			{
				if (blocktag == ("vanish")) {
					Collider2D[] playercolliders = Physics2D.OverlapCircleAll (transform.position, 0.5f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);

					//Only appear back if no player is nearby
					if (playercolliders.Length == 0) {
						GetComponent<BoxCollider2D> ().enabled = true;
						fading = "fadingin";
						startTime = Time.time;
					}
				}

			}
		}


		//Hide arrows
		if (blocktag == "leftarrow" || blocktag == "rightarrow" || blocktag == "uparrow" || blocktag == "downarrow") {
			if (transform.childCount == 6) {
				if (gameObject.transform.GetChild (5).gameObject.activeSelf == true) {
					bool foundplayer = false;
					Collider2D[] colliders = Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), 1f);
					foreach (Collider2D hit in colliders) {
						if (hit.gameObject.tag == "Player") {
							foundplayer = true;
							lastplayer = hit.gameObject;
						}
					}
					if (foundplayer == false) {
						gameObject.transform.GetChild (5).gameObject.SetActive (false);
						if (lastplayer != null) {
							if (blocktag == "uparrow") {
								CorgiController corgicontroller = lastplayer.GetComponent<CorgiController> ();
								corgicontroller.AddVerticalForce (3f);
							}
						}
					}
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		
		GameObject other = collider.gameObject;
		PlayerCollide (other);
		CharacterGravity characterGravity = collider.gameObject.GetComponent<CharacterGravity> ();
		if (characterGravity == null)
		{
			return;
		}
		else
		{
			characterGravity.ReverseHorizontalInputWhenUpsideDown = false;
		}

		//player destroys blocks when enlarging
		if (other.tag == "LargePlayer") {
			if (mcam.GetComponent<CameraFollow> ().editplay == 0) {
				mcam.GetComponent<CameraFollow> ().blockmapd.Remove (gridposString);
				Destroy (gameObject);
			} else {
				mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
				gameObject.SetActive (false);
			}
		}

		//Stop player from falling upon touching ladder
		if (other.tag == "Player") {
			PlayerOnTop = true;

			if (blocktag == "ladder") {
				//other.GetComponent<CorgiController> ().StopVerticalMovement ();
			}
			//Splash effect for water
			else if (blocktag == "water") {
				other.GetComponent<Controls> ().Splash ();
			}
		}
	}


	//Collision with player
	public void PlayerCollide(GameObject other)
	{
		isColliding = true;
		if (!GetComponent<Renderer>().isVisible)
		{
			return;
		}
		if (mcam != null) {
			if (mcam.GetComponent<CameraFollow> () != null) {
				if (mcam.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
					return;
				}
			}
		}
		//collision with bullets
		if (other.tag == "laser")
		{
			Destroy(other);
		}

		//Start falling block timer
		if (blocktag == "falling") {
			FallingStartTime = Time.time;
		}

		Controls pco=null;
		Rigidbody2D prb=null;
		CorgiController corgicontroller = null;
		Character characterControls = other.gameObject.GetComponent<Character> ();
		if (other.tag == "Player") {
			pco = other.GetComponent<Controls> ();
			prb = other.GetComponent<Rigidbody2D> ();
			corgicontroller = pco.gameObject.GetComponent<CorgiController> ();
		}
		else if (other.tag == "playercheck") {
			pco = other.GetComponentInParent<Controls> ();
			prb = other.GetComponentInParent<Rigidbody2D> ();
			corgicontroller = pco.gameObject.GetComponent<CorgiController> ();
		}

		if (pco==null || prb==null)
		{
			return;
		}

		if (blocktag != "bouncy") {
			pco.CanReduceLastFallingSpeed = true;
			pco.LastFallingSpeed = 0f;
		}

		//reverses arrow
		if (blocktag.Contains("arrow"))
		{
			arrowblocktag = blocktag;
			if (pco.protation == 90f) {
				if (blocktag == "uparrow") {
					arrowblocktag = "rightarrow";
				}
				else if (blocktag == "downarrow") {
					arrowblocktag = "leftarrow";
				}
				else if (blocktag == "leftarrow") {
					arrowblocktag = "uparrow";
				}
				else if (blocktag == "rightarrow") {
					arrowblocktag = "downarrow";
				}
			}
			else if (pco.protation == 180f) {
				if (blocktag == "uparrow") {
					arrowblocktag = "downarrow";
				}
				else if (blocktag == "downarrow") {
					arrowblocktag = "uparrow";
				}
				else if (blocktag == "leftarrow") {
					arrowblocktag = "rightarrow";
				}
				else if (blocktag == "rightarrow") {
					arrowblocktag = "leftarrow";
				}
			}
			else if (pco.protation == 270f) {
				if (blocktag == "uparrow") {
					arrowblocktag = "leftarrow";
				}
				else if (blocktag == "downarrow") {
					arrowblocktag = "rightarrow";
				}
				else if (blocktag == "leftarrow") {
					arrowblocktag = "downarrow";
				}
				else if (blocktag == "rightarrow") {
					arrowblocktag = "uparrow";
				}
			}
		}
		//Get collision side
		Vector2 playergridpos = GetGrid(other.transform.position, true);
		Vector2 objgridpos = GetGrid(gameObject.transform.position, true);
		Vector2 playermappos = GetGrid(other.transform.position, false);
		float px = playergridpos.x;
		float py = playergridpos.y;
		float ox = objgridpos.x;
		float oy = objgridpos.y;

		//Collision with other palyers
		if (other.tag == "Player" || other.tag=="playercheck")
		{
			//Set player to crouch
			if (py == oy && pco.onGround)
			{
				Collider2D[] hitColliders6;
				if (pco.facingright == true) {
					//Facingright: check if there is a block directly on right of player before setting player to crouch
					hitColliders6 = Physics2D.OverlapCircleAll (new Vector2 (other.transform.position.x, other.transform.position.y) + new Vector2 (0.07f, -0.3f), 0.01f);
					int hitlength = hitColliders6.Length;
					bool vanishexist = false;
					//Remove vanished blocks from colliders
					foreach (Collider2D block in hitColliders6) {
						if (block.tag == "vanish") {
							hitlength -= 1;
							vanishexist = true;
						}
					}
					if (hitlength ==1 || (hitlength ==0 && vanishexist==true))
					{
						if (px == ox - 1)
						{
							//Collide to block's left side
							pco.crouching = 2;
						}

					}
				} else {
					//Facingleft: check if there is a block directly on left of player before setting player to crouch
					hitColliders6 = Physics2D.OverlapCircleAll (new Vector2 (other.transform.position.x, other.transform.position.y) + new Vector2 (-0.07f, -0.3f), 0.01f);
					int hitlength = hitColliders6.Length;
					//Remove vanished blocks from colliders
					foreach (Collider2D block in hitColliders6) {
						if (block.tag == "vanish") {
							hitlength -= 1;
						}
					}
					if (hitlength ==0)
					{
						if (px == ox + 1)
						{
							//Collide to block's right side
							pco.crouching = 1;
						}
					}
				}

			}

			pco.OnArrow = "";

			pco.OnIce = false;
			pco.Acceleration = pco.MaxAcceleration;
			pco.Deceleration = pco.MaxDeceleration;

			if (px==ox)
			{
				if (py == oy + 1) {
					if (blocktag== ("move") || blocktag== ("brick") || blocktag== ("vanish") || blocktag== ("bomb") || blocktag== ("crumble") || blocktag== ("net") || blocktag== ("push") || blocktag==("portal") || blocktag==("stop") || blocktag.Contains("lock")  || blocktag==("bouncy") || blocktag==("spike") || blocktag==("falling") || blocktag==("fallingnow")) {
					} else {
						//Safespot for player
						if (direction=="") {
							pco.safespot = new Vector3 (playermappos.x, playermappos.y , 0);
							prb.velocity = Vector3.zero;
						}
					}
				}
			}

			/*
            else if (py == oy)
            {
                //print("P" + px + "," + py + ",O" + ox + "," + oy);
                if (px == ox + 1)
                {
                    colside = "right";
					pco.canjump = false;
                }
                else if (px == ox - 1)
                {
                    colside = "left";
					pco.canjump = false;
                }
            }
            */
			//print (colside + ";" + tag + ";px:" + px + ";py:" + py + ";ox" + ox + ";oy" + oy);


		}



		//Sets horcontact and enable player to push off walls
		if (colside == "left") {
			pco.horcontact = "left";
			/*
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || pco.jump) {
				prb.velocity = new Vector2 (-100, prb.velocity.y);
			}
			*/
		} else if (colside == "right") {
			pco.horcontact = "right";
			/*
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || pco.jump) {
				prb.velocity = new Vector2 (100, prb.velocity.y);
			}
			*/
		} else {
			pco.horcontact = "";
		}
		other.gameObject.GetComponent<CharacterWallClinging> ().AbilityPermitted = true;
		other.gameObject.GetComponent<CharacterWalljump> ().AbilityPermitted = true;
		//Performs individual block actions
		if (colside == "bottom") {
			if (blocktag=="brick") {
				corgicontroller.AddVerticalForce (-10f);
			}
		}
		if (blocktag==( "brick"))
		{

			if (colside=="bottom")
			{
				
				//Instantiate(brickparticle, player.transform.position, player.transform.rotation);
				// When the character takes damage, we create an auto destroy hurt particle system
				if (mcam.GetComponent<CameraFollow> ().brickeffect!= null)
				{ 
					Instantiate(mcam.GetComponent<CameraFollow> ().brickeffect,transform.position,transform.rotation);
				}
				if (mcam.GetComponent<CameraFollow> ().editplay == 0) {
					mcam.GetComponent<CameraFollow>().LocalPhotonView.RPC ("ServerRemoveBlock", PhotonTargets.Others, gridposString);
					Destroy (gameObject);
				} else {
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
					gameObject.SetActive (false);
				}
			}
		}
		else if (blocktag==( "ice"))
		{
			if (colside == "top")
			{
				if (pco != null)

				{
					pco.OnIce = true;
					pco.Acceleration = pco.MaxAcceleration/3.33f;
					pco.Deceleration = pco.MaxDeceleration/3.33f;
				}
			}
		}
		else if (blocktag==( "finish"))
		{

			if (colside == "bottom")
			{
				if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
					if (mcam.GetComponent<CameraFollow> ().endgame == 0 && pco.GameMode != "Deathmatch" && pco.photonView.isMine) {
						mcam.GetComponent<CameraFollow> ().PauseGame = true;
						mcam.GetComponent<CameraFollow> ().endgame = 1;

					}
				}

			}
		}
		else if (blocktag==( "itemonce"))
		{
			if (colside == "bottom") {
				if (used == false && pco.photonView.isMine && !gettingitem) {
					mcam.GetComponent<CameraFollow> ().controller.PlayOnce (mcam.GetComponent<CameraFollow> ().controller.a_item);
					gettingitem = true;
					used = true;
					StartCoroutine (EnableItem ());
					int num = Random.Range (0, newitems7.Length);
					pco.ChangeItem (newitems7 [num]);
					GetComponent<SpriteRenderer> ().sprite = mcam.GetComponent<CameraFollow> ().itemopen;
					GetComponent<SpriteRenderer> ().color = new Color (0.4f, 0.4f, 0.4f);
					mcam.GetComponent<CameraFollow> ().uses = 5;
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
					other.GetComponent<CharacterJetpack> ().Reset ();

				}
			}
		}
		else if (blocktag==( "iteminf"))
		{
			if (colside == "bottom") {
				if (pco.photonView.isMine && !gettingitem) {
					mcam.GetComponent<CameraFollow> ().controller.PlayOnce (mcam.GetComponent<CameraFollow> ().controller.a_item);
					gettingitem = true;
					StartCoroutine (EnableItem ());
					int num = Random.Range (0, newitems7.Length);
					mcam.GetComponent<CameraFollow> ().uses = 5;
					pco.ChangeItem (newitems7 [num]);
					other.GetComponent<CharacterJetpack> ().Reset ();
				}
			}

		}
		else if (blocktag==( "weapon"))
		{
			if (colside == "bottom")
			{
				if (used == false && !gettingitem) {
					mcam.GetComponent<CameraFollow> ().controller.PlayOnce (mcam.GetComponent<CameraFollow> ().controller.a_item);
					gettingitem = true;
					used = true;
					StartCoroutine (EnableItem ());
					int num = Random.Range (0, weaponarray5.Length);
					pco.ChangeItem (weaponarray5 [num]);
					string newWeapon = weaponarray5 [num];
					mcam.GetComponent<CameraFollow> ().uses = 5;


					GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);


				}
			}

		}
		else if (blocktag.Contains( "lock") && tag=="key")
		{
			if (colside == "bottom")
			{
				if (canactivatekey) {
					canactivatekey = false;
					Color tempcolor = GetComponent<SpriteRenderer> ().color;
					tempcolor.a = 0.5f;
					GetComponent<SpriteRenderer> ().color = tempcolor;
					string selectedlock = name.Substring (4, 1);
					var alllocks = GameObject.FindGameObjectsWithTag ("locked");
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
					foreach (GameObject locked in alllocks) {
						SnapToGrid locksnap = locked.GetComponent<SnapToGrid> ();
						//Find locks that correspond to this key color
						if (locked.name == "lock" + selectedlock + "2") {
							if (locked.GetComponent<SnapToGrid>().canactivatekey==true)
							{
								mcam.GetComponent<CameraFollow> ().disabledblocks.Add (locked);
								locked.GetComponent<BoxCollider2D> ().enabled = false;
								locked.GetComponent<SnapToGrid>().canactivatekey = false;
								//Instantiate (mcam.GetComponent<CameraFollow> ().brickeffect, locked.transform.position, locked.tra,gamnsform.rotation);
								locksnap.startTime = Time.time;
								locksnap.fading = "fadingout";
								//Re-enable lock blocks after 5 seconds
								//StartCoroutine (locked.GetComponent<SnapToGrid> ().Enablelock (5f, gameObject));
							}
						}
					}
				}
			}

		}

		else if (blocktag==( "time"))
		{
			if (colside == "bottom")
			{
				if (used == false)
				{
					used = true;
					GameObject GameTime = mcam.GetComponent<CameraFollow> ().GameTime;
					GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);

					if (((int)Time.time)- (int)GameTime.GetComponent<GameTime> ().StartTime < 60) {
						//Ensure it will never go below zero
						GameTime.GetComponent<GameTime> ().StartTime =(int)Time.time;
					} else {
						GameTime.GetComponent<GameTime>().StartTime += 60;
					}
				}
			}
		}
		else if (blocktag==( "timefreeze"))
		{
			if (colside == "bottom")
			{
				if (used == false)
				{
					used = true;
					GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);

					mcam.GetComponent<CameraFollow> ().FreezeTimeNum += 1;
					GetComponent<TimeModifier> ().FreezeTimeNum = mcam.GetComponent<CameraFollow> ().FreezeTimeNum;

					GetComponent<TimeModifier> ().FreezeTime ();
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);
				}
			}

		}

		else if (blocktag==( "happy"))
		{
			if (colside == "bottom")
			{
				CharacterHorizontalMovement characterhor = other.GetComponent<CharacterHorizontalMovement> ();
				if (characterhor.WalkSpeed < 12.0f && used==false)
				{
					characterhor.WalkSpeed += 0.5f;
					characterhor.MovementSpeed += 0.5f;
				}

				CharacterJump characterjump = other.GetComponent<CharacterJump> ();
				if (characterjump.JumpHeight < 6.0f && used==false)
				{
					used = true;
					characterjump.JumpHeight += 0.25f;
					GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);
				}
			}
		}
		else if (blocktag==( "heart"))
		{
			if (other.GetComponent<Health> ().CurrentHealth < other.GetComponent<Health> ().MaximumHealth) {
				if (used == false) {

					used = true;
					GetComponent<SpriteRenderer> ().color = new Color (0.4f, 0.4f, 0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);

					if (mcam.GetComponent<CameraFollow> ().GameMode == "Deathmatch") {
						other.GetComponent<Health> ().Damage (-10, gameObject, 1f, 1f);
					}
					Instantiate (mcam.GetComponent<CameraFollow> ().brickeffect, transform.position, transform.rotation);

				}
			}
		}
		else if (blocktag==( "sad"))
		{
			if (colside == "bottom")
			{
				CharacterHorizontalMovement characterhor = other.GetComponent<CharacterHorizontalMovement> ();
				if (characterhor.WalkSpeed > 3.0f && used==false)
				{
					characterhor.WalkSpeed -= 0.5f;
					characterhor.MovementSpeed -= 0.5f;
				}

				CharacterJump characterjump = other.GetComponent<CharacterJump> ();
				if (characterjump.JumpHeight > 1.5f && used==false)
				{
					used = true;
					characterjump.JumpHeight -= 0.25f;
					GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);
				}
			}
		}
		else if (arrowblocktag==( "leftarrow"))
		{
			if (transform.childCount == 6) {
				gameObject.transform.GetChild (5).gameObject.SetActive (true);
			}
			else if (transform.childCount == 7) {
				gameObject.transform.GetChild (6).gameObject.SetActive (true);
			}

			if (colside == "top" || colside=="bottom")
			{
				if (other.GetComponent<CharacterHorizontalMovement> ().IsHoldingHorizontalArrows () == true) {
					if (other.GetComponent<Character> ().IsFacingRight == false) {
						//Holding left on a left arrow
						corgicontroller.AddHorizontalForce (-1.5f);
					} else {
						//Holding right on a left arrow
						corgicontroller.SetHorizontalForce (1.5f);
					}
				} else {
					//Not holding anything on a left arrow
					corgicontroller.AddHorizontalForce (-1f);
				}
			}
			else if (colside == "left" || colside == "right")
			{
				corgicontroller.AddHorizontalForce (-3f);
			}
		}
		else if (arrowblocktag==( "rightarrow"))
		{
			if (transform.childCount == 6) {
				gameObject.transform.GetChild (5).gameObject.SetActive (true);
			}
			else if (transform.childCount == 7) {
				gameObject.transform.GetChild (6).gameObject.SetActive (true);
			}
			if (colside == "top" || colside=="bottom") {
				if (other.GetComponent<CharacterHorizontalMovement> ().IsHoldingHorizontalArrows () == true) {
					if (other.GetComponent<Character> ().IsFacingRight == false) {
						//Holding left on a right arrow
						corgicontroller.SetHorizontalForce (-1.5f);
					} else {
						//Holding right on a right arrow
						corgicontroller.AddHorizontalForce (1.5f);
					}
				} else {
					//Not holding anything on a right arrow
					corgicontroller.AddHorizontalForce (1f);
				}

			}
			else if (colside == "left" || colside == "right")
			{
				corgicontroller.AddHorizontalForce (3f);
			}
		}
		else if (arrowblocktag==( "uparrow"))
		{
			if (transform.childCount == 6) {
				gameObject.transform.GetChild (5).gameObject.SetActive (true);
			}
			else if (transform.childCount == 7) {
				gameObject.transform.GetChild (6).gameObject.SetActive (true);
			}

			other.gameObject.GetComponent<CharacterWallClinging> ().AbilityPermitted = false;
			other.gameObject.GetComponent<CharacterWalljump> ().AbilityPermitted = false;
			if (colside == "bottom") {
				other.GetComponent<Controls> ().CurrentGravity = 0f;
			} else if (colside == "left") {
				if (other.GetComponent<CharacterHorizontalMovement> ().IsHoldingHorizontalArrows()==true && other.GetComponent<Character> ().IsFacingRight==true) {
					if (corgicontroller.Speed.y < 10f) {
						corgicontroller.AddVerticalForce (0.3f);
					}
				} else {
					//Disable highlighted arrows if not holding on to right movement key
					if (transform.childCount == 6) {
						gameObject.transform.GetChild (5).gameObject.SetActive (false);
					}
					else if (transform.childCount == 7) {
						gameObject.transform.GetChild (6).gameObject.SetActive (false);
					}
				}
			} else if (colside == "right") {
				if (other.GetComponent<CharacterHorizontalMovement> ().IsHoldingHorizontalArrows()==true && other.GetComponent<Character> ().IsFacingRight==false) {
					if (corgicontroller.Speed.y < 10f) {
						corgicontroller.AddVerticalForce (0.3f);
					}
				} else {
					//Disable highlighted arrows if not holding on to left movement key
					if (transform.childCount == 6) {
						gameObject.transform.GetChild (5).gameObject.SetActive (false);
					} else if (transform.childCount == 7) {
						gameObject.transform.GetChild (6).gameObject.SetActive (false);
					}
				}
			}
		}
		else if (arrowblocktag==( "downarrow"))
		{
			if (transform.childCount == 6) {
				gameObject.transform.GetChild (5).gameObject.SetActive (true);
			}
			else if (transform.childCount == 7) {
				gameObject.transform.GetChild (6).gameObject.SetActive (true);
			}
			other.gameObject.GetComponent<CharacterWallClinging> ().AbilityPermitted = false;
			other.gameObject.GetComponent<CharacterWalljump> ().AbilityPermitted = false;
			if (colside == "top")
			{
				corgicontroller.AddVerticalForce (-10f);
			}
			else if (colside == "bottom")
			{
				corgicontroller.AddVerticalForce (-2f);
			} 
			else if (colside == "left") {
				if (other.GetComponent<CharacterHorizontalMovement> ().IsHoldingHorizontalArrows()==true && other.GetComponent<Character> ().IsFacingRight==true) {
					corgicontroller.AddVerticalForce (-1f);
				} else {
					//Disable highlighted arrows if not holding on to right movement key
					if (transform.childCount == 6) {
						gameObject.transform.GetChild (5).gameObject.SetActive (false);
					}
					else if (transform.childCount == 7) {
						gameObject.transform.GetChild (6).gameObject.SetActive (false);
					}
				}
			} else if (colside == "right") {
				if (other.GetComponent<CharacterHorizontalMovement> ().IsHoldingHorizontalArrows()==true && other.GetComponent<Character> ().IsFacingRight==false) {
					corgicontroller.AddVerticalForce (-1f);
				} else {
					//Disable highlighted arrows if not holding on to left movement key
					if (transform.childCount == 6) {
						gameObject.transform.GetChild (5).gameObject.SetActive (false);
					} else if (transform.childCount == 7) {
						gameObject.transform.GetChild (6).gameObject.SetActive (false);
					}
				}
			}
		}


		else if (blocktag==( "bouncy"))
		{
			if (colside == "left" || colside == "right") {
				AddExplosionForce(pco.gameObject, 20f, transform.position, 10f);
			}
			else if (colside == "bottom") {
				AddExplosionForce(pco.gameObject, 20f, transform.position, 10f);
			}
			//StartCoroutine(animate(0.055f,bouncyframes,0,gameObject,0));
			StartCoroutine(ResetBouncy());
			gameObject.GetComponent<Animator> ().SetBool (colside, true);
		}



		else if (blocktag==( "bomb"))
		{
			
		}

		else if (blocktag==( "spike"))
		{
			if (colside == "top")
			{

				AddExplosionForce(pco.gameObject, 5f, transform.position, 20f);
				//other.GetComponent<Character> ().Freeze ();
				var health = other.GetComponent<health>();
				if (health != null && mcam.GetComponent<CameraFollow> ().GameMode=="Deathmatch")
				{
					health.TakeDamage(10);
				}


			}
		}

		else if (blocktag==( "giant"))
		{
			if (colside == "bottom") {
				if (used == false) {
					used = true;
					GetComponent<SpriteRenderer> ().color = new Color (0.4f, 0.4f, 0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);

					//Increase size based on current size of player
					if (pco.transform.localScale == new Vector3 (0.3f, 0.3f, 0.3f)) {
						pco.transform.localScale = new Vector3 (0.43f, 0.5f, 0.45f);
					}
					else if (pco.transform.localScale == new Vector3 (0.43f, 0.5f, 0.45f)) {
						Instantiate (mcam.GetComponent<CameraFollow> ().LargePlayer, other.transform.position+new Vector3(0f,0.2f,0f), other.transform.rotation);
						pco.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
						Collider2D[] hitcolliders = Physics2D.OverlapCircleAll (transform.position, 1f, mcam.GetComponent<CameraFollow> ().whatIsGround);
						for (var i = 0; i < hitcolliders.Length; i++) {
							if (mcam.GetComponent<CameraFollow> ().editplay == 0) {
								mcam.GetComponent<CameraFollow> ().blockmapd.Remove ((hitcolliders [i].GetComponent<SnapToGrid>().gridposString));
								Destroy (hitcolliders [i].gameObject);
							} else {
								mcam.GetComponent<CameraFollow> ().disabledblocks.Add (hitcolliders [i].gameObject);
								hitcolliders [i].gameObject.SetActive (false);
							}

						}
					}
					Instantiate (mcam.GetComponent<CameraFollow> ().RobotExplosion, transform.position+new Vector3(0f,-1.5f,0f), transform.rotation);

				}
			}
		}

		else if (blocktag==( "tiny"))
		{
			if (colside == "bottom") {
				if (used == false) {
					used = true;
					GetComponent<SpriteRenderer> ().color = new Color (0.4f, 0.4f, 0.4f);
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add(gameObject);

					//Decrease size based on current size of player
					if (pco.transform.localScale == new Vector3 (0.9f, 0.9f, 0.9f)) {
						pco.transform.localScale = new Vector3 (0.43f, 0.5f, 0.45f);
					}
					else if (pco.transform.localScale == new Vector3 (0.43f, 0.5f, 0.45f)) {
						pco.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
					}
					Instantiate (mcam.GetComponent<CameraFollow> ().RobotExplosion, transform.position+new Vector3(0f,-1.5f,0f), transform.rotation);

				}
			}
		}

		else if (blocktag==( "falling"))
		{
			if (colside == "top") {
				blocktag = "fallingnow";
				StartCoroutine(FallingBlockShake(0.05f,0));
			}
		}
		else if (blocktag==( "crumble"))
		{
			/*
			crumblehealth -= 30;
			if (crumblehealth < 0)
			{
				if (mcam.GetComponent<CameraFollow> ().editplay == 0) {
					mcam.GetComponent<CameraFollow> ().blockmapd.Remove (gridposString);
					Destroy (gameObject);
				} else {
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
					gameObject.SetActive (false);
				}
			}
			*/
		}
		else if (blocktag==( "vanish"))
		{
			if (fading == "none" || fading == "fadingin")
			{
				//if (py != oy + 1 && py != oy)
				startTime = Time.time;
				fading = "fadingout";

			}
		}
		else if (blocktag==( "push"))
		{
		}
		else if (blocktag==( "net"))
		{
			other.transform.position = pco.safespot;
		}

		else if (blocktag==( "checkpoint"))
		{
			if (colside == "top")
			{
				other.GetComponent<Controls> ().checkpointpos = other.transform.position;

				var checkpoints = GameObject.FindGameObjectsWithTag ("checkpoint");
				foreach (GameObject checkpoint in checkpoints) {
					checkpoint.GetComponent<SpriteRenderer> ().sprite = mcam.GetComponent<CameraFollow> ().checkpointsprite;
				}

				GetComponent<SpriteRenderer> ().sprite = mcam.GetComponent<CameraFollow> ().checkpointedsprite;
			}
		}



	}



	void OnTriggerExit2D(Collider2D col)
	{
		isColliding = false;
		GameObject other = col.gameObject;
		var pco = other.GetComponent<Controls>();
		var prb = other.GetComponent<Rigidbody2D>();
		//Hide arrows
		if (blocktag == "leftarrow" || blocktag == "rightarrow" || blocktag == "uparrow" || blocktag == "downarrow") {
			if (transform.childCount == 6) {
				gameObject.transform.GetChild (5).gameObject.SetActive (false);
			} else if (transform.childCount == 7) {
				gameObject.transform.GetChild (6).gameObject.SetActive (false);
			}
		}

		if (other.tag == "Player")
		{
			other.GetComponent<CharacterJump> ().SetJumpOnGround ();
			PlayerOnTop = false;
			if (IsSlightlyUp) {
				IsSlightlyUp = false;
				MoveSlightlyDown ();
			}
			if (blocktag==( "uparrow"))
			{
				other.GetComponent<Controls> ().CurrentGravity = -30f;
				if (!WaitingForUpArrow) {
					WaitingForUpArrow = true;
					StartCoroutine (LateUpArrow (other));
				}
			}
			else if (blocktag == "water") {
				other.GetComponent<Controls> ().Splash ();
			}
		}

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (blocktag == "falling") {
			if (col.gameObject.tag == "Player") {
				PlayerOnTop = true;
			}
		}
	}

	public void setChildrenDirection(GameObject obj)
	{
		if (!blocktag.Contains ("arrow")) {
			if (obj.transform.childCount>=6) {
				obj.transform.GetChild (5).gameObject.GetComponent<SnapToGrid> ().MovingRight = MovingRight;
				obj.transform.GetChild (5).gameObject.GetComponent<SnapToGrid> ().setChildrenDirection (obj.transform.GetChild (5).gameObject);
			}
		}
		else if (blocktag.Contains ("arrow")) {
			if (obj.transform.childCount>=7) {
				obj.transform.GetChild (6).gameObject.GetComponent<SnapToGrid> ().MovingRight = MovingRight;
				obj.transform.GetChild (6).gameObject.GetComponent<SnapToGrid> ().setChildrenDirection (obj.transform.GetChild (6).gameObject);
			}
		}
	}
	public void setrightcheck(GameObject obj)
	{
		if (!blocktag.Contains ("arrow")) {
			obj.transform.GetChild (1).gameObject.transform.position = obj.transform.GetChild (5).transform.GetChild (1).gameObject.transform.position;
			obj.GetComponent<SnapToGrid> ().rightchild = obj.transform.GetChild (5).GetComponent<SnapToGrid> ().rightchild;
		}
		else if (blocktag.Contains ("arrow")) {
			obj.transform.GetChild (1).gameObject.transform.position = obj.transform.GetChild (6).transform.GetChild (1).gameObject.transform.position;
			obj.GetComponent<SnapToGrid> ().rightchild = obj.transform.GetChild (6).GetComponent<SnapToGrid> ().rightchild;
		}

		if (obj.transform.parent!=null) {
			setrightcheck (obj.transform.parent.gameObject);
		}
	}

	public void setleftcheck(GameObject obj)
	{
		if (obj.transform.childCount<=0) {
			return;
		}
		obj.transform.GetChild (0).gameObject.transform.position = obj.transform.parent.transform.GetChild (0).gameObject.transform.position;

		if (!blocktag.Contains ("arrow")) {
			if (obj.transform.childCount > 5) {
				setleftcheck (obj.transform.GetChild (5).gameObject);
			}
		} else if (blocktag.Contains ("arrow")) {
			if (obj.transform.childCount > 6) {
				setleftcheck (obj.transform.GetChild (6).gameObject);
			}
		}
	}

	public void settopcheck(GameObject obj)
	{
		if (!blocktag.Contains ("arrow")) {
			obj.transform.GetChild (3).gameObject.transform.position = obj.transform.GetChild (5).transform.GetChild (3).gameObject.transform.position;
			obj.GetComponent<SnapToGrid> ().topchild = obj.transform.GetChild (5).GetComponent<SnapToGrid> ().topchild;
		}
		else if (blocktag.Contains ("arrow")) {
			obj.transform.GetChild (3).gameObject.transform.position = obj.transform.GetChild (6).transform.GetChild (3).gameObject.transform.position;
			obj.GetComponent<SnapToGrid> ().topchild = obj.transform.GetChild (6).GetComponent<SnapToGrid> ().topchild;
		}

		if (obj.transform.parent!=null) {
			settopcheck (obj.transform.parent.gameObject);
		}
	}

	public void setbottomcheck(GameObject obj)
	{
		if (obj.transform.childCount<=2) {
			return;
		}
		obj.transform.GetChild (2).gameObject.transform.position = obj.transform.parent.transform.GetChild (2).gameObject.transform.position;

		if (!blocktag.Contains ("arrow")) {
			if (obj.transform.childCount > 5) {
				setbottomcheck (obj.transform.GetChild (5).gameObject);
			}
		} else if (blocktag.Contains ("arrow")) {
			if (obj.transform.childCount > 6) {
				setbottomcheck (obj.transform.GetChild (6).gameObject);
			}
		}
	}
	/*
    [Command]
    void CmdDestroy()
    {
        Destroy(gameObject);
    }
*/
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
			Vector2 gridpos = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
			return gridpos;
		}
	}

	public IEnumerator ResetBouncy()
	{
		yield return new WaitForSeconds(1f/3f);
		GetComponent<Animator> ().SetBool ("top", false);
		GetComponent<Animator> ().SetBool ("bottom", false);
		GetComponent<Animator> ().SetBool ("left", false);
		GetComponent<Animator> ().SetBool ("right", false);

	}
	public IEnumerator animate(float time,List<Sprite>list,int index,GameObject obj,int editplay)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		print("animating");
		GetComponent<SpriteRenderer>().sprite=list[index];
		if (index < list.Count - 1) {
			if (mcam.GetComponent<CameraFollow> ().editplay == editplay) {
				StartCoroutine (animate (time, list, index + 1, obj, editplay));
			} else {
				GetComponent<SpriteRenderer>().sprite=list[0];
			}
		}
		else {
			if (blocktag == "bomb") {
				if (mcam.GetComponent<CameraFollow> ().editplay == 0) {
					mcam.GetComponent<CameraFollow> ().blockmapd.Remove (gridposString);
					Destroy (gameObject);
				} else {
					GetComponent<SpriteRenderer>().sprite=list[0];
					mcam.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
					gameObject.SetActive (false);
				}
			}
		}

	}
	public IEnumerator FallingBlockShake(float time,int position)
	{
		yield return new WaitForSeconds(time);
		Collider2D[] verthitcolliders = Physics2D.OverlapCircleAll (gameObject.transform.GetChild (3).gameObject.transform.position, 0.5f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);

		if (verthitcolliders.Length != 0) {
			float verticalShaking = 0f;
			if (Time.time - FallingStartTime >= 2f) {
				if (blocktag == "fallingnow") {
					for (var i = 0; i < verthitcolliders.Length; i++) {
						if (verthitcolliders[i].gameObject.tag=="Player") {
							rb.velocity = new Vector2 (rb.velocity.x, -5f*mcam.GetComponent<CameraFollow> ().TimeScale);
						}
					}


				}
			} else {
				if (Time.time - FallingStartTime >= 1f) {
					if (time == 0.05f) {
						time = 0.02f;
					}
				}
				if (position == 0) {
					gameObject.transform.Translate (new Vector3 (0.05f, 0f, 0f));
					StartCoroutine (FallingBlockShake (time, 1));
				} else if (position == 1) {
					gameObject.transform.Translate (new Vector3 (-0.1f, 0f, 0f));
					StartCoroutine (FallingBlockShake (time, -1));
				} else if (position == -1) {
					gameObject.transform.Translate (new Vector3 (0.1f, 0f, 0f));
					StartCoroutine (FallingBlockShake (time, 1));
				}
			}
		} else {
			transform.position = startpos;
			blocktag = "falling";
		}
	}


	public IEnumerator LateUpArrow(GameObject other)
	{
		while (other.GetComponent<CorgiController> ().IsCollingTop () == true) {
			yield return null;
		}
		WaitingForUpArrow = false;
		other.GetComponent<CorgiController> ().AddVerticalForce (10f);
	}
	//Prevent spamming of getting items
	public IEnumerator EnableItem()
	{
		yield return new WaitForSeconds (0.5f);

		gettingitem = false;
	}

	//Enable preview for level1 after a while
	public IEnumerator EnablePreview(float time)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			previewing = true;
		}

	}

	//Activate lock blocks again after 5 seconds
	public IEnumerator Enablelock(float time,GameObject keyblock)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay

		//Remoe shadow on key block
		keyblock.GetComponent<SnapToGrid>().canactivatekey=true;
		Color tempcolor=keyblock.GetComponent<SpriteRenderer>().color;
		tempcolor.a = 1f;
		keyblock.GetComponent<SpriteRenderer>().color = tempcolor;
		//Only enable if no player is nearby,if not, wait.
		Collider2D[] playercolliders = Physics2D.OverlapCircleAll (transform.position, 0.5f, mcam.GetComponent<CameraFollow> ().whatIsPlayer);
		if (playercolliders.Length == 0) {
			//Instantiate (mcam.GetComponent<CameraFollow> ().brickeffect, transform.position, transform.rotation);
			GetComponent<BoxCollider2D>().enabled=true;
			fading = "fadingin";
			startTime = Time.time;
		} else {
			StartCoroutine(Enablelock(1f,keyblock));
		}
	}

	public void MoveSlightlyUp()
	{
		transform.Translate (new Vector3 (0f, 0.2f, 0f));
	}

	public void MoveSlightlyDown()
	{
		transform.Translate (new Vector3 (0f, -0.2f, 0f));
	}

	public IEnumerator SpawnMoveCheck()
	{
		yield return new WaitForSeconds(2f);
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor") && mcam.GetComponent<CameraFollow> ().editplay == 0) {
			if (name.Contains ("_mr")) {
				//Send out "MovingCheck" in a horizontal direction, so that those blocks that come into contact
				GameObject movecheck = Instantiate (mcam.GetComponent<CameraFollow> ().MoveHorizontalCheck, transform.position, Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "horizontal";
			} else if (name.Contains ("_ml")) {
				GameObject movecheck = Instantiate (mcam.GetComponent<CameraFollow> ().MoveHorizontalCheck, transform.position, Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "horizontal";
			} else if (name.Contains ("_mu")) {
				GameObject movecheck = Instantiate (mcam.GetComponent<CameraFollow> ().MoveVerticalCheck, transform.position, Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "vertical";
			} else if (name.Contains ("_md")) {
				GameObject movecheck = Instantiate (mcam.GetComponent<CameraFollow> ().MoveVerticalCheck, transform.position, Quaternion.identity);
				movecheck.GetComponent<MoveCheck> ().direction = "vertical";
			}
		} else {
		}
		StartCoroutine (SpawnMoveCheck ());
	}
	public void AlwaysActiveHorizontal()
	{
		alwaysActiveHorizontal = true;
		CheckedalwaysActiveHorizontal = false;
		//GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);
	}

	public void AlwaysActiveVertical()
	{
		alwaysActiveVertical = true;
		CheckedalwaysActiveVertical = false;
		//GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f);
	}
}
