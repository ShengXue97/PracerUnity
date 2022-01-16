using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class health : NetworkBehaviour
{
	public bool test = false;
	public const int maxHealth = 100;
	public bool immune = false;
	public bool destroyOnDeath;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	[Command]
	public void CmdTakeDamage(int amount)
	{
		TakeDamage(amount);
	}
	public void TakeDamage(int amount)
	{
		if (Camera.main.GetComponent<CameraFollow>().GameMode != "Deathmatch") {
			return;
		}
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			if (destroyOnDeath)
			{
				Destroy(gameObject);
			}
			else
			{
				currentHealth = maxHealth;

				// called on the Server, will be invoked on the Clients
				if (isServer)
				{
					RpcRespawn();
				}
			}

		}
	}


	void OnChangeHealth(int currentHealth)
	{
		healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// Set the player’s position to origin
			if (GetComponent<Controls> ().GameMode == "Race") {
				var checkpointpos = GetComponent<Controls> ().checkpointpos;
				transform.position = checkpointpos;
			} else if (GetComponent<Controls> ().GameMode == "Deathmatch") {
				Camera.main.GetComponent<CameraFollow>().PauseGame = true;
				Camera.main.GetComponent<CameraFollow>().endgame = 1;
			}
		}
	}



}