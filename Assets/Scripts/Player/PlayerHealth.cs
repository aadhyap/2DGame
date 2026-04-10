using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int health = 10;
    public int currentHealth { get; private set;}
    public int maxHealth {get; private set;}

    public static Action<int> OnPlayerTakeDamage;
    public static Action OnPlayerDie;

    void Awake()
    {
        currentHealth = health;
        maxHealth = health;
    }

    // Update is called once per frame
    public void DamagePlayer(int damageAmount)
{
    

    currentHealth -= damageAmount;

    OnPlayerTakeDamage?.Invoke(currentHealth);

    if (currentHealth <= 0)
    {
        Debug.Log("Player is DEAD. Health <= 0");

        OnPlayerDie?.Invoke();
        Destroy(gameObject);
    }
}
}
