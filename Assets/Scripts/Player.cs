using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Jumping,
        DoubleJumping,
        Falling,
        Hit
    }
    public static Player Instance { get; private set; }

    public event EventHandler OnStateChange;  
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private PlayerVisual playerVisual;
    
    private State currentState;

    private void Awake()
    {
        Instance = this;
        currentState = State.Idle;
    }
    private void Update()
    {
       HandleMovement();
    }

    private void Start()
    {
        GameInput.Instance.OnJump += Player_OnJump;
    }

    private void Player_OnJump(object sender, EventArgs e)
    {
        HandleJumping();
    }

    private void HandleMovement()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();

        // we are moving
        if (inputVector.x != 0)
        {
            //we are moving backward
            if (inputVector.x < 0)
            {
                if (!playerVisual.IsFlipped())
                {
                    playerVisual.Flip();
                }
            }
            // we are moving forward
            else
            {
                if (playerVisual.IsFlipped())
                {
                    playerVisual.Flip();
                }
            }
            Vector3 moveDir = new Vector3(inputVector.x, 0f);
            transform.position += moveDistance * moveDir;

            currentState = State.Walking;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            currentState = State.Idle;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleJumping()
    {
        if (currentState != State.Jumping)
        {
            currentState = State.Jumping;
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            currentState = State.Idle;
        }
    }
    public bool IsWalking()
    {
        return currentState == State.Walking;
    }

    public State GetState()
    {
        return currentState;
    }
}
