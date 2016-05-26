using UnityEngine;
using System.Collections;

public class BombSpawnerScript : MonoBehaviour {

	//public float spawnTimeInSeconds = 1.0f;
	//public bool SpawnAtRegularIntervals = false;
	//public float SpawnInterval = 1.0f;
	public float minSpawnTimeInSeconds = 4.0f;
	public float maxSpawnTimeInSeconds = 6.0f;
	public float TimeUntilNextSpawn = 3f;
	public GameObject[] bombObjArray;
	public GameObject Player;
	FlapjackController flapjack;
	public float force = 3;

	void Start(){
		flapjack =  (FlapjackController)Player.GetComponent(typeof(FlapjackController));
	}
	
	void Update()
	{
		if (flapjack.playerState == FlapjackController.PlayerState.ON_PLANE)
		{
			transform.position = new Vector2( transform.position.x,Player.transform.position.y);
			UpdateSpawnTime();
			if (TimeUntilNextSpawn <= 0f)
			{
				Spawn();
			}
		}
	}
	
	
	public void Spawn()
	{
		//Vector3 dir = Player.transform.position - transform.position;
		//dir = dir.normalized;

				int number = Random.Range (0, bombObjArray.Length);
				GameObject gO = (GameObject)Instantiate(bombObjArray[number],transform.position,Quaternion.identity);
		gO.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1, 1,0) * force);
		//Debug.Log("Spawning " + gO + " with force "+ new Vector3(-1, 1,0) * force + " at time " + Time.time);
		TimeUntilNextSpawn = Random.Range(minSpawnTimeInSeconds,maxSpawnTimeInSeconds);
		return;	 
	}
	
	public void UpdateSpawnTime(){
		TimeUntilNextSpawn -= Time.deltaTime;
	}
	

}
