using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpSpawnerScript : MonoBehaviour {

	public GameObject[] planePowerUpPrefab;
	public GameObject[] honeyPowerUpPrefabs;
	public GameObject[] axePowerUpPrefab;
	public GameObject[] invinciblePowerUpPrefab;

	public ObstacleController.ObstacleType[] planePuCanBeSpawnedDuringArray;
	public ObstacleController.ObstacleType[] honeyPuCanBeSpawnedDuringArray;
	public ObstacleController.ObstacleType[] axePuCanBeSpawnedDuringArray;
	public ObstacleController.ObstacleType[] invinciblePuCanBeSpawnedDuringArray;


	PowerUp PlanePowerUp;
	PowerUp HoneyPowerUp;
	PowerUp AxePowerUp;
	PowerUp InvinciblePowerUp;

	public List<PowerUp> PowerUpList = new List<PowerUp>();
	public GameObject ObstacleControllerObj;
	ObstacleController obstController;

	public float lastObjectSpawnedTime = 0f;
	public float timeUntilNextPowerUp = 10f;

	void Start(){
		obstController =  (ObstacleController)ObstacleControllerObj.GetComponent(typeof(ObstacleController));
		CreatePowerUps();
		LoadPowerUpList();
	}

	void CreatePowerUps(){

		if ( PlanePowerUp == null ){
			PlanePowerUp = new PowerUp(PowerUp.PowerUpType.PLANE,
			                           planePowerUpPrefab,
			                           planePuCanBeSpawnedDuringArray,
			                           30f);
		}

		if ( HoneyPowerUp == null){
			HoneyPowerUp = new PowerUp(PowerUp.PowerUpType.HONEY,
			                           honeyPowerUpPrefabs,
			                           honeyPuCanBeSpawnedDuringArray,
			                           5f);
		}

		if ( AxePowerUp == null){
			AxePowerUp = new PowerUp(PowerUp.PowerUpType.AXE,
			                         axePowerUpPrefab,
			                         axePuCanBeSpawnedDuringArray,
			                         15f);
		}

		if ( InvinciblePowerUp == null){
			InvinciblePowerUp = new PowerUp(PowerUp.PowerUpType.INVINCIBLE,
			                                invinciblePowerUpPrefab,
			                                invinciblePuCanBeSpawnedDuringArray,
			                               	30f);
		}

	}

	void LoadPowerUpList(){
		if (!PowerUpList.Contains(PlanePowerUp))
			PowerUpList.Add(PlanePowerUp);

		if (!PowerUpList.Contains(HoneyPowerUp))
			PowerUpList.Add(HoneyPowerUp);

		if (!PowerUpList.Contains(AxePowerUp))
			PowerUpList.Add(AxePowerUp);

		if (!PowerUpList.Contains(InvinciblePowerUp))
			PowerUpList.Add(InvinciblePowerUp);
	}

	void Update()
	{
		if( timeUntilNextPowerUp <= 0f )
			SpawnPowerUp();
		else
			timeUntilNextPowerUp -= Time.deltaTime;
	}

	void SpawnPowerUp(){
		PowerUp PuToSpawn = new PowerUp();
		int iii = 0;
		do {
			int index = UnityEngine.Random.Range (0,PowerUpList.Count-1);
			PuToSpawn = (PowerUp)PowerUpList[index];
			iii ++;
		} while ( ! PuToSpawn.CanBeSpawnedDuringObstacle(obstController.currentObstacleType) && iii < 3 );
			//} while ( PuToSpawn.type != PowerUp.PowerUpType.PLANE);

		if (! PuToSpawn.CanBeSpawnedDuringObstacle(obstController.currentObstacleType) ){
			//Debug.Log("SpawnPowerUp() - Power up chooser timed out" );
			return;
		} else {
			//Debug.Log("SpawnPowerUp() - " + PuToSpawn.type +  " during " + obstController.currentObstacleType );
			//Debug.Log("SpawnPowerUp() -  iii " + iii );
			PuToSpawn.SpawnRandomAt(transform.position, Quaternion.identity);
			timeUntilNextPowerUp = 10f;
		}
	}

}
