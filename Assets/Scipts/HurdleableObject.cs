using UnityEngine;
using System.Collections;

public class HurdleableObject : MonoBehaviour {

	public LayerMask Player;
	public AchievementTrackerScript.Achievement ThisAchievement;
	
	AchievementTrackerScript AchTrackerScript;
	GUIController guiController;
	bool hasBeenHurdled = false;
	
	void Start ()
	{
		GameObject gameController = GameObject.Find("GameController");
		AchTrackerScript = (AchievementTrackerScript) gameController.GetComponent(typeof(AchievementTrackerScript));
		guiController = (GUIController) gameController.GetComponent(typeof(GUIController));
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D[] results = new RaycastHit2D[1];
		
		if ( (guiController.currentScreen == GUIController.CurrentScreen.GAME_PLAY) && 
		     Physics2D.RaycastNonAlloc(transform.position, Vector2.up,results,Mathf.Infinity,Player) != 0 &&
		    !hasBeenHurdled )
		{
			if ( !AchTrackerScript.AchievementHasBeenMet(ThisAchievement) ){
				//Debug.Log( "Player has hurdled: " + gameObject);
				AchTrackerScript.IncrementCount(ThisAchievement);
				hasBeenHurdled = true;
			}
		}
		
	}
}
