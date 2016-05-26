using UnityEngine;
using System.Collections;
/***********************************************
 * This class 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 ***********************************************/


[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (AudioSource))]

public class JFCharacterController2D : MonoBehaviour {

	public float gravityScale = 10f;
	public float maxSpeed  = 30.0f;
	public float jumpForce = 2500.0f;
	public bool soundIsEnabled = true;
	bool m_IsFacingRight = true;
	SoundControllerScript soundController;
	public AudioClip jumpSound;

	void OnEnable()
	{
		GameObject soundControllerObj = GameObject.Find("SoundController");
		soundController = (SoundControllerScript) soundControllerObj.GetComponent(typeof(SoundControllerScript));
	}

	public void Move(Vector2 v2Movement)
	{ 
		GetComponent<Rigidbody2D>().velocity = v2Movement;
	}

	public void Jump(float jumpForce)
	{
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		PlayJumpSound();
	}

	public void PlayJumpSound ()
	{
		soundController.PlaySound(jumpSound);
	}

	public void AddForce(Vector2 v2Force)
	{
		GetComponent<Rigidbody2D>().AddForce(v2Force);
	}

	public void Flip() {
		m_IsFacingRight = !m_IsFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void setGravity(float newGravity)
	{
		GetComponent<Rigidbody2D>().gravityScale = gravityScale;
	}

	public void setMaxSpeed(float newMaxSpeed)
	{
		maxSpeed = newMaxSpeed;
	}

	public void setJumpForce(float newJumpForce)
	{
		jumpForce = newJumpForce;
	}

	public void setIsFacingRight(bool newIsFacingRight)
	{
		m_IsFacingRight = newIsFacingRight;
	}

	public bool IsFacingRight()
	{
		return m_IsFacingRight;
	}

	public float getJumpForce()
	{
		return jumpForce;
	}


}
