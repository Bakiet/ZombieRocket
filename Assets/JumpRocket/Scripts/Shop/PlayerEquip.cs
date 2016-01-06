using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEquip : MonoBehaviour {

	/// <summary>
	/// The instance private.
	/// </summary>
	private static PlayerEquip instance;
	
	public List<string> data;
	
	bool inited;
	
	private PlayerEquip()
	{
		inited = false;
		
	}
	
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static PlayerEquip Instance
	{
		get 
		{
			if (instance == null)
			{
				GameObject instanceObject = new GameObject("PlayerEquip");
				instance = instanceObject.AddComponent<PlayerEquip>();
				instance.Load();
				
			}
			
			return instance;
		}		
	}
	
	/// <summary>
	/// Part of the Unity3D singleton implementation in order to have a game object.
	/// this makes sure we don't duplicate game objects using it.
	/// </summary>
	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(this);
		}
		
	}
	
	private void Load()
	{
		#if UNITY_EDITOR
		
		#else
		data = GameSerializer.Instance.GetElement<List<string>>("equip_data");
		#endif
		
		if (data == null)
		{
			data = new List<string>();
			Save ();
		}
	}
	
	/// <summary>
	/// Save this data.
	/// </summary>
	private void Save()
	{
		GameSerializer.Instance.SaveElement<List<string>>(data, "equip_data");
	}

	public void Equip( string item)
	{
		data.Add(item);
		Save ();
	}

	public void UnEquip( string item)
	{
		data.Remove(item);
		Save();
	}

	public bool IsEquipped( string item)
	{
		return data.Contains(item);
	}

}
