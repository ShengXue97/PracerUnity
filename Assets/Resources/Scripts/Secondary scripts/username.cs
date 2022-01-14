using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class username : NetworkBehaviour
{


	[SyncVar(hook = "OnChangeHealth")]
	public string currentHealth = "";

	public GameObject usertext;

	public void TakeDamage(string amount)
	{

		currentHealth=amount;

	}


	void OnChangeHealth(string currentHealth)
	{
		usertext.GetComponent<Text> ().text = currentHealth;
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// Set the player’s position to origin
			transform.position = Vector3.zero;
		}
	}
}