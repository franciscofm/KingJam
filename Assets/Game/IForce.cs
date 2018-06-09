using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IForce : MonoBehaviour {

    public virtual Vector3 getForce()
    {
        return Vector3.zero;
    }
}
