using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class loginengine : MonoBehaviour {
	public float LastFPS;
	public GameObject FPS;
	public GameObject LoadingController;
	public GameObject CloseButton;
	public InputField logintext;
	public InputField passwordtext;
	public GameObject UserInput;
	public GameObject PasswordInput;
	public bool remember=true;
	public GameObject RememberButton;
	public GameObject InfoPanel;
	public GameObject SceneController;
	public GUIAnimFREE m_infopanel;
	public GA_FREE_Demo07 anim;
	public Text InfoText;
	public Sprite Remember0;
	public Sprite Remember1;
	public List<string> userlist;
	private bool canlogin = false;
	public string user;
	public string password;
	public bool exist;
	public string myhash = "";
	public string salthash="";
	public string serverhash="";
	public Controllers controller;
	public int SSpeed;
	public int SAcc;
	public int SJump;
	public int SExpBonus;
	protected string MyStorageBucket = "gs://composite-store-188023.appspot.com/";
	public bool InProgress=false;

	private string logText = "";
	public AWSControl AWS;

	void Start()
	{
		GameObject AWSControl = GameObject.FindGameObjectWithTag ("awscontrol");
		AWS = AWSControl.GetComponent<AWSControl> ();
		anim = SceneController.GetComponent<GA_FREE_Demo07> ();

		if (PlayerPrefs.GetString ("remember") != null) {
			if (PlayerPrefs.GetString ("remember") == "true") {
				RememberButton.GetComponent<Image> ().sprite = Remember1;
				UserInput.GetComponent<InputField> ().text = PlayerPrefs.GetString ("user");
				PasswordInput.GetComponent<InputField> ().text = PlayerPrefs.GetString ("password");
				remember = true;
			}
			else {
				RememberButton.GetComponent<Image> ().sprite = Remember0;
				remember = false;
			}
		} else {
			RememberButton.GetComponent<Image> ().sprite = Remember0;
		}
		controller = GameObject.FindGameObjectWithTag ("controller").GetComponent<Controllers> ();
		FPS = GameObject.FindGameObjectWithTag ("FPS");

	}

	// Update is called once per frame
	void Update () {
		if (LastFPS != null && Time.time - LastFPS > 0.5f) {
			LastFPS = Time.time;
			FPS.GetComponent<Text> ().text = (1.0f / Time.deltaTime).ToString ().Split ('.') [0] + " FPS";
		}
		//Append to text file
		/*
		StreamWriter writer2 = new StreamWriter(path, true);
		writer2.WriteLine("Test");
		writer2.Close();
		*/

	}


	//Remember me
	public void clickrememberme()
	{
		if (remember == true) {
			remember = false;
			RememberButton.GetComponent<Image> ().sprite = Remember0;
		} else {
			remember = true;
			RememberButton.GetComponent<Image> ().sprite = Remember1;
		}
	}

	public void login()
	{
		if (InProgress == false) {
			InProgress = true;
			user = logintext.text.ToLower();
			password = passwordtext.text;
			StartCoroutine (getuserlistexist (user, true));
		}
	}

	public IEnumerator login2()
	{
		myhash = Md5Sum (password.ToString ());

		AWS.AWSDownload ("14","user/" + user + "/" + "password.txt");
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			CloseButton.SetActive (true);
			AWS.result = "";
			anim.OnInfoButton ();
			InProgress = false;
		} else if (AWS.result.Split('|')[0]=="14"){
			var arr = AWS.result.Split('|')[1].Split (';');
			salthash=Md5Sum (arr[0].ToString ());
			serverhash=arr[1];
			AWS.result = "";


			//Check password
			InfoText.text= ("Logging in...");
			if (serverhash.ToString().Contains(myhash.ToString() + salthash.ToString())) {

				InfoText.text= ("\nYou have successfully \n logged into " + user + ".");
				CloseButton.SetActive (false);
				InfoPanel.SetActive (false);

				if (remember) {
					PlayerPrefs.SetString ("user", user);
					PlayerPrefs.SetString ("password", password);
					PlayerPrefs.SetString ("remember", "true");
				} else {
					PlayerPrefs.SetString ("remember", "false");
				}
				controller.user = user;
				StartCoroutine (downloadrank ());

			} else {
				CloseButton.SetActive (true);
				InfoPanel.SetActive (true);
				InfoText.text= ("\nYou have entered the wrong \n password. Please try again.");
				anim.OnInfoButton ();
				InProgress = false;
			}
		}



	}


	public void register()
	{
		if (InProgress == false) {
			InProgress = true;
			user = logintext.text.ToLower();
			password = passwordtext.text;
			StartCoroutine (getuserlistexist (user, false));

		}
	}

	public IEnumerator register2()
	{
		string serverhash = Md5Sum (password.ToString ());
		float saltnum = UnityEngine.Random.Range (0, 3406990346);
		string salthash = Md5Sum (saltnum.ToString ());


		AWS.AWSUpload ("user/" + user + "/password.txt",saltnum + ";" + serverhash + salthash);

		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "true") {
			AWS.result = "";
			AWS.AWSUpload ("user/" + user + "/rank.txt", "0;0;50;50;50;0");
		}
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "true") {
			AWS.result = "";
			AWS.AWSUpload ("user/" + user + "/maps/New.txt", "Race#{{\"4,-1\",\"b1\"},{\"3,-1\",\"b1\"},{\"5,0\",\"start\"},{\"5,-1\",\"b1\"},{\"2,-1\",\"b1\"},{\"6,-1\",\"b1\"},{\"7,-1\",\"b1\"},{\"8,-1\",\"b1\"},{\"9,-1\",\"b1\"},{\"1,-1\",\"b1\"}}");
		}
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "true") {
			AWS.result = "";
			AWS.AWSUpload ("user/" + user + "/maps/maplist.txt", "New");
		}
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "true") {
			CloseButton.SetActive (true);
			AWS.result = "";
			InfoPanel.SetActive (true);
			InfoText.text = ("\nYou have successfully \n created " + user + ".");
			anim.OnInfoButton ();
			InProgress = false;
		} else {
			AWS.result="";
			InProgress = false;
		}
	}

	//AWS METHODS
	public IEnumerator getuserlistexist(string user, bool islogin)
	{
		int existing=0;
		//For registering
		if (islogin==false)
		{
			InfoText.text= ("Signing up...");
			AWS.AWSDownload ("15","user/" + user + "/rank.txt");
			while (AWS.result == "") {
				yield return null;
			}
			if (AWS.result=="none") {
				//if user does not exist
				AWS.result="";
				StartCoroutine(register2());
			} else if (AWS.result.Split('|')[0]=="15"){
				//if user already exists
				CloseButton.SetActive (true);
				AWS.result="";
				InfoPanel.SetActive (true);
				InfoText.text=("The username you have \n chosen already exists. \n Please choose another username.");
				existing=1;
				anim.OnInfoButton ();
				InProgress = false;
			}

		}

		//For logging in
		else if (islogin==true)
		{
			InfoText.text= ("Logging in...");
			AWS.AWSDownload ("16","user/" + user + "/rank.txt");
			while (AWS.result == "") {
				yield return null;
			}

			if (AWS.result=="none") {
				//if user does not exist
				CloseButton.SetActive (true);
				AWS.result="";
				InfoPanel.SetActive (true);
				InfoText.text=("The current username does \n not exist. Please enter \n your username again.");
				anim.OnInfoButton ();

				InProgress = false;
			} else if (AWS.result.Split('|')[0]=="16"){
				//if user exists
				AWS.result="";
				existing=1;
				StartCoroutine( login2());
			}
		}	
	}




	//Downloads and sets user rank and experience(rank.txt)
	public IEnumerator downloadrank() {
		AWS.AWSDownload ("17","user/" + controller.user + "/" + "rank.txt");
		while (AWS.result == "") {
			yield return null;
		}

		if (AWS.result=="none") {
			AWS.result="";
			InProgress = false;
		} else if (AWS.result.Split('|')[0]=="17"){
			string fileContents = AWS.result.Split('|')[1];
			AWS.result = "";
			var newarr = fileContents.Split (';');
			float rank = float.Parse (newarr [0]);
			float exp = float.Parse (newarr [1]);

			SSpeed = int.Parse (newarr [2]);
			SAcc = int.Parse (newarr [3]);
			SJump = int.Parse (newarr [4]);
			SExpBonus = int.Parse (newarr [5]);
			if (rank != null && exp != null) {
				controller.rank = rank;
				controller.exp = exp;
				controller.SSpeed = SSpeed;
				controller.SAcc=SAcc;
				controller.SJump=SJump;
				controller.SExpBonus=SExpBonus;
				InProgress = false;
				//Start loading screen


				LoadingController.GetComponent<Loader> ().StartLoad ("NetworkLobby",false);
				//SceneManager.LoadScene ("NetworkLobby");
			}
		}
	}

	public string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}
