using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour {

	public int currentPoints;
	public int pointsPerBlock;

	public List<GameObject> wrongBlocks;
	public List<GameObject> rightBlocks;

	public float currentTime;

	public bool isGameOver;
	public bool started;

	public GameObject adsPrefab;
	public GameObject adsPrefabBanner;

	public List<Color> colors;
	int currentColor;
	GameObject ads;


	// Use this for initialization
	void Start () {
		currentTime = 61;
		isGameOver = false;
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!isGameOver)
		{
			if (started)
				currentTime -= Time.deltaTime;

			if (currentTime<= 0)
			{
				Debug.Log("GameOver");
				OnGameOver();

			}else
			{


				int secs = (int)(currentTime % 60);
				int mins = (int)(currentTime / 60);
				
				GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "TimeLabel", string.Format("{0:00}:{01:00}", mins, secs));

				//GameObject.Find("Time").GetComponent<UILabel>().text = string.Format("{0:00}:{01:00}", mins, secs);
			}

			Camera.main.backgroundColor = Color.Lerp( Camera.main.backgroundColor, colors[currentColor], 0.01f);

		}

	}

	public void OnBlockBreak()
	{
		if (currentPoints % 50 == 1)
		{
			currentColor++;

			currentColor %= colors.Count;
		}

		if (isGameOver)
			return;

		if (currentPoints == 0)
		{
			GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "CheckShowTutorial");
			//GameObject.Find("Tuto01").GetComponent<TweenPosition>().PlayForward();
			started = true;
		}

		currentPoints+= pointsPerBlock;

		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "CurrentPoints", currentPoints);
		//GameObject.Find("Score").GetComponent<UILabel>().text = currentPoints + "";

		int rightPos = Random.Range(0, 3);

		for (int i = 0; i < 3; i++)
		{
			Vector3 spawnPos = new Vector3( 2 * (i -1 ), 16, 0);
			GameObject obj;
			if (i == rightPos)
			{
				obj = GameObject.Instantiate( rightBlocks[ Random.Range(0, rightBlocks.Count)], spawnPos, Quaternion.identity) as GameObject;
				obj.GetComponent<BlockBehavior>().canBeBroken = true;
			}else
			{
				obj = GameObject.Instantiate( wrongBlocks[ Random.Range(0, wrongBlocks.Count)], spawnPos, Quaternion.identity) as GameObject;
				obj.GetComponent<BlockBehavior>().canBeBroken = false;
			}
			obj.GetComponent<BlockBehavior>().startY = 5;
			obj.transform.parent = transform;
		}
	}

	public void OnGameOver()
	{
		isGameOver = true;
		started = false;
		
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "TotalScore", currentPoints);
		//GameObject.Find("TotalScore").GetComponent<UILabel>().text = "" + currentPoints;
		GameObject.Find("TwitterShare").GetComponent<ShareFacebook>().score = currentPoints;

		Score score = GameSerializer.Instance.GetElement<Score>("lastScore");

		Score myNewScore = new Score();
		myNewScore.amount = currentPoints;

		if (score != null)
		{
			if ( score.amount >= myNewScore.amount )
			{
				myNewScore = score;
			}
		}

		if (Random.Range(0, 10) == 1)
		{
			ads = GameObject.Instantiate(adsPrefab) as GameObject;  
			GameObject.Destroy(ads,600);

		}else
		{
			ads = GameObject.Instantiate(adsPrefabBanner) as GameObject;  
			GameObject.Destroy(ads,600);
		}

		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowBestScore");

		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "UpdateBestScore", (int)myNewScore.amount);

		GameSerializer.Instance.SaveElement<Score>( myNewScore, "lastScore");

		//GameObject.Find("ScoreBoard").GetComponent<TweenPosition>().PlayForward();
	}
}
