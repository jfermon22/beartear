using UnityEngine;
using System.Collections;

public class AudioListenerScript : MonoBehaviour {

	public static bool soundIsEnabled;
	void OnEnable()
	{
		GUIController.soundWasToggled += soundWasToggled;
	}

	void OnDisable()
	{
		GUIController.soundWasToggled -= soundWasToggled;
	}
	void OnLoad(){
		soundWasToggled(soundIsEnabled);
	}

	void soundWasToggled(bool enableSound)
	{
		soundIsEnabled = enableSound;
		AudioListener.pause = !soundIsEnabled;
	}
}
