using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackRange = 6f;

    private Transform player;
    private float lastAttackTime;

    private void Start()
    {
        Debug.Log("🧠 Enemy START");

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("✅ Player found: " + player.name);
        }
        else
        {
            Debug.LogError("❌ Player NOT found. Make sure tag = Player");
        }

        animator.SetBool("isAttacking", false);
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        FacePlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
     
            TryAttack();
        }
        else
        {
      
            SetIdle();
        }
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("🔥 ATTACK TRIGGERED");

            lastAttackTime = Time.time;
            animator.SetBool("isAttacking", true);
            animator.SetTrigger("attack");
        }
        else
        {
            Debug.Log("⏳ On cooldown");
            SetIdle();
        }
    }

    private void SetIdle()
    {
        animator.SetBool("isAttacking", false);
    }

    private void FacePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
   
        }
        else if (player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    // 🔥 IMPORTANT FUNCTION
    public void SpawnFireball()
    {
        Debug.Log("💥 SpawnFireball CALLED");

        if (fireballPrefab == null)
        {
                 Debug.Log("💥 fireball null");
       
            return;
        }

        if (firePoint == null)
        {
    
            return;
        }

        if (player == null)
        {

            return;
        }

        Debug.Log("📍 Spawning at: " + firePoint.position);

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        Fireball fireballScript = fireball.GetComponent<Fireball>();

        if (fireballScript == null)
        {
         
            return;
        }

        Vector2 direction;

        if (spriteRenderer.flipX)
        {
        direction = Vector2.right;
        }
        else
        {
        direction = Vector2.left;
        }

        Debug.Log("🎯 Fireball shooting direction: " + direction);
        fireballScript.SetDirection(direction);
    }

    public void EndAttack()
    {
        Debug.Log("🛑 Attack ended");
        animator.SetBool("isAttacking", false);
    }
}