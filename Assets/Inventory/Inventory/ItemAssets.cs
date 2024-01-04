using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    //? gamobject = doi tuong  ItemAssets chua toan bo hinh anh de hien thi cua Item
    public static ItemAssets Instance {get; private set;}

    private void Awake() {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite medkit;
    public Sprite axe;
}
