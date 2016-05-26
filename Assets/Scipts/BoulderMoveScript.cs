using UnityEngine;
using System.Collections;

public class BoulderMoveScript : MonoBehaviour {
	public float speed=10;

	// Update is called once per frame
	void Update () {

		transform.Rotate(Vector3.forward * speed);

	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "Player")
		{
			FlapjackController FpController = (FlapjackController)collision.gameObject.GetComponent(typeof(FlapjackController));
			FpController.DidTrip();
		}
	}

}
