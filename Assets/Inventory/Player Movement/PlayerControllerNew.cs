
using UnityEngine;


public class PlayerControllerNew : MonoBehaviour
{
    [SerializeField] Inventory inventory; // se duoc Awake() goi de khoi tao new inventory
    [SerializeField] UI_Inventory ui_Inventory; // dung de goi ham SetInventoy()
    [SerializeField] private float moveSpeed =1f;
    [SerializeField] private Transform activeWeaponTrans;
    private Vector2 movement;
    private Rigidbody2D rb;
    private PlayerControls playerControls;
    private Animator animator;

    

    #region ScriptableObject
    // [SerializeField] ItemScriptableObject itemSO_Sword;
    // [SerializeField] ItemScriptableObject itemSO_HealthPotion;
    // [SerializeField] ItemScriptableObject itemSO_ManaPotion;
    // [SerializeField] ItemScriptableObject itemSO_Medkit;

#endregion ScriptableObject

    private void Awake() {
        animator = GetComponent<Animator>(); //?

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        inventory = new Inventory(UseItem); // => khoi tao Inventory() => itemList
        //ui_Inventory.SetPlayerPos(this); // uiInventory lay vi tri player

        activeWeaponTrans = GameObject.Find("ActiveWeapon").transform;
    }
    private void Start() {
        //inventory.kieu += UseItem; //todo gan dc ham tai this.class -> bien delegate kieu ben inventory
        ui_Inventory.SetInventory(inventory); // bien inventory in doi tuong UI_Inventory duoi canvas da duoc gan gia tri
        //ui_Inventory.SetInven(inven);
        //? dung static itemWorld goi phuong thuc Spawnworld ra vat phan world
        // ItemWorld.SpawnItemWorld(new Vector3(2,2), new Item {itemScriptableObject = itemSO_HealthPotion, amount =1});
        // ItemWorld.SpawnItemWorld(new Vector3(-2,2), new Item {itemScriptableObject = itemSO_ManaPotion, amount =1});
        // ItemWorld.SpawnItemWorld(new Vector3(0,-2), new Item {itemScriptableObject = itemSO_ManaPotion, amount =1});
        // ItemWorld.SpawnItemWorld(new Vector3(0,2), new Item {itemScriptableObject = itemSO_Medkit, amount =1});
        // ItemWorld.SpawnItemWorld(new Vector3(0,3), new Item {itemType = Item.ItemType.Medkit, amount =1});
    }



    private void UseItem(Item item) {
        switch (item.itemScriptableObject.itemType) //? ~ item.itemType
        {
            case Item.ItemType.HealthPotion: // itemType ben ngoai Item de kiem tra itemtype ben trong SOb
            {
                Debug.Log("su dung HealthPotion");
                inventory.RemoveItem(new Item {itemScriptableObject = item.itemScriptableObject, amount =1});
                break;
            }
            case Item.ItemType.ManaPotion:
            {
                Debug.Log("su dung ManaPotion");
                inventory.RemoveItem(new Item {itemScriptableObject = item.itemScriptableObject, amount =1});
                break;
            }
            case Item.ItemType.Sword_01:
            {
                //? trang bi sword len nguoi player, kho cho trang bi nua
                Debug.Log("su dung Sword_01");
                ChangeWwapon(item);
                break;
            }
            case Item.ItemType.Sword_02:
            {
                //? trang bi sword len nguoi player, kho cho trang bi nua
                Debug.Log("su dung Sword_02");
                ChangeWwapon(item);
                break;
            }
        }
    }
    private void ChangeWwapon(Item item) {
        // if(ActiveWeapon.Instance.CurrenActiveWeapon != null) {
        //     Destroy(ActiveWeapon.Instance.CurrenActiveWeapon.gameObject);
        // }

        foreach (Transform child in ActiveWeapon.Instance.transform) {
            Destroy(child.gameObject);
            Debug.Log("co xoa");
        }

        GameObject newWeapon = item.itemScriptableObject.pfSword;
        Instantiate(newWeapon, ActiveWeapon.Instance.transform);
        newWeapon.GetComponentInChildren<SpriteRenderer>().sprite = null;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>()); // bo script vao
    }


    private void OnEnable() {
        playerControls.Enable();
    }

    void Update()
    {
        PlayerInput();

    }

    private void FixedUpdate() {
        Move();
    }
    public Vector3 GetPosition() {
        return transform.position;
    }

    //todo lay tinh hieu input de suy ra vector2 movement
    private void PlayerInput(){
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        //xet animation nho co dong nay ma player se giu nguyen huong nhin
        animator.SetFloat("speed", 1 * movement.magnitude);
        if(movement != Vector2.zero) {
            animator.SetFloat("XInput", movement.x);
            animator.SetFloat("YInput", movement.y);
        }
        
    }
    

    private void Move(){
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if(itemWorld != null) {
            // lay ve doi tuong item Item.cs ( game object = vat pham pfItemWord vua louch)
            inventory.AddItem(itemWorld.GetItem()); //todo add item vat pham vao trong itemsList => tang them 1 vat pham
            itemWorld.DestroySelf();
        }

        // WeaponInfoWorld weaponInfoWorld = other.GetComponent<WeaponInfoWorld>();
        // if(weaponInfoWorld != null) {
        //     inven.AddWeaponInfo(weaponInfoWorld.GetItem(), weaponInfoWorld.amount);
        //     weaponInfoWorld.DestroySelf();
        // }
    }
    
}
