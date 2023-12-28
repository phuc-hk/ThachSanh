using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;


public enum PlayerState
{
    idle,
    attack,
    interact,
    push
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveAmount;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private PlayerState currentState;

    [SerializeField] WeaponSO defaultWeapon;
    WeaponSO currentWeapon;
    //GameObject equiptWeapon;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentState = PlayerState.idle;
        if (currentWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        currentWeapon = weapon;
        Animator animator = GetComponent<Animator>();
        weapon.Spawn(animator);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started && currentState != PlayerState.attack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        currentState = PlayerState.attack;
        animator.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.idle;
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.idle)
            Move();
        UpdateAnimation();
    }

    private void Move()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveAmount * moveSpeed * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        if (moveAmount != Vector2.zero && currentState == PlayerState.idle)
        {
            animator.SetBool("Walk", true);
            animator.SetFloat("MoveX", moveAmount.x);
            animator.SetFloat("MoveY", moveAmount.y);
        }   
        else
        {
            animator.SetBool("Walk", false);
        }    
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            currentState = PlayerState.push;
            animator.SetBool("Pushed", true);          
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            //currentState = PlayerState.push;
            //animator.SetTrigger("Push");
            //animator.SetBool("Pushed", true);
            Move();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("Pushed", false);
        StartCoroutine(ResetAttack());
    }

}

