using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed;
    private bool isWalking;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
       HandleMovement();
    }

    private void HandleMovement()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();

        if (inputVector.x != 0)
        {
            Vector3 moveDir = new Vector3(inputVector.x, 0f);
            transform.position += moveDistance * moveDir;
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
