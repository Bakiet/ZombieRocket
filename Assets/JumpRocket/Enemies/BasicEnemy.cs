using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {

	public float checkTime;
	public float moveTime;
	public Vector3 direction;
	public float velocity;
	public float startDistance;

	protected float ctimer;
	protected float timer;

	protected bool playerClose;

	protected int currentState;
	protected GameObject player;
	// Use this for initialization
	void Start () {
		currentState = 0;
		player = GameObject.Find("Rocket");
	}
	
	// Update is called once per frame
	void Update () {
		ctimer -= Time.deltaTime;

		if (ctimer < 0)
		{
			CheckProximity();
			ctimer = checkTime;
		}
	
		if (playerClose)
		{
			if (currentState == 0)
			{
				OnStateUp();
			}

			if (currentState == 1)
			{
				OnStateDown();
			}
		}
	}

	public void CheckProximity()
	{
		if (player && (player.transform.position - transform.position).magnitude <= startDistance)
		{
			playerClose = true;
		}else
		{
			playerClose = false;
		}
	}

	public void OnStateUp()
	{
		timer -= Time.deltaTime;
		transform.Translate( direction * velocity * Time.deltaTime);
		if (timer <= 0)
		{
			timer = moveTime;
			currentState = 1;
		}
	}

	public void OnStateDown()
	{
		timer -= Time.deltaTime;

		transform.Translate( direction * -velocity * Time.deltaTime);
		
		if (timer <= 0)
		{
			timer = moveTime;
			currentState = 0;
		}
	}
}
