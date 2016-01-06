using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ProductItem {

	public int itemID;
	public string nameID;
	public string name;
	public string description;
	public int price;
	public float realPrice;
	public bool isRealPrice;
	public string packName;
	public float heightNeeded;
	public int amount;
	public List<string> unequips;
	public bool isEquippable;
	public Sprite sprite;

}
