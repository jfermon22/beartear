using UnityEngine;
using System.Collections;

public class EnemyController2D :JFCharacterController2D {

	public LayerMask CanCollideWith;
	public float bounceForce =1.0f;
	public Animator animator;
	public CircleCollider2D ccollide2d;
	public float m_move = 0.0f; 

	public void Start () 
	{
		if (animator == null) {
						animator = GetComponent<Animator> ();
				}
		if (ccollide2d == null) {
			ccollide2d = GetComponent<CircleCollider2D> ();
		}
		m_move = -1;
		setIsFacingRight (false);
	}


	void Update () {
	
	}

	public void FixedUpdate () 
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

	void OnCollisionStay2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "Wall")
		{
			m_move = -m_move;
		}

	}
	

}
