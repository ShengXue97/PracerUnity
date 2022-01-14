using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

public class PlayerItem : MonoBehaviour {
	public Sprite none; public Sprite jetpack; public Sprite superjump;  public Sprite speedburst; public Sprite lightning; public Sprite teleport ;public Sprite bomb; public Sprite blockitem;
	public Weapon Shotgun;
	public bool canshoot = true;
	public GameObject bulletprefab;
	public SpriteRenderer sr;
	public string LastItem;
	// Use this for initialization
	void Start() {
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	public void CallChangeItem(string CurrentItem,Controls control)
	{
		//Changes CurrentItem sprite
		if (CurrentItem == "none")
		{
			GetComponent<SpriteRenderer>().sprite = none;
			control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
		}

	
		//Handles weapons from vases
		GetComponent<SpriteRenderer>().sprite = none;
		if (CurrentItem == "grenadelauncher") {
			if (LastItem != "grenadelauncher") {
				LastItem = "grenadelauncher";
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (control.GrenadeLauncher);
				GetComponent<SpriteRenderer> ().sprite = none;
			}
		} else if (CurrentItem == "rocketlauncher") {
			if (LastItem != "rocketlauncher") {
				LastItem = "rocketlauncher";
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (control.RocketLauncher);
				GetComponent<SpriteRenderer> ().sprite = none;
			}
		} else if (CurrentItem == "shotgun") {
			if (LastItem != "shotgun") {
				LastItem = "shotgun";
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (control.Shotgun);
				GetComponent<SpriteRenderer> ().sprite = none;
			}
		} else if (CurrentItem == "machinegun") {
			if (LastItem != "machinegun") {
				LastItem = "machinegun";
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (control.CorgiMachineGun);
				GetComponent<SpriteRenderer> ().sprite = none;
			}
		} else if (CurrentItem == "meleeattack") {
			if (LastItem != "meleeattack") {
				LastItem = "meleeattack";
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (control.MeleeAttack);
				GetComponent<SpriteRenderer> ().sprite = none;
			}
		}

		else if (CurrentItem!="grenadelauncher" && CurrentItem!="rocketlauncher" && CurrentItem!="shotgun" && CurrentItem!="machinegun" && CurrentItem!="meleeattack"){
			LastItem = "";
			if (CurrentItem == "jetpack" || CurrentItem == "jetpackused")
			{
				GetComponent<SpriteRenderer>().sprite = jetpack;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
			else if (CurrentItem == "lightning")
			{
				GetComponent<SpriteRenderer>().sprite = lightning;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
			else if (CurrentItem == "teleport")
			{
				GetComponent<SpriteRenderer>().sprite = teleport;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
			else if (CurrentItem == "speedburst")
			{
				GetComponent<SpriteRenderer>().sprite = speedburst;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
			else if (CurrentItem == "superjump")
			{
				GetComponent<SpriteRenderer>().sprite = superjump;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
			else if (CurrentItem == "bomb")
			{
				GetComponent<SpriteRenderer>().sprite = bomb;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
			else if (CurrentItem == "blockitem")
			{
				GetComponent<SpriteRenderer>().sprite = blockitem;
				control.GetComponent<CharacterHandleWeapon> ().ChangeWeapon (null);
			}
	}


}
}

