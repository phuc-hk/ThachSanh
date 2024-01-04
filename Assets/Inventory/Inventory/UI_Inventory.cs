using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CodeMonkey.Utils;


public class UI_Inventory : MonoBehaviour
{
    
    //todo Gameobject = duoi tuong nam duoi Canvas Inventory
    [SerializeField] private Inventory inventory; // se duoc gan vao khi SetInventory is called
    
    //? 2 transform container and templet
    [SerializeField] private Transform itemSlotContainer; // parent folder
    [SerializeField] private Transform itemSlotTemplate; // vi tri cua item se hien len (o vat pham) nam torng bang vat pham
    private PlayerController playerController;
    [SerializeField] private int ItemAmountOnRow = 5;
    [SerializeField] float itemSlotCellSize = 80f;
    
    private void Awake() {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        
    }


    void Update()
    {

    }
    public void SetPlayerPos(PlayerController playerController) {
        this.playerController = playerController;
    }

    //? gan inventory khi player Awake() -> this.inventory
    public void SetInventory(Inventory inventory) {
        this.inventory = inventory; // bien inventory tai class nay duoc gan ben playerController.cs
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshIventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshIventoryItems();
    }

    //? duyet itemList ben trong list itemList | thong qua GetItemList() Inventory.cs
    //todo tao ra transform itemSlotTemplate (o vat pham) trong folder cha itemSlotContainer
    //? sap xep vi tri UI o vat pham ben trong bang vat pham
    private void RefreshIventoryItems() {

        //! tranh tao ra tranform khi duyet Inventory || itemList se tao ra qua nhiu child in itemSlotContainer
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
            Debug.Log("co xoa");
        }
        
        //? toa do itemSlot ben trong bang vat pham
        int x = 0;
        int y = 0;
        //itemSlotCellSize = 80f;


        foreach (Item item in inventory.GetItemList()) // cu moi vat pham ben trong listItems trong Item.cs
        {
            Debug.Log($"name: {item.itemScriptableObject.itemType} _ Add vo UI_Inventory");

            //? tao ra transform itemSlotTemplate trong hierachy
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            //? sau khi add Button_UI codeMonkey su dung mouse left right de se dung or return
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use item
                inventory.UseItem(item); // trong ham UseItem(item) => se chay bien delegate
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                // Drop item
                Item duplicateItem = new Item { itemScriptableObject = item.itemScriptableObject, amount = item.amount }; // khi bi mat item khi drop and add
                inventory.RemoveItem(item);
                ItemWorld.DropItem(playerController.GetPosition(),duplicateItem);
            };

            //? xet vi tri cho o vat pham UI
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);

            //? thay doi hinh anh hien thi cua tung o vat pham 
            //Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            Image image = itemSlotRectTransform.GetChild(2).GetComponent<Image>();
            image.sprite = item.GetSprite(); // ay tu ham GetSprite() - thong qua ItemAsset()
            //image.sprite = item.itemScriptableObject.itemSprite;

            //? hien amount cua item tren bang UIsau khi add them vao > 1
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1) {
                uiText.SetText(item.amount.ToString());
            } else {
                uiText.SetText(""); // neu la cai dau tien lum vao thi ko hien so 1
            }

            // offset x, y vi tri o vat pham tren bang vat pham
            x++;
            if (x >= ItemAmountOnRow) {
                x = 0;
                y++;
            }
        }
    }

}