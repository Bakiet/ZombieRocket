using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopButton : MonoBehaviour {

	public int productID;
	public string productName;
	public bool disableIfExisting;
	ProductItem item;

	void Start()
	{
		
		Score score = GameSerializer.Instance.GetElement<Score>("lastScore");
		item = GameObject.Find("Database").GetComponent<ProductDatabase>().GetItem( productID );
		SetCurrentProduct(productID);
		
		if (item.isEquippable)
		{
			if (score.amount <=  item.heightNeeded)
			{
				transform.FindChild("Lock").gameObject.SetActive(true);
				transform.FindChild("LockText").GetComponent<Text>().text = "Reach" + item.heightNeeded;
				transform.FindChild("BuyButton").GetComponent<Button>().enabled = false;
				//gameObject.GetComponent<UIButton>().enabled = false;
				//gameObject.GetComponent<BoxCollider>().enabled = false;
			}else
			{
				transform.FindChild("Lock").gameObject.SetActive(false);
				transform.FindChild("LockText").GetComponent<Text>().text = "";				
				transform.FindChild("BuyButton").GetComponent<Button>().enabled = true;
			}
		}
	}

	void Update()
	{
		if (item != null && item.isEquippable)
		{
			if (PlayerEquip.Instance.IsEquipped(productName))
			{
				transform.FindChild("Price").gameObject.GetComponent<Text>().text = "In Use";
			}else
			{
				if (Inventory.Instance.GetItemCount( item.nameID ) > 0)
				{
					transform.FindChild("Price").gameObject.GetComponent<Text>().text = "Equip";
				}
			}
		}
	}
	
	public void SetCurrentProduct(int productID)
	{		
		ProductItem item = GameObject.Find("Database").GetComponent<ProductDatabase>().GetItem( productID);
		
		
		transform.FindChild("ItemName").gameObject.GetComponent<Text>().text = string.Format( item.name, item.amount );
		transform.FindChild("Description").gameObject.GetComponent<Text>().text = string.Format( item.description, item.amount );
		transform.FindChild("ItemImage").gameObject.GetComponent<Image>().sprite = item.sprite;
		
		if (!item.isRealPrice)
		{
			
			if (Inventory.Instance.GetItemCount( item.nameID ) > 0)
			{
				//transform.FindChild("EquipButton").gameObject.SetActive(true);
				transform.FindChild("Price").gameObject.GetComponent<Text>().text = "Equip";
				transform.FindChild("BuyButton").GetComponent<Button>().enabled = false;
				transform.FindChild("CoinIcon").gameObject.GetComponent<CanvasGroup>().alpha = 0;
			}
			else
			{
				//transform.FindChild("EquipButton").gameObject.SetActive(false);
				transform.FindChild("BuyButton").GetComponent<Button>().enabled = true;
				transform.FindChild("Price").gameObject.GetComponent<Text>().text = "" + item.price;
				//transform.FindChild("BuyButton").FindChild("Currency").gameObject.GetComponent<UISprite>().width = 54;
				//transform.FindChild("BuyButton").FindChild("Currency").gameObject.GetComponent<UISprite>().height = 34;
			}
		}else
		{
			//transform.FindChild("EquipButton").gameObject.SetActive(false);
			transform.FindChild("BuyButton").gameObject.SetActive(true);
						
			transform.FindChild("Price").gameObject.GetComponent<Text>().text = "$" + item.realPrice;
			
			transform.FindChild("CoinIcon").gameObject.GetComponent<CanvasGroup>().alpha = 0;
			
			transform.FindChild("Lock").gameObject.SetActive(false);
			transform.FindChild("LockText").GetComponent<Text>().text = "";				
			transform.FindChild("BuyButton").GetComponent<Button>().enabled = true;
			
			//transform.FindChild("BuyButton").FindChild("Currency").gameObject.GetComponent<UISprite>().width = 0;
			//transform.FindChild("BuyButton").FindChild("Currency").gameObject.GetComponent<UISprite>().height = 0;
		}
		
	}

	public void OnButtonClick()
	{
		gameObject.GetComponent<PurchaseItem>().OnPurchase( productID);	
	}

}
