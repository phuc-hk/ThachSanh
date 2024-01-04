using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour 
{

    //? gameobject ko duoc gan vao Gameobject cu the
    //? duoc goi khi player Awake()

    public event EventHandler OnItemListChanged;//? giu dc RefreshIventoryItems() ben UI_inventory.cs

    private List<Item> itemList; //? chau cac phan tu kieu Item(itemType, amount)
    
    private Action<Item> useItemAction; //? delegate void KIEU(item);
    // public delegate void KIEU(Item item);
    // public KIEU kieu;
    // public event EventHandler<YourItem> OnAmountUsedItemChanged;
    // public class YourItem : EventArgs {
    //     public Item item1;
    // }


#region ScriptableObject


#endregion ScriptableObject


    //?player Awake() - khoi toa Inventoy() -> khoi toa itemList() kieu Item - KHI TEST 1
    //todo bien useitemAction dang giu dc ham useItem() col 35 Playercontroller.cs
    public Inventory(Action<Item> useItemAction) //todo tham so la void_(item);
    {
        this.useItemAction = useItemAction;
        Debug.Log("player -> khoi tao Inventory -> itemList -> add");

        itemList = new List<Item>();

        //? tao moi item kieu ScriptableObject va add vao itemList
        AddItem(new Item {itemScriptableObject = new ItemScriptableObject() {
            itemType = Item.ItemType.ManaPotion },
            amount = 5});

        AddItem(new Item {itemScriptableObject = new ItemScriptableObject() {
            itemType = Item.ItemType.HealthPotion }, 
            amount = 10});

        PrinItemList();
    }


    //todo ham tra ve gia tri itemlist | RefreshIventoryItems() UI_Inventory.cs call
    public List<Item> GetItemList() {
        return itemList;
    }

    private void PrinItemList() {
        Debug.Log("itemListCount = "+ itemList.Count);
        foreach (var item in itemList)
        {
            Debug.Log($"name: {item.itemScriptableObject.itemType}_{item.IsStackable()} _ add vao ItemList");
        }
    }

    //todo add item vao trong itemList col 11
    public void AddItem(Item item) {
        if(item.IsStackable()) {
            //? neu cung loai thi se tang amount
            //? so sanh Item chuan vi add voi cac item da co san xem xo cung loai hay khong
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if(inventoryItem.itemScriptableObject.itemType == item.itemScriptableObject.itemType) {
                    Debug.Log("add o day do  cung loai");
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory) {
                Debug.Log("add o day do KO cung loai");
                itemList.Add(item);
            }
        }else {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    internal void RemoveItem(Item item)
    {
        if (item.IsStackable()) {
            Item itemInInventory = null;    
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemScriptableObject.itemType == item.itemScriptableObject.itemType) {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0) {
                itemList.Remove(itemInInventory);
            }
        } else {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    internal void UseItem(Item item)
    {
        //useItemAction(item); //? UseItem(Item item) col 35 playercontroller.cs
        //kieu?.Invoke(item); //? use delegae instead of Action<>
        useItemAction?.Invoke(item);
    }


}
