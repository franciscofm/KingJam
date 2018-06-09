using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class Enemy : MonoBehaviour {
		public Rigidbody body;
		public float speed;

		Transform target;

		void Awake() {
			if (body != null) body = GetComponent<Rigidbody> ();
		}
		// Use this for initialization
		void Start () {
			target = Controller.instance.playT;
		}
		
		// Update is called once per frame
		void Update () {
			Vector3 dir = (target.position - transform.position);
			dir.y = 0f;
			dir.Normalize();
			body.AddForce(dir * speed);
		}
	}
}