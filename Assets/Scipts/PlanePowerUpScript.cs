using UnityEngine;
using System.Collections;

public class PlanePowerUpScript : MonoBehaviour {
	
	public int Value = 1;
	public int DurationInSecs = 10;
	ScoreKeeper scoreKeeper;
	FlapjackController playerController;
	
	void Start ()
	{
		GameObject gameController = GameObject.Find("GameController");
		scoreKeeper = (ScoreKeeper) gameController.GetComponent(typeof(ScoreKeeper));
		GameObject player = GameObject.Find ("Player");
		playerController = (FlapjackController) player.GetComponent(typeof(FlapjackController));
	}
	
	void  OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Player") {
			scoreKeeper.setScoreMultiplier(Value);
			playerController.setPowerUpDuration(DurationInSecs);
			playerController.setPlayerState(FlapjackController.PlayerState.ON_PLANE);
			Destroy (gameObject);
		} 
		
	}

}
