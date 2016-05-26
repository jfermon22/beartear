using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public int Value = -1000;
	ScoreKeeper scoreKeeper;
	FlapjackController playerController;
	
	void Start ()
	{
		GameObject gameController = GameObject.Find("GameController");
		scoreKeeper = (ScoreKeeper) gameController.GetComponent(typeof(ScoreKeeper));
		GameObject player = GameObject.Find ("Player");
		playerController = (FlapjackController) player.GetComponent(typeof(FlapjackController));
		
	}
	
	public void AddToScore(int newValue = 0){
		if (scoreKeeper == null) {
			GameObject gameController = GameObject.Find("GameController");
			scoreKeeper = (ScoreKeeper) gameController.GetComponent(typeof(ScoreKeeper));
		}

		if ( newValue == 0 )
			scoreKeeper.AddToScore(Value);
		else 
			scoreKeeper.AddToScore(newValue);
	}
	
	void  OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Player") {
			scoreKeeper.AddToScore(Value);
			playerController.setPlayerState(FlapjackController.PlayerState.NORMAL);
			Destroy (gameObject);
		} 
		
	}
	
	public ScoreKeeper getScoreKeeper(){
		return scoreKeeper;
	}
}
