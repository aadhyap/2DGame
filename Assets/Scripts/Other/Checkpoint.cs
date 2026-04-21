using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Vector3 pos = respawnPoint != null ? respawnPoint.position : transform.position;
        GameSession.SetCheckpoint(pos);

        Debug.Log("Checkpoint saved at: " + pos);
    }
}