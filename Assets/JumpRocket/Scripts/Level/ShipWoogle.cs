using UnityEngine;
using System.Collections;

public class ShipWoogle : MonoBehaviour {

	public float frequency;
	public float amplitude;

	Vector3 initialPos;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = initialPos + (Mathf.Sin (Time.time * frequency) * Vector3.up) * amplitude;
	}
}
