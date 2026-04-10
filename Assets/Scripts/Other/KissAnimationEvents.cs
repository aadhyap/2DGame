using UnityEngine;

public class KissAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string boolName = "canKiss";

    private PlayerFriendshipInteraction player;

    private void OnEnable()
    {
        PlayerFriendshipInteraction.KissUsed += PlayKissFill;
        GameController.OnPlayerSpawned += SetPlayerReference;
    }

    private void OnDisable()
    {
        PlayerFriendshipInteraction.KissUsed -= PlayKissFill;
        GameController.OnPlayerSpawned -= SetPlayerReference;
    }

    private void SetPlayerReference(GameObject playerObject)
    {
        player = playerObject.GetComponent<PlayerFriendshipInteraction>();

        if (player != null)
        {
            Debug.Log("KissAnimationEvents linked to player: " + player.gameObject.name + " | id=" + player.GetInstanceID());
        }
    }

    private void PlayKissFill()
    {
        if (animator == null)
        {
            Debug.LogWarning("Kiss animator is missing.");
            return;
        }

        animator.SetBool(boolName, false);
        Debug.Log("Set Animator bool " + boolName + " = false");
    }

    public void OnKissReady()
    {
        Debug.Log("OnKissReady animation event fired");

        if (animator != null)
        {
            animator.SetBool(boolName, true);
            Debug.Log("Set Animator bool " + boolName + " = true");
        }

        if (player != null)
        {
            Debug.Log("Calling reset on player object: " + player.gameObject.name + " | id=" + player.GetInstanceID());
            player.OnKissReset();
        }
        else
        {
            Debug.LogWarning("Player reference missing");
        }
    }
}