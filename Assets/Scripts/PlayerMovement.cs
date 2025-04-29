using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;

    private Rigidbody2D rb;
    public Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.Set(PlayerInputManager.PlayerMovement.x, PlayerInputManager.PlayerMovement.y);

        rb.linearVelocity = movement * moveSpeed;

        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);
    }

}
