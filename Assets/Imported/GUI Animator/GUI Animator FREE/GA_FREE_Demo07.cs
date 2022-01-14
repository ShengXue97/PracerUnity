// GUI Animator FREE
// Version: 1.1.5
// Compatilble: Unity 5.5.1 or higher, see more info in Readme.txt file.
//
// Developer:							Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
//
// Unity Asset Store:					https://www.assetstore.unity3d.com/en/#!/content/58843
// See Full version:					https://www.assetstore.unity3d.com/en/#!/content/28709
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion // Namespaces

// ######################################################################
// GA_FREE_Demo07 class
// - Animates all GUIAnimFREE elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA FREE - Demo07 (960x600px)" scene.
// ######################################################################

public class GA_FREE_Demo07 : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################
	
	#region Variables

	// Canvas
	public GameObject InfoPanel;
	public Canvas m_Canvas;
	// GUIAnimFREE object of dialogs
	public GUIAnimFREE m_Dialog;
	public GUIAnimFREE m_DialogButtons;
	
	// GUIAnimFREE objects of buttons
	public GUIAnimFREE m_Button1;
	public GUIAnimFREE m_infopanel;
	
	#endregion // Variables
	
	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################
	
	#region MonoBehaviour
	
	// Awake is called when the script instance is being loaded.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
	void Awake ()
	{
		if(enabled)
		{
			// Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start ()
	{
		
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		// MoveIn all buttons
		m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		// Enable all scene switch buttons
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);

	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update ()
	{		
	}
	
	#endregion // MonoBehaviour
	
	// ########################################
	// MoveIn/MoveOut functions
	// ########################################
	
	#region MoveIn/MoveOut


	
	// MoveOut all dialogs and buttons
	public void HideAllGUIs()
	{
		// MoveOut all dialogs
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		
		// MoveOut all buttons
		m_Button1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

	}

	
	#endregion // MoveIn/MoveOut

	
	// ########################################
	// UI Responder functions
	// ########################################
	
	#region UI Responder

	public void OnButton_1()
	{
		// MoveOut m_Button1
		MoveButtonsOut();


		// Set next move in of m_Button1 to new position
		StartCoroutine(SetButtonMove(GUIAnimFREE.ePosMove.UpperScreenEdge, GUIAnimFREE.ePosMove.UpperScreenEdge));
	}
	

	
	public void OnDialogButton()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);


		// Moves m_Dialog back to screen
		//StartCoroutine(DialogMoveIn());
	}

	public void OnInfoButton()
	{
		// Set next MoveIn position of m_Button1 to PosMoveIn
		m_infopanel.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
		// Reset m_Button1
		m_infopanel.Reset();
		// MoveIn m_Button1
		m_infopanel.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	#endregion // UI Responder
	
	// ########################################
	// Move Dialog/Button functions
	// ########################################
	
	#region Move Dialog/Button
	
	// MoveOut all buttons
	void MoveButtonsOut()
	{
		m_Button1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	// MoveOut info panel
	public void MoveInfoOut()
	{
		m_infopanel.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	// Set next move in of all buttons to new position
	IEnumerator SetButtonMove(GUIAnimFREE.ePosMove PosMoveIn, GUIAnimFREE.ePosMove PosMoveOut)
	{
		yield return new WaitForSeconds(2.0f);
		
		// Set next MoveIn position of m_Button1 to PosMoveIn
		m_Button1.m_MoveIn.MoveFrom = PosMoveIn;
		// Reset m_Button1
		m_Button1.Reset();
		// MoveIn m_Button1
		m_Button1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	// Set next move in of info panel
	public void SetInfoMove(GUIAnimFREE.ePosMove PosMoveIn, GUIAnimFREE.ePosMove PosMoveOut)
	{
		InfoPanel.SetActive (true);
		// Set next MoveIn position of m_Button1 to PosMoveIn
		m_infopanel.m_MoveIn.MoveFrom = PosMoveIn;
		// Reset m_Button1
		m_infopanel.Reset();
		// MoveIn m_Button1
		m_infopanel.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	public void MoveDialogIn()
	{
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
	// Moves m_Dialog back to screen
	IEnumerator DialogMoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		
		m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
	
	#endregion // Move Dialog/Button
}
