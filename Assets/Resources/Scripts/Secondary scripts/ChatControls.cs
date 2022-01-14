using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatControls : MonoBehaviour {
	public PhotonView photonView;
	public networkcamera NetworkCamera;
	// Use this for initialization
	void Start () {
		NetworkCamera = Camera.main.GetComponent<networkcamera> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateChat(InputField input)
	{	
		string user = NetworkCamera.controller.GetComponent<Controllers> ().user;
		string rank = NetworkCamera.controller.GetComponent<Controllers> ().rank.ToString();

		NetworkCamera.SelectedChannelText.GetComponent<Text> ().text += user+"("+rank+"): "+ input.GetComponent<InputField>().text + "\n";
		photonView.RPC ("SendChat", PhotonTargets.OthersBuffered,user+"("+rank+"): "+ input.GetComponent<InputField>().text + "\n");
		input.GetComponent<InputField> ().text = "";
	}

	[PunRPC]
	public void SendChat(string input)
	{
		Camera.main.GetComponent<networkcamera> ().SelectedChannelText.GetComponent<Text> ().text += input;
	}


}
