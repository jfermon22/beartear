using UnityEngine;
using System.Collections;

public class LaserDestroyerScript : MonoBehaviour {

	public LayerMask DestroyableLayers;
	
	void  OnTriggerEnter2D (Collider2D other) 
	{
				//Debug.Log("laser colliding");
		//DestroyableLayers is bitmask, so to check if object is in bitmask:
		//		Bitshift 1 by layer number
		//		Bitwise AND with DestroyableLayers mask
		//		If result is not equal to zero, 
		//			then object is in a layer included
		//			in DestroyableLayers

				//Debug.Log ("Object: " + other.gameObject + "  layer: "+ other.gameObject.layer + "Destroyablelayers: " + DestroyableLayers );
		if (((( 1 << other.gameObject.layer ) & DestroyableLayers) != 0)) 
		{
				Destroy (other.gameObject);
		} 
	}
}
