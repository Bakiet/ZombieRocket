using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class UIFillBarControl : MonoBehaviour {

	public bool showLabel;
	public string label;
	[Range(0,1)]
	public float fillAmount;
	
	// private fields.
	private Image fillImage;
	private Text fillText;
	
	void Awake()
	{
		foreach (Image t in GetComponentsInChildren<Image>())
		{
			if (t.gameObject.name == "FillImage")
			{
				fillImage = t;
			}
		}
		foreach (Text t in GetComponentsInChildren<Text>())
		{
			if (t.gameObject.name == "FillText")
			{
				fillText = t;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fillImage)
		{
			fillImage.fillAmount = fillAmount;
		}		
		
		if (fillText)
		{
			fillText.text = label;
		}
	}
}
