using UnityEngine;
using System.Collections;

public class OffscreenDetectorScript: MonoBehaviour {

	public GameObject Player;

	void  OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == Player) {
			//Debug.Log("Player went off screen into " + gameObject.name);
			GUIController guiController = (GUIController) GameObject.Find("GameController").GetComponent(typeof(GUIController));
			if (guiController.currentScreen != GUIController.CurrentScreen.GAME_OVER )
				guiController.ShowGameOverScreen();
			return;
		}
		//Destroy(other.gameObject);
	}

}
