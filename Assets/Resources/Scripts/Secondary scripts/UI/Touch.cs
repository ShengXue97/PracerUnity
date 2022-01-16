using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour
{
	private Controls pco;
	public Rigidbody2D prb;
	public CameraFollow camfollow;
	public Controllers controller;

    // Use this for initialization
    void Start()
    {
		GameObject mycontroller = GameObject.FindGameObjectWithTag ("controller");
		if (mycontroller!=null)
		{
			controller = mycontroller.GetComponent<Controllers> ();
		}

		camfollow = Camera.main.GetComponent<CameraFollow> ();
    }

	void Update()
	{
		var players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject myplayer in players) {
			if (myplayer.GetComponent<Controls>().user==controller.user)
			{
				pco = myplayer.GetComponent<Controls> ();
				prb= myplayer.GetComponent<Rigidbody2D> ();
			}
		}
	}


	public void UseItem()
	{
		pco.useitem = true;
	}


	public void ReleaseUseItem()
	{
		pco.useitem = false;
	}
}