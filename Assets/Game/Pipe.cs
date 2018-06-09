using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public Transform floorT;
	public Transform spawnT;
	public Transform targetT;
	public Transform playerT;

	public GameObject enemyP;
	public GameObject bombP;

	bool playing;
	float spawnStart;
	float spawnTime;
	IEnumerator routineSpawn;
	void Start () {
		spawnStart = 5f;
		spawnTime = 4f;
		yPos = transform.position.y;
	}
	public void StartGame() {
		playing = true;
		StartCoroutine (StartGameRoutine ());
		StartCoroutine (MoveRoutine ());
	}
	IEnumerator StartGameRoutine() {
		yield return new WaitForSeconds (spawnStart);
		StartCoroutine (SpawnRoutine ());
	}

	IEnumerator SpawnRoutine() {
		while (playing) {
			SpawnEnemy ();
			yield return new WaitForSeconds (spawnTime);
		}
	}
	void SpawnEnemy() {
		GameObject t = Instantiate (enemyP);
		t.transform.position = spawnT.position;
	}

	public float radius;
	public float speed;
	public float yPos;
	IEnumerator routineMove;
	void Update() {
		transform.position = Vector3.Lerp (transform.position, targetT.position, Time.deltaTime * speed);
	}
	public bool random = true;
	public float retarget = 1f;
	[Range(0f,1f)] public float randomWait = 0.1f;
	public float randomChange = 0.05f;
	public int clearIterations = 0;
	public int maxClearIterations = 50;
	IEnumerator MoveRoutine() {
		while (true) {
			while (random) {
				yield return new WaitForSeconds (randomWait);
				float r = Random.Range (0f, 1f);
				if (r < randomChange) {
					ReasignPipeRandom ();
					clearIterations = 0;
				} else {
					++clearIterations;
					if (clearIterations > maxClearIterations) {
						ReasignPipePlayer ();
						clearIterations = 0;
					}
				}
			}
			while (!random) {
				++clearIterations;
				if (clearIterations > maxClearIterations * 0.5f) {
					random = true;
					clearIterations = 0;
				}
			}
		}
	}
	void ReasignPipeRandom() {
		Vector3 newPos = floorT.position + Utils.RandomV3 (-radius, radius);
		newPos.y = yPos;
		targetT.position = newPos;
		random = false;
	}
	void ReasignPipePlayer() {
		Vector3 newPos = playerT.position;
		newPos.y = yPos;
		targetT.position = newPos;
		random = false;
	}
}
