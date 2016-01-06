using UnityEngine;
using System.Collections;

public class ShipUpdater : MonoBehaviour {

	public GameObject shipPrefab;
	public GameObject changeParticles;
	public GameObject finalParticles;

	StateMachine stateMachine;

	GameObject currentShip;
	float timer;

	void Start()
	{
		stateMachine = new StateMachine();

		stateMachine.CreateState("WAITING", OnWaiting);
		stateMachine.CreateState("CHANGING", OnChanging);

		stateMachine.CreateTransition("WAITING", "CHANGING", "CHANGE", null, OnChangingIn);
		stateMachine.CreateTransition("WAITING", "CHANGING", "EQUIP", null, OnEquipIn );
		stateMachine.CreateTransition("CHANGING", "WAITING", "TIME", null, OnWaitingIn);
	
		stateMachine.SetCurrentState("WAITING");
		currentShip = GameObject.Find("RocketShop");
	}

	void Update()
	{
		stateMachine.Update();
	}


	void OnWaitingIn()
	{
		GameObject.Destroy(currentShip);
		currentShip = GameObject.Instantiate( shipPrefab ) as GameObject;
		GameObject finPart = GameObject.Instantiate( finalParticles ) as GameObject;
		GameObject.Destroy(finPart, 3);
	}

	void OnWaiting()
	{

	}

	void OnChangingIn()
	{
		timer = 2;
		GameObject part = GameObject.Instantiate(changeParticles) as GameObject;
		GameObject.Destroy(part, timer);
	}

	void OnEquipIn()
	{
		timer = 0.5f;
		GameObject part = GameObject.Instantiate(changeParticles) as GameObject;
		GameObject.Destroy(part, timer);
	}

	void OnChanging()
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0){
			stateMachine.ProcessTriggerEvent("TIME");
		}
	}

	public void Change()
	{
		stateMachine.ProcessTriggerEvent("CHANGE");
	}

	public void Equip()
	{
		stateMachine.ProcessTriggerEvent("EQUIP");
	}
}
