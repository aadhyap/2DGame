using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 3f;

    private Vector2 moveDirection;

    private void Start()
    {
        Debug.Log("🔥 Fireball spawned at: " + transform.position);
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        Debug.Log("➡️ MoveDirection: " + moveDirection);

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;

        Debug.Log("🎯 Direction SET to: " + moveDirection);

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("💥 Hit: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by fireball");
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}