using UnityEngine;
using System.Collections;

public class BoulderSpawnerScript: MonoBehaviour {

	public GameObject[] obj;
	
	public void Spawn()
	{
		int number = Random.Range (0, obj.Length);
		Instantiate(obj[number],transform.position,Quaternion.identity);
		return;	 
	}
	
}
