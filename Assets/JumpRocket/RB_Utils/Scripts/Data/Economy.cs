using UnityEngine;
using System.Collections;

public class Economy {

	private static Economy instance;

	private EconomyData data;

	private Economy()
	{

	}

	public static Economy Instance
	{
		get		
		{
            
			if (instance == null)
			{
                //PlayerPrefs.DeleteAll();
                instance = new Economy();
				instance.Load();
			}
			return instance;
		}
	}
	/// <summary>
	/// Load this data.
	/// </summary>
	private void Load()
	{
		data = GameSerializer.Instance.GetElement<EconomyData>("economy_data");
		if (data == null)
		{
			data = new EconomyData();
			Save ();
		}
#if UNITY_EDITOR
		if (data.FM <= 5000)
		{
			data.FM = 10000;
		}
#endif
	}

	/// <summary>
	/// Save this data.
	/// </summary>
	private void Save()
	{
		GameSerializer.Instance.SaveElement<EconomyData>(data, "economy_data");
	}

	public void AddFM( float amount )
	{
		data.FM += amount;
		Save ();
	}

	public void AddPM( float amount )
	{
		data.PM += amount;
		Save ();
	}

	public void AddEnergy( float amount )
	{
		data.Energy += amount;
		Save ();
	}

	public float GetFM()
	{
		return data.FM;
	}

	public float GetPM()
	{
		return data.PM;
	}

	public float GetEnergy()
	{
		return data.Energy;
	}

}
