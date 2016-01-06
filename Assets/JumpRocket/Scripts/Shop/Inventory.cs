using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	/// <summary>
	/// The instance private.
	/// </summary>
	private static Inventory instance;

	public List<InventoryEntry> data;

	bool inited;
	
	private Inventory()
	{
		inited = false;

	}
	
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static Inventory Instance
	{
		get 
		{
			if (instance == null)
			{
				GameObject instanceObject = new GameObject("Inventory");
				instance = instanceObject.AddComponent<Inventory>();



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
#if DONT_SAVE_DATA
    Debug.LogWarning("You're not saving data, review your Environment Variables");
#else
		data = GameSerializer.Instance.GetElement<List<InventoryEntry>>("inventory_data");
#endif

        if (data == null)
		{
			data = new List<InventoryEntry>();
			Save ();
		}
	}
	
	/// <summary>
	/// Save this data.
	/// </summary>
	private void Save()
	{
		GameSerializer.Instance.SaveElement<List<InventoryEntry>>(data, "inventory_data");
	}

	public void AddItem( string name, float amount)
	{
		bool changed = false;
		foreach( InventoryEntry e in data)
		{
			if (e.name == name)
			{
				e.amount += amount;
				changed = true;
				break;
			}
		}
		if (!changed)
		{
			InventoryEntry e = new InventoryEntry();
			e.amount = amount;
			e.name = name;
			data.Add(e);
		}
		Save();
	}

	public bool ConsumeItem( string name)
	{
		bool changed = false;
		foreach( InventoryEntry e in data)
		{
			if (e.name == name)
			{
				if (e.amount > 0)
				{
					e.amount -= 1;
					changed = true;				
					Save();
				}
				break;
			}
		}

		return changed;
	}

	public int GetItemCount( string itemID)
	{
		foreach( InventoryEntry e in data)
		{
			if (e.name == itemID)
			{
				return (int)e.amount;
				break;
			}
		}
		return 0;
	}

	public void Print()
	{
		foreach( InventoryEntry e in data)
		{
			Debug.Log("Entry : " + e.name + " > " + e.amount );
		}
	}

}
