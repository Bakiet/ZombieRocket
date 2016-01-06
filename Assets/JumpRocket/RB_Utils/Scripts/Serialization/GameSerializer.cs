using UnityEngine;
using System.Collections;


/// <summary>
/// Game serializer.
/// Used to serialize any data in your player prefs storage
/// You can use this class to save your game and data.
/// </summary>
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System;

public class GameSerializer {

	/// <summary>
	/// The instance private variable.
	/// </summary>
	private static GameSerializer instance;

	/// <summary>
	/// Privatelly Initializes a new instance of the <see cref="GameSerializer"/> class.
	/// </summary>
	private GameSerializer()
	{

	}

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static GameSerializer Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new GameSerializer();
			}
			return instance;
		}
	}

	/// <summary>
	/// Saves the element on the given name slot.
	/// </summary>
	/// <param name='element'>
	/// Element.
	/// </param>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public void SaveElement<T>( T element, string name )
	{
        
        
		var bFormatter = new XmlSerializer(typeof(T));       
		var mStream = new MemoryStream();
		
		bFormatter.Serialize(mStream, element);

        string str = "";
        foreach (byte b in mStream.ToArray())
        {
            str += (char)b;
        }
        
		this.SaveString(name,str);
	}
	
	/// <summary>
	/// Gets the element.
	/// </summary>
	/// <returns>
	/// The element.
	/// </returns>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public T GetElement<T>( string name )
	{
		var data = PlayerPrefs.GetString(name);
		
		if(!string.IsNullOrEmpty(data))
		{

            var bFormatter = new XmlSerializer(typeof(T));
            byte[] barray = new byte[data.ToCharArray().Length];
            int index = 0;
            foreach (char c in data.ToCharArray())
            {
                barray[index] = (byte)c;
                index++;              
            }

            var mStream = new MemoryStream(barray);

            Debug.Log(mStream.ToString());

            return (T)bFormatter.Deserialize(mStream);
		}
		
		return default(T);
	}
	
	/// <summary>
	/// Saves the string.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="valor">Valor.</param>
	public void SaveString( string key, string valor )
	{
		PlayerPrefs.SetString(key, valor);
	}

	/// <summary>
	/// Gets the string.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="key">Key.</param>
	public string GetString( string key )
	{
		return PlayerPrefs.GetString(key);	
	}

}
