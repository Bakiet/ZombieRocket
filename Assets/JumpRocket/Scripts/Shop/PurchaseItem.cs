using UnityEngine;
using System.Collections;

public class PurchaseItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPurchase( int productID)
	{		
		ProductItem product = GameObject.Find("Database").GetComponent<ProductDatabase>().GetItem(productID);


		Debug.Log("Purchasing item: " + productID );
		Debug.Log(" " + product.name + " " + product.packName);

		if (Inventory.Instance.GetItemCount( product.nameID ) > 0 && product.isEquippable)
		{
			if (!PlayerEquip.Instance.IsEquipped(product.nameID))
			{
				PlayerEquip.Instance.Equip( product.nameID );
				foreach( string ueq in product.unequips )
				{
					PlayerEquip.Instance.UnEquip( ueq );
				}
				GameObject.Find("ShipUpdater").GetComponent<ShipUpdater>().Equip();
			}
		}
		else
		{
			if (Economy.Instance.GetFM() >= product.price)
			{
				if (!product.isRealPrice)
				{
					// basic thruster.
					Economy.Instance.AddFM(-product.price);
					Inventory.Instance.AddItem( product.nameID, product.amount);
					PlayerEquip.Instance.Equip( product.nameID );
					foreach( string ueq in product.unequips )
					{
						PlayerEquip.Instance.UnEquip( ueq );
					}
					GameObject.Find("ShipUpdater").GetComponent<ShipUpdater>().Change();

				}

			}else
			{
				GameObject.Find("NoCoins").GetComponent<UIScreenDisplay>().Show();
			}
		}
	}
}
