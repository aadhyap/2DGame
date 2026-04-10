using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackToSelf = new Vector2(3f, 3f);
    [SerializeField] private Vector2 knockbackToPlayer = new Vector2(3f, 3f);
    [SerializeField] private int damage = 3;

    public void Friend()
    {
        
    }

    public void HitPlayer(Transform playerTransform)
    {
        int direction = GetDirection(playerTransform);

        PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
        PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();

        if (playerMovement != null)
        {
            playerMovement.KnockbackPlayer(knockbackToPlayer, direction);
        }

        if (playerHealth != null)
        {
            playerHealth.DamagePlayer(damage);
        }
    }

    private int GetDirection(Transform playerTransform)
    {
        if (transform.position.x > playerTransform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}