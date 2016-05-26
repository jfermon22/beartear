using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Player tilt contoller
/// This script controls a 2d player on either a computer
/// or a touch device.
/// 
/// Touch Device Behavior:
/// 	Left and Right movement is controlled by tilting device
/// 	Jump movement controlled by tapping screen
/// 
/// PC Device Behavior:
/// 	Left and Right movement is controlled by L & R arrow keys
/// 	Jump movement controlled by Up arrow key
/// 
/// </summary>
[RequireComponent (typeof (Animator))]

public class PlayerTiltContoller2D : JFCharacterController2D {
	
	public LayerMask Ground;
	public Transform GroundChecker;
	public	bool canDoubleJump = false;
	public float doubleJumpForce = 2500f;

	bool m_doubleJumpUsed = false;
	bool m_isTouchDevice = false;

	public Animator m_animator;
	float m_move = 0.0f; 
	bool m_bShouldJump = false;
	float m_groundRadius = 0.2f;
	bool m_bIsGrounded = false;

	void Awake()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Debug.Log("On Iphone");
			m_isTouchDevice = true;
		}
	}

	void Start () 
	{
		m_animator =  GetComponent<Animator>();
	}

	void Update () 
	{
		getPlayerInput();
	}

	void FixedUpdate () 
	{

		IsGrounded ();
		m_animator.SetBool("Grounded", m_bIsGrounded);
		m_animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);  

		if ( m_bShouldJump && m_bIsGrounded)
		{
			m_animator.SetBool("Grounded", false);
			Jump (jumpForce);
		} 
		else if ( canDoubleJump && !m_doubleJumpUsed && m_bShouldJump ){
			Jump (doubleJumpForce);
			m_doubleJumpUsed = true;
		}

		if (Math.Abs(m_move) > 0 )
		{
			if ( m_move > 0 && ! IsFacingRight())
			{
				m_move = 1;
				Flip();
			}
			else if(m_move < 0 && IsFacingRight())
			{
				m_move = -1;
				Flip();
			}
		}
		else
		{
			m_move = 0;
		}
		m_animator.SetBool ("isRunning", Convert.ToBoolean(m_move));

		Move (new Vector2 (m_move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y));
		m_bShouldJump = false;
	}

	void getPlayerInput()
	{
		if (m_isTouchDevice){
			m_bShouldJump = m_bShouldJump || ( Input.touchCount > 0);
			m_move = Input.acceleration.x;
		} else {
			m_bShouldJump = m_bShouldJump || Input.GetKeyDown(KeyCode.UpArrow);
			m_move = Input.GetAxis("Horizontal");
		}
	}

	bool IsGrounded ()
	{
		m_bIsGrounded = Physics2D.OverlapCircle(GroundChecker.position,m_groundRadius, Ground);
		if(m_bIsGrounded)
		{
			m_doubleJumpUsed = false;
		}
		return m_bIsGrounded;
	}

	/*void printMaxMinVSpeedValues()
	{
		static float max=-20;
		static float min=1000;

		if (rigidbody2D.velocity.y > max)
		{
			max = rigidbody2D.velocity.y;
		} 
		else if (rigidbody2D.velocity.y < min)
		{
			min = rigidbody2D.velocity.y;
		}
		Debug.Log("vSpeed Max: " + max + " | Min:" + min);
	}*/
}
