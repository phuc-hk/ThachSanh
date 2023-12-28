using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Equip Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] AnimatorOverrideController animatorOverride;
    //[SerializeField] float weaponRange;
   
    GameObject weapon;

    public void Spawn( Animator animator)
    {
        if (animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }

        //if (weaponPrefab != null)
        //{
        //    return weapon;
        //}
        //else
        //    return null;
    }
}
