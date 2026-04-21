using UnityEngine;
using System;

public class FriendshipLevel : MonoBehaviour
{
    [SerializeField] private int friendship = 10;
    [SerializeField] private string enemyId;

    [Header("Heart Effect")]
    [SerializeField] private GameObject heartEffectPrefab;
    [SerializeField] private Vector3 heartOffset = new Vector3(0f, 2f, 0f);
    [SerializeField] private float heartLifetime = 1f;
    [SerializeField] private Transform heartSpawnPoint;
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private Collider2D playerCollider;

    [Header("Friend Visuals")]
    [SerializeField] private Animator animator;
    [SerializeField] private string friendBoolName = "isFriend";
    [SerializeField] private MonoBehaviour enemyAttackScript; // optional
    [SerializeField] private MonoBehaviour enemyMovementScript; // optional

    public int currentFriendship { get; private set; }
    public int maxFriendship { get; private set; }
    public bool isFriend { get; private set; }

    public static Action<FriendshipLevel> OnFriendshipTargetChanged;
    public static Action<int, int> OnFriendshipChanged;
    public static Action<FriendshipLevel> OnBecameFriend;
    public static Action OnFriendshipTargetCleared;
    public static Action<Transform> OnFriendshipMaxed;

    private AudioManager audioManager;

    private void Awake()
    {
        currentFriendship = 0;
        maxFriendship = friendship;
        isFriend = false;

        if (animator == null)
            animator = GetComponent<Animator>();

        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
        }
    }

    public void SetEnemyId(string id)
    {
        enemyId = id;

        if (GameSession.IsEnemyFriend(enemyId))
        {
            RestoreFriendState();
        }
    }

    public void SetAsCurrentTarget()
    {
        OnFriendshipTargetChanged?.Invoke(this);
    }

    public void ClearAsCurrentTarget()
    {
        OnFriendshipTargetCleared?.Invoke();
    }

    public void AddFriendship(int amount)
    {
        if (isFriend)
            return;

        currentFriendship += amount;
        currentFriendship = Mathf.Clamp(currentFriendship, 0, maxFriendship);

        OnFriendshipChanged?.Invoke(currentFriendship, maxFriendship);

        PlayHeartEffect();

        if (currentFriendship >= maxFriendship)
        {
            BecomeFriend();
            Invoke(nameof(PlayEnemyVoice), 2f);
        }
    }

    private void PlayEnemyVoice()
    {
        if (audioManager != null)
        {
            audioManager.PlayRandomEnemyVoice();
        }
    }

    private void PlayHeartEffect()
    {
        if (heartEffectPrefab == null)
            return;

        Vector3 spawnPosition = heartSpawnPoint != null
            ? heartSpawnPoint.position
            : transform.position + heartOffset;

        GameObject heart = Instantiate(heartEffectPrefab, spawnPosition, Quaternion.identity, transform);
        Destroy(heart, heartLifetime);
    }

    private void BecomeFriend()
    {
        if (isFriend)
            return;

        isFriend = true;
        currentFriendship = maxFriendship;

        GameSession.MarkEnemyAsFriend(enemyId);

        ApplyFriendState();

        OnBecameFriend?.Invoke(this);
        OnFriendshipMaxed?.Invoke(transform);
    }

    private void RestoreFriendState()
    {
        isFriend = true;
        currentFriendship = maxFriendship;

        ApplyFriendState();
    }

    private void ApplyFriendState()
    {
        if (enemyCollider == null)
        {
            enemyCollider = GetComponent<Collider2D>();
        }

        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player != null)
        {
            Collider2D playerCol = player.GetComponent<Collider2D>();
            if (enemyCollider != null && playerCol != null)
            {
                Physics2D.IgnoreCollision(enemyCollider, playerCol, true);
            }
        }

        // Play friend animation
        if (animator != null)
        {
            animator.SetBool(friendBoolName, true);
        }

        // Optional: stop enemy scripts
        if (enemyAttackScript != null)
        {
            enemyAttackScript.enabled = false;
        }

        if (enemyMovementScript != null)
        {
            enemyMovementScript.enabled = false;
        }
    }
}
