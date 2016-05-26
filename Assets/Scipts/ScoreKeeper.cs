using UnityEngine;
using System;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	float score = 0;
	float scoreMultiplier = 1;

	GUIController guiController;
	AchievementTrackerScript achTrackerScript;

	void Start (){
		guiController = (GUIController) gameObject.GetComponent(typeof(GUIController));
		achTrackerScript = (AchievementTrackerScript) gameObject.GetComponent(typeof(AchievementTrackerScript));

		score = 0;
	}
	
	// Update is called once per frame
	void Update (){
		if (guiController.currentScreen == GUIController.CurrentScreen.GAME_PLAY){
			score += ((Time.deltaTime * scoreMultiplier) * 100);
			achTrackerScript.UpdateHighScore ((int) score);
			guiController.UpdateScore((int)score);
		}
	}

	public void AddToScore(float scoreAdjust){
		score += scoreAdjust;
	}

	public void setScoreMultiplier(float newScoreMultiplier){
		scoreMultiplier += newScoreMultiplier;
	}
	public int GetScore() {
		return (int)score;
	}
}
