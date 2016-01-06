using UnityEngine;

using System.Collections;
using UnityEngine.UI;


public class RBShopTabFiller : MonoBehaviour {

	public ProductItem product;
	public Text title;
	public Text description;
	public Text price;
    public Text reachText;
    public Image lockSprite;
	public Image sprite;

    Score score;

    // Use this for initialization
    void Start()
    {

        score = GameSerializer.Instance.GetElement<Score>("lastScore");
        title.text = product.name;
        description.text = product.description;
        if (product.heightNeeded > score.amount)
        {
            lockSprite.CrossFadeAlpha(1, 0.1f, true);
            price.text = "Blocked";
            reachText.text = "Reach: " + product.heightNeeded + "m";
        }
        else
        {
            lockSprite.CrossFadeAlpha(0, 0.1f, true);
            price.text = product.price + "";
            reachText.text = "";
        }

        sprite.sprite = product.sprite;

        if (product.isRealPrice)
        {
            //we find the products in our IAP entries.
            price.text = product.realPrice + " $";
        }

        if (Inventory.Instance.GetItemCount(product.nameID) > 0)
        {
            if (PlayerEquip.Instance.IsEquipped(product.nameID))
            {
                price.text = "In Use";
            }
            else
            {
                price.text = "Equip";
            }
            
        }
    }

	public void OnItemPurchase()
	{
        if (product.heightNeeded <= score.amount)
        {           
            GameObject.FindObjectOfType<PurchaseManager>().PurchaseItem(product);            
        }
    }

    public void Update()
    {
        if (Inventory.Instance.GetItemCount(product.nameID) > 0)
        {
            if (PlayerEquip.Instance.IsEquipped(product.nameID))
            {
                price.text = "In Use";
            }
            else
            {
                price.text = "Equip";
            }
        }
    }
}
