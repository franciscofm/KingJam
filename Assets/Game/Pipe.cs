using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public Transform floorT;
	public Transform spawnT;
	public Transform targetT;

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
}
