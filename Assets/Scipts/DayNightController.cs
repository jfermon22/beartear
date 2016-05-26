using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	public GameObject Sun;
	public GameObject Moon;
	GameObject CelestialBody;
	public float scrollSpeed = -10f;
	public Vector2 SpawnPoint = new Vector2(0,0);
	public bool IsNight = false;
	GameObject[] Sky;

	void Start()
	{
		Spawn ("Sun");
	}


	void Update () 
	{
			Move (CelestialBody);
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("Sky")) 
			{
					go.GetComponent<Animator>().SetBool ("IsNight", IsNight);
			}
	}

	void  OnTriggerEnter2D (Collider2D other) 
	{
	
		if (other.gameObject.tag == "Sun") 
		{
			Destroy(other.gameObject);
			Spawn ("Moon");
			return;
		}
		else if (other.gameObject.tag == "Moon") 
		{
			Destroy(other.gameObject);
			Spawn ("Sun");
			return;
		} 
	}

	public void Move(GameObject sunMoon)
	{
		sunMoon.GetComponent<Rigidbody2D>().velocity = new Vector2(scrollSpeed,0);
	}

	void Spawn(string sunMoon)
	{
		if( CelestialBody == null){
			CelestialBody = new GameObject(sunMoon);
		}

		if (sunMoon == "Moon") 
		{
				CelestialBody = (GameObject)Instantiate (Moon, new Vector2 (SpawnPoint.x + transform.position.x, SpawnPoint.y + transform.position.y), Quaternion.identity);
				IsNight = true;
		} 
		else if (sunMoon == "Sun") 
		{
				CelestialBody = (GameObject)Instantiate (Sun, new Vector2 (SpawnPoint.x + transform.position.x, SpawnPoint.y + transform.position.y), Quaternion.identity);
				IsNight = false;
			
		}

		CelestialBody.transform.parent = transform;
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Sky")) 
		{
			go.GetComponent<Animator>().SetTrigger("ActivateFade");
			go.GetComponent<Animator>().SetBool("IsNight",IsNight);
		}
	}
	
}
