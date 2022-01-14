using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

public class Controllers : MonoBehaviour {
	[Header("Audio")]
	public AudioSource source;
	public AudioListener listener;

	public AudioClip a_countdown;
	public AudioClip a_win;
	public AudioClip a_explosion;

	[Space]

	[Header("Item")]
	public AudioClip a_timebomb;
	public AudioClip a_speedburst;
	public AudioClip a_superjump;
	public AudioClip a_jetpack;
	public AudioClip a_thunder;

	[Space]

	[Header("Block")]
	public AudioClip a_item;
	public AudioClip a_water;
	[Space]

	[Header("Weapon")]
	public AudioClip a_gun;
	public AudioClip a_sword;
	public AudioClip a_shotgun;
	public AudioClip a_rocketlauncher;
	public AudioClip a_grenadelauncher;
	[Space]

	public bool AudioEnabled = true;
	public string user;
	public float rank=0;
	public float exp=0;
	public int SSpeed=100;
	public int SAcc=100;
	public int SJump=100;
	public int SExpBonus=100;
	public string lastmap = "";
	public string lastmapname="";


	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		listener = GetComponent<AudioListener> ();
	}




	// Update is called once per frame
	void Update () {

	}


	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void PlayOnce(AudioClip sound)
	{
		source.PlayOneShot (sound);
	}

}
