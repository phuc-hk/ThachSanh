
using UnityEngine;

public class SetAttackType : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers; // animator can ghi de len animator cua plauer

    [SerializeField] private PlayerAnimatorOverride playerAnimatorOverride; // animator cua player

    public void Set(int value) {
        playerAnimatorOverride.SetAnimations(overrideControllers[value]);
    }
}
