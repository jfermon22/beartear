using UnityEngine;
using System.Collections;

public class SkySpawnerScript : SpawnerScript {

	void Start(){
		//only defined to overload SpawnerScript definiton
	}
	
	void  OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "SkyEndPoint") {
			//Debug.Log ("Collision Detected with " + other.gameObject.name);
			transform.position = other.transform.position;
			//Debug.Log ("Destroying " + other.gameObject.transform.parent.gameObject.name);
			Spawn();
		} 
		
	}
}
