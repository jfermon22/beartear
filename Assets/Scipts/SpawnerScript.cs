using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {


	public float spawnTimeInSeconds = 1.0f;
	public bool SpawnAtRegularIntervals = false;
	public float SpawnInterval = 1.0f;
	public float minSpawnTimeInSeconds = 2.0f;
	public float maxSpawnTimeInSeconds = 4.0f;
	public GameObject[] obj;

	void Start () {
		if (SpawnAtRegularIntervals){
			InvokeRepeating("Spawn",SpawnInterval,SpawnInterval);
		}
		else {
			StartCoroutine( RepeatingFunction() );
		}
	}

	IEnumerator RepeatingFunction () {
		while(true) {
			Spawn ();
			yield return new WaitForSeconds(Random.Range(minSpawnTimeInSeconds,maxSpawnTimeInSeconds));
		}
	}
	
	public void Spawn()
	{
		int number = Random.Range (0, obj.Length);
		Instantiate(obj[number],transform.position,Quaternion.identity);

		//Debug.Log ("Spawning "+ obj[number].name +" at position" +transform.position);
		return;	 
	}
}
