using UnityEngine;
using UnityEngine.SceneManagement;

public class KnightEndTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "IntroScene"; // set in inspector
    [SerializeField] private float delay = 1f; // optional delay

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            Debug.Log("Player reached knight → loading next scene");

            StartCoroutine(LoadNextScene());
        }
    }

    private System.Collections.IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}