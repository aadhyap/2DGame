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

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text dialogueText;

    [Header("Player Exit")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float exitX = 14f;

    [Header("Knight Shake")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeAmount = 0.08f;

    [Header("Scene Transition")]
    [SerializeField] private string gameplaySceneName = "SampleScene";

    private Vector3 knightStartPos;

    private void Start()
{
    Debug.Log("playerAnimator: " + playerAnimator);
    Debug.Log("knightAnimator: " + knightAnimator);
    Debug.Log("playerTransform: " + playerTransform);
    Debug.Log("dialogueText: " + dialogueText);

    knightStartPos = knightAnimator.transform.position;
    StartCoroutine(PlayIntroSequence());
}

    private IEnumerator PlayIntroSequence()
    {
        dialogueText.text = "";

        // Knight starts talking / yelling
        yield return SayKnight("MY SON...", "Yell", 1.3f, true);
        yield return SayKnight("GO FORTH AND PROVE YOURSELF.", "Yell", 1.7f, true);
        yield return SayKnight("We have a genocide to complete.", "Yell", 1.7f, true);

        // Player reacts
        yield return SayPlayer("...", "Surprised", 1.0f);
        yield return SayPlayer("me?", "Nervous", 1.2f);

        // Knight yells again
        yield return SayKnight("YES, YOU!", "Yell", 1.0f, true);
        yield return SayKnight("Take up my blade and rid our land of those wretched goblins", "Yell", 3.6f, true);
        yield return SayKnight("Fulfill your prophecy—rise as the next Goblin Slayer, and carry on my legacy", "Yell", 3.6f, true);
        yield return SayKnight("I will see you at sundown to collect the skulls", "Yell", 2.6f, true);

        // Player reaction
        yield return SayPlayer("okay!!", "Scared", 1.0f);

        dialogueText.text = "";

        // Player walks off screen
        playerAnimator.SetBool("isRunning", true);

        while (playerTransform.position.x < exitX)
        {
            playerTransform.position += Vector3.right * walkSpeed * Time.deltaTime;
            yield return null;
        }

        playerAnimator.SetBool("isRunning", false);

        // Small pause before cut
        yield return new WaitForSeconds(0.5f);

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