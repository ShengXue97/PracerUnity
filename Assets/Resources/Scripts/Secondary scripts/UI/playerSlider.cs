using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerSlider : MonoBehaviour {
	public GameObject PlayerName;
	public GameObject PlayerRank;
	public GameObject Star;
	public GameObject Handle;

	public float position;
	public int rank;

	public Sprite GreenSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Change colour to red if it belongs to the current player
		if (name == "Slider" + PhotonNetwork.player.NickName) {
			Handle.GetComponent<Image> ().sprite = GreenSprite;
		}

		rank = 1;
		var sliders = GameObject.FindGameObjectsWithTag ("Slider");
		foreach (GameObject slider in sliders) {
			if (slider.GetComponent<playerSlider> ().position > position) {
				rank += 1;
			}
		}

		//Only spawn star if not deathmatch
		if (Camera.main.GetComponent<CameraFollow> ().GameMode != "Deathmatch") {
			PlayerRank.GetComponent<Text> ().text = rank.ToString ();
			//Spawn star if first
			if (rank == 1) {
				if (Star.activeSelf == false) {
					Camera.main.GetComponent<CameraFollow> ().CreateRobotExplosion (name);
					Star.SetActive (true);
				}
			} else {
				Star.SetActive (false);
			}
		} else {
			PlayerRank.GetComponent<Text> ().text = "";
			Star.SetActive (false);
		}
	}
}
