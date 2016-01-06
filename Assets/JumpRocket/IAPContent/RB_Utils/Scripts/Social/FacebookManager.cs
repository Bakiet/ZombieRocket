using UnityEngine;
using System.Collections;


public class FacebookManager : MonoBehaviour {

	
	/// <summary>
	/// The instance private.
	/// </summary>
	private static FacebookManager instance;

	bool inited;
	bool logged;

	private FacebookManager()
	{
		inited = false;
	}
	
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static FacebookManager Instance
	{
		get 
		{
			if (instance == null)
			{
				GameObject instanceObject = new GameObject("FacebookManager");
				instance = instanceObject.AddComponent<FacebookManager>();
			}
			
			return instance;
		}		
	}
	
	/// <summary>
	/// Part of the Unity3D singleton implementation in order to have a game object.
	/// this makes sure we don't duplicate game objects using it.
	/// </summary>
	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(this);
		}
		
	}

	// Use this for initialization
	public void InitFacebook () {
#if USE_IAP
		if (!inited)
		{			
			inited = true;
			SPFacebook.Instance.OnInitCompleteAction += OnInit;
			//SPFacebook.Instance.OnAuthCompleteAction += OnAuth;
			
			
			SPFacebook.Instance.OnPostingCompleteAction += OnPost;
			
			SPFacebook.Instance.Init();
		}else
		{
			if (!logged)
			{
				//SPFacebook.instance.Logout();
				//SPFacebook.instance.Login();
			}
		}

#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnInit()
	{
#if USE_IAP
		if(SPFacebook.Instance.IsLoggedIn) {	
			print ("MYGAME>>" + "User is logged in");

		} else {
			// user not authed, we need to use authenticate user first  if we want to use API
			SPFacebook.Instance.Login();
		}

#endif
	}

#if USE_IAP
    void OnAuth(FBResult result)
	{
		print ("BLOCKS>" + "User logged in");
		logged = true;
	}



    void OnAuthFailed(FBResult result)
	{
		print ("BLOCKS>" + "User logged in failed");
	}

	void OnUserDataLoaded(FBResult result)
	{

	}

	void OnUserDataLoadFailed(FBResult result)
	{
		
	}

	void OnFriendsDataLoaded(FBResult result)
	{
		
	}

	void OnFriendDataLoadFailed(FBResult result)
	{
		
	}

	void OnPostFailed(FBResult result)
	{
		
	}

	void OnPost(FBPostResult result)
	{
        if (result.IsSucceeded)
        {
            Debug.Log("Post done!");
        }
        else
        {
            Debug.Log("Post Error: " + result.Result.Error);
        }
        
	}

	void OnFocusChanged(FBResult result)
	{
		
	}
#endif
}
