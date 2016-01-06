using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundColors : MonoBehaviour {

	public List<Color> backgroundColors;
	public int currentColor;
	public float timeForChange;
	float percent;
	float currentTime;
	void Start()
	{
		currentTime = 0;
	}

	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		percent = Mathf.Min( 1, currentTime / timeForChange);

		Camera.main.backgroundColor = Color.Lerp( Camera.main.backgroundColor, backgroundColors[currentColor], percent);	
	}

	public void NextColor()
	{
		currentTime = 0;
		currentColor = currentColor +1;
		currentColor %= backgroundColors.Count;
	}
}
