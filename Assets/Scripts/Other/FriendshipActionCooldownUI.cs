using UnityEngine;

public class FriendshipActionVisuals : MonoBehaviour
{
    [SerializeField] private Animator complimentAnimator;
    [SerializeField] private Animator kissAnimator;

    private void OnEnable()
    {
        PlayerFriendshipInteraction.ComplimentUsed += PlayCompliment;
        PlayerFriendshipInteraction.KissUsed += PlayKiss;
    }

    private void OnDisable()
    {
        PlayerFriendshipInteraction.ComplimentUsed -= PlayCompliment;
        PlayerFriendshipInteraction.KissUsed -= PlayKiss;
    }

    private void PlayCompliment()
    {
        if (complimentAnimator == null)
            return;

        complimentAnimator.ResetTrigger("isCompliment");
        complimentAnimator.SetTrigger("isCompliment");
    }

    private void PlayKiss()
    {
        if (kissAnimator == null)
            return;

        kissAnimator.ResetTrigger("isKissed");
        kissAnimator.SetTrigger("isKissed");
    }
}