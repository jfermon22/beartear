using UnityEngine;
using System.Collections;

public class SpaceShipController : JFCharacterController2D {

	public enum AttackType{
		LASER,
		ROCK,
		TRACTOR,
	}

	Vector3 INACTIVE_POSITION = new Vector3(29,7,20);
	Vector3 ACTIVE_POSITION = new Vector3(12,7,20);
	public  float MaxActiveXPos = 20f;
	public float MinActiveYPos = 2f;

	public bool ShouldMoveShip = false;
	float journeyStartTime;
	Vector3 journeyStart;
	Vector3 journeyFinish;
	float journeyLength;

	AttackType currentAttackType;

	public GameObject LaserShooter;
	public bool LaserIsActive = false;
	public bool LaserIsFiring = false;
	public GameObject Laser;
	Animator LaserAnimator;

	public GameObject Dropper;
	public bool DropperIsActive = false;
	public GameObject BoulderSpawnerObj;
	BoulderSpawnerScript boulderSpawnerScript;
	public GameObject TractorBeam;
	public bool TractorIsFiring = false;


	public bool isOnScreen = false;
	Animator animator;


	public float moveSpeed = 5f;


	// Use this for initialization
	void Start () {
		transform.localPosition = INACTIVE_POSITION;
		animator = GetComponent<Animator>(); 
		if (!LaserShooter){
			Debug.Log ("SpaceShipController - Laser shooter is null");
			Debug.Break();
		}

		if (!Laser){
			Debug.Log ("SpaceShipController - Laser is null");
			Debug.Break();
		}

		LaserAnimator = LaserShooter.GetComponent<Animator>();

		if (!Dropper){
			Debug.Log ("SpaceShipController - Dropper is null");
			Debug.Break();
		}

		boulderSpawnerScript = (BoulderSpawnerScript)BoulderSpawnerObj.GetComponent<BoulderSpawnerScript>();

		if (!TractorBeam){
			Debug.Log ("SpaceShipController - Tractor Beam is null");
			Debug.Break();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isOnScreen ){
			if ( !LaserIsFiring ){
				//Debug.Log("moving");
				moveSpeed = 5f;
			} else {
				//Debug.Log("NOT moving");
				moveSpeed = 0f;
			}

			MoveShip();
			animator.SetFloat("hSpeed",GetComponent<Rigidbody2D>().velocity.x);
		} 
		else if (ShouldMoveShip)
		{
			float distCovered = (Time.time - journeyStartTime) * 8;
			float fracJourney = distCovered / journeyLength;
			transform.localPosition = Vector3.Lerp(journeyStart, journeyFinish, fracJourney);
			if (fracJourney > 1f){
				ShouldMoveShip = false;
				if(transform.localPosition == ACTIVE_POSITION){
					isOnScreen=true;
				}
			}
		}
		else if(!isOnScreen)
		{
			transform.localPosition = INACTIVE_POSITION;
		}
	}
	
	public void BringOnscreen(){
		//Debug.Log("ShipCont::BringOnscren - Bringing on Screen now "+Time.time);
		MoveToPosition(ACTIVE_POSITION);
		return;
	}

	public void TakeOffscreen(){
		//Debug.Log("ShpCont::TakeOffscren - Taking Off Screen now "+Time.time);
		if (LaserIsActive || DropperIsActive){
			DeactivateAttack();
		}
		MoveToPosition(INACTIVE_POSITION);
		return;
	}
	void MoveToPosition (Vector3 newPosition){
		//Debug.Log("ShpCont::MoveToPosition - Moving to Position: "+newPosition+" - "+Time.time);
		isOnScreen=false;
		ShouldMoveShip = true;
		journeyStartTime = Time.time;
		journeyStart = transform.localPosition;
		journeyFinish = newPosition;
		journeyLength = Vector3.Distance(journeyStart,journeyFinish);
		return;
	}

	public void ActivateAttack(AttackType attackType){
		currentAttackType = attackType;
		switch (currentAttackType){
		case AttackType.LASER:
			animator.SetTrigger("LowerLaser");
			LaserIsActive = true;
			break;
		case AttackType.ROCK:
			animator.SetTrigger("LowerDropper");
			DropperIsActive = true;
			break;
		case AttackType.TRACTOR:
			animator.SetTrigger("LowerDropper");
			DropperIsActive = true;
			break;
		} 
		return;
	}

	public void DeactivateAttack(){
		switch (currentAttackType){
		case AttackType.LASER:
			if(LaserIsFiring){
				StopLaser();
			}
			if(LaserIsActive){
				animator.SetTrigger("PullUpLaser");
				LaserIsActive = false;
			}
			break;
		case AttackType.ROCK:
			if(DropperIsActive){
				animator.SetTrigger("PullUpDropper");
				DropperIsActive = false;
			}
			break;
		case AttackType.TRACTOR:
			if(DropperIsActive){
				StopTractorBeam();
				animator.SetTrigger("PullUpDropper");
				DropperIsActive = false;
			}
			break;
		} 
		return;
	}

	void MoveShip(){
		float xPos = 3*Mathf.Sin(Time.time*moveSpeed);
		Move(new Vector2(xPos,0));
	}

	public void ShootLaser(){
		if (LaserIsActive && !LaserIsFiring){
			//Debug.Log ("FireLLaserCommand - " + Time.time);
			LaserAnimator.SetTrigger("ShootLaser");
			LaserIsFiring = true;
		}
	}

	public void StopLaser(){
		if (LaserIsActive && LaserIsFiring){
			//Debug.Log ("StopLaserCommand - " + Time.time);
			LaserAnimator.SetTrigger("CoolDown");
			LaserIsFiring = false;
		}
	}

	public void SpawnBoulder(){
		if(DropperIsActive){
			//Debug.Log ("Spawn Boulder - " + Time.time);
			boulderSpawnerScript.Spawn();
		}
	}

	public void StartTractorBeam(){
		if (DropperIsActive && !TractorIsFiring){
			//Debug.Log ("StartTractorBeam - " + Time.time);
			TractorBeam.SetActive(true);
			TractorIsFiring = true;
		}
	}
	
	public void StopTractorBeam(){
		if (DropperIsActive && TractorIsFiring){
			//Debug.Log ("StopTractorBeam - " + Time.time);
			TractorBeam.SetActive(false);
			TractorIsFiring = false;
		}
	}

	public bool DeviceIsActive(){
		return (LaserIsActive || DropperIsActive || TractorIsFiring);
	}


}
