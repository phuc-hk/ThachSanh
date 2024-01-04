using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestGetImage : MonoBehaviour
{
    private List<Item> itemList;
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        itemList = new List<Item>();
        //itemList.Add(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) {

            foreach (Item item in itemList) {
                //image.sprite = item.GetSprite();
                image.sprite = item.itemScriptableObject.itemSprite;
            }
        }
    }
}
