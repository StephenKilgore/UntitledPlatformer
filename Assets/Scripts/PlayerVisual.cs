using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerVisual : MonoBehaviour
{
   private bool isFlipped = false;
 
   private void Start()
   {
      Player.Instance.OnStateChange += Player_OnStateChange;
   }

   private void Player_OnStateChange(object sender, EventArgs e)
   {
      Animator animator = GetComponent<Animator>();
      switch (Player.Instance.GetState())
      {
         case Player.State.Idle:
            animator.SetBool("Is Walking", false);
            animator.SetBool("Is Idle", true);
            break;
         case Player.State.Walking:
            animator.SetBool("Is Idle", false);
            animator.SetBool("Is Walking", true);
            break;
         case Player.State.Falling:
            break;
         case Player.State.Jumping:
            break;
         case Player.State.DoubleJumping:
            break;
      }
   }
   
   
   public void Flip()
   {
      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

      spriteRenderer.flipX = !isFlipped;
      isFlipped = !isFlipped;
   }

   public bool IsFlipped()
   {
      return isFlipped;
   }
   
}
