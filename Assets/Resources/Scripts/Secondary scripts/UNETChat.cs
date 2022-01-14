using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UNETChat : Chat
{
	//just a random number
	private const short chatMessage = 131;
	public Controllers controller;
	public Text ChatText;
	public GameObject ChatView;
	public GameObject ChatPrefab;
	public CameraFollow camfollow;
	public string User;
	public float rank;
	public int previouslength = 0;
	public string fullchat = "";
	public string lastchat="";
	public float fadingmin=0.03f;
	public List<GameObject> chatbubbles;
	private void Start()
	{
		controller = GameObject.FindGameObjectWithTag ("controller").GetComponent<Controllers> ();
		camfollow = Camera.main.GetComponent<CameraFollow> ();
		User = controller.user;
		rank = controller.rank;
		//if the client is also the server
		if (NetworkServer.active) 
		{
			//registering the server handler
			NetworkServer.RegisterHandler(chatMessage, ServerReceiveMessage);
		}

		//registering the client handler
		//NetworkManager.singleton.client.RegisterHandler (chatMessage, ReceiveMessage);
	}
	public void Update()
	{
		foreach (GameObject chatbubble in chatbubbles) {
			Color chatbubbleColor = chatbubble.GetComponent<SpriteRenderer> ().color;
			//if chatbubble is fading in 
			if (chatbubble.name.Contains("chatbubblefadingin")) {
				float startTime = float.Parse( chatbubble.name.Split(';')[1]);
				float minimum = fadingmin;
				float maximum = 0.8f;
				float time = 1.0f;
				float t = (Time.time - startTime) / time;
				chatbubble.GetComponent<SpriteRenderer> ().color = new Color (chatbubble.GetComponent<SpriteRenderer> ().color.r, chatbubble.GetComponent<SpriteRenderer> ().color.g, chatbubble.GetComponent<SpriteRenderer> ().color.b, Mathf.SmoothStep (minimum, maximum, t));
				chatbubble.GetComponentInChildren<Text>().color = new Color (chatbubble.GetComponentInChildren<Text>().color.r, chatbubble.GetComponentInChildren<Text>().color.g, chatbubble.GetComponentInChildren<Text>().color.b, Mathf.SmoothStep (minimum, maximum, t));

				if (chatbubble.GetComponent<SpriteRenderer> ().color == new Color (chatbubble.GetComponent<SpriteRenderer> ().color.r, chatbubble.GetComponent<SpriteRenderer> ().color.g, chatbubble.GetComponent<SpriteRenderer> ().color.b, 0.8f)) {
					chatbubble.name = "chatsolid;"+startTime;
				}
			}
			//else if chatbubble is fading out
			else if (chatbubble.name.Contains("chatbubblefadingout")) {
				float startTime = float.Parse( chatbubble.name.Split(';')[1]);
				float minimum = 0.03f;
				float maximum = 0.8f;
				float time = 1.0f;
				float t = (Time.time - startTime) / time;
				chatbubble.GetComponent<SpriteRenderer> ().color = new Color (chatbubble.GetComponent<SpriteRenderer> ().color.r, chatbubble.GetComponent<SpriteRenderer> ().color.g, chatbubble.GetComponent<SpriteRenderer> ().color.b, Mathf.SmoothStep(maximum, minimum, t));
				chatbubble.GetComponentInChildren<Text>().color = new Color (chatbubble.GetComponentInChildren<Text>().color.r, chatbubble.GetComponentInChildren<Text>().color.g, chatbubble.GetComponentInChildren<Text>().color.b, Mathf.SmoothStep (maximum, minimum, t));

				if (chatbubble.GetComponent<SpriteRenderer> ().color == new Color (chatbubble.GetComponent<SpriteRenderer> ().color.r, chatbubble.GetComponent<SpriteRenderer> ().color.g, chatbubble.GetComponent<SpriteRenderer> ().color.b, 0.03f)) {
					chatbubble.name = "chatempty;"+startTime;
					fadingmin = 0.03f;
					chatbubble.SetActive (false);

				}
			}

		}
	}

	private void ReceiveMessage(NetworkMessage message)
	{
		//reading message
		string text = message.ReadMessage<StringMessage> ().value;

		//Message about spawning blocks
		if (text [0] == '#') {
			var split = text.Split ('#');
			//GetComponent<BlockSpawner> ().SpawnBlock (split [1], split [2], split [3], split [4], split [5], split [6], split [7],split[8],split[9],split[10],blockmessage);

			return;
		}
		//Message about deleting blocks
		if (text [0] == '%') {
			GetComponent<BlockSpawner> ().RemoveBlock (text.Split ('%') [1]);

			return;
		}

		AddMessage ();

		//Prevent spam messages
		if (text!=lastchat)
		{
			lastchat = text;
			fullchat+=text;

			var ChatList = fullchat.Split (';');
			var ChatSpawn = Instantiate (ChatPrefab, ChatView.transform);
			ChatSpawn.transform.position = new Vector3 (191f, ((ChatList.Length) * 30f), 0f);

			var split = text.Split (':');
			var nosemi = split [1].Split (';');
			ChatSpawn.GetComponent<Text> ().text = "<color=#676EFFFF>" + split [0] +"</color>"+":"+nosemi[0];
			if (camfollow.ChatPanel.GetComponentInChildren<Scrollbar> () != null) {
				camfollow.ChatPanel.GetComponentInChildren<Scrollbar> ().value = camfollow.ChatPanel.GetComponentInChildren<Scrollbar> ().size;
			}
			//chat bubble
			string user=split[0];
			var players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players) {
				if (1==1) {
					if (user.Split('(')[0] == player.GetComponentInChildren<Text> ().text.Split('(')[0]) {
						GameObject chatbubble = player.transform.GetChild (9).gameObject;
						chatbubble.SetActive (true);
						chatbubble.GetComponentInChildren<Text> ().text = nosemi[0];
						float startTime=Time.time;
						//only fade in if chat bubble is hidden
						if (chatbubble.name.Contains("chatempty"))
						{
							chatbubble.name = "chatbubblefadingin+;"+startTime.ToString();
							chatbubbles.Add (chatbubble);
						}
						//reset timer for fading out if chat bubble is solid
						else if (chatbubble.name.Contains("chatsolid"))
						{
							chatbubble.name = "chatsolid+;"+startTime.ToString();
						}
						//fade in if chat bubble is fading out
						else if (chatbubble.name.Contains("chatbubblefadingout"))
						{
							chatbubble.name = "chatbubblefadingin+;"+startTime.ToString();
							fadingmin = chatbubble.GetComponent<SpriteRenderer> ().color.a;
						}
						StartCoroutine (DisableChatBubble (5f, chatbubble, startTime));
					}
				}
			}
		}
			

	}

	private void ServerReceiveMessage(NetworkMessage message)
	{
		StringMessage myMessage = new StringMessage ();
		//we are using the connectionId as player name only to exemplify
		myMessage.value = message.ReadMessage<StringMessage> ().value;

		//sending to all connected clients
		NetworkServer.SendToAll (chatMessage, myMessage);
	}

	public override void SendMessage (UnityEngine.UI.InputField input)
	{
		StringMessage myMessage = new StringMessage ();
		//getting the value of the input
		myMessage.value =User + "("+rank.ToString()+")"+":" +  input.text+";";

		//sending to server
		NetworkManager.singleton.client.Send (chatMessage, myMessage);
	}

	public void SendBlockMessage (string inputtext)
	{
		StringMessage myMessage = new StringMessage ();
		//getting the value of the input
		myMessage.value =inputtext;

		//sending to server
		NetworkManager.singleton.client.Send (chatMessage, myMessage);
	}

	public void SendRemoveBlockMessage (string inputtext)
	{
		StringMessage myMessage = new StringMessage ();
		//getting the value of the input
		myMessage.value =inputtext;

		//sending to server
		NetworkManager.singleton.client.Send (chatMessage, myMessage);
		print ("Sending msg" + inputtext);
	}

	public IEnumerator DisableChatBubble(float time, GameObject chatbubble,float starttime)
	{

		yield return new WaitForSeconds (time);

		// Code to execute after the delay
		if (starttime.ToString() == chatbubble.name.Split (';') [1]) {
			chatbubble.name = "chatbubblefadingout+;" + Time.time.ToString ();
		}
	}

	public void ShutDownNow()
	{
		NetworkServer.Shutdown ();
		NetworkManager.Shutdown ();
	}
}