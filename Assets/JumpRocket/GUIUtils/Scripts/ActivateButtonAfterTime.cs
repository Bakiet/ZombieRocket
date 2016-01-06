using UnityEngine;
using System.Collections;

public class ActivateButtonAfterTime : MonoBehaviour {

	public Vector3 initialScale;
	public Vector3 finalScale;

	public float timeToActivate;

	float timer;

	bool started;

	// Use this for initialization
	void Start () {
		GetComponent<Collider>().enabled = false;
		started = false;
		timer = timeToActivate;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (started)
		{
			timer -= Time.deltaTime;
			
			if (timer <= 0)
			{
				GetComponent<Collider>().enabled = true;
				this.enabled = false;
			}
			
			transform.localScale = Vector3.Lerp( initialScale, finalScale, Mathf.Min(1f, (1f - (timer / timeToActivate))));
		}
	}

	public void StartTransition()
	{
		started = true;
	}
}
