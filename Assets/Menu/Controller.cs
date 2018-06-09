using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
	public class Controller : MonoBehaviour {

		public void ShowMenu() {
			float rot = transform.localRotation.eulerAngles.z;
			rot = Mathf.Clamp (rot, -20, 20);
			//transform.rotation.eulerAngles = new Vector3(0f,0f,rot);
		}

		public void StartGame() {

		}

		public void ShowSettings() {

		}
	}
}