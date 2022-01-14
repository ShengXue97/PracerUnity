using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioToggle : MonoBehaviour {
	public Sprite audioenabled;
	public Sprite audiodisabled;
	public Controllers controller;
	public int audioprefs;
	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);

		GameObject mycontroller = GameObject.FindGameObjectWithTag ("controller");
		if (mycontroller!=null)
		{
			controller = mycontroller.GetComponent<Controllers> ();
		}

		if (!controller.AudioEnabled) {
			controller.AudioEnabled = false;
			controller.GetComponent<AudioSource>().volume=0.0f;
			Camera.main.GetComponent<AudioSource>().volume=0.0f;

			GetComponent<Image> ().sprite = audiodisabled;

		} else if (controller.AudioEnabled){
			controller.AudioEnabled = true;
			controller.GetComponent<AudioSource>().volume=1.0f;
			Camera.main.GetComponent<AudioSource>().volume=1.0f;

			GetComponent<Image> ().sprite = audioenabled;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick()
	{
		if (controller.AudioEnabled) {
			controller.AudioEnabled = false;
			controller.GetComponent<AudioSource>().volume=0.0f;
			Camera.main.GetComponent<AudioSource>().volume=0.0f;

			GetComponent<Image> ().sprite = audiodisabled;

		} else if (!controller.AudioEnabled){
			controller.AudioEnabled = true;
			controller.GetComponent<AudioSource>().volume=1.0f;
			Camera.main.GetComponent<AudioSource>().volume=1.0f;

			GetComponent<Image> ().sprite = audioenabled;
		}
	}
}
