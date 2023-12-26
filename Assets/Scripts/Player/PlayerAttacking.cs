using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    //public AnimationEvents AnimationEvents;
    public event Action<string> OnCustomEvent = s => { };
    public Transform Edge;
    //private int damage = 1;
    //private int hitCount = 0;
    //[SerializeField] GameObject slashEffect;
    //[SerializeField] GameObject axeWeapon;
    //[SerializeField] Transform axePos;

    /// <summary>
    /// Listen animation events to determine hit moments.
    /// </summary>
    public void Start()
    {
        OnCustomEvent += OnAnimationEvent;
    }

    public void OnDestroy()
    {
        OnCustomEvent -= OnAnimationEvent;
    }

    private void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "Hit":
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(Edge.position, Edge.localScale, 0);
                foreach (Collider2D hitCollider in hitColliders)
                {
                    Pot combatTarget = hitCollider.GetComponent<Pot>();
                    if (combatTarget == null) continue;
                    combatTarget.GetComponent<Pot>().Smash();
                }
                break;
            //case "Throw":
            //    //Debug.Log("Throw axe ne");
            //    Instantiate(axeWeapon, axePos.position, Quaternion.identity);
            //    break;
            default: return;
        }
    }    
}
