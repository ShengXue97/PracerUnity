using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

	public GameObject LoadingPanel;
	public Slider LoadingSlider;
	public Text LoadingText;
	public Text LoadingText1;
	public bool isLoading = false;

	public int levelsizeint;
	//Lobby to networklobby
	public void StartLoad(string scenename,bool isPhoton)
	{
		if (!isLoading) {
			isLoading = true;
			StartCoroutine (LoadAsync (scenename, isPhoton));
			StartCoroutine (LoadingTextController (0));
		}
	}

	void Update()
	{
		float progress=0f;

		if (LoadingText != null) {
			if (LoadingText.text != "100%") {
				//print ("progress" + progress);
				LoadingSlider.value = progress;
				LoadingText.text = progress * 100 + "%";
				if (LoadingText.text == "100%") {
					LoadingPanel.SetActive (false);
				}	
			}
		}
	}

	IEnumerator LoadAsync(string scenename,bool isPhoton)
	{
		AsyncOperation operation;
		if (!isPhoton) {
			operation = SceneManager.LoadSceneAsync (scenename);
		} else {
			operation = PhotonNetwork.LoadLevelAsync (scenename);
		}
		LoadingPanel.SetActive (true);

		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);

			LoadingSlider.value = progress;
			LoadingText.text = progress * 100 + "%";

			yield return null;
		}
		isLoading = false;
	}

	IEnumerator LoadingTextController(int num)
	{
		yield return new WaitForSeconds(0.3f);
		LoadingText1.text = "Loading";

		if (num != 0) {
			for (int i = 0; i < num; i++) {
				LoadingText1.text += ".";
			}
		}

		if (num <= 2) {
			num += 1;
		} else {
			num = 0;
		}
		StartCoroutine(LoadingTextController(num));
	}
}
