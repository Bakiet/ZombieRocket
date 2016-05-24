using UnityEngine;
using System.Collections;
#if USE_IAP
#endif
public class RBIAPManager : MonoBehaviour {

	private static bool _isInited = false;
	
	//--------------------------------------
	//  INITIALIZE
	//--------------------------------------
	
	
	//replace with your consumable item
	public const string TIER1 = "com.bktgames.item1";
	public const string TIER2 = "com.bktgames.item2";
	public const string TIER3 = "com.bktgames.item3";
	public const string TIER4 = "com.bktgames.item4";
	public const string TIER5 = "com.bktgames.item5";
	public const string TIER6 = "com.bktgames.item6";
	public const string MAGNET_3 = "com.bktgames.magnet";
	public const string HEADSTART_2 = "com.bktgames.headstart2";
	public const string HEADSTART_3 = "com.bktgames.headstart3";
	public const string NOADS = "android.iap.code.noads";

	//these are the amounts we want to give the player.
	public static int baseAmount = 1000;
	public static int tier1Amount = baseAmount * 2; // 2
	public static int tier2Amount = baseAmount * 5 + (int)((baseAmount * 5) * 0.10f); // 5
	public static int tier3Amount = baseAmount * 8 + (int)((baseAmount * 8) * 0.15f); // 8
	public static int tier4Amount = baseAmount * 16 + (int)((baseAmount * 16) * 0.20f); // 16
	public static int tier5Amount = baseAmount * 40 + (int)((baseAmount * 40) * 0.40f); // 40
	public static int tier6Amount = baseAmount * 80 + (int)((baseAmount * 80) * 0.80f); // 80
	
	private static bool ListnersAdded = false;

	#if USE_IAP
	public static void init() {


		if(ListnersAdded) {
			return;
		}
		
		//listening for purchase and consume events
		AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
		AndroidInAppPurchaseManager.ActionProductConsumed +=  OnProductConsumed;
		
		//initilaizing store
		AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
		
		//you may use loadStore function without parametr if you have filled base64EncodedPublicKey in plugin settings
		AndroidInAppPurchaseManager.Instance.LoadStore();
		
		ListnersAdded = true;

	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	
	public static void purchase(string SKU) {
		AndroidInAppPurchaseManager.Instance.Purchase (SKU);
	}
	
	public static void consume(string SKU) {
		AndroidInAppPurchaseManager.Instance.Consume (SKU);
	}
	
	//--------------------------------------
	//  GET / SET
	//--------------------------------------
	
	public static bool isInited {
		get {
			return _isInited;
		}
	}
	
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	private static void OnProcessingPurchasedProduct(GooglePurchaseTemplate purchase) {
		//some stuff for processing product purchse. Add coins, unlock track, etc

		switch(purchase.SKU) {
			case TIER1:
				consume(TIER1);
				break;
			case TIER2:
				consume(TIER2);
				break;
			case TIER3:
				consume(TIER3);
				break;
			case TIER4:
				consume(TIER4);
				break;
			case TIER5:
				consume(TIER5);
				break;
			case TIER6:
				consume(TIER6);
				break;
			case MAGNET_3:
				consume (MAGNET_3);
				break;
			case HEADSTART_2:
				consume(HEADSTART_2);
				break;
			case HEADSTART_3:
				consume(HEADSTART_3);
				break;
			case NOADS:
				consume (NOADS);
				break;
		}
	}
	
	private static void OnProcessingConsumeProduct(GooglePurchaseTemplate purchase) {
		switch(purchase.SKU) {
			case TIER1:
				Economy.Instance.AddFM( tier1Amount);
				break;
			case TIER2:
				Economy.Instance.AddFM( tier2Amount);
				break;
			case TIER3:
				Economy.Instance.AddFM( tier3Amount);
				break;
			case TIER4:
				Economy.Instance.AddFM( tier4Amount);
				break;
			case TIER5:
				Economy.Instance.AddFM( tier5Amount);
				break;
			case TIER6:
				Economy.Instance.AddFM( tier6Amount);
				break;
			case MAGNET_3:
				Inventory.Instance.AddItem("MAGNET_3", 5);
				break;
			case HEADSTART_2:
				Inventory.Instance.AddItem("HEADSTART_2", 20);
				break;
			case HEADSTART_3:
				Inventory.Instance.AddItem("HEADSTART_3", 200);
				break;
			case NOADS:
				Inventory.Instance.AddItem("NOADS", 1);
				break;
		}
	}

	private static void OnProductPurchased(BillingResult result) {
		
		
		//this flag will tell you if purchase is avaliable
		//result.isSuccess
		
		
		//infomation about purchase stored here
		//result.purchase
		
		//here is how for example you can get product SKU
		//result.purchase.SKU
		
		
		if(result.isSuccess) {
			OnProcessingPurchasedProduct (result.purchase);
		} else {
			//AndroidMessage.Create("Product Purchase Failed", result.response.ToString() + " " + result.message);
		}
		
		Debug.Log ("Purchased Responce: " + result.response.ToString() + " " + result.message);
	}
	
	
	private static void OnProductConsumed(BillingResult result)
    {				
		if(result.isSuccess) {
			OnProcessingConsumeProduct (result.purchase);
		} else {
			//AndroidMessage.Create("Product Cousume Failed", result.response.ToString() + " " + result.message);
		}
		
		Debug.Log ("Cousume Response Success: " + result.response.ToString() + " " + result.message);
	}
	
	
	private static void OnBillingConnected(BillingResult result)
    {		
		AndroidInAppPurchaseManager.ActionBillingSetupFinished -=  OnBillingConnected;
		
		if(result.isSuccess) {
			//Store connection is Successful. Next we loading product and customer purchasing details
			AndroidInAppPurchaseManager.ActionRetrieveProducsFinished +=  OnRetriveProductsFinised;
			AndroidInAppPurchaseManager.Instance.RetrieveProducDetails();

			//AndroidMessage.Create("Connection Response Success: ", result.response.ToString() + " " + result.message);	
		} else
		{
			//AndroidMessage.Create("Connection Response Error: ", result.response.ToString() + " " + result.message);	
		}
		

		Debug.Log ("Connection Responce: " + result.response.ToString() + " " + result.message);
	}
	
	private static void OnRetriveProductsFinised(BillingResult result) {
        		
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -=  OnRetriveProductsFinised;
		
		if(result.isSuccess) {
			
			UpdateStoreData();
			_isInited = true;
			
			
		} else {
			//AndroidMessage.Create("Connection Response Error ", result.response.ToString() + " " + result.message);
		}
	}

	private static void UpdateStoreData() {
		//chisking if we already own some consuamble product but forget to consume those
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(TIER1)) {
			consume(TIER1);
		}
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(TIER2)) {
			consume(TIER2);
		}
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(TIER3)) {
			consume(TIER3);
		}
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(TIER4)) {
			consume(TIER4);
		}
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(TIER5)) {
			consume(TIER5);
		}
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(TIER6)) {
			consume(TIER6);
		}

		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(HEADSTART_2)) {
			consume(HEADSTART_2);
		}
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(HEADSTART_3)) {
			consume(HEADSTART_3);
		}

		
		//Check if non-consumable rpduct was purchased, but we do not have local data for it.
		//It can heppens if game was reinstalled or download on oher device
		//This is replacment for restore purchase fnunctionality on IOS
		
		
		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(NOADS)) {
			Inventory.Instance.AddItem("NOADS", 1);
		}

		if(AndroidInAppPurchaseManager.Instance.Inventory.IsProductPurchased(MAGNET_3)) {
			Inventory.Instance.AddItem("MAGNET_3", 1);
		}
	}
	#endif
}
