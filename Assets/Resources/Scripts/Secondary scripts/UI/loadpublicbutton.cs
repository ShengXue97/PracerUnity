using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadpublicbutton : MonoBehaviour {
	public Sprite Remember0;
	public Sprite Remember1;
	public GameObject UICanvas;
	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick()
	{



	}
}
