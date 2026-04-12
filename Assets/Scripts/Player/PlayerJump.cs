using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float jumpForce = 4;
        private bool isGrounded;
        private float playerHalfHeight;


        private void Start()
        {
                playerHalfHeight = spriteRenderer.bounds.extents.y;
        }
        
        void Update()
        {
               
                if (Input.GetButtonDown("Jump") && GetIsGrounded())
                {
                        Jump();
                }
                
        }

        private bool GetIsGrounded()
        {
                return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.001f, LayerMask.GetMask("Ground"));
        }

        private void Jump()
        {
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
}