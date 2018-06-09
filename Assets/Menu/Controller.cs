using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
	public class Controller : MonoBehaviour {

		public float startGameWait = 3f;
		public Animator animator;

		bool blocked;

		void Start() {
			blocked = false;
		}
		public void StartGame() {
			blocked = true;
			animator.Play ("StartGame");
			StartCoroutine (StartGameRoutine ());
		}
		IEnumerator StartGameRoutine() {
			yield return new WaitForSeconds (startGameWait);
			//Game.Controller.instance.StartGame ();
		}

		public void ShowMenu() {
			blocked = false;
			animator.Play ("ShowMenu");
			//Enseñar panel
		}

		public void ShowSettings() {
			
		}
	}
}