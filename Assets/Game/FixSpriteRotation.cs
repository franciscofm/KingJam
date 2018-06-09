using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSpriteRotation : MonoBehaviour {

    public static Transform floor;
    public float offset;
    SpriteRenderer spriteRenderer;
    IForce parentForce;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentForce = gameObject.GetComponentInParent<IForce>();
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(Camera.main.transform);

        // Offset sprite vs suelo.
        var position = transform.parent.position;
        position += Vector3.up * offset;
        transform.position = position;

        spriteRenderer.flipX = parentForce.getForce().x > 0;
    }
}
