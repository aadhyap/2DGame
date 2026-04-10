using UnityEngine;

public class KissAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Animator animator;
    [SerializeField] private string boolName = "canKiss";

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
        PlayerFriendshipInteraction.KissUsed += PlayKissFill;
    }

    private void OnDisable()
    {
        PlayerFriendshipInteraction.KissUsed -= PlayKissFill;
    }

    private void PlayKissFill()
    {
        if (animator == null)
        {
            Debug.LogWarning("Kiss animator is missing.");
            return;
        }

        Debug.Log("PlayKissFill called");

        // Start refill
        animator.SetBool(boolName, false);

        Debug.Log("Set Animator bool " + boolName + " = false");
    }

    public void OnKissReady()
    {
        Debug.Log("OnKissReady animation event fired");

        if (animator != null)
        {
            // Go back to idle / ready
            animator.SetBool(boolName, true);
            Debug.Log("Set Animator bool " + boolName + " = true");
        }

        if (player != null)
        {
            Debug.Log("Calling reset on player object: " + player.gameObject.name);
            player.OnKissReady();
        }
        else
        {
            Debug.LogWarning("Player reference missing");
        }
    }
}