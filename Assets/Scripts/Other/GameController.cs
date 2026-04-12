using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject player;
    private GameObject enemy;
    public static Action<GameObject> OnPlayerSpawned;
    public static Action<GameObject> OnEnemySpawned;
    private void Awake()
    {
        player = Instantiate(playerPrefab);
        enemy =  Instantiate(enemyPrefab);
        
    }

    // Update is called once per frame
    private void Start()
    {
        OnPlayerSpawned?.Invoke(player);
        OnEnemySpawned?.Invoke(enemy);
        
    }

    private void ResetScene()
    {
        Invoke("ResetSceneDelay", 2f);
    }

    private void ResetSceneDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDie += ResetScene;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDie -= ResetScene;
    }
}
