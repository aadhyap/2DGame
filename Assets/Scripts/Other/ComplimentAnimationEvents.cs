using UnityEngine;

public class ComplimentAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string boolName = "canCompliment";

    private PlayerFriendshipInteraction player;

    private void OnEnable()
    {
        PlayerFriendshipInteraction.ComplimentUsed += PlayComplimentFill;
        GameController.OnPlayerSpawned += SetPlayerReference;
    }

    private void OnDisable()
    {
        PlayerFriendshipInteraction.ComplimentUsed -= PlayComplimentFill;
        GameController.OnPlayerSpawned -= SetPlayerReference;
    }

    private void SetPlayerReference(GameObject playerObject)
    {
        player = playerObject.GetComponent<PlayerFriendshipInteraction>();

        if (player != null)
        {
            Debug.Log("ComplimentAnimationEvents linked to player: " + player.gameObject.name + " | id=" + player.GetInstanceID());
        }
        else
        {
            Debug.LogWarning("PlayerFriendshipInteraction missing on spawned player object.");
        }
    }

    private void PlayComplimentFill()
    {
        if (animator == null)
        {
            Debug.LogWarning("Compliment animator is missing.");
            return;
        }

        Debug.Log("PlayComplimentFill called");

        animator.SetBool(boolName, false);

        Debug.Log("Set Animator bool " + boolName + " = false");
    }

    public void OnComplimentReady()
    {
        Debug.Log("OnComplimentReady animation event fired");

        if (animator != null)
        {
            animator.SetBool(boolName, true);
            Debug.Log("Set Animator bool " + boolName + " = true");
        }

        if (player != null)
        {
            Debug.Log("Calling reset on player object: " + player.gameObject.name + " | id=" + player.GetInstanceID());
            player.OnComplimentReady();
        }
        else
        {
            Debug.LogWarning("Player reference missing");
        }
    }
}