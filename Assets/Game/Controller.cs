using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Controller : MonoBehaviour {

		public Transform playT;
		public static Controller instance;

		void Awake() {
			if (instance != null) Destroy (instance.gameObject);
			instance = this;
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}