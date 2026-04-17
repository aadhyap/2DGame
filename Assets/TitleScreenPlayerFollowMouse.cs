using UnityEngine;

public class TitleScreenPlayerFollowMouse : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundMarker;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stopDistance = 0.05f;

    private Camera mainCam;
    private float playerHalfWidth;
    private float minX;
    private float maxX;
    private float lastX;

    private void Start()
    {
        mainCam = Camera.main;
        playerHalfWidth = spriteRenderer.bounds.extents.x;

        Vector3 leftEdge = mainCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 rightEdge = mainCam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));

        minX = leftEdge.x + playerHalfWidth;
        maxX = rightEdge.x - playerHalfWidth;

        Vector3 pos = transform.position;
        pos.y = groundMarker != null ? groundMarker.position.y : pos.y;
        transform.position = pos;

        lastX = transform.position.x;
    }

    private void Update()
    {
        FollowMouseX();
        HandleFlip();
    }

    private void FollowMouseX()
    {
        if (mainCam == null)
            return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float targetX = Mathf.Clamp(mouseWorld.x, minX, maxX);

        Vector3 pos = transform.position;
        float newX = Mathf.MoveTowards(pos.x, targetX, moveSpeed * Time.deltaTime);

        pos.x = newX;
        pos.y = groundMarker != null ? groundMarker.position.y : pos.y;
        transform.position = pos;

        float distanceToTarget = Mathf.Abs(targetX - newX);
        bool isRunning = distanceToTarget > stopDistance;

        if (animator != null)
        {
            animator.SetBool("isRunning", isRunning);
        }
    }

    private void HandleFlip()
    {
        float currentX = transform.position.x;

        if (currentX > lastX)
        {
            spriteRenderer.flipX = false;
        }
        else if (currentX < lastX)
        {
            spriteRenderer.flipX = true;
        }

        lastX = currentX;
    }
}