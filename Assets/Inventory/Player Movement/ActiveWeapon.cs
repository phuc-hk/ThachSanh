using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    //? gameobject = player
    [SerializeField] private Animator playerAnimator;
    public static ActiveWeapon Instance;
    [SerializeField] public MonoBehaviour CurrenActiveWeapon {get; private set;}
    public AnimatorOverrideController overrideControllers;
    private string weaponName;
    //bool attackButton, isAttacking = false;

    private void Awake() {
        
    }
    private void Start() {
        Instance = this;
        playerAnimator = GetComponentInParent<Animator>();
    }
    private void Update() {
        //Attack(); //Phuc comment
        Debug.Log(weaponName);
    }
    public void NewWeapon(MonoBehaviour newWeapon) // ham nay duoc goi ben ActiveInventory khi Instite vu khi va bo class weapon vao day
    {
        Debug.Log("Co chay");
        CurrenActiveWeapon = newWeapon;
        // AttackCoolDown();
        // timeBetweenAttacks = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
        weaponName = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().itemName;
        overrideControllers = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().animatorOverrideController;
        playerAnimator.runtimeAnimatorController = overrideControllers; //-> Phuc them

    }
    public void WeaponNull() {
        CurrenActiveWeapon = null;
    }

    private void Attack()
    {
        // if (attackButton && !isAttacking && CurrenActiveWeapon)
        // {
        //     isAttacking = true;
            
        //     AttackCoolDown();

        //     (CurrenActiveWeapon as IWeapon).Attack(); //? vu khi dang equip se Attack() as IWeapon

        // }
        if(Input.GetKeyDown(KeyCode.Mouse0) && CurrenActiveWeapon) {
            (CurrenActiveWeapon as IWeapon).Attack();
            playerAnimator.runtimeAnimatorController = overrideControllers;
            playerAnimator.SetTrigger("Attack");
        }
            

    }
    private void SetAnimation() {
        Debug.Log("Co chay");
        playerAnimator.runtimeAnimatorController = (CurrenActiveWeapon as IWeapon).GetWeaponInfo().animatorOverrideController;
    }
    
    
}
