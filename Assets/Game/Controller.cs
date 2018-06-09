using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Controller : MonoBehaviour {

		public GameObject enemyP;
		public Transform playT;
		public Transform spawnT;
		public Transform floorT;
        public Pipe pipeS;
		public static Controller instance;

		Player player;
		List<Enemy> enemies;

		void Awake() {
			if (instance != null) Destroy (instance.gameObject);
			instance = this;
            FixSpriteRotation.floor = floorT;
        }
		void Start() {
			pipeS.StartGame ();
		}
	}
}