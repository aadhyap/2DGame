using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackToSelf = new Vector2(3f,3f);
    [SerializeField] private Vector2 knockbackToPlayer = new Vector2(3f,3f);

    public void Friend()
    {
        
    }

    public void HitPlayer(Transform playerTransform)
    {
        if (playerTransform == null)
        {

            return;
        }
        int direction = GetDirection(playerTransform);

        FindObjectOfType<PlayerMovement>().KnockbackPlayer(knockbackToPlayer, direction);
   
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
