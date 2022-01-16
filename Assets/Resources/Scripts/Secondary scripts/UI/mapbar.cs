using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mapbar : MonoBehaviour
{

    void Start()
    {
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("NetworkLobby")) {
			Button btn = GetComponent<Button> ();
			btn.onClick.AddListener (TaskOnClick);
		} else {
			Button mapbutton = GetComponent<Button> ();
			mapbutton.onClick.AddListener (TaskOnClick);

			Button deletebutton = transform.GetChild(0).GetComponent<Button> ();
			deletebutton.onClick.AddListener (DeleteMap);
		}
    }
	void DeleteMap()
	{
		Camera.main.GetComponent<CameraFollow> ().MapToDelete = transform.GetChild (1).GetComponent<Text> ().text;
		Camera.main.GetComponent<CameraFollow> ().ConfirmDeletePanel.GetComponentInChildren<InputField>().text=transform.GetChild (1).GetComponent<Text> ().text;
		Camera.main.GetComponent<CameraFollow> ().ConfirmDeletePanel.SetActive (true);
	}
    void TaskOnClick()
    {

		if (Camera.main.GetComponent<CameraFollow> () != null) {
			Camera.main.GetComponent<CameraFollow> ().Callloadmap (GetComponentInChildren<Text> ().text, true);
		}
		if (Camera.main.GetComponent<networkcamera> () != null) {
			//check if level is a campaign



			Camera.main.GetComponent<networkcamera> ().multiplayertab.GetComponent<GUIAnimFREE>().MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

			if (Camera.main.GetComponent<networkcamera> ().multiplayertab != null) {
				//Check if map is created from message tab
				if (Camera.main.GetComponent<networkcamera> ().multiplayertab.activeSelf == false && Camera.main.GetComponent<networkcamera> ().friendtab.activeSelf == true) {
					Camera.main.GetComponent<networkcamera> ().friendtab.SetActive (false);
					Camera.main.GetComponent<networkcamera> ().multiplayertab.SetActive (true);
					Camera.main.GetComponent<networkcamera> ().friendtab.GetComponent<GUIAnimFREE> ().MoveOut (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					Camera.main.GetComponent<networkcamera> ().multiplayertab.GetComponent<GUIAnimFREE> ().MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
					StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (Camera.main.GetComponent<networkcamera> ().FriendUserText.GetComponent<Text> ().text, GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));

				}

				if (Camera.main.GetComponent<networkcamera> ().IsLevelEditor == false && transform.childCount >= 4) {
					if (Camera.main.GetComponent<networkcamera> ().MultiplayerToggleImage.activeSelf == true) {
						if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab == "latest") {
							//Check if loading latest level
							StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (transform.GetChild (3).GetComponent<Text> ().text.Substring (9), GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
						} else if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab == "popular") {
							//Check if loading latest level
							StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (transform.GetChild (3).GetComponent<Text> ().text.Substring (9), GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
						} else if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab == "search") {
							//Check if loading my level
							StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (Camera.main.GetComponent<networkcamera> ().SearchingUsername, GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
						}
					}
				} else {
					if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab == "latest") {
						//Check if loading latest level
						StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (transform.GetChild (0).GetComponent<Text> ().text.Substring (9), GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
					} else if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab == "popular") {
						//Check if loading latest level
						StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (transform.GetChild (0).GetComponent<Text> ().text.Substring (9), GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
					} else if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab == "search") {
						//Check if loading my level
						StartCoroutine (Camera.main.GetComponent<networkcamera> ().loadmap (Camera.main.GetComponent<networkcamera> ().SearchingUsername, GetComponentInChildren<Text> ().text, Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
					}
				}


				if (Camera.main.GetComponent<networkcamera> ().CurrentLevelTab=="mylevels") {
					//Check if loading my level
					StartCoroutine( Camera.main.GetComponent<networkcamera> ().loadmap (Camera.main.GetComponent<networkcamera> ().controller.user, GetComponentInChildren<Text> ().text,Camera.main.GetComponent<networkcamera> ().campaigntab.activeSelf));
				}

				Camera.main.GetComponent<networkcamera> ().MultiplayerToggleImage.SetActive (true);
				Camera.main.GetComponent<networkcamera>().CustomiseToggleImage.SetActive (false);
				Camera.main.GetComponent<networkcamera>().CampaignToggleImage.SetActive (false);
				Camera.main.GetComponent<networkcamera>().FriendToggleImage.SetActive (false);
				Camera.main.GetComponent<networkcamera>().MessageToggleImage.SetActive (false);
				Camera.main.GetComponent<networkcamera>().ChatToggleImage.SetActive (false);
			}
		}
		/*
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("NetworkLobby")) {
			Camera.main.GetComponent<networkcamera>().GetComponent<LobbyServerList> ().OnClickCreateMatchmakingGame ();
		}
		*/
	}
	void Update()
	{

	}
}