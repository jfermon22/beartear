using UnityEngine;
using System.Collections;

public class TopOffscreenDetectorScriot : MonoBehaviour {

	public GameObject Player;
	AchievementTrackerScript AchTrackerScript;


	void Start ()
	{
		GameObject gameController = GameObject.Find("GameController");
		AchTrackerScript = (AchievementTrackerScript) gameController.GetComponent(typeof(AchievementTrackerScript));
	}

	void  OnTriggerEnter2D (Collider2D other) {
		
		if (other.gameObject == Player) {
			AchTrackerScript.IncrementCount(AchievementTrackerScript.Achievement.OFF_SCREEN);
			return;
		}
	}
}
