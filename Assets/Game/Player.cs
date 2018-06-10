using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : IForce, ILevelUp {

        int currentLevel = 0;

		public Animator animator;

        Vector3 force = Vector3.zero;
		public float speed;
		// Use this for initialization
		void Start () {
			if (body == null) body = GetComponent<Rigidbody> ();
		}
		
		// Update is called once per frame
		void Update () {
			Vector3 dir = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
			body.AddForce(force = (speed * dir));
            if (Mathf.Abs(force.magnitude) > 0)
            {
                animator.Play("walk_" + currentLevel);
            }
            else
            {
                animator.Play("idle_" + currentLevel);
            }
        }

        public void LevelUp(int to) {
            currentLevel = to;
        }

        override public Vector3 getForce() {
            return force;
        }

		void OnDestroy() {
			Controller.instance.PlayerLost ();
		}
	}
}