using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroScene : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator knightAnimator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text dialogueText;

    [Header("Player Exit")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float offscreenPadding = 1.5f;

    [Header("Knight Shake")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeAmount = 0.08f;

    [Header("Scene Transition")]
    [SerializeField] private string gameplaySceneName = "SampleScene";

    private Vector3 knightStartPos;

    private void Start()
    {
        Debug.Log("Intro started");

        if (playerAnimator == null) Debug.LogError("playerAnimator missing");
        if (knightAnimator == null) Debug.LogError("knightAnimator missing");
        if (playerTransform == null) Debug.LogError("playerTransform missing");
        if (playerSpriteRenderer == null) Debug.LogError("playerSpriteRenderer missing");
        if (dialogueText == null) Debug.LogError("dialogueText missing");

        knightStartPos = knightAnimator.transform.position;

        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        dialogueText.text = "";

        yield return SayKnight("MY SON...", "Yell", 1.3f, true);
        yield return SayKnight("GO FORTH AND PROVE YOURSELF.", "Yell", 1.7f, true);
        yield return SayKnight("We have a genocide to complete.", "Yell", 1.7f, true);

        yield return SayPlayer("...", "Surprised", 1.0f);

        yield return SayKnight("Take up my blade and kill those wretched goblins", "Yell", 3.6f, true);
        yield return SayKnight("Fulfill your prophecy—rise as the next Goblin Slayer, and carry on my legacy", "Yell", 3.6f, true);
        yield return SayKnight("I will see you at sundown to collect the skulls", "Yell", 2.6f, true);

        dialogueText.text = "";

        playerAnimator.SetBool("isRunning", true);

        Camera cam = Camera.main;
        float cameraRightEdge = cam.transform.position.x + cam.orthographicSize * cam.aspect;
        float playerHalfWidth = playerSpriteRenderer.bounds.extents.x;
        float targetX = cameraRightEdge + playerHalfWidth + offscreenPadding;

        while (playerTransform.position.x < targetX)
        {
            playerTransform.position += Vector3.right * walkSpeed * Time.deltaTime;
            yield return null;
        }

        playerAnimator.SetBool("isRunning", false);

        yield return new WaitForSeconds(0.5f);

        Debug.Log("LOADING SampleScene NOW");
        SceneManager.LoadScene(gameplaySceneName);
    }

    private IEnumerator SayKnight(string line, string triggerName, float waitTime, bool shake)
    {
        dialogueText.text = line;

        if (!string.IsNullOrEmpty(triggerName))
        {
            knightAnimator.SetTrigger(triggerName);
        }

        if (shake)
        {
            yield return StartCoroutine(ShakeKnight());
        }

        yield return new WaitForSeconds(waitTime);
    }

    private IEnumerator SayPlayer(string line, string triggerName, float waitTime)
    {
        dialogueText.text = line;

        if (!string.IsNullOrEmpty(triggerName))
        {
            playerAnimator.SetTrigger(triggerName);
        }

        yield return new WaitForSeconds(waitTime);
    }

    private IEnumerator ShakeKnight()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0f
            );

            knightAnimator.transform.position = knightStartPos + randomOffset;
            yield return null;
        }

        knightAnimator.transform.position = knightStartPos;
    }
}