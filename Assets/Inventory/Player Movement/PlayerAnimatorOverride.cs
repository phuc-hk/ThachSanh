using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorOverride : MonoBehaviour
{
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        
    }

    // ham de chon loia animator override len cho animator cua player
    public void SetAnimations(AnimatorOverrideController overrideController) {
        animator.runtimeAnimatorController = overrideController;
    }
}
