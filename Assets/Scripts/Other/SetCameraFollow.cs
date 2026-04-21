using System.Collections;
using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineStateDrivenCamera vCam;
    [SerializeField] private float focusDuration = 20f;
    [SerializeField] private string happyTriggerName = "happy";

    private Transform playerTransform;
    private Animator playerAnimator;
    private Coroutine focusCoroutine;

    private void OnEnable()
    {
        GameController.OnPlayerSpawned += SetFollow;
        FriendshipLevel.OnFriendshipMaxed += FocusEnemyTemporarily;
    }

    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetFollow;
        FriendshipLevel.OnFriendshipMaxed -= FocusEnemyTemporarily;
    }

    private void SetFollow(GameObject player)
    {


        playerTransform = player.transform;
        playerAnimator = player.GetComponent<Animator>();

        vCam.Follow = playerTransform;
        vCam.m_AnimatedTarget = playerAnimator;
    }

    private void FocusEnemyTemporarily(Transform enemy)
    {


        if (enemy == null || playerTransform == null)
            return;

        if (focusCoroutine != null)
        {
            StopCoroutine(focusCoroutine);
        }

        focusCoroutine = StartCoroutine(FocusRoutine(enemy));
    }

    private IEnumerator FocusRoutine(Transform enemy)
    {


        vCam.Follow = enemy;

        Animator enemyAnimator = enemy.GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.ResetTrigger(happyTriggerName);
            enemyAnimator.SetTrigger(happyTriggerName);
     
        }
        else
        {
            Debug.LogWarning("No Animator found on enemy: " + enemy.name);
        }

        yield return new WaitForSeconds(focusDuration);

        if (playerTransform != null)
        {
  
            vCam.Follow = playerTransform;
            vCam.m_AnimatedTarget = playerAnimator;
        }

        focusCoroutine = null;
    }
}