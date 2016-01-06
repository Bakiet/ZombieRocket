using UnityEngine;
using System.Collections;

public class RBShopScrollFiller : MonoBehaviour {

	public GameObject shopTabPrefab;
	public GameObject container;
    

	public void Start()
	{
		ProductDatabase builder = GameObject.FindObjectOfType<ProductDatabase>();
        float totalScrollContentWidth = 0;
		foreach (ProductItem entry in builder.items)
		{
			GameObject tab = GameObject.Instantiate(shopTabPrefab) as GameObject;

			tab.GetComponent<RBShopTabFiller>().product = entry;
			tab.transform.parent = container.transform;
			tab.transform.localScale = Vector3.one;
            totalScrollContentWidth += tab.transform.FindChild("Background").GetComponent<RectTransform>().rect.width / 2;
		}
        totalScrollContentWidth += shopTabPrefab.transform.FindChild("Background").GetComponent<RectTransform>().rect.width * 2f;
        RectTransform r = container.GetComponent<RectTransform>();        
        r.sizeDelta = new Vector2(totalScrollContentWidth, r.sizeDelta.y);
	}

}
