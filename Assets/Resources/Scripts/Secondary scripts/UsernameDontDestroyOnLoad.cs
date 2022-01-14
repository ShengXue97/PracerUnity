using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsernameDontDestroyOnLoad : MonoBehaviour {
	public string user;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "level1") {
			Camera.main.GetComponent<CameraFollow> ().user = user;
		}
		if (SceneManager.GetActiveScene ().name == "NetworkLobby") {
			if (Camera.main != null) {
				Camera.main.GetComponent<networkcamera> ().user = user;
			}
		}
	}
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

}
