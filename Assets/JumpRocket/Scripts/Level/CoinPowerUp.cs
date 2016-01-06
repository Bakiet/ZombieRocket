using UnityEngine;
using System.Collections;

public class CoinPowerUp : MonoBehaviour {

	public float totalCoins;
	public GameObject particle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 90 * Time.deltaTime, 0);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			GameObject.Find("Rocket").GetComponent<ShipController>().currentCoins += totalCoins;
			Economy.Instance.AddFM(totalCoins);
			GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "ShowEconomy");
			//GameObject.Find("EconomyTab").GetComponents<TweenPosition>()[0].PlayForward();
			GameObject part = GameObject.Instantiate(particle, transform.position,transform.rotation) as GameObject;
			GameObject.Destroy(part, 1);
			GameObject.Destroy(gameObject);
		}
	}
}
