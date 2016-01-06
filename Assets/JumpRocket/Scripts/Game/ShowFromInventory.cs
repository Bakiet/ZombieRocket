using UnityEngine;
using System.Collections;

public class ShowFromInventory : MonoBehaviour {

	public string itemName;

	// Use this for initialization
	void Start () 
	{

		if (PlayerEquip.Instance.IsEquipped(itemName))
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}
