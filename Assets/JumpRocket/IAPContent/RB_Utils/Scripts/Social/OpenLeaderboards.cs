using UnityEngine;
using System.Collections;

public class OpenLeaderboards : MonoBehaviour {

	public void Open()
	{
#if USE_IAP
		GooglePlayManager.Instance.ShowLeaderBoardsUI();
#endif
	}
}
