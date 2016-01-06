using UnityEngine;
using System.Collections;

public class ShareFacebook : MonoBehaviour {

	public string message;
	public Texture2D shareImage;
	public float score;
	public string appLink;

	public bool isScoreShare;

	string messageFormated;

	void Start()
	{
		
	}

	public void Share()
	{
#if USE_IAP
		FacebookManager.Instance.InitFacebook();

		if (SPFacebook.Instance.IsLoggedIn)
		{
			if (isScoreShare )
			{
				string formatted = string.Format("{0:###0.00}", score) ;
				string messageFormated = string.Format(message, formatted);

				RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
				Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
				
				//Camera.main.targetTexture = rt;
				//Camera.main.Render();
				
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

				SPFacebook.Instance.PostWithAuthCheck (
					link: appLink,
					linkName: "Rocket Phew Sidereal Guardians",
					linkCaption: "How far will you get?",
					linkDescription: messageFormated,
					picture: "http://yourweb.picture.png"
					);
				
				
				//SPFacebook.instance.SubmitScore((int)score);
				//StartCoroutine(PostScreenshot());
			}
		}

#endif
	}

	private IEnumerator PostScreenshot() {
		
		
		yield return new WaitForEndOfFrame();
#if USE_IAP
        // Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
		
		AndroidSocialGate.StartShareIntent("Rocket Phew Sidereal Guardians", messageFormated + "\n" + appLink, tex);
		
		Destroy(tex);
#endif
    }

}