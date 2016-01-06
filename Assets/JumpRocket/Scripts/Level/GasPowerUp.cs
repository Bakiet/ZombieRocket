using UnityEngine;
using System.Collections;

public class GasPowerUp : MonoBehaviour {

	public float totalGas;
	public GameObject particle;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 90 * Time.deltaTime, 45* Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			GameObject.Find("Rocket").GetComponent<ShipController>().AddGas(totalGas);
			GameObject part = GameObject.Instantiate(particle, transform.position,transform.rotation) as GameObject;
			GameObject.Destroy(part, 1);
			GameObject.Destroy(gameObject);
		}
	}
}
