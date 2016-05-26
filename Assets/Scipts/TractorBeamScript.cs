using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TractorBeamScript : MonoBehaviour {

	public float tractorForce = 1f;
	public List<GameObject> InTractorNow = new List<GameObject>();
	public float MaxHeight = -3f;

		// Update is called once per frame
	void OnEnable() {
			InTractorNow.Clear ();
	}

	void Update () {

			InTractorNow.ForEach(MoveObjectUp);
			InTractorNow.Clear();
	}

	void MoveObjectUp(GameObject gobj)
	{
		if (gobj.transform.position.y < MaxHeight)
			//gobj.transform.Translate(Vector3.up * Time.deltaTime * tractorForce, Space.World);
			gobj.transform.Translate(Vector3.up * 5, Space.World);

	}

	void  OnTriggerEnter2D (Collider2D other) 
	{
		//Debug.Log ("Ontrigger Enter called on " + other.gameObject);
		if (other.tag == "Ground" ) {
			//Debug.Log("Adding " + other.gameObject + " from  InBeam ArrayList| "+Time.time);
			InTractorNow.Add(other.gameObject);
		} 
		
	}
}
