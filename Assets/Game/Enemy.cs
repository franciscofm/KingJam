using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Enemy : IForce, ILevelUp {
		public Rigidbody body;
		public float speed;
		public bool delayed;
		public bool sprayed;
		public float distance;
		public float retargetTime = 3f;
        public RuntimeAnimatorController level1;
        public RuntimeAnimatorController level2;
        public Animator animator;
        Vector3 force = Vector3.zero;

		Transform target;
		Vector3 delayedPos;

        override public Vector3 getForce()
        {
            return force;
        }

		void Awake() {
			if (body != null) body = GetComponent<Rigidbody> ();
		}
		// Use this for initialization
		void Start () {
			target = Controller.instance.playT;
			if(delayed) {
				delayedPos = target.position;
				StartCoroutine(RetargetRoutine());
				if (sprayed) {
					delayedPos.x += Random.Range (-distance, distance);
					delayedPos.z += Random.Range (-distance, distance);
				}
			}
		}
		IEnumerator RetargetRoutine() {
			yield return new WaitForSeconds (retargetTime);
			delayedPos = target.position;
			if (sprayed) {
				delayedPos.x += Random.Range (-distance, distance);
				delayedPos.z += Random.Range (-distance, distance);
			}
		}
		
		// Update is called once per frame
		void Update () {
			Vector3 target = (!delayed) ? this.target.position : delayedPos;
			Vector3 dir = (target - transform.position);
			dir.y = 0f;
			dir.Normalize ();
			body.AddForce (force = (dir * speed));
		}

        public void LevelUp(int to)
        {
            switch (to)
            {
                case 1:
                    animator.runtimeAnimatorController = level1;
                    break;
                case 2:
                    animator.runtimeAnimatorController = level2;
                    break;
            }
        }
	}
}