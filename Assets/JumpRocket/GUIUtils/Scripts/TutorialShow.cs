using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialShow : MonoBehaviour {

	string tutorialState;
	public bool show;
	
	// Use this for initialization
	void Start () {

		tutorialState = PlayerPrefs.GetString("tutorialState");
        if (tutorialState == null)
		{
			tutorialState = "show";
			PlayerPrefs.SetString("tutorialState", tutorialState);
		}else
		{
			/** to force tutorial appearing uncoment these two lines below **/
			//tutorialState = "show";
			//GameSerializer.Instance.SaveElement<string>(tutorialState, "tutorialState");		
			
			if (tutorialState == "hide")
			{
                PlayerPrefs.SetString("tutorialState", tutorialState);
                //GameObject.Find("Rocket").GetComponent<ShipController>().CanPlay();		
                transform.parent.GetComponent<UIScreenDisplay>().Hide();
			}
			else
			{
				GameObject.Find("Tutorial").GetComponent<UIScreenDisplay>().Show();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DontShow()
	{
		GameObject.Find("Tutorial").GetComponent<UIScreenDisplay>().Hide();	
		tutorialState = "hide";
        PlayerPrefs.SetString("tutorialState", tutorialState);

    }
}
