using UnityEngine;
using System.Collections;

public class PlanePropellorScript : MonoBehaviour {

	Animator animator;
	public bool IsActive;
	// Use this for initialization
	void Start () {
		 animator = GetComponent<Animator> ();
	}
	
	public void SetIsActive(bool IsActive){
		animator.SetBool("isActive",IsActive);
	
	}
}
