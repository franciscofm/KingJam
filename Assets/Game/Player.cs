using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : MonoBehaviour {

		public Rigidbody body;
		public float speed;
		// Use this for initialization
		void Start () {
			if (body == null) body = GetComponent<Rigidbody> ();
		}
		
		// Update is called once per frame
		void Update () {
			Vector3 dir = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
			Debug.Log (dir);
			body.AddForce (speed * dir);
		}
	}
}