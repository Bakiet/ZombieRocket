using UnityEngine;
using System.Collections;

public class IAPStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if USE_IAP
		RBIAPManager.init();
		GameObject.Find ("Database").GetComponent<ProductDatabase>().GetItem(14).amount = RBIAPManager.tier1Amount;
		GameObject.Find ("Database").GetComponent<ProductDatabase>().GetItem(15).amount = RBIAPManager.tier2Amount;
		GameObject.Find ("Database").GetComponent<ProductDatabase>().GetItem(16).amount = RBIAPManager.tier3Amount;
		GameObject.Find ("Database").GetComponent<ProductDatabase>().GetItem(17).amount = RBIAPManager.tier4Amount;
		GameObject.Find ("Database").GetComponent<ProductDatabase>().GetItem(18).amount = RBIAPManager.tier5Amount;
		GameObject.Find ("Database").GetComponent<ProductDatabase>().GetItem(19).amount = RBIAPManager.tier6Amount;
#endif
	
	}

}
