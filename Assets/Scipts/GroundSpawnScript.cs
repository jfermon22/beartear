using UnityEngine;
using System.Collections;
using System;

public class GroundSpawnScript : SpawnerScript {

	public enum GroundType {
		FLAT,
		OBSTACLE_EASY,
		OBSTACLE_MEDIUM,
		OBSTACLE_DIFFICULT,
	}

	String type = "Flat";


	void Start(){
		//only defined to overload SpawnerScript definiton
	}

	void  OnTriggerEnter2D (Collider2D other) 
	{
			if (other.tag == "GroundEndPoint") {
			//Debug.Log ("Collision Detected with " + other.gameObject.name);
			transform.position = other.transform.position;
				//Debug.Log ("Destroying " + other.gameObject.transform.parent.gameObject.name);
				Spawn();
			} 

	}

	public void SpawnType(GroundType newtype){
		switch(newtype)
		{
		case GroundType.FLAT:
				type = "Flat";
			break;
		case GroundType.OBSTACLE_EASY:
			type = "Easy";
			break;
		case GroundType.OBSTACLE_MEDIUM:
			type = "Medium";
			break;
		case GroundType.OBSTACLE_DIFFICULT:
			type = "Difficult";
			break;
		}
	}

	public  new void Spawn()
	{
		int number = 0;
		
		do {
			number = UnityEngine.Random.Range (0, obj.Length);
		} while (obj[number].tag != type);

		if ( type != "Flat"){ 
			type = "Flat";
		}

		Instantiate(obj[number],transform.position,Quaternion.identity);
		
		//Debug.Log ("Spawning "+ obj[number].name +" at position" +transform.position);
		return;	 
	}

}
