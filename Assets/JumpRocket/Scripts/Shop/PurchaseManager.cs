using UnityEngine;
using System.Collections;

/// <summary>
/// This class is the hub to process purchases.
/// Modify it to access your own IAP or to make it work with other items like consumable.
/// </summary>
public class PurchaseManager : MonoBehaviour {
        
    public void PurchaseItem( ProductItem product)
    {
        Debug.Log("Purchasing item: " + product.nameID);
        Debug.Log(" " + product.name + " " + product.packName);

        /// If we already have the item in our inventory. (we purchased it already)
        /// we equip it into our ship whitout expending additional money.
        /// We do that with the isEquippable value.
        if (Inventory.Instance.GetItemCount(product.nameID) > 0 && product.isEquippable)
        {
            if (!PlayerEquip.Instance.IsEquipped(product.nameID))
            {
                PlayerEquip.Instance.Equip(product.nameID);
                foreach (string ueq in product.unequips)
                {
                    PlayerEquip.Instance.UnEquip(ueq);
                }
                GameObject.FindObjectOfType<ShipUpdater>().Equip();
            }
        }
        else
        {
            ///If the product can be purchased with soft money then we substract the coins or we check if we have enough coins.
            if (!product.isRealPrice)
            {
                /// If we still don't have the product we purchase it with coins / IAP.
                if (Economy.Instance.GetFM() >= product.price)
                {

                    // basic thruster.
                    Economy.Instance.AddFM(-product.price);
                    Inventory.Instance.AddItem(product.nameID, product.amount);
                    PlayerEquip.Instance.Equip(product.nameID);
                    foreach (string ueq in product.unequips)
                    {
                        PlayerEquip.Instance.UnEquip(ueq);
                    }
                    GameObject.Find("ShipUpdater").GetComponent<ShipUpdater>().Change();                    
                }
                else
                {
                    GameObject.Find("NoCoins").GetComponent<UIScreenDisplay>().Show();
                }
            }
            else
            {
                /// If the product is "RealPrice" then we call the IAP system to trigger a purchase.
                /// Call your IAP manager here so it handles the IAP purchase for the given pack.
                /// 
#if USE_IAP
                RBIAPManager.purchase(product.packName);                                
#endif
            }
        }

    }
}
