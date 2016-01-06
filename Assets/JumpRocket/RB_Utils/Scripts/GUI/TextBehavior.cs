using UnityEngine;
using System.Collections;

public class TextBehavior : MonoBehaviour {

	public int points;
	public float upVelocity;

	// Use this for initialization
	void Start () {

		gameObject.GetComponent<TextMesh>().text = "" + points;

		float punchScale = Mathf.Max(2, points / 80);

		iTween.PunchScale(gameObject, iTween.Hash("x", punchScale, "y", punchScale, "z", punchScale, "time", 2));

		GameObject score = GameObject.Find("TotalScore");

		score.transform.localScale = Vector3.one * punchScale;

		iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "includechildren", true, "time",2f, "oncomplete", "EndText"));
	}

	void EndText()
	{	
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate(0, upVelocity, 0);

	}
}
