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
// GA_FREE_Demo08 class
// - Animates all GUIAnimFREE elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA FREE - Demo08 (960x600px)" scene.
// ######################################################################

public class GA_FREE_Demo08 : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################
	
	#region Variables

	// Canvas
	public Canvas m_Canvas;


	// GUIAnimFREE objects of top, left, right and bottom bars
	public GUIAnimFREE m_Bar1;
	public GUIAnimFREE m_CustomiseTab;
	public GUIAnimFREE m_CampaignTab;
	public GUIAnimFREE m_ChatTab;
	public GUIAnimFREE m_MultiplayerTab1;
	public GUIAnimFREE m_MultiplayerTab2;
	public GUIAnimFREE m_FriendTab;
	public GUIAnimFREE m_MessageTab;
	public GUIAnimFREE m_SettingTab;
	public GUIAnimFREE m_LevelTab;
	public GUIAnimFREE m_LoadPanel;
	public GUIAnimFREE m_SavePanel;
	public GUIAnimFREE m_SearchPanel;
	public GUIAnimFREE m_InvitePanel;

	public GUIAnimFREE m_infopanel;
	// Toggle state of top, left, right and bottom bars
	bool m_Bar1_IsOn = false;

	public GameObject InfoPanel;
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
		//OnInfoButton ();
	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start ()
	{
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());

		// Disable all scene switch buttons
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
		m_MultiplayerTab2.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);	
		OnButton_1 ();

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

	
	// MoveOut all primary buttons
	public void HideAllGUIs()
	{
		// MoveOut all side bars
		if(m_Bar1_IsOn==true)
			m_Bar1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	#endregion // MoveIn/MoveOut
	
	// ########################################
	// Enable/Disable button functions
	// ########################################
	
	#region Enable/Disable buttons
	
	// Enable/Disable all scene switch Coroutine
	IEnumerator EnableAllDemoButtons()
	{
		yield return new WaitForSeconds(1.0f);

		// Enable all scene switch buttons
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}
	
	// Disable all buttons for a few seconds
	IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		// Disable all buttons
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, false);
		
		yield return new WaitForSeconds(DisableTime);
		
		// Enable all buttons
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, true);
	}
	
	#endregion // Enable/Disable buttons
	
	// ########################################
	// UI Responder functions
	// ########################################
	
	#region UI Responder

	public void MoveInfoIn()
	{
		InfoPanel.SetActive (true);
		// Set next MoveIn position of m_Button1 to PosMoveIn
		m_infopanel.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
		// Reset m_Button1
		m_infopanel.Reset();
		// MoveIn m_Button1
		m_infopanel.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

	// MoveOut info panel
	public void MoveInfoOut()
	{
		m_infopanel.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		InfoPanel.SetActive (false);
	}


	public void OnButton_1()
	{
		// Toggle m_Bar1
		ToggleBar1();


	}

	public void MovePanels(int no)
	{
		//Customise tab
		if (no == 0) {
			m_CustomiseTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 1) {
			//m_CampaignTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_CampaignTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 2) {
			//m_ChatTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_ChatTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		}  else if (no == 3) {
			//m_MultiplayerTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			//m_MultiplayerTab1.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
			m_MultiplayerTab2.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 4) {
			//m_FriendTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_FriendTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 5) {
			//m_MessageTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_MessageTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 6) {
			m_SettingTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_SettingTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 7) {
			m_SettingTab.MoveOut (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 8) {
			m_LoadPanel.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 9) {
			m_SavePanel.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 10) {
			m_LevelTab.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_LevelTab.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 11) {
			m_LevelTab.MoveOut (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 12) {
			m_SearchPanel.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
			m_SearchPanel.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 13) {
			m_SearchPanel.MoveOut (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 14) {
			m_InvitePanel.MoveIn (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		} else if (no == 15) {
			m_InvitePanel.MoveOut (GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

		}
	}

	#endregion // UI Responder
	
	// ########################################
	// Toggle button functions
	// ########################################
	
	#region Toggle Button
	
	// Toggle m_Bar1
	void ToggleBar1()
	{
		m_Bar1_IsOn = !m_Bar1_IsOn;
		if(m_Bar1_IsOn==true)
		{
			// m_Bar1 moves in
			m_Bar1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_Bar1 moves out
			m_Bar1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		}
	}

	
	#endregion // Toggle Button

}
