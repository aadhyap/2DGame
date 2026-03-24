using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    
    [Header("Input")]
    [SerializeField] private float speed = 5f;
    private Vector2 movement;
    private Vector2 screenbounds;
    private float playerHalfWidth;
    private float xPosLastFrame;
    

    private void Start()
        {
                
                screenbounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                playerHalfWidth = spriteRenderer.bounds.extents.x;
                print(playerHalfWidth);
        }

    //update is called once per frame
    void Update()
    {
        
        HandleMovement();
        //ClampMovement();
        FlipCharacterX();

        
    }



    private void ClampMovement()
        {
                float clampedX = Mathf.Clamp(transform.position.x, -screenbounds.x + playerHalfWidth, screenbounds.x - playerHalfWidth);
                Vector2 pos = transform.position; //Get player's current pos
                pos.x = clampedX; // Reassign the X value to the clamped position
                transform.position = pos; // Reassign the clamped value back to the player
                
        }

    public void KnockbackPlayer(Vector2 knockbackForce, int direction)
        {
                knockbackForce.x *= direction;
                rigidBody.linearVelocity = Vector2.zero;
                rigidBody.angularVelocity = 0f;
                rigidBody.AddForce(knockbackForce, ForceMode2D.Impulse);
                
        }

    private void HandleMovement()
        {
                // input will store a value between -1 and +1
                // GetAxisRaw() takes exactly -1 or +1 
                // GetAxis() takes a value between and up to -1 and +1 (useful for acceleration)
                // Getting the axis is mapped to A/D, left/right arrow and joystick left/right

                float input = Input.GetAxis("Horizontal");
                movement = new Vector2(input, 0f);
                transform.Translate(movement * speed * Time.deltaTime);
                if (input != 0)
                {
                        animator.SetBool("isRunning", true);
                }
                else
                {
                        animator.SetBool("isRunning", false);
                }
                
        }

    private void FlipCharacterX()
        {
                float input = Input.GetAxis("Horizontal");
                if (input > 0 && (transform.position.x > xPosLastFrame))
                {
                        //We are moving right
                        spriteRenderer.flipX = false;
                }
                else if (input < 0 && (transform.position.x < xPosLastFrame))
                {
                        //we are moving left
                        spriteRenderer.flipX = true;
                }

                xPosLastFrame = transform.position.x;
        
        }
}