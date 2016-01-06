using UnityEngine;
using System.Collections;

public class ButtonLoadScene : MonoBehaviour {

	public string sceneToLoad;

	public void LoadScene()
	{
		Application.LoadLevel(sceneToLoad);
	}

}
