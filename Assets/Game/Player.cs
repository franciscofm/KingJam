using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : IForce, ILevelUp {

		public Animator animator;
		public RuntimeAnimatorController RuntimeAnim1;
		public RuntimeAnimatorController RuntimeAnim2;

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
		}

        public void LevelUp(int to) {
			switch (to) {
			case 0:
				break;
			case 1:
				animator.runtimeAnimatorController = RuntimeAnim1;
				break;
			case 2:
			default:
				animator.runtimeAnimatorController = RuntimeAnim2;
				break;
			}
        }

        override public Vector3 getForce() {
            return force;
        }

		void OnDestroy() {
			Controller.instance.PlayerLost ();
		}
	}
}