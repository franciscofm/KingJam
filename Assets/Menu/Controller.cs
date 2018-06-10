using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
	public class Controller : MonoBehaviour {

		public Image holdImage;
		public Sprite playHeld;
		public Sprite playNormal;
		public float startGameWait = 3f;
		public GameObject escPanel;
		//public Animator animator;

		public bool blocked, holding, starting;
		public bool inMain = true;
		public float holdTimer;

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
		}
		void StartGame() {
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