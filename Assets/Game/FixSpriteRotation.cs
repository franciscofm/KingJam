using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSpriteRotation : MonoBehaviour {

    public static Transform floor;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = floor.rotation;
	}
}
