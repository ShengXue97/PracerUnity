using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class loadbutton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(() => { loadbuttondown2(); });
	}
	
	// Update is called once per frame
	void Update () {
	}
	//click load button
	public void loadbuttondown2()
	{
		Camera.main.GetComponent<networkcamera> ().loadbuttondown (false);

	}
}
