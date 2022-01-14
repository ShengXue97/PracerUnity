using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

using MoreMountains.Tools;
using MoreMountains.CorgiEngine;

public class BlockSpawner : MonoBehaviour {
	public string[] blocka = { "b1", "b2", "b3", "b4", "brick", "finish", "ice", "itemonce", "iteminf", "leftarrow", "rightarrow", "uparrow", "downarrow", "bomb", "crumble", "vanish", "move", "rotateleft", "rotateright", "push", "happy", "sad", "net", "heart", "time", "water","start","stop","ladder","antigravity","bouncy","spike","checkpoint","portal","door","oneway","rocket","falling","giant","tiny","sticky","fan","key","locked","weapon","npc","sign","timefreeze"};
	public GameObject b1; public GameObject b2; public GameObject b3; public GameObject b4; public GameObject brick; public GameObject finish; public GameObject ice; public GameObject itemonce; public GameObject iteminf; public GameObject leftarrow; public GameObject rightarrow; public GameObject uparrow; public GameObject downarrow; public GameObject bomb; public GameObject crumble; public GameObject vanish; public GameObject move; public GameObject rotateleft; public GameObject rotateright; public GameObject push; public GameObject happy; public GameObject sad; public GameObject net; public GameObject heart; public GameObject time; public GameObject water; public GameObject start; public GameObject stop; public GameObject ladder; public GameObject antigravity; public GameObject bouncy; public GameObject spike; public GameObject checkpoint; public GameObject portal; public GameObject door; public GameObject oneway; public GameObject rocket; public GameObject falling; public GameObject giant; public GameObject tiny; public GameObject sticky; public GameObject fan; public GameObject key; public GameObject locked; public GameObject weapon; public GameObject npc; public GameObject sign; public GameObject timefreeze;
	public GameObject portal0; public GameObject portal1; public GameObject portal2; public GameObject portal3; public GameObject portal4; public GameObject portal5; public GameObject portal6; public GameObject portal7; public GameObject portal8; public GameObject portal9; 
	public GameObject key0; public GameObject key1; public GameObject key2; public GameObject key3; public GameObject key4; public GameObject key5; public GameObject key6; public GameObject key7; public GameObject key8; public GameObject key9; 
	public GameObject locked0; public GameObject locked1; public GameObject locked2; public GameObject locked3; public GameObject locked4; public GameObject locked5; public GameObject locked6; public GameObject locked7; public GameObject locked8; public GameObject locked9; 

	public CameraFollow camfollow=null;
	// Use this for initialization
	void Start () {
		camfollow = Camera.main.GetComponent<CameraFollow> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void RemoveBlock(string gridposString)
	{
		camfollow = Camera.main.GetComponent<CameraFollow> ();
		if (camfollow.mapd.ContainsKey (gridposString)) {
			//Do not allow removal of start blocks
			if (camfollow.mapd [gridposString] != "start") {
				if (!camfollow.mapd [gridposString].Contains ("portal")) {
					camfollow.mapdKeyCache.Add (gridposString);
					camfollow.mapdValueCache.Add (camfollow.mapd [gridposString]);
					camfollow.mapdHistory.Add ("remove");
				}
				camfollow.mapd.Remove (gridposString);

			}
			//Destroy (camfollow.blockmapd [gridposString]);
		}
	}
	public void SpawnBlock(string blockmessage)
	{
		var split = blockmessage.Split ('#');
		string gridposString=split [0];
		string posxstring=split [1];
		string posystring=split [2];
		string poszstring=split [3];
		string ExtraPortalID=split [4]; 
		string ExtraKeyLockID=split [5];
		string selectedblockstring=split [6];
		string selectedportal=split [7];
		string selectedkeylock=split [8];
		string blockdir=split [9];


		camfollow = Camera.main.GetComponent<CameraFollow> ();
		GameObject block = null;
		int selectedblock = int.Parse (selectedblockstring);
		float posx = float.Parse (posxstring);
		float posy = float.Parse (posystring);
		float posz = float.Parse (poszstring);
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
		if (block == portal) {
			if (selectedportal == "01" || selectedportal == "02") {
				block = portal0;
			} else if (selectedportal == "11" || selectedportal == "12") {
				block = portal1;
			} else if (selectedportal == "21" || selectedportal == "22") {
				block = portal2;
			} else if (selectedportal == "31" || selectedportal == "32") {
				block = portal3;
			} else if (selectedportal == "41" || selectedportal == "42") {
				block = portal4;
			} else if (selectedportal == "51" || selectedportal == "52") {
				block = portal5;
			} else if (selectedportal == "61" || selectedportal == "62") {
				block = portal6;
			} else if (selectedportal == "71" || selectedportal == "72") {
				block = portal7;
			} else if (selectedportal == "81" || selectedportal == "82") {
				block = portal8;
			} else if (selectedportal == "91" || selectedportal == "92") {
				block = portal9;
			}
		}

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
			var startblock = GameObject.Find ("start");
			Destroy (startblock);

			Dictionary<string, string> tempmapd = new Dictionary<string, string> () { };
			tempmapd.Clear ();
			foreach (KeyValuePair<string, string> item in camfollow.mapd) {
				if (item.Value == "start") {
					tempmapd.Add (item.Key, item.Value);
				}
			}

			foreach (KeyValuePair<string, string> item2 in tempmapd) {
				camfollow.mapd.Remove (item2.Key);
			}
		}

		//Ensures there is only one of each unique portal block
		if (selectedblock == 34) {
			var portalblock = GameObject.Find ("portal" + selectedportal);
			Destroy (portalblock);

			Dictionary<string, string> tempmapd = new Dictionary<string, string> () { };
			tempmapd.Clear ();
			foreach (KeyValuePair<string, string> item in camfollow.mapd) {
				if (item.Value.Contains ("portal" + selectedportal)) {
					tempmapd.Add (item.Key, item.Value);
				}
			}

			foreach (KeyValuePair<string, string> item2 in tempmapd) {
				camfollow.mapd.Remove (item2.Key);
			}
		}

		if (!camfollow.mapd.ContainsKey (gridposString)) {
			if (block==crumble || block==move || block==push || block==checkpoint ||block==heart || block==start || block==ladder || block==oneway || block==sign || block==brick || block==falling)
			{
				//Do not allow directions to these blocks
				blockdir = "";
			}

			camfollow.mapd.Add (gridposString, blocka [selectedblock - 1] + ExtraPortalID + ExtraKeyLockID + blockdir);

			//Do not allow undo and redo of start blocks
			if (camfollow.mapd [gridposString] != "start" && !camfollow.mapd [gridposString].Contains("portal")) {
				camfollow.mapdKeyCache.Add (gridposString);
				camfollow.mapdValueCache.Add (blocka [selectedblock - 1] + ExtraPortalID + ExtraKeyLockID + blockdir);
				camfollow.mapdHistory.Add ("add");
			}
		}
		GameObject newblock =Instantiate (block, new Vector3 (posx, posy, posz), Quaternion.identity);

		newblock.name = blocka [selectedblock - 1] + ExtraPortalID + ExtraKeyLockID + blockdir;
		if (!camfollow.blockmapd.ContainsKey (gridposString)) {
			camfollow.blockmapd.Add (gridposString, newblock);
		} else {
			camfollow.blockmapd [gridposString] = newblock;
		}
	}
}
