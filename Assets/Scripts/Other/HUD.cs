using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Slider healthbar;
    private int maxHealth;

    private void SetupHealthBar(GameObject player)
    {
        healthbar.value = healthbar.maxValue;
        maxHealth = player.GetComponent<PlayerHealth>().maxHealth;
        
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthbar.value = (float)currentHealth / maxHealth;
        healthbar.value = Mathf.Clamp01(healthbar.value);
        
    }
    private void OnEnable()
    {
        GameController.OnPlayerSpawned += SetupHealthBar;
        PlayerHealth.OnPlayerTakeDamage += UpdateHealthBar;
        
    }

    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetupHealthBar;
        PlayerHealth.OnPlayerTakeDamage -= UpdateHealthBar;
    }
}
