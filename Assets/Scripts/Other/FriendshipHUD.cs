using UnityEngine;
using UnityEngine.UI;

public class FriendshipHUD : MonoBehaviour
{
    [SerializeField] private Slider friendshipBar;

    private void OnEnable()
    {
        FriendshipLevel.OnFriendshipTargetChanged += SetupFriendshipBar;
        FriendshipLevel.OnFriendshipChanged += UpdateFriendshipBar;
        FriendshipLevel.OnFriendshipTargetCleared += ClearFriendshipBar;
    }

    private void OnDisable()
    {
        FriendshipLevel.OnFriendshipTargetChanged -= SetupFriendshipBar;
        FriendshipLevel.OnFriendshipChanged -= UpdateFriendshipBar;
        FriendshipLevel.OnFriendshipTargetCleared -= ClearFriendshipBar;
    }

    private void SetupFriendshipBar(FriendshipLevel friendshipLevel)
    {
        friendshipBar.minValue = 0f;
        friendshipBar.maxValue = 1f;
        friendshipBar.value = (float)friendshipLevel.currentFriendship / friendshipLevel.maxFriendship;
        friendshipBar.value = Mathf.Clamp01(friendshipBar.value);

        gameObject.SetActive(true);

        Debug.Log("Friendship HUD tracking: " + friendshipLevel.gameObject.name);
    }

    private void UpdateFriendshipBar(int currentFriendship, int maxFriendship)
    {
        friendshipBar.value = (float)currentFriendship / maxFriendship;
        friendshipBar.value = Mathf.Clamp01(friendshipBar.value);

        Debug.Log("Friendship bar updated to: " + friendshipBar.value);
    }

    private void ClearFriendshipBar()
    {
        friendshipBar.value = 0f;
        gameObject.SetActive(false);

        Debug.Log("Friendship HUD cleared");
    }
}