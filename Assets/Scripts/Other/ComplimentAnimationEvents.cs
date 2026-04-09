using UnityEngine;

public class ComplimentAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerFriendshipInteraction player;
    [SerializeField] private Animator animator;
    [SerializeField] private string stateName = "complimentfill";

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

        animator.Play(stateName, 0, 0f);
    }

    public void OnComplimentReady()
    {
        if (player != null)
        {
            player.OnComplimentReady();
        }
    }
}