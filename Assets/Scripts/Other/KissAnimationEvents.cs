using UnityEngine;

public class KissAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerFriendshipInteraction player;
    [SerializeField] private Animator animator;
    [SerializeField] private string stateName = "kissfill";

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

    Debug.Log("Before enabling, animator.enabled = " + animator.enabled);

    animator.enabled = true;

    Debug.Log("After enabling, animator.enabled = " + animator.enabled);

    animator.Play(stateName, 0, 0f);
    animator.Update(0f);

    Debug.Log("Tried to play state: " + stateName);
}

   public void OnKissReady()
{
    Debug.Log("OnKissReady animation event fired");

    if (player != null)
    {
        player.OnKissReady();
    }

    if (animator != null)
    {
        Debug.Log("Before disabling, animator.enabled = " + animator.enabled);
        animator.enabled = false;
        Debug.Log("After disabling, animator.enabled = " + animator.enabled);
    }
}
}