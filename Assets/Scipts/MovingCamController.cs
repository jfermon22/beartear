using UnityEngine;
using System.Collections;

public class MovingCamController : JFCharacterController2D {
	
	public Transform ObjectToFollow;
	public float zOffset = -20f;
	public float xOffset = 6f;
	public float yOffset = 0f;

	void Start(){
			float xPosition = ObjectToFollow.position.x + xOffset;;

			float  yPosition = ObjectToFollow.position.y + yOffset;	

		
		transform.position = new Vector3 (xPosition, yPosition, zOffset);
	}

	// Update is called once per frame
	void Update () {
		Move(new Vector2(maxSpeed,0f));
	}
}
