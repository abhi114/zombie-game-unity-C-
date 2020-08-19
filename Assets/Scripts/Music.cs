using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] musicGameObject = GameObject.FindGameObjectsWithTag ("music");
		//dont destroy the sound when opening another scene
		if(musicGameObject.Length >1){
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
