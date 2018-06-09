using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
	public static Vector3 RandomV3(float min, float max) {
		return new Vector3 (Random.Range (min, max), Random.Range (min, max), Random.Range (min, max));
	}
}
