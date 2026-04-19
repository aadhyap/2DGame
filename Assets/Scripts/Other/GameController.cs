using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
}

public class GameController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("Knight")]
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private Transform knightSpawnPoint;

    [Header("Enemies")]
    [SerializeField] private EnemySpawnData[] enemySpawns;

    private GameObject player;
    private GameObject knight;
    private List<GameObject> enemies = new List<GameObject>();

    public static Action<GameObject> OnPlayerSpawned;
    public static Action<GameObject> OnEnemySpawned;

    private void Awake()
    {
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        knight = Instantiate(knightPrefab, knightSpawnPoint.position, Quaternion.identity);

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            if (enemySpawns[i].enemyPrefab == null || enemySpawns[i].spawnPoint == null)
                continue;

            GameObject enemy = Instantiate(
                enemySpawns[i].enemyPrefab,
                enemySpawns[i].spawnPoint.position,
                Quaternion.identity
            );

            enemies.Add(enemy);
        }
    }

    private void Start()
    {
        OnPlayerSpawned?.Invoke(player);

        foreach (GameObject enemy in enemies)
        {
            OnEnemySpawned?.Invoke(enemy);
        }
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