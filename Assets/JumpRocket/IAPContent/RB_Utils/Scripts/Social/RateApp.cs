using UnityEngine;
using System.Collections;

/// <summary>
/// Used to rate the app, just call the method GoToMyApp.
/// </summary>
public class RateApp : MonoBehaviour {

    public string storeLinkURL = "market://details?id=com.rainbirth.rocketphew";
	
	public void Rate()
	{
		GoToMyApp(true);
	}

    /// <summary>
    /// Call this method to open the store rate page.
    /// </summary>
    /// <param name="googlePlay"></param>
	public void GoToMyApp(bool googlePlay) {//true if Google Play, false if Amazone Store
		Application.OpenURL(storeLinkURL);
	}
}
