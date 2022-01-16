using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {
    public GameObject enemyprefab;
    public Transform start;
    public int numberOfEnemies;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var enemy = (GameObject)Instantiate(enemyprefab, new Vector3(
                Random.Range(-8.0f, 8.0f),
                Random.Range(-8.0f, 8.0f),
                0.0f), Quaternion.identity);
            NetworkServer.Spawn(enemy);
        }
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
