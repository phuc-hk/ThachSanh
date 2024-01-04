using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Item
{
    //? Invenotry.cs goi khi khoi tao Inventoty()
    public enum ItemType
    {
        Sword_01,
        Sword_02,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
        Axe
    }

    //public ItemType itemType;
    public ItemScriptableObject itemScriptableObject; // se chau cac thuoc tinh rieng
    public int amount = 1;
    

    public override string ToString()
    {
        return itemScriptableObject.name;
    }
    public Sprite GetSprite() {
        //return GetSprite(itemType);
        return GetSprite(itemScriptableObject.itemType);
    }

    //? so sanh loai item trong Enum va tra ve loai sprite image dang luu trong ItemAssets.cs
    public Sprite GetSprite(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.Sword_01:        return ItemAssets.Instance.swordSprite;
        case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
        case ItemType.ManaPotion:   return ItemAssets.Instance.manaPotionSprite;
        case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
        case ItemType.Medkit:       return ItemAssets.Instance.medkit;
        case ItemType.Axe:          return ItemAssets.Instance.axe;
        }
    }

    public bool IsStackable() {
        //return true;
        return IsStackable(itemScriptableObject.itemType);
    }

    //todo loai vat pham nao co the duoc cong don
    public bool IsStackable(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.Coin:
        case ItemType.HealthPotion:
        case ItemType.ManaPotion:
            return true;

        case ItemType.Sword_01:
        case ItemType.Medkit:
        case ItemType.Axe:
            return false;
        }
    }

}

