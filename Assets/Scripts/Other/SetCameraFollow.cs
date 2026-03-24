using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineStateDrivenCamera vCam;

    private void OnEnable()
    {
        GameController.OnPlayerSpawned += SetFollow;
    }

    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetFollow;
    }

    private void SetFollow(GameObject player)
    {
        Debug.Log("Setting state-driven camera target to: " + player.name);

        vCam.Follow = player.transform;
        vCam.m_AnimatedTarget = player.GetComponent<Animator>();
    }
}