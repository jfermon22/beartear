using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FlapjackController : JFCharacterController2D {
	public LayerMask Ground;
	public Transform GroundChecker;
	public	bool canDoubleJump = false;
	public float doubleJumpForce = 1250;
	public float PlaneAcceleration = 10f;
	public float MaxYHeight = 12;
	public bool IsInvincible = false;
	public GameObject TopStopper;
	public float TripModifier = 3f;
	
	bool m_doubleJumpUsed = false;
	bool m_isTouchDevice = false;
	
	public Animator m_animator;
	public bool m_bShouldJump = false;
	float m_groundRadius = 0.7f;
	bool m_bIsGrounded = false;
	public bool m_bShouldTrip = false;
	public int tripFrameCount = 0;

	float powerUp;
	bool hasAxe = false;
	bool onPlane;
	float powerUpDuration = 0;

	public AchievementTrackerScript achTracker;
	public GUIController guiController;


	SpriteRenderer FArmRenderer;
	SpriteRenderer RArmRenderer;
	SpriteRenderer TorsoRenderer;
	SpriteRenderer FFootRenderer;
	SpriteRenderer RFootRenderer;
	List<SpriteRenderer> FlapJackRendererList = new List<SpriteRenderer>();




	public enum PlayerState {
		NORMAL,
		HAS_AXE,
		ON_PLANE,
		//HAS_LASER,
		INVINCIBLE,
	} ;

	public PlayerState playerState = PlayerState.NORMAL;

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
		GameObject gameController = GameObject.Find("GameController");
		achTracker = (AchievementTrackerScript) gameController.GetComponent(typeof(AchievementTrackerScript));	
		guiController = (GUIController) gameController.GetComponent(typeof(GUIController));
		m_animator =  GetComponent<Animator>();
		GetPlayerRenderers();
	}
	
	void Update () 
	{
		getPlayerMove();
		if (playerState == PlayerState.NORMAL) 
		{
			TopStopper.SetActive(false);
			RunningUpdate();
		}
		else if (playerState == PlayerState.HAS_AXE && powerUpDuration > 0)
		{
			TopStopper.SetActive(false);
			UpdatePowerUpDuration ();
			RunningUpdate();
		}
		else if (playerState == PlayerState.ON_PLANE && powerUpDuration > 0)
		{
			//IgnoreGrounded = true;
			TopStopper.SetActive(true);
			//UpdatePowerUpDuration ();
			PlaneUpdate();
		}
		//else if (playerState == PlayerState.HAS_LASER && powerUpDuration > 0)
		//{
		//	TopStopper.SetActive(false);
		//	UpdatePowerUpDuration ();
		//}
		else if (playerState == PlayerState.INVINCIBLE && powerUpDuration > 0)
		{
			TopStopper.SetActive(false);
			ChangeColor();
			UpdatePowerUpDuration ();
			RunningUpdate();
		}
		if (guiController.currentScreen == GUIController.CurrentScreen.GAME_PLAY){
			achTracker.UpdateDistanceAchievements((int)transform.position.x);
		}
	}

	void RunningUpdate () 
	{
		checkGrounded ();
		m_animator.SetBool("Grounded", m_bIsGrounded);
		m_animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);  
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Ground"), GetComponent<Rigidbody2D>().velocity.y > 0);
		if ( m_bShouldJump && m_bIsGrounded)
		{
			m_animator.SetBool("Grounded", false);
			Jump (jumpForce);
		} 
		else if ( canDoubleJump && !m_doubleJumpUsed && m_bShouldJump ){
			Jump (doubleJumpForce);
			m_doubleJumpUsed = true;
		}
		
		
		m_animator.SetBool ("isRunning", Convert.ToBoolean(1));
		if (tripFrameCount > 0){
			Move (new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y));
			tripFrameCount--;
		} else {
			transform.Translate(Vector3.right * maxSpeed *Time.deltaTime);
			//Move (new Vector2 ( rigidbody2D.velocity.x, rigidbody2D.velocity.y));

		}
		m_bShouldJump = false;
		return;
	}

	void PlaneUpdate () 
	{
		m_animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);  
		float verticalAccel = GetComponent<Rigidbody2D>().velocity.y;
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Ground"), verticalAccel > 0);
		//if ( m_bShouldJump && transform.position.y < MaxYHeight)
		if ( m_bShouldJump)
		{

			verticalAccel += PlaneAcceleration;
		
			//Debug.Log ("planeUpdate - vertaccel is " +verticalAccel);
		}
		//transform.Translate(Vector3.right * maxSpeed *Time.deltaTime);

		Move (new Vector2 (maxSpeed, verticalAccel));

		m_bShouldJump = false;
	}

	void OnCollisionEnter2D (Collision2D other) {

		if (other.gameObject.tag == "Enemy" && playerState != PlayerState.INVINCIBLE) {
						//Debug.Log("FlapjackController::OnCollisionEnter2D - nothing happens");
				}
		//Debug.Log ("Collision Detected with" + other.gameObject.name);
		//Debug.Log ("Collision Detected at"  + other.contacts[0]);
	}
	
	public void setPowerUpDuration(int seconds) {
		powerUpDuration = seconds; 
	}

	void UpdatePowerUpDuration ()
	{
		powerUpDuration -= Time.deltaTime;
		if (powerUpDuration <= 0) 
		{
			setPlayerState(PlayerState.NORMAL);
		}
	}
	
	public void setPlayerState(PlayerState newPlayerState)
	{
		playerState = newPlayerState;
		if (playerState == PlayerState.NORMAL) 
		{
			resetPlayerState();
		}
		else if (playerState == PlayerState.HAS_AXE)
		{
			m_animator.SetBool("HasAxe", true);
			m_animator.SetBool("HasPlane", false);
		}
		else if (playerState == PlayerState.ON_PLANE)
		{
			m_animator.SetBool("HasAxe", false);
			m_animator.SetBool("HasPlane", true);
		}
		else if (playerState == PlayerState.INVINCIBLE)
		{
			m_animator.SetBool("HasAxe", false);
			m_animator.SetBool("HasPlane", false);
		}
		//else if (playerState == PlayerState.HAS_LASER )
		//{
		//
		//}
	}

	public PlayerState getPlayerState()
	{
		return playerState;
	}
	
	void ChangeColor()
	{
		for(int iii = 0; iii < FlapJackRendererList.Count; iii++)
		{
			if(FlapJackRendererList[iii].color == Color.white)
				FlapJackRendererList[iii].color = Color.red;
			else if(FlapJackRendererList[iii].color == Color.red)
				FlapJackRendererList[iii].color = Color.green;
			else if(FlapJackRendererList[iii].color == Color.green)
				FlapJackRendererList[iii].color = Color.blue;
			else if(FlapJackRendererList[iii].color == Color.blue)
				FlapJackRendererList[iii].color = Color.magenta;
			else if(FlapJackRendererList[iii].color == Color.magenta)
				FlapJackRendererList[iii].color = Color.yellow;
			else if(FlapJackRendererList[iii].color == Color.yellow)
				FlapJackRendererList[iii].color = Color.red;
		}
		return;
	}

	public void getPlayerMove()
	{
		if (m_isTouchDevice) {

			switch (playerState) {
				case PlayerState.NORMAL:
				m_bShouldJump = m_bShouldJump || didTap();
		 			break;
 				case PlayerState.HAS_AXE:
				m_bShouldJump = m_bShouldJump || didTap();
					break;
				case PlayerState.ON_PLANE:
				m_bShouldJump = m_bShouldJump || (Input.touchCount > 0);
					break;
				//case PlayerState.HAS_LASER:
				//m_bShouldJump = m_bShouldJump || didTap();
				//	break;
				case PlayerState.INVINCIBLE:
				m_bShouldJump = m_bShouldJump || didTap();
					break;
			}
		} else {
			switch (playerState) {
				case PlayerState.NORMAL:
					//m_bShouldJump = m_bShouldJump || Input.GetKeyDown (KeyCode.Space);
					m_bShouldJump = m_bShouldJump || didTap();
					break;
				case PlayerState.HAS_AXE:
					//m_bShouldJump = m_bShouldJump || Input.GetKeyDown (KeyCode.Space);
					m_bShouldJump = m_bShouldJump || didTap();
					break;
				case PlayerState.ON_PLANE:
					m_bShouldJump = m_bShouldJump || Input.GetMouseButton(0) || Input.GetMouseButton(1);
					break;
				//case PlayerState.HAS_LASER:
				//	//m_bShouldJump = m_bShouldJump || Input.GetKeyDown (KeyCode.Space);
				//	m_bShouldJump = m_bShouldJump || didTap();
				//	break;
				case PlayerState.INVINCIBLE:
					//m_bShouldJump = m_bShouldJump || Input.GetKeyDown (KeyCode.Space);
					m_bShouldJump = m_bShouldJump || didTap();
				break;
			}
		}
	}

	bool didTap(){
		if(guiController.currentScreen != GUIController.CurrentScreen.GAME_PLAY){
			return false;
		}
		if (m_isTouchDevice) {
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began && touch.tapCount == 1)
					return true;
			}
			return false;
		} else {
			if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ){
				return true;
			}
			return false;
		}
	}

	bool checkGrounded ()
	{
		m_bIsGrounded = Physics2D.OverlapCircle(GroundChecker.position,m_groundRadius, Ground);
		if(m_bIsGrounded)
		{
			m_doubleJumpUsed = false;
		}
		return m_bIsGrounded;
	}

	void GetPlayerRenderers()
	{
		foreach(Transform child in transform)
			{
				foreach(SpriteRenderer comp in child.GetComponentsInChildren<SpriteRenderer>())
				{
				FlapJackRendererList.Add(comp);
				}
			}
	}

	void resetPlayerState(){
		setHasAxe(false);
		setOnPlane (false);
		for(int iii = 0; iii < FlapJackRendererList.Count; iii++){
			FlapJackRendererList[iii].color = Color.white;
		}
	}
	
	public void setHasAxe(bool newHasAxe) {
		hasAxe = newHasAxe;
		m_animator.SetBool("HasAxe", hasAxe);
	}
	
	public void setOnPlane(bool newOnPlane) {
		onPlane = newOnPlane;
		m_animator.SetBool("HasPlane", onPlane);
	}

	public void DidTrip(){
		tripFrameCount = 16;
	}

}
