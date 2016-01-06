using UnityEngine;
using System.Collections;

public class OpenLeaderboards : MonoBehaviour {

	public void Open()
	{
#if USE_IAP
		GooglePlayManager.instance.showLeaderBoardsUI();
#endif
	}
}
