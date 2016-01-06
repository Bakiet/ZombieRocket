using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;


public class ShipController : MonoBehaviour {

    /// <summary>
    /// The force to apply to the rocket when the screen is tapped.
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// The gravity force applied to the rocket.
    /// </summary>
	public float gravityForce;

    /// <summary>
    /// The max speed the rocket can reach.
    /// </summary>
    public float maxVelocity;

    /// <summary>
    /// The explossion object to spawn when the ship dies.
    /// </summary>
    public GameObject explosion;

	/// <summary>
	/// Tells if we use Attrackt control or Push control.
	/// </summary>
	public bool attractControl;

    /// <summary>
    /// If the game has started.
    /// </summary>
    public bool started;

    public bool isTiltController;

    /// <summary>
    /// The height that will trigger the win condition.
    /// </summary>
    public int heightToWin = 1000;

    /// <summary>
    /// The total gas the ship can store.
    /// </summary>
    ///
    [HideInInspector]
	public float totalGas;
    [HideInInspector]
	public float currentGas;
    [HideInInspector]
	public float currentCoins;
    [HideInInspector]
	public float currentHeight;
    [HideInInspector]
    public int astronauts;


	public AudioClip thrustle;
	public AudioClip close;
	

	float attractDistance;
	bool destroyed;
	float timer;
	GameObject ads;
    [HideInInspector]
	public StateMachine stateMachine;
	int lastColorChange;
	float currentJumpforce;

	bool canPlay;
	
	Quaternion leftRot;
	Quaternion rightRot;

	AudioSource audioSource;

	int thrust = 0;
	int gas = 0;
	int magnet = 0;

    Vector3 collisionPoint;


	// Use this for initialization
	void Start () {

		started = false;
		audioSource = gameObject.GetComponent<AudioSource>();


		stateMachine = new StateMachine();

		stateMachine.CreateState("WAITING", OnWaiting);
		stateMachine.CreateState("PLAYING", OnPlaying);
        stateMachine.CreateState("ADS_SHOW", OnAds);
		stateMachine.CreateState("GAME_OVER_GAS", OnGameOverGas);
		stateMachine.CreateState("GAME_OVER_KILL", OnGameOverKill);
		stateMachine.CreateState("WINNING", OnGameWin);

		stateMachine.CreateTransition("WAITING", "PLAYING", "TAP", null, OnPlayingIn);
        stateMachine.CreateTransition("PLAYING", "ADS_SHOW", "COLLISION", null, OnAdsCollisionIn);
        stateMachine.CreateTransition("PLAYING", "ADS_SHOW", "NO_GAS", null, OnAdsNoGasIn);
        stateMachine.CreateTransition("ADS_SHOW", "PLAYING", "CONTINUE", OnPlayingInFromAds);
        stateMachine.CreateTransition("ADS_SHOW", "GAME_OVER_KILL", "NO_ADS", OnNoAds);     
		stateMachine.CreateTransition("PLAYING", "WINNING", "WIN", null, OnGameWinIn);

		stateMachine.SetCurrentState("WAITING");

		rightRot = Quaternion.Euler(0, 30, 0);
		leftRot = Quaternion.Euler(0, -30, 0);

		currentJumpforce = jumpForce;

		thrust = 0;
		gas = 0;
		magnet = 0;

		currentGas = totalGas;

	}

	
	// Update is called once per frame
	void Update () {

		stateMachine.Update();

	}

	public void CanPlay()
	{
		canPlay = true;
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("HideTapAnywhere");
	}
	
	void CheckControl()
	{
		if (!canPlay)
		{
			return;
		}

		if (isTiltController)
		{
			OnTiltControl();
		}else
		{
			if (Input.touchCount >= 1)
			{
				if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
				{
					// starting touch.
					OnTap(Input.GetTouch(0).position);
				}
				
				if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					//moving touch.
				}
				
				if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) 
				{
					//multitouch				
				}   
			}else
			{
				if (Input.GetMouseButtonDown(0))
				{
					//mouse.
					OnTap (Input.mousePosition);
					
				}
				
				if (Input.GetMouseButton(0))
				{
					//mouse.
					//OnTap (Input.mousePosition);
				}
			}
		}
	}
    

	void OnCollisionEnter( Collision collision)
	{
		if ( collision.gameObject.tag != "PowerUp")
		{            
            stateMachine.ProcessTriggerEvent("COLLISION");            
		}
	}

	public void AddGas( float amount)
	{
		currentGas = Mathf.Min( totalGas, currentGas + amount);
		#if UNITY_EDITOR
		//currentGas = 100000;
		#endif
	}

	public void SimulateGravity()
	{
		GetComponent<Rigidbody>().AddForce(Vector3.down * gravityForce);
	}

	public void LimitSpeed()
	{
		if (GetComponent<Rigidbody>().velocity.magnitude >= maxVelocity)
		{
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxVelocity;
		}
	}

	public void CalculateHeight()
	{
		currentHeight = Mathf.Max(currentHeight, transform.position.y / 5);

		int index = (int)(transform.position.y);

		if (index % 10 == 0)
		{
			GameObject.Find("ObstacleCreator").GetComponent<ObstacleCreator>().SpawnBlock(index + 10); 
		}
		
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "UpdateHUD" );
		
	}

	void OnTiltControl()
	{
		/**
		Vector3 dir = -Input.acceleration.y * 0.5f;
		Vector3 vel = rigidbody.velocity;

		vel *= 0.4f;
		if (dir.magnitude > 0.2f && dir.magnitude <= 1)
		{
			vel += dir * maxVelocity;
		}

		rigidbody.velocity = vel;
		*/
	}

	public void OnTap( Vector3 position )
	{
		if (currentGas > 0)
		{
			stateMachine.ProcessTriggerEvent("TAP");

			Ray ray = Camera.main.ScreenPointToRay( position );

			Vector3 worldPos = ray.GetPoint( -Camera.main.transform.position.z );


			Vector3 vel = GetComponent<Rigidbody>().velocity;
		

			if (thrust == 1)
			{
				vel.y = 0;
				vel.x = vel.x * 0.4f;
				currentJumpforce = jumpForce * 2f;
				GetComponent<Rigidbody>().drag = 0.75f;
			}else
			{
				if (thrust == 2)
				{
					vel.y = vel.y * 0.3f;
					currentJumpforce = jumpForce * 1.5f;
					GetComponent<Rigidbody>().drag = 0.1f;

				}else
				{
					if (thrust == 3)
					{
						vel.y = vel.y * 0.6f;
						currentJumpforce = jumpForce * 1.1f;
						GetComponent<Rigidbody>().drag = 0.05f;
					}
				}
			}

			GetComponent<Rigidbody>().velocity = vel;
			if (attractControl)
			{
				GetComponent<Rigidbody>().AddForce(-(transform.position - worldPos).normalized * currentJumpforce);
			}else
			{
				GetComponent<Rigidbody>().AddForce( (transform.position - worldPos).normalized * currentJumpforce);
			}
			currentGas -= 5f;

			audioSource.clip = thrustle;
			audioSource.Play();
		}
	}

	void SaveScore()
	{
	
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "TotalScoreGameOver", string.Format("{0:###0.00}", currentHeight) + "m");
		//GameObject.Find("TotalScoreGameOver").GetComponent<UILabel>().text = string.Format("{0:###0.00}", currentHeight) + "ml";
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "TotalScoreWin", string.Format("{0:###0.00}", currentHeight) + "m");
		//GameObject.Find("TotalScoreWin").GetComponent<UILabel>().text = string.Format("{0:###0.00}", currentHeight) + "ml";

		Debug.Log("Saving Score");

		Score score = GameSerializer.Instance.GetElement<Score>("lastScore");
		Score astr = GameSerializer.Instance.GetElement<Score>("astronauts");

		Score myNewScore = new Score();
		Score newAstr = new Score();
		myNewScore.amount = currentHeight;
		newAstr.amount = astronauts;

		if (score != null)
		{
			if ( score.amount >= myNewScore.amount )
			{
				myNewScore = score;
			}else
			{
				// Record!!.

			}
		}


		if (astr != null)
		{
			astr.amount += newAstr.amount;
			newAstr = astr;
		}

		GameSerializer.Instance.SaveElement<Score>(newAstr, "astronauts");


		//GameObject.Find("GameOver").GetComponent<TweenPosition>().PlayForward();
		
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("SetGasPercent", (currentGas / totalGas));
		
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "BestScore", string.Format("{0:###0.00}", myNewScore.amount) + "m");
		//GameObject.Find("BestScoreGameOver").GetComponent<UILabel>().text = string.Format("{0:###0.00}", myNewScore.amount) + "ml";
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage( "BestScoreWin", string.Format("{0:###0.00}", myNewScore.amount) + "m");
		//GameObject.Find("BestScoreWin").GetComponent<UILabel>().text = string.Format("{0:###0.00}", myNewScore.amount) + "ml";
		
		GameSerializer.Instance.SaveElement<Score>( myNewScore, "lastScore");
		
		Economy.Instance.AddFM(currentCoins);

	}

	void AttractItems()
	{

		if (attractDistance > 0)
		{
			Collider [] hits = Physics.OverlapSphere(transform.position, attractDistance);

			foreach( Collider hit in hits)
			{

				if (hit.gameObject.tag == "PowerUp")
				{
					GameObject pup = hit.gameObject;
					pup.transform.position = Vector3.Lerp( pup.transform.position, transform.position, 0.05f);
				}
			}
		}
	}

	void Consume()
	{
		
		if (PlayerEquip.Instance.IsEquipped("THRUST_1"))
		{
			thrust = 1;
		}else
		{
			if (PlayerEquip.Instance.IsEquipped("THRUST_2"))
			{
				thrust = 2;
			}else
			{
				if (PlayerEquip.Instance.IsEquipped("THRUST_3"))
				{
					thrust = 3;
				}
			}

		}


		if (PlayerEquip.Instance.IsEquipped("GAS_1"))
		{
			gas = 1;
			totalGas += 100;
		}else
		{
			if (PlayerEquip.Instance.IsEquipped("GAS_2"))
			{
				gas = 2;
				totalGas += 150;
			}else
			{
				if (PlayerEquip.Instance.IsEquipped("GAS_3"))
				{
					gas = 3;
					totalGas += 200;
				}
			}
			
		}

		if (PlayerEquip.Instance.IsEquipped("MAGNET_1"))
		{
			magnet = 1;
			attractDistance = 1f;
		}else
		{
			if (PlayerEquip.Instance.IsEquipped("MAGNET_2"))
			{
				magnet = 2;
				attractDistance = 2f;
			}else
			{
				if (PlayerEquip.Instance.IsEquipped("MAGNET_3"))
				{
					magnet = 3;
					attractDistance = 3f;
				}
			}			
		}
		
		currentGas = totalGas;
	}

	void OnWaiting()
	{
		CheckControl();
	}

	void OnPlayingIn()
	{
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowTutorial");		
		//GameObject.Find("Tuto01").GetComponent<TweenAlpha>().PlayForward();
		timer = 5;
		GameObject part = GameObject.Instantiate(explosion, transform.position + (Vector3.down * 0.3f), transform.rotation) as GameObject;
		GameObject.Destroy(part, 5);
		Camera.main.GetComponent<AudioSource>().Play();

		Consume();

	}

    void OnPlayingInFromAds()
    {
        ObstacleCreator obstacles = GameObject.FindObjectOfType<ObstacleCreator>();
        obstacles.DestroyObstaclesAt( transform.position);
        GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("HideAds");
        currentGas = totalGas;
    }
	
	void OnPlaying()
	{
		CheckControl();
		
		SimulateGravity();
		LimitSpeed();
		CalculateHeight();

		AttractItems();
			
		if (currentGas <= 0)
		{
			timer -= Time.deltaTime;
			
			if ( timer <= 0)
			{
				stateMachine.ProcessTriggerEvent("NO_GAS");
			}
		}

		//GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("SetGasPercent", (currentGas / totalGas));
		//GameObject.Find("GasBar").GetComponent<UISlider>().value = (currentGas / totalGas);

		if ((((int)currentHeight) % 10) == 0 && lastColorChange != (((int)currentHeight)))
		{
			lastColorChange = (((int)currentHeight));
			Camera.main.GetComponent<BackgroundColors>().NextColor();
		}

		if (currentHeight >= heightToWin)
		{
            currentHeight = heightToWin;
			stateMachine.ProcessTriggerEvent("WIN");
		}
		if (!destroyed)
		{
			transform.FindChild("Graphics").transform.rotation = Quaternion.Lerp( rightRot, leftRot, (GetComponent<Rigidbody>().velocity.x * 0.15f) + 0.5f );
		}

	}

    void OnAdsCollisionIn()
    {
        GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowAds");      
    }

    void OnAdsNoGasIn()
    {
        GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowAds");       
    }

    void OnNoAds()
    {
        if (!destroyed)
        {
            GameObject part = GameObject.Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            GameObject.Destroy(part, 5);
            GameObject.Destroy(transform.FindChild("Graphics").gameObject);
            destroyed = true;
        }
        GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("HideAds");
        SaveScore();
        StartCoroutine("GameOverShow", 2);
    }

    void OnAds()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }




	void OnGameOverGasIn()
	{
		StartCoroutine("GameOverShow", 1);
		SaveScore();
	}

	void OnGameOverGas()
	{

		Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp( Camera.main.GetComponent<AudioSource>().volume, 0, 0.01f);
	}

	void OnGameWinIn()
	{
		SaveScore();
		//Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp( Camera.main.GetComponent<AudioSource>().volume, 0, 0.01f);
	}
	
	void OnGameWin()
	{
		//GameObject.Find("GameOver").GetComponent<TweenPosition>().PlayForward();
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowWinGame");
		//GameObject.Find("WinGame").GetComponent<TweenPosition>().PlayForward();
		//GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowUpgrades");
		//GameObject.Find("Upgrades").GetComponent<TweenPosition>().PlayForward();
		GameObject.Destroy(gameObject);
	}

	void OnGameOverKillIn()
	{
		SaveScore();
		timer = 2;
		Camera.main.GetComponent<FollowCamera>().target = null;
		StartCoroutine("GameOverShow", 2);
			
	}

	void OnGameOverKill()
	{
		Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp( Camera.main.GetComponent<AudioSource>().volume, 0, 0.1f);
	}

	IEnumerator GameOverShow(float time)
	{
		yield return new WaitForSeconds (time);
		GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowGameOver");
		//GameObject.Find("GameOver").GetComponent<TweenPosition>().PlayForward();
		yield return new WaitForSeconds(0.25f);
		//GameObject.Find("GUIManager").GetComponent<GUIManager>().SendMessage("ShowUpgrades");
		//GameObject.Find("Upgrades").GetComponent<TweenPosition>().PlayForward();
		//GameObject.Destroy(gameObject);
	}
}
