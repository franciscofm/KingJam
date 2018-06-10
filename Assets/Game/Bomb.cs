using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Bomb : MonoBehaviour, ILevelUp {

		public Animator animator;
		public string DelayAnimation = "Delay";
		public string IgniteAnimation = "Ignite";
		public string ExplodeAnimation = "Explode";
		public enum Type { Explosion, Implosion }
		public Type type = Type.Explosion;
		public float delay = 3f;
		public float duration = 3f;
		public float dieWait = 1f;
		public float range = 1f;
		public float force = 1f;

		public void SetType(Type type) {
			this.type = type;
			if (type == Type.Explosion) {
				DelayAnimation += "_1";
				IgniteAnimation += "_1";
				ExplodeAnimation += "_1";
			} else {
				DelayAnimation += "_2";
				IgniteAnimation += "_2";
				ExplodeAnimation += "_2";
			}
			StartCoroutine (Ignite());
		}

		IEnumerator Ignite() {
			animator.Play ("Delay");
			yield return new WaitForSeconds (delay);
			StartCoroutine (IgniteRoutine ());
		}
		IEnumerator IgniteRoutine() {
			animator.Play ("Ignite");
			yield return new WaitForSeconds (duration);
			StartCoroutine(Explode ());
		}
		IEnumerator Explode() {
			animator.Play ("Explode");
			List<Enemy> enemies = Controller.instance.enemiesS;
			for (int i = 0; i < enemies.Count; ++i) {
				float dist = Vector3.Distance (enemies [i].transform.position, transform.position);
				if (dist < range) {
					Vector3 dir = 
						(type == Type.Explosion) ? 
						enemies [i].transform.position - transform.position : 
						transform.position - enemies [i].transform.position;
					enemies [i].Push (force, dir.normalized);
				}
			}
			float dist2 = Vector3.Distance (Controller.instance.playerT.position, transform.position);
			if (dist2 < range) {
				Vector3 dir2 = 
					(type == Type.Explosion) ? 
					Controller.instance.playerT.position - transform.position : 
					transform.position - Controller.instance.playerT.position;
				Controller.instance.playerS.Push (force, dir2.normalized);
			}
			yield return new WaitForSeconds (dieWait);
			Destroy (gameObject);
		}

		void OnTriggerEnter(Collider col) {
			transform.parent = col.gameObject.transform;
			transform.localScale = Vector3.one;
			Destroy (GetComponent<Rigidbody> ());
		}

		public void LevelUp(int to) {

		}

		void OnDrawGizmosSelected() {
			Gizmos.color = (type == Type.Explosion) ? Color.red : new Color(161, 0, 255);
			Gizmos.DrawWireSphere (transform.position, range);
		}
	}
}