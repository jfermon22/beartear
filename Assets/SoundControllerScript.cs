using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class SoundControllerScript : MonoBehaviour {
	

	public void PlaySound (AudioClip newClip)
	{
			GetComponent<AudioSource>().clip = newClip;
			GetComponent<AudioSource>().Play();
	}
}
