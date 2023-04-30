
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    public List<Item> AvailableItems = new List<Item>();

    private List<Item> selection = new List<Item>();

    public GameObject ShopDisplay;
    public ShopDisplay Display;

    public StatsHolder StatsHolder;

    private void Start()
    {
        RefreshShop();
    }

    public void RefreshShop()
    {
        if (AvailableItems.Count >= 3)
        {
            SelectItems();
            SetItems();
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    private void SelectItems()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, AvailableItems.Count);
            selection.Add(AvailableItems[index]);
            AvailableItems.RemoveAt(index);
        }
    }

    private void SetItems()
    {
        Display.SetSlot1(selection[0].Name, GenerateItemDescription(selection[0]), selection[0].Price);
        Display.SetSlot2(selection[1].Name, GenerateItemDescription(selection[1]), selection[1].Price);
        Display.SetSlot3(selection[2].Name, GenerateItemDescription(selection[2]), selection[2].Price);
    }

    private string GenerateItemDescription(Item item)
    {
        String result = "";
        if (item.Health != 0) result += "Health+" + item.Health + System.Environment.NewLine;
        if (item.MovementSpeed != 0) result += "Movement Speed+" + item.MovementSpeed + System.Environment.NewLine;
        if (item.ProjectileSize != 0) result += "Package Size+" + item.ProjectileSize + System.Environment.NewLine;
        if (item.ProjectileRegenTime != 0) result += "Package Regen-" + item.ProjectileRegenTime + System.Environment.NewLine;
        if (item.MaxProjectiles != 0) result += "Max. Packages+" + item.MaxProjectiles + System.Environment.NewLine;
        if (item.ShootSpeed != 0) result += "Throw Speed+" + item.ShootSpeed + System.Environment.NewLine;
        if (item.ShootDistance != 0) result += "Throw Distance+" + item.ShootDistance + System.Environment.NewLine;
        if (item.ShootCooldown != 0) result += "Cooldown-" + item.ShootCooldown + System.Environment.NewLine;
        return result;
    }

    public void PurchaseItem(int slot)
    {
        if (selection[slot].Price <= StatsHolder.Coins)
        {
            GameObject.Find("Sound").GetComponent<Sound>().PlaySound(3);
            
            StatsHolder.SetCoins(StatsHolder.Coins - selection[slot].Price);
            StatsHolder.SetHealth(StatsHolder.Health + selection[slot].Health);
            StatsHolder.SetProjectileSize(StatsHolder.ProjectileSize + selection[slot].ProjectileSize);
            StatsHolder.SetMovementSpeed(StatsHolder.MovementSpeed + selection[slot].MovementSpeed);
            StatsHolder.SetProjectileRegenTime(StatsHolder.ProjectileRegenTime - selection[slot].ProjectileRegenTime);
            StatsHolder.SetMaxProjectiles(StatsHolder.MaxProjectiles + selection[slot].MaxProjectiles);
            StatsHolder.SetShootSpeed(StatsHolder.ShootSpeed + selection[slot].ShootSpeed);
            StatsHolder.SetShootDistance(StatsHolder.ShootDistance + selection[slot].ShootDistance);
            StatsHolder.SetShootCooldown(StatsHolder.ShootCooldown - selection[slot].ShootCooldown);
            
            selection.RemoveAt(slot);

            foreach (Item item in selection)
            {
                AvailableItems.Add(item);
            }
            selection.Clear();
            RefreshShop();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Display.ShowShop();
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Display.HideShop();
        }
    }
}
