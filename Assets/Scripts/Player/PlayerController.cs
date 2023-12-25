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
    interact
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveAmount;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private PlayerState currentState;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentState = PlayerState.idle;
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
        if (moveAmount != Vector2.zero)
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
}

