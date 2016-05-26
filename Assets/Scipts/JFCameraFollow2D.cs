using UnityEngine;
using System.Collections;

public class JFCameraFollow2D : MonoBehaviour {

	public Transform ObjectToFollow;
	public float zOffset = -20f;
	public float xOffset = 6f;
	public float yOffset = 0f;
	public bool shouldFollowHorizontal = true;
	public bool shouldFollowVertical = true;
	public bool shouldWindow = false;

	float dampTime = 0.3f; //offset from the viewport center to fix damping
	Vector3 velocityCam = Vector3.zero;
	
	public float xWindowMax = -5f;

	public float xPosition = 0;
	public float yPosition = 0;

	public float xSpeed = 25f;


	void Start(){
		if (shouldFollowHorizontal) {
			xPosition = ObjectToFollow.position.x + xOffset;;
		}
		
		if (shouldFollowVertical) {
			yPosition = ObjectToFollow.position.y + yOffset;	
		}
		
		transform.position = new Vector3 (xPosition, yPosition, zOffset);
	}

	// Update is called once per frame
	void Update () 
	{
		if (shouldWindow) 
		{
			if(transform.position.x < (ObjectToFollow.position.x + xOffset + xWindowMax) ){
					xPosition = ObjectToFollow.position.x + xOffset + xWindowMax;
			} else {
					xPosition = transform.position.x + xSpeed * Time.deltaTime;
			}
		} 
		else 
		{

			if (shouldFollowHorizontal)
					xPosition = ObjectToFollow.position.x + xOffset ;

			if (shouldFollowVertical)
					yPosition = ObjectToFollow.position.y + yOffset;	
		}

		transform.position = Vector3.SmoothDamp(transform.position,
		                                        new Vector3 (xPosition, yPosition, zOffset), 
		                                        ref velocityCam, 
		                                        dampTime*Time.deltaTime);
	}
}
