using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : IForce, ILevelUp {

        int currentLevel = 0;

		public Animator animator;
        AudioSource corre;

        Vector3 force = Vector3.zero;
		public float speed;
		// Use this for initialization
		void Start () {
			if (body == null) body = GetComponent<Rigidbody> ();
            corre = GetComponent<AudioSource>();
		}
		
		// Update is called once per frame
		void Update () {
			Vector3 dir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical"));
			body.AddForce(force = (speed * dir));
            if (Mathf.Abs(force.magnitude) > 0)
            {
                animator.Play("walk_" + currentLevel);
                //corre.Play();
            }
            else
            {
                animator.Play("idle_" + currentLevel);
                corre.Stop();
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