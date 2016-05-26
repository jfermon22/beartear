using UnityEngine;
using System.Collections;

public class DeadZoneCameraFollow2D : MonoBehaviour {
	
	public Transform ObjectToFollow;
	public float zOffset = -20f;
	public float xOffset = 15f;
	public float yOffset = 0f;

	public float xPosition = 0;
	public float yPosition = 0;
	public float moveSpeed = 29;
	
	void Start(){

		xPosition = ObjectToFollow.position.x + xOffset;
		transform.position = new Vector3 (xPosition, yPosition, zOffset);
	}
	// Update is called once per frame
	void FixedUpdate () 
	{

		if (IsAtBufferEdge())
		{
			xPosition = ObjectToFollow.position.x + xOffset;;
			transform.position = new Vector3 (xPosition, yPosition, zOffset);
		}
		else
			transform.Translate(new Vector3 (.2f, 0, 0));
	}

	bool IsAtBufferEdge()
	{
		return (transform.position.x <= ObjectToFollow.position.x + xOffset);
	}

}
