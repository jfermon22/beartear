using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AudioSource))]

public class PowerUpScript : MonoBehaviour {

	public int Value = 10;
	ScoreKeeper scoreKeeper;
	SoundControllerScript soundController;
	public AudioClip PowerUpSound;

	void Start ()
	{
		GameObject gameController = GameObject.Find("GameController");
		scoreKeeper = (ScoreKeeper) gameController.GetComponent(typeof(ScoreKeeper));
		GameObject soundControllerObj = GameObject.Find("SoundController");
		soundController = (SoundControllerScript) soundControllerObj.GetComponent(typeof(SoundControllerScript));

	}

	public void AddToScore(int newValue = 0){
		if (scoreKeeper == null) {
			GameObject gameController = GameObject.Find("GameController");
			scoreKeeper = (ScoreKeeper) gameController.GetComponent(typeof(ScoreKeeper));
		}
		if( newValue == 0 ){
			scoreKeeper.AddToScore(Value);
		} else {
			scoreKeeper.AddToScore(newValue);
		}
	}

	void  OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Player") {
			soundController.PlaySound(PowerUpSound);
			scoreKeeper.AddToScore(Value);
			Destroy (gameObject);
		} 
		
	}

	public ScoreKeeper getScoreKeeper(){
		return scoreKeeper;
	}
	



}
