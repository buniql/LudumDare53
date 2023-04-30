using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Price;
    public Image Background;

    public void SetShopSlot(string name, string description, int price)
    {
        this.Name.SetText(name);
        this.Description.SetText(description);
        this.Price.SetText(price.ToString());
        SetSlotColor(price);
    }

    public void SetSlotColor(int price)
    {
        if (price <= 10)
        {
            Background.GetComponent<Image>().color = new Color32(255,255,225,255);
            return;
        }
        if (price <= 20)
        {
            Background.GetComponent<Image>().color = new Color32(80,251,117,255);
            return;
        }
        if (price <= 30)
        {
            Background.GetComponent<Image>().color = new Color32(200,80,251,255);
            return;
        }
        if (price <= 40)
        {
            Background.GetComponent<Image>().color = new Color32(243,103,47,255);
            return;
        }
        Background.GetComponent<Image>().color = new Color32(255,255,0,255);
        return;
    }
}
