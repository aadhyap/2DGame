using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 3;
    [SerializeField] private Vector2 knockbackToPlayer = new Vector2(3f, 3f);

    private Vector2 moveDirection;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            

            int direction = moveDirection.x < 0 ? -1 : 1;

            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
            else
            {
                Debug.LogError("❌ PlayerHealth not found on player");
            }

            if (playerMovement != null)
            {
                playerMovement.KnockbackPlayer(knockbackToPlayer, direction);
            }
            else
            {
                Debug.LogError("❌ PlayerMovement not found on player");
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}