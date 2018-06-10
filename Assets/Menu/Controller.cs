using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
	public class Controller : MonoBehaviour {

		public Image holdImage;
		public Sprite Level0Hold;
		public Sprite Level1Hold;
		public Sprite Level2Hold;
		public float holdDuration = 3f;
		public float startGameWait = 3f;
		public GameObject escPanel;
		//public Animator animator;

		public bool blocked, holding, starting;
		public bool inMain = true;
		public float holdTimer;

		public void StartHolding() {
			holding = true;
			holdTimer = 0f;
			starting = false;
		}
		public void ReleaseHolding() {
			holding = false;
			if(!starting)
				holdImage.sprite = Level0Hold;
		}
		void Update() {
			if (holding && !blocked) {
				holdTimer += Time.deltaTime;
				if (holdTimer > holdDuration) {
					holding = false;
					blocked = true;
					starting = true;
					StartGame ();
				} else {
					float p = holdTimer / holdDuration;
					if (p > .33f && p < .66f) {
						holdImage.sprite = Level1Hold;
					} else if (p >= .66f) {
						holdImage.sprite = Level2Hold;
					}
				}
			}
		}
		public void Reset() {
			holdImage.sprite = Level0Hold;
			starting = false;
			inMain = true;
			escPanel.SetActive (false);
		}
		void StartGame() {
			//TODO Game.Controller empezar
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