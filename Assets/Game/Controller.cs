using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Controller : MonoBehaviour {

		public GameObject enemyP;
		public Transform playT;
		public Transform spawnT;
		public static Controller instance;

		Player player;
		List<Enemy> enemies;

		void Awake() {
			if (instance != null) Destroy (instance.gameObject);
			instance = this;
		}

		bool playing;
		float spawnStart;
		float spawnTime;
		IEnumerator routine;
		IEnumerator Start () {
			playing = true;
			spawnStart = 5f;
			spawnTime = 4f;
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
	}
}