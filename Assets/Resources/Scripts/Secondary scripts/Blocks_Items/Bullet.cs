using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
	public Controls pco;
	public Rigidbody2D prb;
	public List<Sprite> frames;
	public CameraFollow mcam;
    void OnTriggerEnter2D(Collider2D other)
    {
		if (name.Contains ("Lightning")) {
			return;
		}
		if (Camera.main.GetComponent<CameraFollow> ().editplay == 0) {
			return;
		}
		var hit = other.gameObject;
		pco = hit.GetComponent<Controls> ();
		prb = hit.GetComponent < Rigidbody2D> ();
		if (tag==( "bomb"))
		{
			var health = other.GetComponent<health>();
			if (health != null)
			{
				health.TakeDamage(10);
			}

			AddExplosionForce(prb, 400f, transform.position, 10f);
			StartCoroutine(animate(0.025f,frames,0,gameObject));
			//exploding = true;
		}

		if (hit != null && hit.tag!="teleportcheck" && tag!="bomb")
		{
			//Sword
			if (pco != null) {
				if (name.Contains ("Sword") && name != pco.user + "Sword") {
					//hit.GetComponent<Controls>().isLocalPlayer
					AddExplosionForce(prb, 400f, transform.position, 10f);
					var health = hit.GetComponent<health> ();
					if (health != null) {
						health.TakeDamage (10);
					}



				} else if (!name.Contains ("Sword")) {
					AddExplosionForce(prb, 400f, transform.position, 10f);
					var health = hit.GetComponent<health> ();
					if (health != null) {
						health.TakeDamage (10);
					}
				}
			}
			//Bullet and sword interacts with blocks
			var snap = hit.GetComponent<SnapToGrid> ();
			if (hit.tag == "bomb") {
				//StartCoroutine(snap.animate(0.01f,snap.frames,0,hit.gameObject));
				if (mcam.editplay == 0) {
					Destroy (hit.gameObject);
				} else {
					mcam.disabledblocks.Add (hit.gameObject);
					hit.gameObject.SetActive (false);
				}
			}
			if (hit.tag=="brick") {
				if (mcam.editplay == 0) {
					Destroy (hit.gameObject);
				} else {
					mcam.disabledblocks.Add (hit.gameObject);
					hit.gameObject.SetActive (false);
				}
			}
			else if (hit.tag == "vanish")
			{
				
				if (snap.fading == "none" || snap.fading == "fadingin")
				{
					//if (py != oy + 1 && py != oy)
					snap.startTime = Time.time;
					snap.fading = "fadingout";

				}

			}
			else if (hit.tag == "crumble")
			{
				snap.crumblehealth -= 30;
				if (snap.crumblehealth < 0)
				{
					if (mcam.editplay == 0) {
						Destroy (hit.gameObject);
					} else {
						mcam.disabledblocks.Add (hit.gameObject);
						hit.gameObject.SetActive (false);
					}
				}

			}
			Destroy(gameObject);


		}
        
    }

    // Use this for initialization
    void Start()
    {
		mcam = Camera.main.GetComponent<CameraFollow> ();

		//bomb exploding
		if (tag=="bomb")
		{
			frames.Add (mcam.bomb1);
			frames.Add (mcam.bomb2);
			frames.Add (mcam.bomb3);
			frames.Add (mcam.bomb4);
			frames.Add (mcam.bomb5);
			frames.Add (mcam.bomb6);
			frames.Add (mcam.bomb7);
			frames.Add (mcam.bomb8);
			frames.Add (mcam.bomb9);
			frames.Add (mcam.bomb10);
			frames.Add (mcam.bomb11);
			frames.Add (mcam.bomb12);
			frames.Add (mcam.bomb13);
			frames.Add (mcam.bomb14);
			frames.Add (mcam.bomb15);
			frames.Add (mcam.bomb16);
			frames.Add (mcam.bomb17);
			frames.Add (mcam.bomb18);
			frames.Add (mcam.bomb19);
			frames.Add (mcam.bomb20);
			frames.Add (mcam.bomb21);
			frames.Add (mcam.bomb22);
			frames.Add (mcam.bomb23);
			frames.Add (mcam.bomb24);
			frames.Add (mcam.bomb25);
			frames.Add (mcam.bomb26);
			frames.Add (mcam.bomb27);
			frames.Add (mcam.bomb28);
			frames.Add (mcam.bomb29);
			frames.Add (mcam.bomb30);

			//Snap to grid

			var pos = transform.position;

			double val1 = pos.x / 0.32;
			float val2 = (float)val1;

			double val3 = 0.32;
			float val4 = (float)val3;
			float val5 = (float)Mathf.Round (val2) * val4;

			double val6 = pos.y / 0.32;
			float val7 = (float)val6;

			double val8 = 0.32;
			float val9 = (float)val8;
			float val10 = (float)Mathf.Round (val7) * val9;

			//print(pos);
			Vector2 temp = new Vector2 (val5, val10);
			transform.position = temp;
    	}
	}

    // Update is called once per frame
    void Update()
    {
        //Lightning
        if (name.Contains("Lightning"))
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
				pco = player.GetComponent<Controls> ();
				prb = player.GetComponent < Rigidbody2D> ();
				if (pco != null) {
					if (name != pco.user + "Lightning" && pco.stunned==false) {
						AddExplosionForce(prb, 400f, transform.position, 10f);
						var health = player.GetComponent<health> ();
						if (health != null && mcam.GameMode=="Deathmatch") {
							health.TakeDamage (10);
						}


					}
				}
            }
            Destroy(gameObject);

        }




    }

	public static void AddExplosionForce(Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
	{
		var dir = (body.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0)
		{
			calc = 0;
		}

		body.AddForce(dir.normalized * expForce * calc);
	}

	public IEnumerator animate(float time,List<Sprite>list,int index,GameObject obj)
	{
		yield return new WaitForSeconds(time);

		// Code to execute after the delay
		GetComponent<SpriteRenderer>().sprite=list[index];
		if (index < list.Count - 1) {	
			StartCoroutine (animate (time,list,index+1,obj));
		} 
		else {
			if (tag == "bomb") {
				if (mcam.editplay == 0) {
					Destroy (obj);
				} else {
					mcam.disabledblocks.Add (obj);
					obj.SetActive (false);
				}
			}
		}

	}
}