using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    [SerializeField] private Collider2D standingCollider;
[SerializeField] private Collider2D duckingCollider;

    [Header("Input")]
    [SerializeField] private float speed = 5f;

    private Vector2 movement;
    private Vector2 screenbounds;
    private float playerHalfWidth;
    private float xPosLastFrame;
    private bool canMove = true;
    private bool isDucking = false;

    private void Start()
    {
        screenbounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    playerHalfWidth = spriteRenderer.bounds.extents.x;

    standingCollider.enabled = true;
    duckingCollider.enabled = false;
    }

    private void Update()
    {
        HandleDuck();
        HandleMovement();
        FlipCharacterX();
    }

    private void ClampMovement()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -screenbounds.x + playerHalfWidth, screenbounds.x - playerHalfWidth);
        Vector2 pos = transform.position;
        pos.x = clampedX;
        transform.position = pos;
    }

    public void KnockbackPlayer(Vector2 knockbackForce, int direction)
    {
        knockbackForce.x *= direction;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        rigidBody.AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    private void HandleDuck()
{
    bool duckPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

    isDucking = duckPressed;
    animator.SetBool("isDucking", isDucking);

    if (isDucking)
    {
        standingCollider.enabled = false;
        duckingCollider.enabled = true;
    }
    else
    {
        standingCollider.enabled = true;
        duckingCollider.enabled = false;
    }
}

    private void HandleMovement()
    {
        if (!canMove)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        // Don't move while ducking
        if (isDucking)
        {
            animator.SetBool("isRunning", false);
            return;
        }

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
            spriteRenderer.flipX = false;
        }
        else if (input < 0 && (transform.position.x < xPosLastFrame))
        {
            spriteRenderer.flipX = true;
        }

        xPosLastFrame = transform.position.x;
    }

    public void DisableMovement(float duration)
    {
        StartCoroutine(DisableMovementCoroutine(duration));
    }

    private IEnumerator DisableMovementCoroutine(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}