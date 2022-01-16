using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICanvas : MonoBehaviour {
	public GameObject LoadPanel;
	public GameObject PublicPanel;
	public GameObject PublicPanelText;
	public GameObject ScrollBarVertical;
	public GameObject content;
	public GameObject loadpublicbutton;
	public GameObject mapinputfield;
	public GameObject CreateButton;
	public Button LoadButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main != null) {
			if (Camera.main.GetComponent<networkcamera> () != null) {
				Camera.main.GetComponent<networkcamera> ().LoadPanel = LoadPanel;
				Camera.main.GetComponent<networkcamera> ().ScrollBarVertical = ScrollBarVertical;
				Camera.main.GetComponent<networkcamera> ().content = content;
			}
		}
	}
}
