using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour {

	public GUIAnimFREE Title;
	public GA_FREE_Demo07 scenecontroller;
	// Use this for initialization
	void Start () {
		Title.MoveIn ();
		//StartCoroutine (MoveInUI ());
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (5f, 0f);
	}

	public IEnumerator MoveInUI()
	{
		yield return new WaitForSeconds(4f);
		Title.MoveOut ();
		// MoveIn 
		// MoveIn all dialogs and buttons
		// MoveIn all dialogs
		scenecontroller.m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		scenecontroller.m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		// MoveIn all buttons
		scenecontroller.m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		// Enable all scene switch buttons
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(scenecontroller.m_Canvas, true);
	}
}
