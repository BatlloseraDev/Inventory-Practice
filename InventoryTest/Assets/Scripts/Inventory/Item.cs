using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
   public enum ItemType
    {
        Sword,
        Bow,
        Arrow,
        HealthPotion,
        InvincibilityPotion

    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:                return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:         return ItemAssets.Instance.healthPotionSprite;
            case ItemType.Bow:                  return ItemAssets.Instance.bowSprite;
            case ItemType.Arrow:                return ItemAssets.Instance.arrowSprite;
            case ItemType.InvincibilityPotion:  return ItemAssets.Instance.invincibilityPotionSprite;
        }
    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:                return new Color(1, 1, 1); 
            case ItemType.HealthPotion:         return new Color(1, 0, 0);
            case ItemType.Bow:                  return new Color(0, 1, 1);
            case ItemType.Arrow:                return new Color(1, 1, 0);
            case ItemType.InvincibilityPotion:  return new Color(1, 0, 1);
        }
    }
    
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Arrow:
            case ItemType.HealthPotion:
            case ItemType.InvincibilityPotion:
                return true;
            case ItemType.Bow:
            case ItemType.Sword:
                return false;
        }
    }
}
