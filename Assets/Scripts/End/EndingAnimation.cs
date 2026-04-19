using System.Collections;
using UnityEngine;
using TMPro;

public class EndSceneController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject playerPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform knightSpawnPoint;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("UI")]

    [SerializeField] private TMP_Text creditsText;
    [SerializeField] private TMP_Text thanks;

    [Header("Animation State Names")]
    [SerializeField] private string knightStateName = "knight";
    [SerializeField] private string playerStateName = "player";

    [Header("Timing")]
    [SerializeField] private float delayBeforeKnight = 0.5f;
    [SerializeField] private float knightDuration = 6f;
    [SerializeField] private float delayBeforePlayer = 0.3f;
    [SerializeField] private float playerDuration = 2f;
    [SerializeField] private float delayBeforeCredits = 2f;

    [Header("Shake Settings")]
    [SerializeField] private float shakeAmount = 0.05f;
    [SerializeField] private float shakeSpeed = 40f;


    private GameObject knight;
    private GameObject player;

    private Animator knightAnimator;
    private Animator playerAnimator;

    private Vector3 knightStartPos;
    AudioManager audioManager;

    private void Start()
    {
        knight = Instantiate(knightPrefab, knightSpawnPoint.position, Quaternion.identity);
        knightAnimator = knight.GetComponent<Animator>();

        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerAnimator = player.GetComponent<Animator>();
        player.SetActive(false);



        if (creditsText != null)
            creditsText.gameObject.SetActive(false);

        if (thanks != null)
            thanks.gameObject.SetActive(false);

        knightStartPos = knight.transform.position;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        yield return new WaitForSeconds(delayBeforeKnight);

      

        if (knightAnimator != null)
        {
            knightAnimator.Play(knightStateName, 0, 0f);
            StartCoroutine(ShakeKnight(knightDuration));
        }

        audioManager.PlaySFX(audioManager.knightVoice);
        

        yield return new WaitForSeconds(knightDuration);

 

        if (knight != null)
            knight.SetActive(false);

        yield return new WaitForSeconds(delayBeforePlayer);

        if (player != null)
            player.SetActive(true);

        if (playerAnimator != null)
            playerAnimator.Play(playerStateName, 0, 0f);

        yield return new WaitForSeconds(playerDuration);
        yield return new WaitForSeconds(delayBeforeCredits);

        if (creditsText != null)
        {
            creditsText.gameObject.SetActive(true);
            creditsText.text =
                "GAME / ART / SFX - AADHYA PUTTUR\n" +
                "MUSIC - ARYAN PUTTUR";
        }
        yield return new WaitForSeconds(delayBeforeCredits);
        if (thanks != null)
        {
            thanks.gameObject.SetActive(true);
            thanks.text =
                "Thanks for playing <3";
        }
    }

    private IEnumerator ShakeKnight(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float x = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            float y = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;

            if (knight != null)
            {
                knight.transform.position = knightStartPos + new Vector3(x, y, 0f);
            }

            yield return null;
        }

        if (knight != null)
        {
            knight.transform.position = knightStartPos;
        }
    }
}