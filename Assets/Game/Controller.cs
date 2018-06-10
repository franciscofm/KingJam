using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Controller : MonoBehaviour {

		public GameObject playerP;
		public GameObject enemyP;
        public Transform playerT;
		public Transform spawnT;
		public Transform floorT;
		public Transform pipeT;
		public static Controller instance;

		Vector3 pipeStartPos, playerStartPos;

		public float[] TimeLevels = new float[]{5f, 10f, 20f};
		public float gameTime;
		public bool finished;

		[HideInInspector] public Player playerS;
		[HideInInspector] public List<Enemy> enemiesS = new List<Enemy>();
		[HideInInspector] public Pipe pipeS;
		[HideInInspector] public Floor floorS;
		[HideInInspector] int level;

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
			playerS = playerT.GetComponent<Player> ();
			pipeS = pipeT.GetComponent<Pipe> ();
			floorS = floorT.GetComponent<Floor> ();

			pipeStartPos = pipeT.position;
			playerStartPos = playerT.position;

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

		public void PlayerLost() {
            pipeS.playing = false;
            finished = true;
        }
		void Restart() {
            while (enemiesS.Count != 0)
            {
                Destroy(enemiesS[0].gameObject);
                enemiesS.RemoveAt(0);
            }
            pipeT.position = pipeStartPos;
            pipeS.LevelUp(0);
            floorS.LevelUp(0);
            playerT = Instantiate(playerP).transform;
            pipeS.playing = true;
            finished = false;
        }
	}
}