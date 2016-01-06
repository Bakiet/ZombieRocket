using UnityEngine;
using System.Collections;
using System.IO;

public class ShareTwitter : MonoBehaviour {

	public string message;
	public Texture2D shareImage;
	public string appLink;
	public float score;

	public bool isScoreShare;
	bool grab;

	private static int tempFileCount = 0;

	public void Start()
	{
	}

	public void Share()
	{
#if USE_IAP
		string formatted = string.Format("{0:###0.00}", score);
		string all = string.Format( message, formatted) + " " + appLink;
		Debug.Log("Sharing to twitter");
		if (isScoreShare)
		{

			RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
			Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
			
			Camera.main.targetTexture = rt;
			Camera.main.Render();
			Camera.main.targetTexture = null;
			
			//Render from all!
			foreach(Camera cam in Camera.allCameras)
			{
				cam.targetTexture = rt;
				cam.Render();
				cam.targetTexture = null;
			}
			
			RenderTexture.active = rt;            
			screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			Camera.main.targetTexture = null;
			RenderTexture.active = null; //Added to avoid errors
			Destroy(rt);
			
			shareImage = screenShot;

			AndroidTwitterManager.instance.PostWithAuthCheck(all, shareImage);
		}
		else
		{
			AndroidTwitterManager.instance.PostWithAuthCheck(all, shareImage);
			
		}

#endif
	}

	void OnPostRender() {
		if (grab) {

			grab = false;
		}
	}
}
