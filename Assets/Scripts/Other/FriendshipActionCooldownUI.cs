using System.Collections;
using UnityEngine;

public class FriendshipActionCooldownUI : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator complimentAnimator;
    [SerializeField] private Animator kissAnimator;

    [Header("Cooldown Times")]
    [SerializeField] private float complimentCooldown = 1.5f;
    [SerializeField] private float kissCooldown = 3f;

    [Header("Animator Trigger Names")]
    [SerializeField] private string complimentTriggerName = "UseCompliment";
    [SerializeField] private string kissTriggerName = "UseKiss";

    private bool complimentOnCooldown = false;
    private bool kissOnCooldown = false;

    public bool TryUseCompliment()
    {
        if (complimentOnCooldown)
            return false;

        StartCoroutine(RunComplimentCooldown());
        return true;
    }

    public bool TryUseKiss()
    {
        if (kissOnCooldown)
            return false;

        StartCoroutine(RunKissCooldown());
        return true;
    }

    private IEnumerator RunComplimentCooldown()
    {
        complimentOnCooldown = true;

        if (complimentAnimator != null)
        {
            complimentAnimator.ResetTrigger(complimentTriggerName);
            complimentAnimator.SetTrigger(complimentTriggerName);
        }

        yield return new WaitForSeconds(complimentCooldown);

        complimentOnCooldown = false;
    }

    private IEnumerator RunKissCooldown()
    {
        kissOnCooldown = true;

        if (kissAnimator != null)
        {
            kissAnimator.ResetTrigger(kissTriggerName);
            kissAnimator.SetTrigger(kissTriggerName);
        }

        yield return new WaitForSeconds(kissCooldown);

        kissOnCooldown = false;
    }
}