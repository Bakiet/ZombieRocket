
using UnityEngine;
#if USE_ADS
using UnityEngine.Advertisements;
#endif
using System.Collections;

public class UnityAdsShow : MonoBehaviour {

    public string gameID = "77558";

    public void Start()
    {
        #if USE_ADS
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID); 
        }
        else
        {
            Debug.Log("Platform not supported");
        }
        #endif
    }

    public void OnShowAds()
    {
        #if USE_ADS
        Advertisement.Show(null, new ShowOptions
        {
            resultCallback = result =>
            {
                ShipController shipControl = GameObject.FindObjectOfType<ShipController>();
                shipControl.stateMachine.ProcessTriggerEvent("CONTINUE");
            }
        });
        #endif
    }

    public void OnCancelAds()
    {
        //#if USE_ADS
        ShipController shipControl = GameObject.FindObjectOfType<ShipController>();
        shipControl.stateMachine.ProcessTriggerEvent("NO_ADS");
        //#endif
    }
}
