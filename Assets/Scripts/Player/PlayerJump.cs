using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float groundCheckExtra = 0.05f;
    [SerializeField] private float fallingThreshold = -0.1f;

    private float playerHalfHeight;

    private void Start()
    {
        playerHalfHeight = spriteRenderer.bounds.extents.y;
    }

    private void Update()
    {
        bool grounded = GetIsGrounded();

        animator.SetBool("isGrounded", grounded);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        bool isFalling = !grounded && rigidbody2D.linearVelocity.y < fallingThreshold;
        animator.SetBool("isFalling", isFalling);
    }

    public bool IsGrounded()
    {
        return GetIsGrounded();
    }

    private bool GetIsGrounded()
    {
        return Physics2D.Raycast(
            transform.position,
            Vector2.down,
            playerHalfHeight + groundCheckExtra,
            LayerMask.GetMask("Ground")
        );
    }

    private void Jump()
    {
        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0f);
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (spriteRenderer == null)
            return;

        Gizmos.color = Color.green;
        float halfHeight = spriteRenderer.bounds.extents.y;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.down * (halfHeight + groundCheckExtra);
        Gizmos.DrawLine(start, end);
    }
}