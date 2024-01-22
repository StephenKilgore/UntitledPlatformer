using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
   public event EventHandler OnJump; 
   public static GameInput Instance { get; private set; }
   private PlayerInputActions playerInputActions;
   
   private void Awake()
   {
      Instance = this;
      playerInputActions = new PlayerInputActions();
      playerInputActions.Player.Move.Enable();
      playerInputActions.Player.Jump.Enable();

      playerInputActions.Player.Jump.performed += JumpOnperformed;
   }

   private void JumpOnperformed(InputAction.CallbackContext obj)
   {
      OnJump?.Invoke(this, EventArgs.Empty);
   }

   public Vector2 GetInputVectorNormalized()
   {
      return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
   }
}
