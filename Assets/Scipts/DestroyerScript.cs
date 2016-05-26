using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour 
{
	public LayerMask DestroyableLayers;

	void  OnTriggerEnter2D (Collider2D other) 
	{
		//DestroyableLayers is bitmask, so to check if object is in bitmask:
		//		Bitshift 1 by layer number
		//		Bitwise AND with DestroyableLayers mask
		//		If result is not equal to zero, 
		//			then object is in a layer included
		//			in DestroyableLayers
		if (((( 1 << other.gameObject.layer ) & DestroyableLayers) != 0)) 
		{
			if (other.gameObject.transform.parent) 
			{
					//Debug.Log ("Destroying " + other.gameObject.transform.parent.gameObject.name);
					Destroy (other.gameObject.transform.parent.gameObject);
			} else {
					//Debug.Log ("Destroying " + other.gameObject.name);
					Destroy (other.gameObject);
			}

		} 
	}
}
