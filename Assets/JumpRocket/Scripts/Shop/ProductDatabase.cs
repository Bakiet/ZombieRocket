using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductDatabase : MonoBehaviour {
	
	public List<ProductItem> items;


	public ProductItem GetItem( int itemID )
	{
		foreach(ProductItem item in items)
		{
			if (item.itemID == itemID)
			{
				return item;
			}
		}
		return null;
	}

}
