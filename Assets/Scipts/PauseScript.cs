using UnityEngine;
using System.Collections;

/*
If you set Time.timeScale to 0, yet you still want to do some processing 
(say, for animating pause menus), remember that Time.realtimeSinceStartup 
is not affected by Time.timeScale. You could effectively set up your own 
"deltaTime" in your animated menus by subtracting the previous recorded 
Time.realtimeSinceStartup from the current one.
*/



public class PauseScript : MonoBehaviour {
	public bool isPaused = false;
	//public GameObject pauseLabelObj;
	//UILabel pauselabel;
	//public GameObject PauseButton;
	//UISprite pauseButtonSprite;
	//public GameObject StartButton;
	//public GameObject scoreLabel;


	/*void Start (){
		pauselabel = (UILabel)pauseLabelObj.GetComponent(typeof(UILabel));
		pauseButtonSprite = (UISprite)PauseButton.GetComponent(typeof(UISprite));
	}

	public void Pause(){
		isPaused = true;
		Time.timeScale = 0;
		return;
	}

	public void Unpause(){
		isPaused = false;
		Time.timeScale = 1;
		return;
	}
	public bool IsPaused(){
		return isPaused;
	}


	public void PauseButtonClicked(){
		isPaused = !isPaused;
		if (isPaused){
			pauselabel.text = "Paused";
			pauseButtonSprite.spriteName = "PauseButton-03";
			Time.timeScale = 0;
		}
		else 
		{
			pauselabel.text = "";
			pauseButtonSprite.spriteName = "PauseButton-01";
			Time.timeScale = 1;
		}

		return;
	}*/

}
