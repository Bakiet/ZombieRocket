using UnityEngine;
using System.Collections;

public class BlockBehavior : MonoBehaviour {

	public bool canBeBroken;
	public int startY;
	public AudioClip soundClip;
	public Material usedMaterial;
	public Material wrongMaterial;
	public bool isInitial;

	int currentPos = 0;



	// Use this for initialization
	void Start () {
		//transform.position = new Vector3(transform.position.x, currentPos + startY, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp( transform.position, new Vector3(transform.position.x, currentPos + startY, transform.position.z), 0.3f);

		if (startY + currentPos > 0)
		{
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}else
		{
			gameObject.GetComponent<BoxCollider>().enabled = true;
		}
	}

	void OnMouseDown()
	{
		if (startY + currentPos > -1 && (GameObject.Find("BlockCreator").GetComponent<BlockManager>().started || isInitial))
		{
			GameObject.Find("AudioPlayer").GetComponent<AudioSource>().clip = soundClip;
			GameObject.Find("AudioPlayer").GetComponent<AudioSource>().Play ();

			if (canBeBroken)
			{
				if (currentPos + startY <= 0 && !GameObject.Find("BlockCreator").GetComponent<BlockManager>().isGameOver)
				{
					GameObject.Find("BlockCreator").BroadcastMessage("MoveDown");
					GameObject.Find("BlockCreator").GetComponent<BlockManager>().OnBlockBreak();
				}
			}else
			{
				GameObject.Find("BlockCreator").GetComponent<BlockManager>().OnGameOver();
				transform.FindChild("Graphics").transform.FindChild("BlockBase_00").GetComponent<Renderer>().material = wrongMaterial;
			}
		}
	}

	void MoveDown()
	{
		currentPos--;

		if (startY + currentPos == -1)
		{
			transform.FindChild("Graphics").transform.FindChild("BlockBase_00").GetComponent<Renderer>().material = usedMaterial;
		}

		if (startY + currentPos <= -2)
		{
			GameObject.Destroy (gameObject);
		}
	}
}
