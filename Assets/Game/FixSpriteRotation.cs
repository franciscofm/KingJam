using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSpriteRotation : MonoBehaviour {

    public static Transform floor;
    public float offset;
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);

        // Offset sprite vs suelo.
        var position = transform.parent.position;
        position += Vector3.up * offset;
        transform.position = position;
    }
}
