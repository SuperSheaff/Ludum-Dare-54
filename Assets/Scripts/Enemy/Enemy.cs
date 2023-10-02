using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public BattleController BattleController    { get; private set; }

    private int attackDamage;
    private int maxHealth;
    private int currentHealth;
    private int enemyType;
    private bool isTarget;

    private GameObject TargetIndicator;

    private TextMeshPro textMeshProComponent;

    private void Start()
    {
        BattleController  = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
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
        updateTargetIndicator();
    }

    private void OnMouseDown()
    {
        Debug.Log("test");
        if (BattleController.GetCanPickTarget())
        {
            BattleController.ResetAllTargets();
            SetIsTarget(true);
        }
    }

    private void updateHealthIndicator()
    {
        textMeshProComponent.text = currentHealth.ToString();
    }

    private void updateTargetIndicator()
    {
        Transform TargetIndicator = transform.Find("Target Indicator");

        if (TargetIndicator != null)
        {
            if (isTarget)
            {
                TargetIndicator.gameObject.SetActive(true);
            }
            else 
            {
                TargetIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("ChildObject not found.");
        }
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

    public void SetIsTarget(bool value)
    {
        isTarget = value;
    }

    public bool GetIsTarget()
    {
        return isTarget;
    }

    public void DealDamage(int damageToDeal)
    {
        currentHealth -= damageToDeal;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

}
