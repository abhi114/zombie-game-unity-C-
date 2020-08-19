using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject[] Zombies;

	private bool isRising = false;
	private bool isFalling = false;

	private int activeZombieIndex = 0;
	private Vector2 startPosition;

	public int riseSpeed = 1;
	public int scoreThreshold = 5;

	private int zombiesSmashed;

	private int livesRemaining;
	private bool gameOver;

	public Image life01;
	public Image life02;
	public Image life03;
	public Button GameOverButton;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		gameOver = false;
		zombiesSmashed = 0;
		scoreText.text = zombiesSmashed.ToString ();
		livesRemaining = 3;
		pickNewZombie ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			if (isRising) {
				//zombie moving up logic goes in here
				if (Zombies [activeZombieIndex].transform.position.y - startPosition.y >= 2.5f) {
					isRising = false;
					isFalling = true;
				} else {
					Zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
				}


			} else if (isFalling) {
				//all the logic while the zombie is going down goes in here
				if (Zombies [activeZombieIndex].transform.position.y - startPosition.y <= 0f) {
					isFalling = false;
					isRising = false;
					livesRemaining--;
					UpdateLifeUi ();

				} else {
					Zombies [activeZombieIndex].transform.Translate (Vector2.down * Time.deltaTime * riseSpeed);
				}

			} else {
				//anything else happens here
				Zombies[activeZombieIndex].transform.position = startPosition;
				pickNewZombie ();
			}

		}

		
	}
	private void UpdateLifeUi(){
		if (livesRemaining == 3) {
			life01.gameObject.SetActive (true);
			life02.gameObject.SetActive (true);
			life03.gameObject.SetActive (true);
			
		}
		if (livesRemaining == 2) {
			life01.gameObject.SetActive (true);
			life02.gameObject.SetActive (true);
			life03.gameObject.SetActive (false);

		}if (livesRemaining == 1) {
			life01.gameObject.SetActive (true);
			life02.gameObject.SetActive (false);
			life03.gameObject.SetActive (false);

		}
		if (livesRemaining == 0) {
			life01.gameObject.SetActive (false);
			life02.gameObject.SetActive (false);
			life03.gameObject.SetActive (false);
			gameOver = true;
			GameOverButton.gameObject.SetActive (true);
		}
		
	}
	private void pickNewZombie(){
		isRising = true;
		isFalling = false;
		activeZombieIndex = UnityEngine.Random.Range (0, Zombies.Length); // this is going to generate a number between zero and six
		//GameObject activeZombie = Zombies[activeZombieIndex];
		//Transform activeTransform = activeZombie.transform;
		//Vector2 startPosition = activeTransform.position;
		startPosition = Zombies[activeZombieIndex].transform.position; // vector2 is the 2d vector
		
	}

	public void KillEnemy(){
		
		zombiesSmashed++;
		//increase speed
		IncreaseSpawnSpeed();
		//updating the ui
		scoreText.text = zombiesSmashed.ToString();
		//write code for killing enemy
		Zombies[activeZombieIndex].transform.position = startPosition;
		//now choose a new zombie
		pickNewZombie ();


	}
	private void IncreaseSpawnSpeed(){
		if (zombiesSmashed >= scoreThreshold) {
			riseSpeed++;
			scoreThreshold *= 2;
		}
	}
	public void OnRestart(){
		//Debug.Log ("hi");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void OnMainMenu(){
		SceneManager.LoadScene (0);
	}
}
