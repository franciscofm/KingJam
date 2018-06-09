using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampBaseRotation : MonoBehaviour {

    static float maxRotation = 20f;
    static float midRotation = maxRotation + (360f - maxRotation * 2f) / 2f;

    public AnimationCurve clampCurve;
    public Rigidbody body;

    void Update () {
        float rotation = transform.eulerAngles.z;
        Vector3 r = new Vector3(0f, 0f, rotation);
        if (rotation > maxRotation)
        {
            if (rotation < midRotation)
            {
                r.z = maxRotation;
                body.angularVelocity = Vector3.zero;
            }
            else if (rotation < (360 - maxRotation))
            {
                r.z = 360f - maxRotation;
                body.angularVelocity = Vector3.zero;
            }
        }
        transform.eulerAngles = r;
    }
}