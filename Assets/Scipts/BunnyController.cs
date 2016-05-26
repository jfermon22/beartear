using UnityEngine;
using System.Collections;

public class BunnyController : EnemyController2D {
	public GameObject explosion;
	public int PowerUpPoints = 1000;
	AchievementTrackerScript AchTrackerScript;
	GUIController guiController;

	new void Start () 
	{
		if (animator == null) {
			animator = GetComponent<Animator> ();
		}
		if (ccollide2d == null) {
			ccollide2d = GetComponent<CircleCollider2D> ();
		}
		m_move = -1;
		setIsFacingRight (false);

		GameObject gameController = GameObject.Find("GameController");
		guiController = (GUIController) gameController.GetComponent(typeof(GUIController));
		AchTrackerScript = (AchievementTrackerScript) gameController.GetComponent(typeof(AchievementTrackerScript));
	}


	new void FixedUpdate () 
	{
		if ( m_move > 0 && ! IsFacingRight())
		{
			Flip();
		}
		else if(m_move < 0 && IsFacingRight())
		{
			Flip();
		}
		Move (new Vector2 (m_move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y));
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "EnemyDestroyer")
		{
			//Debug.Log("bunny destroyed by " + collision.gameObject.name);
			//GameObject explosion = (GameObject)Instantiate(Resources.Load("Explosion"));
			if ( !AchTrackerScript.AchievementHasBeenMet(AchievementTrackerScript.Achievement.CHOPPED_BUNNIES) )
			{
				AchTrackerScript.IncrementCount(AchievementTrackerScript.Achievement.CHOPPED_BUNNIES);
			}
			DestroyBunny();
		}
		else if (collision.gameObject.tag == "Player")
		{
			FlapjackController FpController = (FlapjackController)collision.gameObject.GetComponent(typeof(FlapjackController));
			if(FpController.getPlayerState() == FlapjackController.PlayerState.INVINCIBLE)
			{
				DestroyBunny();
				return;
			} else {
				guiController.ShowGameOverScreen();
			}
		}
	}

	void DestroyBunny()
	{
		Instantiate(explosion,new Vector2(transform.position.x,transform.position.y-1),Quaternion.identity);
		Destroy(gameObject);
		PowerUpScript PUScript = (PowerUpScript)gameObject.AddComponent(typeof(PowerUpScript));
		PUScript.AddToScore(PowerUpPoints);
	}
}
