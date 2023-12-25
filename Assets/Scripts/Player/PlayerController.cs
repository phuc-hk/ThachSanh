using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveAmount;
    private Rigidbody2D playerRigidbody;
    private Animator animator;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void FixedUpdate()
    {
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

