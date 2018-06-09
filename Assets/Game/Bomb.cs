using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Bomb : MonoBehaviour {

		public Animator animator;
		public enum Type { Explosion, Implosion }
		public Type type = Type.Explosion;
		public float delay = 3f;
		public float duration = 3f;
		public float range = 1f;
		public float force = 1f;

		void Start() {
			StartCoroutine (Ignite ());
		}

		IEnumerator Ignite() {
			animator.Play ("Delay");
			yield return new WaitForSeconds (delay);
			StartCoroutine (IgniteRoutine ());
		}
		IEnumerator IgniteRoutine() {
			animator.Play ("Ignite");
			yield return new WaitForSeconds (duration);
			Explode ();
		}
		void Explode() {
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
			float dist2 = Vector3.Distance (Controller.instance.playT.position, transform.position);
			if (dist2 < range) {
				Vector3 dir2 = 
					(type == Type.Explosion) ? 
					Controller.instance.playT.position - transform.position : 
					transform.position - Controller.instance.playT.position;
				Controller.instance.playerS.Push (force, dir2.normalized);
			}
		}

		void OnDrawGizmosSelected() {
			Gizmos.color = (type == Type.Explosion) ? Color.red : new Color(161, 0, 255);
			Gizmos.DrawWireSphere (transform.position, range);
		}
	}
}