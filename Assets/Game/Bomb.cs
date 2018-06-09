using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Bomb : MonoBehaviour {

		public Animator animator;
		public float delay = 3f;
		public float duration = 3f;
		public float range = 1f;
		public float force = 1f;

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
				Vector3 dir = enemies [i].transform.position - transform.position;
				if (Mathf.Abs (dir.magnitude) < range) {
					enemies [i].Push (force, dir.normalized);
				}
			}
			Vector3 dir2 = Controller.instance.playT.position - transform.position;
			if (Mathf.Abs (dir2.magnitude) < range) {
				Controller.instance.playerS.Push (force, dir2.normalized);
			}
		}
	}
}