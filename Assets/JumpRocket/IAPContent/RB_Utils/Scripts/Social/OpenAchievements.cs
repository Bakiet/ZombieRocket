using UnityEngine;
using System.Collections;

public class OpenAchievements : MonoBehaviour {

	public void Open()
	{
#if USE_IAP
		GooglePlayManager.Instance.ShowAchievementsUI();
#endif

	}
}
