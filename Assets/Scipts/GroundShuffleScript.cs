using UnityEngine;
using System.Collections;

public class GroundShuffleScript : MonoBehaviour {

	public enum MovementType {
		UP_DOWN,
		WAVE,
	}

	public GameObject UpperGround;
	public GameObject LowerGround;
	public GameObject Ground1;
	public GameObject Ground2;
	public GameObject Ground3;
	public GameObject Ground4;
	public GameObject Ground5;
	public GameObject Ground6;
	public GameObject Ground7;
	public float UpperLimit = 90f;
	public float LowerLimit = 0f;
	public float MovementSpeed = 2f;
	public MovementType type = MovementType.UP_DOWN;
	float RangeMedian = 0f;



	// Update is called once per frame
	void Start() {

	}

	void Update () {
		RangeMedian = (UpperLimit - LowerLimit)/2f;
		switch (type){
		case MovementType.UP_DOWN:
			ApplyUpDownMovement();
			break;
		case MovementType.WAVE:
			ApplyWaveMovement();
			break;
		}
	}
	void ApplyUpDownMovement(){
		MoveUp (UpperGround);
		MoveDown (LowerGround);
	}


	void ApplyWaveMovement(){
		MoveSinWithDelay(Ground1);
		MoveSinWithDelay(Ground2);
		MoveSinWithDelay(Ground3);
		MoveSinWithDelay(Ground4);
		MoveSinWithDelay(Ground5);
		MoveSinWithDelay(Ground6);
		MoveSinWithDelay(Ground7);

	}

	bool UpperGroundIsMovingDown(){
		return (UpperGround.GetComponent<Rigidbody2D>().velocity.y < 0);
	}

	bool UpperGroundIsMovingUp(){
		return (UpperGround.GetComponent<Rigidbody2D>().velocity.y > 0);
	}

	bool UpperGroundIsAboveMax(){
		return (UpperGround.transform.localPosition.y >= UpperLimit);
	}

	bool UpperGroundIsBelowMin(){
		return (UpperGround.transform.localPosition.y <= LowerLimit);
	}

	void MoveUp(GameObject gameObject)
	{
		//gameObject.rigidbody2D.velocity = new Vector2 (0, MovementSpeed);
		float value = RangeMedian * Mathf.Sin(Time.time * MovementSpeed) + RangeMedian;
		Vector3 vect3 =  new Vector3(0,value,0);
		gameObject.transform.localPosition = vect3;
	}

	void MoveDown(GameObject gameObject)
	{
		//gameObject.rigidbody2D.velocity = new Vector2 (0, -MovementSpeed);
		float value = -RangeMedian * Mathf.Sin(Time.time * MovementSpeed) + RangeMedian;
		Vector3 vect3 =  new Vector3(gameObject.transform.localPosition.x,value,gameObject.transform.localPosition.z);
		gameObject.transform.localPosition = vect3;
	}
	void MoveSinWithDelay(GameObject gameObject)
	{
		//gameObject.rigidbody2D.velocity = new Vector2 (0, MovementSpeed);
		float value = -RangeMedian * Mathf.Sin(Time.time * MovementSpeed - gameObject.transform.localPosition.x) + RangeMedian;
		Vector3 vect3 =  new Vector3(gameObject.transform.localPosition.x,value,gameObject.transform.localPosition.z);
		gameObject.transform.localPosition = vect3;
	}

}
