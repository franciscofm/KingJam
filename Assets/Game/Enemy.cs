using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Enemy : IForce, ILevelUp {
		//public Rigidbody body;
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

        override public Vector3 getForce() { return force; }

		void Awake() {
			if (body != null) body = GetComponent<Rigidbody> ();
		}
		// Use this for initialization
		void Start () {
			target = Controller.instance.playerT;
			delayedPos = target.position;
			if(delayed) {
				StartCoroutine(RetargetRoutine());
				if (sprayed) {
					delayedPos.x += Random.Range (-distance, distance);
					delayedPos.z += Random.Range (-distance, distance);
				}
			}
		}
		IEnumerator RetargetRoutine() {
			yield return new WaitForSeconds (retargetTime);
            if (target != null)
            {
                delayedPos = target.position;
                if (sprayed)
                {
                    delayedPos.x += Random.Range(-distance, distance);
                    delayedPos.z += Random.Range(-distance, distance);
                }
            }
		}
		
		// Update is called once per frame
		void Update () {
            if (this.target != null)
            {
                Vector3 target = (!delayed) ? this.target.position : delayedPos;
                Vector3 dir = (target - transform.position);
                dir.y = 0f;
                dir.Normalize();
                body.AddForce(force = (dir * speed));
            }
    	}

		[Header("LevelUp stats")]
		public float speed1 = 7f;
		public float speedError1 = 1f;
		public float speed2 = 8f;
		public float speedError2= 1.5f;

		public float chanceToDelayed1 = 0.1f;
		public float chanceToDelayed2 = 0.1f;
		public float chanceToSprayed1 = 0.15f;
		public float chanceToSprayed2 = 0.1f;

		public void LevelUp(int to) {
			
			bool delayedBefore = delayed;
			bool sprayedBefore = sprayed;
			float r;

			switch (to) {
            case 0:
                break;
			case 1:
				animator.runtimeAnimatorController = level1;
				speed = speed1 + speedError1 * Random.Range (0f, 1f);
				if (!delayedBefore) {
					r = Random.Range (0f, 1f);
					delayed = r < chanceToDelayed1;
				}
				if (!sprayedBefore) {
					r = Random.Range (0f, 1f);
					sprayed = r < chanceToSprayed1;
				}
                break;
            case 2:
            default:
				animator.runtimeAnimatorController = level2;
				speed = speed1 + speedError2 * Random.Range (0f, 1f);
				if (!delayedBefore) {
					r = Random.Range (0f, 1f);
					delayed = r < chanceToDelayed2;
				}
				if (!sprayedBefore) {
					r = Random.Range (0f, 1f);
					sprayed = r < chanceToSprayed2;
				}
                break;
            }

			if (!delayedBefore && delayed) {
                if (target == null)
                {
                    delayed = false;
                    sprayed = false;
                    return;
                }
				delayedPos = target.position;
				StartCoroutine(RetargetRoutine());
				if (sprayed) {
					delayedPos.x += Random.Range (-distance, distance);
					delayedPos.z += Random.Range (-distance, distance);
				}
			}
        }
	}
}