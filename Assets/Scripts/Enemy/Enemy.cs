using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackToSelf = new Vector2(3f,3f);
    [SerializeField] private Vector2 knockbackToPlayer = new Vector2(3f,3f);

    [SerializeField] private int damage = 3;

    public void Friend()
    {
        
    }

    public void HitPlayer(Transform playerTransform)
    {
        
        int direction = GetDirection(playerTransform);

        FindObjectOfType<PlayerMovement>().KnockbackPlayer(knockbackToPlayer, direction);
        FindObjectOfType<PlayerHealth>().DamagePlayer(damage);
   
    }

    

    private int GetDirection(Transform playerTransform)
    {
        if (transform.position.x > playerTransform.position.x)
        {
            //our enemy is to the right of the player
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
