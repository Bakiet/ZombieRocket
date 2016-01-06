using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreenDisplay : MonoBehaviour {

	float targetAlpha;
	CanvasGroup cGroup;
	
	void Start()
	{
		cGroup = gameObject.GetComponent<CanvasGroup>();
		targetAlpha = cGroup.alpha;
	}

	void Update()
	{
		cGroup.alpha = Mathf.Lerp(cGroup.alpha, targetAlpha, 0.1f);
	
		if (cGroup.alpha <= 0.01f)
		{
			cGroup.alpha = 0;
		}
		
		if (cGroup.alpha >= 0.99f)
		{
			cGroup.alpha = 1;
		}
	
	}
	
	public void Show()
	{
		targetAlpha = 1;
		Activate();
	}
	
	public void Hide()
	{
		targetAlpha = 0;	
		Disable();
	}
	
	public void Activate()
	{	
		cGroup.blocksRaycasts = true;
		cGroup.interactable = true;
	}
	
	public void Disable()
	{
		cGroup.blocksRaycasts = false;
		cGroup.interactable = false;	
	}
}
