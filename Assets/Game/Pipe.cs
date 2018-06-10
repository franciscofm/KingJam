using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, ILevelUp {
	[Header("References")]
	public Transform floorT;
	public Transform spawnT;
	public Transform targetT;
	public Transform playerT;

	public GameObject enemyP;
	public GameObject bombP;


	public bool playing;
	[Header("Spawn")]
	public float spawnStart = 5f;
	public float spawnTime = 4f;
	public Animator animator;
	[Header("LevelUp")]
	public RuntimeAnimatorController Level1Animator;
	public RuntimeAnimatorController Level2Animator;
	IEnumerator routineSpawn;
	void Start () {
		yPos = transform.position.y;
		if (animator == null) animator = GetComponent<Animator> ();
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
        Game.Controller.instance.NewEnemy(t.GetComponent<Game.Enemy>());
	}

	[Header("Movement")]
	public float radius;
	public float speed;
	public float yPos;
	IEnumerator routineMove;
	void Update() {
		if (playing) {
			if (playerT == null) {
				playing = false;
				StopAllCoroutines ();
				Debug.Log ("Pipe STOP: Lost player target");
				return;
			}
			transform.position = Vector3.Lerp (transform.position, targetT.position, Time.deltaTime * speed);
		}
	}
	public bool random = true;
	public float retarget = 1f;
	[Range(0f,1f)] public float randomWait = 0.1f;
	public float randomChange = 0.05f;
	public int clearIterations = 0;
	public int maxClearIterations = 50;
	IEnumerator MoveRoutine() {
		while (playing) {
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

	public void LevelUp(int to) {
		switch (to)
		{
		case 1:
			animator.runtimeAnimatorController = Level1Animator;
			break;
		case 2:
			animator.runtimeAnimatorController = Level2Animator;
			break;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position - Vector3.up*transform.localScale.y, radius);
	}
}
