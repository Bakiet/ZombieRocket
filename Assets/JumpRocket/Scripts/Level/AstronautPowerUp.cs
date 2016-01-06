using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AstronautPowerUp : MonoBehaviour {

	
	public float totalCoins;
	public GameObject particle;

	public List<AudioClip> sounds;

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

			GameObject.Find("Rocket").GetComponent<ShipController>().currentCoins += totalCoins;
			GameObject.Find("Rocket").GetComponent<ShipController>().astronauts += 1;
			Economy.Instance.AddFM(totalCoins);
			GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "ShowEconomy");
			//GameObject.Find("EconomyTab").GetComponents<TweenPosition>()[0].PlayForward();
			GameObject part = GameObject.Instantiate(particle, transform.position,transform.rotation) as GameObject;
			part.GetComponent<AudioSource>().clip = sounds[Random.Range(0, sounds.Count)];
			part.GetComponent<AudioSource>().Play();
			GameObject.Destroy(part, 1);
			GameObject.Destroy(gameObject);
		}
	}
}
