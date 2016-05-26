using UnityEngine;
using System.Collections;

public class AxePowerUpScript : MonoBehaviour {

	public int Value = 2;
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
			playerController.setPlayerState(FlapjackController.PlayerState.HAS_AXE);
			Destroy (gameObject);
		} 
		
	}
}
