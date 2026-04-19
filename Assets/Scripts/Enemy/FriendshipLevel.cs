using UnityEngine;
using System;

public class FriendshipLevel : MonoBehaviour
{
    [SerializeField] private int friendship = 10;

    [Header("Heart Effect")]
    [SerializeField] private GameObject heartEffectPrefab;
    [SerializeField] private Vector3 heartOffset = new Vector3(0f, 2f, 0f);
    [SerializeField] private float heartLifetime = 1f;
    [SerializeField] private Transform heartSpawnPoint;
    [SerializeField] private Collider2D enemyCollider;
[SerializeField] private Collider2D playerCollider;

    public int currentFriendship { get; private set; }
    public int maxFriendship { get; private set; }
    public bool isFriend { get; private set; }

    public static Action<FriendshipLevel> OnFriendshipTargetChanged;
    public static Action<int, int> OnFriendshipChanged;
    public static Action<FriendshipLevel> OnBecameFriend;
    public static Action OnFriendshipTargetCleared;

    public static Action<Transform> OnFriendshipMaxed;
    AudioManager audioManager;

    private void Awake()
    {
        currentFriendship = 0;
        maxFriendship = friendship;
        isFriend = false;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

        Debug.Log("=== AddFriendship CALLED ===");
        Debug.Log("Enemy: " + gameObject.name);
        Debug.Log("Friendship BEFORE: " + currentFriendship);
        Debug.Log("Amount added: " + amount);

        currentFriendship += amount;
        currentFriendship = Mathf.Clamp(currentFriendship, 0, maxFriendship);

        Debug.Log("Friendship AFTER: " + currentFriendship);

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
    BecomeFriend();
    Debug.Log("RANDOM VOICE");
    audioManager.PlayRandomEnemyVoice();
}


    private void PlayHeartEffect()
{
    if (heartEffectPrefab == null)
    {
        Debug.LogWarning("Heart effect prefab is missing on " + gameObject.name);
        return;
    }

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

    OnBecameFriend?.Invoke(this);
    OnFriendshipMaxed?.Invoke(transform);
    }
}