using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public AudioClip[] smashSounds;
	private AudioSource audioSource;
	public GameObject bloodEffect;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		
	}
	

	void Update () {
		//checking for input here
		// map it to the left mouse button
		if(Input.GetMouseButtonDown(0)){
			// we will reycast here
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // we have created a ray that points from the main camera to the poin where mouse is clicked
			RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);// shoot the ray from the origin point to the direction and store what it hits in the hit variable

			//check if it hits something
			if (hit.collider != null) {
				// if we hit zombie
				if (hit.collider.tag == "Enemy") {
					// this is where we kill enemy
					//need to play the smash audio
					audioSource.PlayOneShot(smashSounds[Random.Range(0,smashSounds.Length)], 0.5f);//0,1,2,3
					gameObject.GetComponent<GameManager>().KillEnemy();
					Camera.main.GetComponent<Animator> ().SetTrigger ("`Shake");
					DisplayBloddEffect (Camera.main.ScreenToWorldPoint(Input.mousePosition));
					
				}
			}
		}
		
	}

	private void DisplayBloddEffect(Vector2 pos){
		bloodEffect.transform.position = pos;
		bloodEffect.GetComponent<Animator>().SetTrigger ("Smashed");
	}
}
