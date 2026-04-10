using System;
using UnityEngine;

public class PlayerFriendshipInteraction : MonoBehaviour
{
    public static event Action ComplimentUsed;
    public static event Action KissUsed;

    [Header("Input")]
    [SerializeField] private KeyCode complimentKey = KeyCode.E;
    [SerializeField] private KeyCode kissKey = KeyCode.F;

    [Header("Friendship Gain")]
    [SerializeField] private int complimentAmount = 2;
    [SerializeField] private int kissAmount = 5;

    private FriendshipLevel currentEnemyFriendship;

    private bool canUseCompliment = true;
    private bool canUseKiss = true;

    private void Update()
    {
        if (Input.GetKeyDown(complimentKey))
        {
            Debug.Log("E pressed");

            if (currentEnemyFriendship == null)
            {
                Debug.LogWarning("No enemy in range.");
                return;
            }

            if (!canUseCompliment)
            {
                Debug.Log("Compliment not ready yet.");
                return;
            }

            currentEnemyFriendship.AddFriendship(complimentAmount);

            Debug.Log("Invoking ComplimentUsed event");
            ComplimentUsed?.Invoke();
        }

        if (Input.GetKeyDown(kissKey))
        {
            Debug.Log("F pressed");

            if (currentEnemyFriendship == null)
            {
                Debug.LogWarning("No enemy in range.");
                return;
            }

            if (!canUseKiss)
            {
                Debug.Log("Kiss not ready yet.");
                return;
            }

            Debug.Log("Invoking KissUsed event");
            //canUseKiss = false;
            currentEnemyFriendship.AddFriendship(kissAmount);
            KissUsed?.Invoke();
        }
    }

    public void OnComplimentReady()
    {
        canUseCompliment = true;
        Debug.Log("Compliment ready again");
    }

    public void OnKissReady()
    {
        canUseKiss = true;
        Debug.Log("Kiss ready again");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FriendshipLevel friendshipLevel = other.GetComponentInParent<FriendshipLevel>();

        if (friendshipLevel == null)
            return;

        currentEnemyFriendship = friendshipLevel;
        currentEnemyFriendship.SetAsCurrentTarget();

       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        FriendshipLevel friendshipLevel = other.GetComponentInParent<FriendshipLevel>();

        if (friendshipLevel == null)
            return;

        if (friendshipLevel == currentEnemyFriendship)
        {
            currentEnemyFriendship.ClearAsCurrentTarget();
            currentEnemyFriendship = null;


        }
    }
}