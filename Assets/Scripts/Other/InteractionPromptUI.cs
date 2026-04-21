using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [Header("Cover Boxes")]
    [SerializeField] private GameObject complimentCoverBox;
    [SerializeField] private GameObject kissCoverBox;

    public void SetInteractionAvailable(bool isAvailable)
    {
        if (complimentCoverBox != null)
        {
            complimentCoverBox.SetActive(!isAvailable);
        }

        if (kissCoverBox != null)
        {
            kissCoverBox.SetActive(!isAvailable);
        }
    }
}