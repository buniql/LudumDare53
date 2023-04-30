using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    public GameObject ShopUI;
    
    public ShopSlot Slot1;
    public ShopSlot Slot2;
    public ShopSlot Slot3;

    public void ShowShop()
    {
        ShopUI.SetActive(true);
    }
    
    public void HideShop()
    {
        ShopUI.SetActive(false);
    }

    public void SetSlot1(string name, string description, int price)
    {
        Slot1.SetShopSlot(name, description, price);
    }
    
    public void SetSlot2(string name, string description, int price)
    {
        Slot2.SetShopSlot(name, description, price);
    }
    
    public void SetSlot3(string name, string description, int price)
    {
        Slot3.SetShopSlot(name, description, price);
    }
}
