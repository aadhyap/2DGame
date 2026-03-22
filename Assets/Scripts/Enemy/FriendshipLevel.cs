using UnityEngine;
using System;

public class FriendshipLevel : MonoBehaviour
{
    [SerializeField] private int friendship = 10;

    public int currentFriendship { get; private set; }
    public int maxFriendship { get; private set; }
    public bool isFriend { get; private set; }

    public static Action<FriendshipLevel> OnFriendshipTargetChanged;
    public static Action<int, int> OnFriendshipChanged;
    public static Action<FriendshipLevel> OnBecameFriend;
    public static Action OnFriendshipTargetCleared;

    private void Awake()
    {
        currentFriendship = 0;
        maxFriendship = friendship;
        isFriend = false;
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
        if (isFriend) return;

        Debug.Log("=== AddFriendship CALLED ===");
        Debug.Log("Enemy: " + gameObject.name);
        Debug.Log("Friendship BEFORE: " + currentFriendship);
        Debug.Log("Amount added: " + amount);

        currentFriendship += amount;
        currentFriendship = Mathf.Clamp(currentFriendship, 0, maxFriendship);

        Debug.Log("Friendship AFTER: " + currentFriendship);

        OnFriendshipChanged?.Invoke(currentFriendship, maxFriendship);

        if (currentFriendship >= maxFriendship)
        {
            BecomeFriend();
        }
    }

    private void BecomeFriend()
    {
        if (isFriend) return;

        isFriend = true;

        Debug.Log(gameObject.name + " became a friend");

        OnBecameFriend?.Invoke(this);
    }
}