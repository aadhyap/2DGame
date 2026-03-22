using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float bounceForce = 6f;

    private float halfHeight;

    void Start()
    {
        
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            CollideWithEnemy(other);
        }
    }

    public void KnockbackPlayer(Vector2 knockbackForce, int direction)
        {
                knockbackForce.x *= direction;
                rigidBody.linearVelocity = Vector2.zero;
                rigidBody.angularVelocity = 0f;
                rigidBody.AddForce(knockbackForce, ForceMode2D.Impulse);
                
        }

    private void CollideWithEnemy(Collision2D other)
    {


       Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (Physics2D.Raycast(transform.position, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Enemy")))
        {
            //no pegging but there should be a case where when the enemey is a friend

        }
        else
        { 
            //player should knock back 
            //player should flash red and take damage
            //enemy should also pause movement
            enemy.HitPlayer(transform);
        
        }
        
    }
}
