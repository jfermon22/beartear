using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {

	
	
	void Start ()
		
	{
		
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "foreground";
		
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
		
	}
}
