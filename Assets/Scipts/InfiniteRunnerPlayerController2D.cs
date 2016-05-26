using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InfiniteRunnerPlayerController2D : JFCharacterController2D {

	public LayerMask Ground;
	public Transform GroundChecker;
	public	bool canDoubleJump = false;
	public float doubleJumpForce = 1250;
	public bool IgnoreGrounded = false;
	
	bool m_doubleJumpUsed = false;
	bool m_isTouchDevice = false;
	
	public Animator m_animator;
	bool m_bShouldJump = false;
	float m_groundRadius = 0.5f;
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
		getPlayerMove();
	}
	
	public void FixedUpdate () 
	{
		
		checkGrounded ();
		m_animator.SetBool("Grounded", m_bIsGrounded);
		m_animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);  
		
		if ( m_bShouldJump && (m_bIsGrounded || IgnoreGrounded) )
		{
			m_animator.SetBool("Grounded", false);
			Jump (jumpForce);
		} 
		else if ( canDoubleJump && !m_doubleJumpUsed && m_bShouldJump ){
			Jump (doubleJumpForce);
			m_doubleJumpUsed = true;
		}
		

		m_animator.SetBool ("isRunning", Convert.ToBoolean(1));
		Move (new Vector2 (maxSpeed, GetComponent<Rigidbody2D>().velocity.y));
		m_bShouldJump = false;
	}
	
	public void getPlayerMove()
	{
		if (m_isTouchDevice){
			m_bShouldJump = m_bShouldJump || ( Input.touchCount > 0);
		} else {
			m_bShouldJump = m_bShouldJump || Input.GetKeyDown(KeyCode.UpArrow);
		}
	}

	public bool checkGrounded ()
	{
		m_bIsGrounded = Physics2D.OverlapCircle(GroundChecker.position,m_groundRadius, Ground);
		if(m_bIsGrounded)
		{
			m_doubleJumpUsed = false;
		}
		return m_bIsGrounded;
	}

	public bool IsGrounded(){
		return m_bIsGrounded;
	}

	public bool ShouldJump(){
		return m_bShouldJump;
	}

	public void SetShouldJump(bool newShouldJump){
		m_bShouldJump=newShouldJump;
	}
	
	public bool IsDoubleJumpUsed(){
		return m_doubleJumpUsed;
		}

	public void SetDoubleJumpUsed(bool newDoubleJumpUsed){
		m_doubleJumpUsed = newDoubleJumpUsed;
	}
	
	public Animator getAnimator(){
		return m_animator;
		}
}
