using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;
	private bool restart;
	private bool gameOver;


	IEnumerator spawnWaves(){
		yield return new WaitForSeconds(startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				float randomWait = Random.Range (.2f, .75f);
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait + randomWait);
			}
			yield return new WaitForSeconds (waveWait);

			if(gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}	


	void Update(){
		if (restart) {
				if(Input.GetKeyDown (KeyCode.R)){
				Application.LoadLevel (Application.loadedLevel);
				}
			}
	}

	void Start(){
		StartCoroutine (spawnWaves ());
		score = 0;
		updateScore ();
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
		restart = true;
		}

	public void addScore (int newScoreValue){
		score += newScoreValue;
		updateScore ();
	}


	void updateScore(){
		scoreText.text = "Score: " + score;
	}
}
