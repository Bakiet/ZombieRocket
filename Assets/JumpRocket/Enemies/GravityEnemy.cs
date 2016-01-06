using UnityEngine;
using System.Collections;

public class GravityEnemy : BasicEnemy {

	public float forceDirection;
	public float attractForce;
	public float attractRange;
	
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
			OnEffect();

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

	public void OnEffect()
	{

		if (player)
		{
			Vector3 dir = (transform.position - player.transform.position);		
			if (dir.magnitude <= attractRange)
			{
				player.GetComponent<Rigidbody>().AddForce(dir.normalized * attractForce * Time.deltaTime * forceDirection);
			}
		}
	}
}
