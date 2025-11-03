using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PertsonaiMugimendua : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float mugiAbiadura = 5f;
    [SerializeField] float saltoIndarra = 5f;
    [SerializeField] float ClimbingVelocity = 5f;
    [SerializeField] float ProyectileVelocity = 25f;
    [SerializeField] GameObject ProyectilePrefab;
    [SerializeField] Transform ProyectileSpawnPoint;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D BodyCollider;
    BoxCollider2D FeetCollider;
    float gravityScale;
    bool ControlsDisabled = false;

    private bool IsGrounded => FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    
    private bool IsOnLadder => !FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && BodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")); 

    private bool CanClimbLadder => FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && BodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        BodyCollider = GetComponent<CapsuleCollider2D>();
        FeetCollider = GetComponent<BoxCollider2D>();
        gravityScale = rb.gravityScale;
        ControlsDisabled = false;
    }

    private void Update()
    {
        if (ControlsDisabled) return;
        Run();
        Climb();
        FlipSprite();
        RunningAnimation();
    }

    void OnAttack()
    {
        if (ControlsDisabled) return;
        var CreatedStar = Instantiate(ProyectilePrefab, ProyectileSpawnPoint.position, ProyectileSpawnPoint.rotation);
        float direction = transform.localScale.x > 0 ? -1f : 1f;

        // Aplicar velocidad al Rigidbody2D del proyectil
        CreatedStar.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(direction * ProyectileVelocity, 0f);

    }

    private void Climb()
    {
        animator.SetBool("OnLadder", this.IsOnLadder);
        if (this.IsOnLadder || this.CanClimbLadder)
        {

            rb.linearVelocityY = moveInput.y * ClimbingVelocity;
            rb.gravityScale = 0;
            if (this.IsOnLadder && Mathf.Abs(rb.linearVelocityY) < Mathf.Epsilon)
            {
                animator.speed = 0f;
                return;
            }
        } else
        {
            rb.gravityScale = gravityScale;
        }
         animator.speed = 1f;
    }

    private void RunningAnimation()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(-Mathf.Sign(rb.linearVelocity.x), 1f);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnSprint()
    {
        if (this.IsGrounded)
            animator.SetTrigger("Roll");
    }

    void OnJump()
    {
        if (ControlsDisabled) return;
        if (this.IsGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, saltoIndarra);
    }

    void Run()
    {
        rb.linearVelocity = new Vector2(moveInput.x * mugiAbiadura, rb.linearVelocityY);
    }

    public void DisableControls()
    {
        ControlsDisabled = true;
    }
}
