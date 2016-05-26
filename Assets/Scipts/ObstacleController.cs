using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public enum ObstacleType {
		NONE,
		GROUND,
		BUNNIES,
		SPACE_SHIP,
	}

	enum Difficulty {
		NONE,
		EASY,
		MEDIUM,
		DIFFICULT
	}

	ScoreKeeper scoreKeeper;


	//Obsactle spawners
    GroundSpawnScript groundSpawner;
	EnemySpawnerScript enemySpawner;
	SpaceShipController shipController;
	SpaceShipController.AttackType ShipAttackType;

	
	//score threshholds
	public int MinEasyScore = 5000;
	public int MinMediumScore = 10000;
	public int MinDifficultScore = 20000;

	public ObstacleType currentObstacleType;

	public float ObstacleSpawnDuration = 10f;
	float maxObstacleSpawnDuration;
	public float TimeOfLastObstacleSpawn = 0f;
	public float TimeOfStart = 0f;
	public float NoObstacleStartDuration =10f;
	public float AllowedTimeBetweenObstacles = 10f;

	public float TimeUntilNextLaserFire = 0;
	public float TimeUntilStopLaserFire = 0;

	public float TimeUntilStopTractorFire = 0f;
	public float TimeUntilNextTractorFire = 0f;

	public float TimeUntilNextBoulder = 1f;

	public float TractorTimeDelay=1f;



	// Use this for initialization
	void Start () {
		TimeOfStart = Time.time;
		scoreKeeper = (ScoreKeeper)GameObject.Find("GameController").GetComponent(typeof(ScoreKeeper));
		groundSpawner = (GroundSpawnScript)GameObject.Find("GroundSpawner").GetComponent(typeof(GroundSpawnScript));
		enemySpawner = (EnemySpawnerScript)GameObject.Find("EnemySpawner").GetComponent(typeof(EnemySpawnerScript));
		enemySpawner.enabled = false ;
		shipController = (SpaceShipController)GameObject.Find("SpaceShip").GetComponent(typeof(SpaceShipController));
		maxObstacleSpawnDuration = ObstacleSpawnDuration;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentObstacleType){
			case ObstacleType.NONE:
				if(GetTimeSinceLastObstacleSpawned() > AllowedTimeBetweenObstacles){
								Debug.Log("only spawing laser");
					//currentObstacleType = GetRandomEnum<ObstacleType>();
								currentObstacleType = ObstacleType.SPACE_SHIP;
					setAllowedTimeBetweenObstacles();
				}
				break;
			case ObstacleType.BUNNIES:
				SpawnGroundEnemies();
				UpdateObstacleDuration();
				break;
			case ObstacleType.GROUND:
				SpawnGroundObstacles();
				UpdateObstacleDuration();
				break;
			case ObstacleType.SPACE_SHIP:
				UpdateShipAttack();
				UpdateObstacleDuration();
		        break;
		}
	
	}

	void SpawnGroundObstacles(){
		if(scoreKeeper.GetScore() < MinEasyScore ){
			//Debug.Log("Spawning: LESS THAN EASY");
			groundSpawner.SpawnType(GroundSpawnScript.GroundType.FLAT);
			
		} else if(scoreKeeper.GetScore() < MinMediumScore ){
			//Debug.Log("Spawning: EASY");
			groundSpawner.SpawnType(GroundSpawnScript.GroundType.OBSTACLE_EASY);
			
		} else if(scoreKeeper.GetScore() < MinDifficultScore ){
			//Debug.Log("Spawning: Medium");
			groundSpawner.SpawnType(GroundSpawnScript.GroundType.OBSTACLE_MEDIUM);
			
		} else {
			//Debug.Log("Spawning: Difficult");
			groundSpawner.SpawnType(GroundSpawnScript.GroundType.OBSTACLE_DIFFICULT);
		}
	}

	void SpawnGroundEnemies(){
		enemySpawner.enabled = true;
		return;
	}

	void UpdateShipAttack()
	{
		if(shipController.isOnScreen )
		{
			if( !shipController.DeviceIsActive())
			{
				ShipAttackType = GetRandomEnum<SpaceShipController.AttackType>();
				ResetShipAttackTimes();
				shipController.ActivateAttack(ShipAttackType);
			} 
			else if (shipController.LaserIsActive)
			{
				if(shipController.LaserIsFiring)
				{
					if( TimeUntilStopLaserFire <= 0 )
					{
						shipController.StopLaser();
						setTimeUntilNextLaserFire();
					} else {

						TimeUntilStopLaserFire-=Time.deltaTime;
					}
				} 
				else 
				{
					if ( TimeUntilNextLaserFire <= 0 )
					{
						shipController.ShootLaser();
						setTimeUntilStopLaserFire();
					} else {
						TimeUntilNextLaserFire-=Time.deltaTime;
					}
				}
		 	}
			else if (shipController.DropperIsActive)
			{
				switch(ShipAttackType)
				{
					case SpaceShipController.AttackType.ROCK:
						if (TimeUntilNextBoulder <= 0)
						{
							shipController.SpawnBoulder();
							setTimeUntilNextBoulder();
						} 
						else 
						{
							TimeUntilNextBoulder-=Time.deltaTime;
						}
					break;

					case SpaceShipController.AttackType.TRACTOR:
						if(!shipController.TractorIsFiring && TractorTimeDelay <= 0)
						{
							if ( TimeUntilNextTractorFire <= 0 )
							{
								shipController.StartTractorBeam();
								setTimeUntilStopTractorrFire();
							} else {
								TimeUntilNextTractorFire-=Time.deltaTime;
							}
						}
						else if (shipController.TractorIsFiring )
						{
							if( TimeUntilStopTractorFire <= 0 )
							{
								shipController.StopTractorBeam();
								setTimeUntilNextTractorFire();
							} else {
								TimeUntilStopTractorFire-=Time.deltaTime;
							}
						}
						else if (TractorTimeDelay > 0)
						{
							TractorTimeDelay-=Time.deltaTime;
						} 
					break;
				}
			}
		}
		else if (!shipController.ShouldMoveShip)
		{
			shipController.BringOnscreen();
			ObstacleSpawnDuration = 20;
		}
	}

	void setTimeUntilStopLaserFire(){
		TimeUntilStopLaserFire = Random.Range (1f,1.4f);
	}

	void setTimeUntilNextLaserFire(){ 
		TimeUntilNextLaserFire =  Random.Range (0.7f,1f);
	}

	void setTimeUntilNextBoulder(){ 
		TimeUntilNextBoulder =  Random.Range (0.7f,1.5f);
	}

	void setTimeUntilStopTractorrFire(){
		TimeUntilStopTractorFire = Random.Range (0.7f,1.3f);
	}
	
	void setTimeUntilNextTractorFire(){ 
		TimeUntilNextTractorFire =  Random.Range (0.7f,1f);
	}

	void setAllowedTimeBetweenObstacles(){ 
		AllowedTimeBetweenObstacles =  Random.Range (3f,10f);
	}

	void setTractorTimeDelay(){ 
		TractorTimeDelay = 1f;
	}

	void UpdateObstacleDuration ()
	{
		ObstacleSpawnDuration -= Time.deltaTime;
		if (ObstacleSpawnDuration <= 0) 
		{
			currentObstacleType = ObstacleType.NONE;
			enemySpawner.enabled = false;
			if(shipController.isOnScreen){
				shipController.TakeOffscreen();
			}
			TimeOfLastObstacleSpawn = Time.time;
			ObstacleSpawnDuration = maxObstacleSpawnDuration;
		}
	}

	float GetTimeSinceLastObstacleSpawned(){
		if ( Time.time - TimeOfStart < NoObstacleStartDuration){
			return Time.time - TimeOfStart;
		}
		return Time.time - TimeOfLastObstacleSpawn;
	}

	public static T GetRandomEnum<T>()
	{
		System.Array values = ObstacleType.GetValues(typeof(T));
		return  (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
	}
	
	bool NoDeviceActive()
	{
		return (!shipController.LaserIsActive && !shipController.DropperIsActive);
	}

	void ResetShipAttackTimes()
	{
		setTimeUntilNextBoulder();
		setTimeUntilNextLaserFire();
		setTractorTimeDelay();
	}
}
