using UnityEngine;

public class PlayerFriendshipInteraction : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode complimentKey = KeyCode.E;
    [SerializeField] private KeyCode kissKey = KeyCode.F;

    [Header("Friendship Gain")]
    [SerializeField] private int complimentAmount = 2;
    [SerializeField] private int kissAmount = 5;

    private FriendshipLevel currentEnemyFriendship;

    private void Update()
    {
        Debug.Log("Current enemy is: " + (currentEnemyFriendship == null ? "NULL" : currentEnemyFriendship.gameObject.name));

        if (Input.GetKeyDown(complimentKey))
        {
            Debug.Log("E pressed");

            if (currentEnemyFriendship == null)
            {
                Debug.LogWarning("E pressed but no enemy is currently in range.");
                return;
            }

            Debug.Log("Complimenting: " + currentEnemyFriendship.gameObject.name);
            currentEnemyFriendship.AddFriendship(complimentAmount);
        }

        if (Input.GetKeyDown(kissKey))
        {
            Debug.Log("F pressed");

            if (currentEnemyFriendship == null)
            {
                Debug.LogWarning("F pressed but no enemy is currently in range.");
                return;
            }

            Debug.Log("Kissing: " + currentEnemyFriendship.gameObject.name);
            currentEnemyFriendship.AddFriendship(kissAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D with: " + other.gameObject.name + " tag: " + other.gameObject.tag);

        FriendshipLevel friendshipLevel = other.GetComponent<FriendshipLevel>();

        if (friendshipLevel == null)
        {
            Debug.LogWarning("No FriendshipLevel found on trigger object: " + other.gameObject.name);
            return;
        }

        currentEnemyFriendship = friendshipLevel;
        currentEnemyFriendship.SetAsCurrentTarget();

        Debug.Log("Entered interaction range of: " + other.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D with: " + other.gameObject.name);

        FriendshipLevel friendshipLevel = other.GetComponent<FriendshipLevel>();

        if (friendshipLevel != null && friendshipLevel == currentEnemyFriendship)
        {
            Debug.Log("Exited interaction range of: " + other.gameObject.name);

            currentEnemyFriendship.ClearAsCurrentTarget();
            currentEnemyFriendship = null;
        }
    }
}