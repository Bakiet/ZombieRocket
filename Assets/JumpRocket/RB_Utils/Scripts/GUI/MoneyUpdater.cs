using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUpdater : MonoBehaviour {

	Text theLabel;

	void Start () {		
		theLabel = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		theLabel.text = "" + Mathf.Round(Economy.Instance.GetFM());

	}
}
