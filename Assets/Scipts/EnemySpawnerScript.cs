using UnityEngine;
using System.Collections;

public class EnemySpawnerScript: MonoBehaviour {
		
	//public float spawnTimeInSeconds = 1.0f;
	//public bool SpawnAtRegularIntervals = false;
	//public float SpawnInterval = 1.0f;
	public float minSpawnTimeInSeconds = 2.0f;
	public float maxSpawnTimeInSeconds = 3.0f;
	public float TimeUntilNextSpawn = 0f;
	public GameObject[] obj;
		

    void Update()
	{
		UpdateSpawnTime();
		if (TimeUntilNextSpawn <= 0f)
		{
			Spawn();
		}

	}
	
		
	public void Spawn()
	{
		int number = Random.Range (0, obj.Length);
		Instantiate(obj[number],transform.position,Quaternion.identity);
		TimeUntilNextSpawn = Random.Range(minSpawnTimeInSeconds,maxSpawnTimeInSeconds);
		return;	 
	}

	public void UpdateSpawnTime(){
		TimeUntilNextSpawn -= Time.deltaTime;
	}


}
