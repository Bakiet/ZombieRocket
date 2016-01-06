using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject ship;
	public GameObject totalHeightHUDLabel;
	
	public GameObject gameOverPanel;
	public GameObject tapAnywherePanel;
    public GameObject adsPanel;
	
	public GameObject gasBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetGasPercent( float amount)
	{
	
	}
	
	public void UpdateHUD()
	{		
		totalHeightHUDLabel.GetComponent<Text>().text = string.Format("{0:###0.00}", ship.GetComponent<ShipController>().currentHeight) + "m";
		
		gasBar.GetComponent<UIFillBarControl>().fillAmount = ship.GetComponent<ShipController>().currentGas / ship.GetComponent<ShipController>().totalGas;
	}
	
	public void HideTapAnywhere()
	{
		tapAnywherePanel.SendMessage("Hide");
	}
	
	public void ShowGameOver()
	{
		gameOverPanel.SendMessage("Show");
	}

    public void ShowAds()
    {
        adsPanel.SendMessage("Show");
    }


    public void HideAds()
    {
        adsPanel.SendMessage("Hide");
    }

	public void HideGameOver()
	{
		gameOverPanel.SendMessage("Hide");
	}
	
	public void ShowTutorial()
	{
		//GameObject.Find("Tutorial").SendMessage("Show");
	}
	
	public void ShowEconomy()
	{
		//gameOverPanel.SendMessage("Hide");
	}
	
	public void TotalScoreGameOver( string value)
	{
		gameOverPanel.transform.FindChild("CurrentScore").GetComponent<Text>().text = value;
	}
	
	public void BestScore( string value)
	{
		gameOverPanel.transform.FindChild("BestScore").GetComponent<Text>().text = value;
	}
	
	public void TotalScoreWin( string value)
	{
		gameOverPanel.transform.FindChild("CurrentScore").GetComponent<Text>().text = value;
	}
	
	public void BestScoreWin( string value)
	{
		//gameOverPanel.SendMessage("Hide");
	}
}
