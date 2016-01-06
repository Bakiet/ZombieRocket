using UnityEngine;
using System.Collections;

public class LogoAppears : MonoBehaviour {

	Color color;
	bool finished;

	public string loadScene;
	public float timeToLoad;

	float timer = float.MaxValue;
	// Use this for initialization
	void Start () {

		iTween.PunchScale(gameObject, iTween.Hash( "x", 0.4f, "y", 0.1f,"time", 1, "delay", 1, "oncomplete", "PlaySound" ));

	}

	void PlaySound()
	{

	}

	// Update is called once per frame
	void Update () {
		if (!finished)
		{
			color = gameObject.GetComponent<SpriteRenderer>().color;

			color.a += 0.5f * Time.deltaTime; 

			color.a = Mathf.Min(1, color.a);

			gameObject.GetComponent<SpriteRenderer>().color = color;

			if (color.a >= 1 && !finished)
			{
				gameObject.GetComponent<AudioSource>().Play();
				finished = true;
				timer = timeToLoad;
			}
		}

		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			//GooglePlayConnection.instance.connect ();
			Application.LoadLevel( loadScene );
			timer = 1000000;
		}


	}
}
