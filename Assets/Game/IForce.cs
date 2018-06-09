using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IForce : MonoBehaviour {

	public Rigidbody body;

    public virtual Vector3 getForce()
    {
        return Vector3.zero;
    }

	public virtual void Push(float force, Vector3 dir) {
		body.AddForce (dir * force, ForceMode.Impulse);
	}
}
