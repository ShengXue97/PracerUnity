using System.Collections;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

	public Transform[] backgrounds;
	public GameObject[] clouds;
	private float[] parallaxScales;
	public float smoothing;
	private Vector3 previousCameraPosition;

	public GameObject tbg;
	// Use this for initialization
	void Start () {
		smoothing = 50f;
		previousCameraPosition = transform.position;

		var bglist = GameObject.FindGameObjectsWithTag ("bgp");
		backgrounds = new Transform[bglist.Length];

		int count = 0;
		foreach (GameObject bg in bglist) {
			backgrounds [count] = bg.transform;
			count += 1;
		}

		parallaxScales=new float[backgrounds.Length];
		for (int i = 0; i < parallaxScales.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}

		//Move clouds

		foreach (GameObject cloud in clouds) {
			float randx=Random.Range(-10f,10f);
			cloud.transform.position = new Vector3 (randx + cloud.transform.position.x, cloud.transform.position.y, cloud.transform.position.z);

			float speed = Random.Range (0.3f, 0.5f);
			cloud.GetComponent<Rigidbody2D> ().velocity = new Vector2(-speed,0f);
		}
	}
	//0.47
	void Update()
	{
		if (Camera.main != null) {
			if (Camera.main.GetComponent<CameraFollow> () != null) {
				if (Camera.main.GetComponent<CameraFollow> ().GameStarted) {
					foreach (GameObject cloud in clouds) {
						if (cloud.transform.position.x - transform.position.x < -20f && cloud.transform.position.x - transform.position.x > -30f) {
							cloud.transform.position = new Vector3 (40f + cloud.transform.position.x, cloud.transform.position.y, cloud.transform.position.z);
							float speed = Random.Range (0.3f, 0.5f);
							cloud.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed, 0f);
						}
					}

					//print (tbg.transform.position.x - transform.position.x);
			

					foreach (Transform bg in backgrounds) {
						if (bg.transform.position.x - transform.position.x < -31.8f * 2) {
							bg.transform.position = new Vector3 (31.8f * 3 + bg.transform.position.x, bg.transform.position.y, bg.transform.position.z);
						} else if (bg.transform.position.x - transform.position.x > 31.8f * 2) {
							bg.transform.position = new Vector3 (-31.8f * 3 + bg.transform.position.x, bg.transform.position.y, bg.transform.position.z);
						}
					}
				}
			}
		}
	}
	// Update is called once per frame
	void LateUpdate () {
		for (int i = 0; i < backgrounds.Length; i++) {
			Vector3 parallax = (previousCameraPosition - transform.position) * (parallaxScales [i] / smoothing);

			backgrounds [i].position = new Vector3 (backgrounds [i].position.x - parallax.x, backgrounds [i].position.y, backgrounds [i].position.z);
		}

		previousCameraPosition = transform.position;
	}
}
