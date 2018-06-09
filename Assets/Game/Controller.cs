using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Controller : MonoBehaviour {

		public GameObject enemyP;
		public Transform playT;
		public Transform spawnT;
		public Transform floorT;
		public Transform pipeT;
		public static Controller instance;


		public float[] TimeLevels = new float[]{5f, 10f, 20f};
		public float gameTime;
		public bool finished;

		public Player playerS;
		public List<Enemy> enemiesS = new List<Enemy>();
		public Pipe pipeS;
		public Floor floorS;
		int level;

        public void NewEnemy(Enemy enemy)
        {
            enemy.LevelUp(level);
            enemiesS.Add(enemy);
        }

		void Awake() {
			if (instance != null) Destroy (instance.gameObject);
			instance = this;
            FixSpriteRotation.floor = floorT;
        }

		void Start() {
			playerS = playT.GetComponent<Player> ();
			pipeS = pipeT.GetComponent<Pipe> ();
			floorS = floorT.GetComponent<Floor> ();

			gameTime = 0f;
			level = 0;
			finished = false;
			pipeS.StartGame ();
		}
		void Update() {
			gameTime += Time.deltaTime;
			if (!finished) {
				if (gameTime > TimeLevels [level]) {
					++level;
					LevelUp (); 
					if (level >= TimeLevels.Length) {
						finished = true;
						return;
					}
				}
			}
		}
		void LevelUp() {
            //playerS.LevelUp ();
            for (int i = 0; i < enemiesS.Count; ++i)
                enemiesS[i].LevelUp(level);
            //pipeS.LevelUp();
            floorS.LevelUp(level);
		}
	}
}