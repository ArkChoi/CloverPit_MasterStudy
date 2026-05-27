using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour {
	public static GameController instance;
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount=10;
	public float spawnWait = 0.5f;
	public float startWait=1;
	public float waveWait=4;

	public TMP_Text scoreText;
	public TMP_Text restartText;
	public TMP_Text gameOverText;

	private bool restart;
	private int score;
	public int health=100;                 // Add for 3D FPS Game


	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

	void Start ()
	{
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		UpdateHealth ();
		StartCoroutine(SpawnWaves());
	}

	void Update()
	{
		if(restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 
				                                     Random.Range (1, spawnValues.y), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	// Add for 3D FPS Game  
	public void MinusHealth(int newHealthValue)
	{
		health -= newHealthValue;
		UpdateHealth ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	// Add for 3D FPS Game  
	void UpdateHealth()
	{
		restartText.text = "Health: " + health;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		restartText.text = "Press 'R' for Restart";
		restart = true;
	}
}
