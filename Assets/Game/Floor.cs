using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, ILevelUp {

    static float maxRotation = 20f;
    static float midRotation = maxRotation + (360f - maxRotation * 2f) / 2f;

    public AnimationCurve clampCurve;
    public Rigidbody body;

    public Material level1;
    public Material level2;

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

    public void LevelUp(int to)
    {
        switch (to)
        {
            case 1:
                GetComponent<MeshRenderer>().material = level1;
                break;
            case 2:
                GetComponent<MeshRenderer>().material = level2;
                break;
        }
    }
}