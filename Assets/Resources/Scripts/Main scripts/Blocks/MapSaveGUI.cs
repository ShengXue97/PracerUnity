using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
public class MapSaveGUI : MonoBehaviour
{
	public GameObject FinishPanel;
    public GameObject LoadPanel;
    public GameObject SavePanel;
    public GameObject ScrollBarVertical;
	public GameObject loadpublicbutton;
	public GameObject mapbarparent;
	public Sprite Remember0;
	public Sprite Remember1;
    public string maps;
	public List<string> fileInfo;
    public CameraFollow maincam;
    public int stringlocation;
    public string lastmaplocation;
    public bool loadpanelactive = false;
    public int count;
	public bool loadpublic=false;
	public bool IsLoadingMaps=false;
	protected string MyStorageBucket = "gs://composite-store-188023.appspot.com/";
	public Controllers controller;

	private string logText = "";
	public AWSControl AWS;
    // Use this for initialization
    void Start()
    {
		GameObject mycontroller = GameObject.FindGameObjectWithTag ("controller");
		if (mycontroller!=null)
		{
			controller = mycontroller.GetComponent<Controllers> ();
		}

		GameObject AWSControl = GameObject.FindGameObjectWithTag ("awscontrol");
		AWS = AWSControl.GetComponent<AWSControl> ();
		if (SceneManager.GetActiveScene()==SceneManager.GetSceneByName("LevelEditor"))
		{
			if (LoadPanel != null) {
				LoadPanel.SetActive (false);
			}
			if (SavePanel != null) {
				SavePanel.SetActive (false);
			}
			if (FinishPanel != null) {
				FinishPanel.SetActive (false);
			}
		}
		maincam = Camera.main.GetComponent<CameraFollow>();
    }
    // Update is called once per frame
    void Update()
    {
		//Exit if camera or the component does not exist
		if (Camera.main != null) {
			if (Camera.main.GetComponent<CameraFollow> () == null) {
				return;
			}
		}
		if (Camera.main == null) {
			return;
		}

        //Quit is client has not joined a game

		if (Camera.main.GetComponent<CameraFollow>().GameStarted == false)
		{
			return;
		}
		if (Camera.main.GetComponent<CameraFollow>().PauseGame == true)
		{
			return;
		}
	    
		if (SceneManager.GetActiveScene()!=SceneManager.GetSceneByName("LevelEditor"))
		{
			return;
		}


		if (Input.GetKeyDown (KeyCode.M)) {
			Camera.main.GetComponent<CameraFollow> ().PauseGame = true;
			savemap ();
		}
        if (Input.GetKeyDown(KeyCode.L))
        {
			Camera.main.GetComponent<CameraFollow> ().PauseGame = true;
			loadmap ();
        }



    }
	public void savemap()
	{
		if (SavePanel.activeSelf == true) {
			Camera.main.GetComponent<CameraFollow> ().PauseGame = false;
			return;
		}
		Camera.main.GetComponent<CameraFollow> ().issaving = true;
		Camera.main.GetComponent<CameraFollow> ().PauseGame = true;
		SavePanel.SetActive (true);

	}

	public void loadmap()
	{
		if (IsLoadingMaps) {
			return;
		}
		IsLoadingMaps = true;
		Camera.main.GetComponent<CameraFollow>().PauseGame = false;
		if (maincam != null) {
			if (maincam.maplist != null) {
				maincam.maplist.Clear ();
			}
		}

		Camera.main.GetComponent<CameraFollow> ().issaving = false;
		Camera.main.GetComponent<CameraFollow>().PauseGame = true;
		LoadPanel.SetActive(true);

		StartCoroutine( listmap ());
	}
	public void callListmap()
	{
		StartCoroutine (listmap ());
	}
	public IEnumerator listmap()
	{
		maincam.DynamicScrollView.clearEditorList ();
		fileInfo.Clear ();
		maincam.maplist.Clear ();



		//Get list of files in directory
		var mapbarspublic=GameObject.FindGameObjectsWithTag("public");
		var mapbarsprivate=GameObject.FindGameObjectsWithTag("private");

		foreach (GameObject mapbar in mapbarspublic) {
			Destroy (mapbar);
		}
		foreach (GameObject mapbar in mapbarsprivate) {
			Destroy (mapbar);
		}

		//Download map list first
		string dir;
		string fileContents;
		if (loadpublic == true || controller.user=="") {
			dir = "maplist.txt";
		} else {
			dir = "user/"+controller.user+"/maps/" + "maplist.txt";
		}
		AWS.AWSDownload ("18",dir);
		while (AWS.result == "") {
			yield return null;
		}
		if (AWS.result == "none") {
			AWS.result = "";
		} else if (AWS.result.Split('|')[0]=="18"){
			fileContents = AWS.result.Split('|')[1];
			AWS.result = "";

			var arr = fileContents.Split ('/');
			foreach (string file in arr) {
				fileInfo.Add(file);
			}
			loadmaps();	
		}


	}

	public void loadmaps()
	{
		//fileInfo.Reverse ();
		foreach (var file in fileInfo)
		{
			string filename = file.ToString();
			string myfile = filename.Replace(".txt", "");
			maincam.maplist.Add(myfile);
		}

		foreach (string file in maincam.maplist)
		{
			maincam.DynamicScrollView.AddEditorItem (0, file);
		}
		IsLoadingMaps = false;
	}
}






