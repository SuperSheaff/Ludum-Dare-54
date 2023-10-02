using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public BattleController BattleController    { get; private set; }
    public Animator Animator                    { get; private set; }
    private ParticleSystem damageParticleSystem;

    private int attackDamage;
    private int maxHealth;
    private int currentHealth;
    private int enemyType;
    private bool isTarget;

    private GameObject TargetIndicator;
    private TextMeshPro textMeshProComponent;

    private void Start()
    {
        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Animator            = GetComponent<Animator>();

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

        Transform HealthIcon = transform.Find("Health Icon");
        if (HealthIcon != null)
        {
            // Access the TextMeshPro component within the child object
            damageParticleSystem = HealthIcon.GetComponent<ParticleSystem>();

            if (damageParticleSystem != null)
            {
                damageParticleSystem.Stop();
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
        if (BattleController.GetCanPickTarget() && !isTarget)
        {
            BattleController.ResetAllTargets();
            SetIsTarget(true);
            BattleController.AudioManager.PlayAudio("click", 0.8f);
        }
    }

    private void OnMouseEnter()
    {
        if (!isTarget)
        {
            Animator.SetBool("isHover", true);
            BattleController.AudioManager.PlayAudio("hover", 0.8f);
        }
    }

    private void OnMouseExit()
    {
        Animator.SetBool("isHover", false);
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

        if (damageToDeal > 0)
        {
            StartCoroutine(DamageParticles());
            BattleController.AudioManager.PlayAudio("hit", 0.3f);
        } else if (damageToDeal < 0)
        {
            BattleController.AudioManager.PlayAudio("heal", 0.8f);
        }

        currentHealth -= damageToDeal;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public IEnumerator DamageParticles()
    {
        damageParticleSystem.Play();
        yield return new WaitForSeconds(0.1f); // Adjust the delay duration as needed
        damageParticleSystem.Stop();
    }

}
