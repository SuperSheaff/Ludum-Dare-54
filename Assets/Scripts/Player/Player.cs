using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int attackDamage = 10;
    private int maxHealth = 20;
    private int currentHealth;

    private TextMeshPro textMeshProComponent;

    private void Start()
    {
        // EnemyAnimator    = GetComponent<Animator>();

        Transform HealthAmountText = transform.Find("Health Amount");
        if (HealthAmountText != null)
        {
            // Access the TextMeshPro component within the child object
            textMeshProComponent = HealthAmountText.GetComponent<TextMeshPro>();

            if (textMeshProComponent != null)
            {
            }
            else
            {
                Debug.LogError("TextMeshPro component not found in childObject.");
            }
        }
        else
        {
            Debug.LogError("ChildObject not found.");
        }
    }

    private void Update()
    {
        updateHealthIndicator();
    }

    private void updateHealthIndicator()
    {
        textMeshProComponent.text = currentHealth.ToString();
    }

    public void SetAttackDamage(int amount)
    {
        attackDamage = amount;
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
    }

    public void SetCurrentHealth(int amount)
    {
        currentHealth = amount;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
