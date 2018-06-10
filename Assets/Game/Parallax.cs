using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour, ILevelUp {

	public enum Type { Vertical, Horizontal }
	public enum Direction { Positive, Negative }
	public Type type = Type.Horizontal;
	public Direction direction = Direction.Positive;
	public float speed;

	public Sprite level0Sprite;
	public Sprite level1Sprite;
	public Sprite level2Sprite;

	[Header("Debug")]
	public SpriteRenderer sr;
	public float distance;
	public Vector3 _speed;
	public Transform[] items;

	public Vector3 center;
	public Vector3 outside;

	public void LevelUp(int to) {
		if (to == 0) sr.sprite = level0Sprite;
		else if (to == 1) sr.sprite = level1Sprite;
		else if (to == 2) sr.sprite = level2Sprite;
	}

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		center = transform.position;
		Vector2 size =  0.9f * transform.localScale.x * GetComponent<SpriteRenderer>().size;
		distance = (type == Type.Horizontal) ? size.x : size.y;

		_speed = new Vector3 ();
		_speed.x = (type == Type.Horizontal) ? 1f : 0f;
		_speed.y = (type == Type.Horizontal) ? 0f : 1f;
		_speed *= (direction == Direction.Positive) ? 1f : -1f;

		items = new Transform[2] { transform, Instantiate (gameObject).transform };
		items [1].position -= _speed * distance;
		outside = _speed * distance;
		Destroy(items [1].GetComponent<Parallax>());
		
		_speed *= speed;
	}

	void Update() {
		for (int i = 0; i < items.Length; ++i)
			items [i].position += _speed * Time.deltaTime;
		for (int i = 0; i < items.Length; ++i) {
			if (Vector3.Distance (center, items [i].position) > distance) {
				items [i].position = items [(i + 1) % items.Length].position - outside;
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Vector2 size = GetComponent<SpriteRenderer>().size;
		Gizmos.DrawWireCube (transform.position, 0.99f * transform.localScale.x * size);
	}
}
