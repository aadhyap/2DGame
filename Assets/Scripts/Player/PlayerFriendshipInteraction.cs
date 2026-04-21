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
    [SerializeField] private int kissAmount = 4;

    private AudioManager audioManager;
    private FriendshipLevel currentEnemyFriendship;
    private InteractionPromptUI interactionPromptUI;

    private bool canUseCompliment = true;
    private bool canUseKiss = true;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        interactionPromptUI = FindFirstObjectByType<InteractionPromptUI>();
        Debug.Log("interactionPromptUI found: " + interactionPromptUI);
    }

    private void Start()
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetInteractionAvailable(false);
        }
    }

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

            canUseCompliment = false;

            audioManager.PlayRandomCompliment();
            currentEnemyFriendship.AddFriendship(complimentAmount);

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

            canUseKiss = false;

            audioManager.PlaySFX(audioManager.kiss);
            currentEnemyFriendship.AddFriendship(kissAmount);

            KissUsed?.Invoke();
        }
    }

    public void OnComplimentReset()
    {
        canUseCompliment = true;
        Debug.Log("Compliment ready again");
    }

    public void OnKissReset()
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

        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetInteractionAvailable(true);
        }
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

            if (interactionPromptUI != null)
            {
                interactionPromptUI.SetInteractionAvailable(false);
            }
        }
    }
}