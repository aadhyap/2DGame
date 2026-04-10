using UnityEngine;

public class ComplimentAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Animator animator;
    [SerializeField] private string boolName = "canCompliment";

    private PlayerFriendshipInteraction player;

    private void Awake()
    {
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerFriendshipInteraction>();
        }

        if (player == null)
        {
            Debug.LogWarning("PlayerFriendshipInteraction component not found on playerObject.");
        }
    }

    private void OnEnable()
    {
        PlayerFriendshipInteraction.ComplimentUsed += PlayComplimentFill;
    }

    private void OnDisable()
    {
        PlayerFriendshipInteraction.ComplimentUsed -= PlayComplimentFill;
    }

    private void PlayComplimentFill()
    {
        if (animator == null)
        {
            Debug.LogWarning("Compliment animator is missing.");
            return;
        }

        Debug.Log("PlayComplimentFill called");

        // Start refill
        animator.SetBool(boolName, false);

        Debug.Log("Set Animator bool " + boolName + " = false");
    }

    public void OnComplimentReady()
    {
        Debug.Log("OnComplimentReady animation event fired");

        if (animator != null)
        {
            // Go back to idle / ready
            animator.SetBool(boolName, true);
            Debug.Log("Set Animator bool " + boolName + " = true");
        }

        if (player != null)
        {
            Debug.Log("Calling reset on player object: " + player.gameObject.name);
            player.OnComplimentReady();
        }
        else
        {
            Debug.LogWarning("Player reference missing");
        }
    }
}