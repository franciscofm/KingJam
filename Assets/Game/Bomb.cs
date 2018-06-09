using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	}
}
