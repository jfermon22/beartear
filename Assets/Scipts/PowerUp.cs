using UnityEngine;
using System.Collections;


public class PowerUp  {

	public enum PowerUpType {
		PLANE,
		HONEY,
		AXE,
		INVINCIBLE,
		NONE
	}

	public PowerUpType  type;
	public GameObject[] prefabs;
	public ObstacleController.ObstacleType[] canBeSpawnedDuringObstacles;
	public float minSecsBetweenSpawns;

	public PowerUp ()
	{
		type = PowerUpType.NONE;
		//prefabs = new GameObject();
		//canBeSpawnedDuringObstacles = new ObstacleController.ObstacleType();
		minSecsBetweenSpawns = 0f;
	}

	public PowerUp (PowerUpType newType,GameObject[] newPrefabs,ObstacleController.ObstacleType[] newCanBeSpawnedDuringObstacle,float newMinSecsBetweenSpawns)
	{
		type = newType;
		prefabs = newPrefabs;
		canBeSpawnedDuringObstacles = newCanBeSpawnedDuringObstacle;
		minSecsBetweenSpawns = newMinSecsBetweenSpawns;
	}

	public GameObject GetRandomPrefab(){
		return (GameObject)prefabs.GetValue(UnityEngine.Random.Range (0,prefabs.Length));
	}

	public bool CanBeSpawnedDuringObstacle (ObstacleController.ObstacleType obstacle){
		foreach( ObstacleController.ObstacleType thisObs in canBeSpawnedDuringObstacles){
			if (thisObs == obstacle)
				return true;
		}
		return false;
	}

	public void SpawnRandomAt(Vector3 position, Quaternion quaternion){
		//Debug.Log ("Spawning powerup: " + type + " time: "+Time.time);
		MonoBehaviour.Instantiate(GetRandomPrefab(),position,quaternion);

	}

}
