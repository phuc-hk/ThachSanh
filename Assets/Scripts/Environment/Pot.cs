using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Smash()
    {
        animator.SetBool("Smash", true);
        //Debug.Log("Smash");
    }

    public void DestroyPot()
    {
        Destroy(gameObject);
    }
}
