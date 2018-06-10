using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
	public class Controller : MonoBehaviour {

		public static Controller instance;
		public Image holdImage;
		public Sprite playHeld;
		public Sprite playNormal;
		public float startGameWait = 3f;
		public GameObject escPanel;
		public GameObject show;
		public GameObject tuto1;
		public GameObject tuto2;
		public GameObject tuto3;
		public GameObject tuto4;
		//public Animator animator;

		public bool blocked, holding, starting;
		public bool inMain = true;
		public float holdTimer;

		void Awake() {
			instance = this;
		}

		public void StartHolding() {
			holdImage.sprite = playHeld;
		}
		public void ReleaseHolding() {
			holdImage.sprite = playNormal;
			StartGame ();
		}

		public void Reset() {
			holdImage.sprite = playNormal;
			starting = false;
			inMain = true;
			escPanel.SetActive (false);
			tuto1.SetActive (false);
			tuto2.SetActive (false);
			tuto3.SetActive (false);
			tuto4.SetActive (false);
			show.SetActive (true);
		}
		void StartGame() {
			show.SetActive (false);
			StartCoroutine (StartGameRoutine ());
		}
		IEnumerator StartGameRoutine() {
			tuto1.SetActive (true);
			yield return new WaitForSecondsRealtime (1.5f);
			tuto1.SetActive (false);
			tuto2.SetActive (true);
			yield return new WaitForSecondsRealtime (1.5f);
			tuto2.SetActive (false);
			tuto3.SetActive (true);
			yield return new WaitForSecondsRealtime (1.5f);
			tuto3.SetActive (false);
			tuto4.SetActive (true);
			yield return new WaitForSecondsRealtime (1.5f);
			tuto4.SetActive (false);
			Game.Controller.instance.StartGame ();
		}

		public void PressEsc() {
			escPanel.SetActive (inMain);
			inMain = !inMain;
		}
		public void PressLicense() {

		}
		public void PressCredits() {

		}
	}
}