using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BattleController BattleController    { get; private set; }
    public Animator Animator                    { get; private set; }

    private int attackDamage = 10;
    private int maxHealth = 20;
    private int currentHealth = 20;
    private int currentBlock = 0;
    private int uniqueDiceNumber;

    private TextMeshPro healthTextObject;
    private TextMeshPro blockTextObject;
    private ParticleSystem blockParticleSystem;
    private ParticleSystem damageParticleSystem;
    private ParticleSystem healParticleSystem;

    public List<InventoryDice> diceInventory = new List<InventoryDice>();

    private void Start()
    {
        uniqueDiceNumber = 0;
        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Animator            = GetComponent<Animator>();

        AddDiceToInventory(new Vector2((float)1, (float)6));
        AddDiceToInventory(new Vector2((float)1, (float)7));
        AddDiceToInventory(new Vector2((float)1, (float)8));

        Transform HealthAmountText = transform.Find("Health Amount");
        if (HealthAmountText != null)
        {
            // Access the TextMeshPro component within the child object
            healthTextObject = HealthAmountText.GetComponent<TextMeshPro>();

            if (healthTextObject != null)
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
        if (HealthAmountText != null)
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

        Transform BlockAmountText = transform.Find("Block Amount");
        if (BlockAmountText != null)
        {
            // Access the TextMeshPro component within the child object
            blockTextObject = BlockAmountText.GetComponent<TextMeshPro>();

            if (blockTextObject != null)
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

        Transform BlockIcon = transform.Find("Block Icon");
        if (BlockIcon != null)
        {
            // Access the TextMeshPro component within the child object
            blockParticleSystem = BlockIcon.GetComponent<ParticleSystem>();

            if (blockParticleSystem != null)
            {
                blockParticleSystem.Stop();
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

        Transform HealIcon = transform.Find("Heal Icon");
        if (HealIcon != null)
        {
            // Access the TextMeshPro component within the child object
            healParticleSystem = HealIcon.GetComponent<ParticleSystem>();

            if (healParticleSystem != null)
            {
                healParticleSystem.Stop();
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
        updateBlockIndicator();
    }

    private void updateHealthIndicator()
    {
        healthTextObject.text = currentHealth.ToString();
    }

    private void updateBlockIndicator()
    {
        Transform BlockAmountText = transform.Find("Block Amount");
        Transform BlockIcon = transform.Find("Block Icon");

        if (currentBlock == 0)
        {
            BlockAmountText.gameObject.SetActive(false);
            BlockIcon.gameObject.SetActive(false);
        }
        else
        {
            BlockAmountText.gameObject.SetActive(true);
            BlockIcon.gameObject.SetActive(true);
            blockTextObject.text = currentBlock.ToString();
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

    public List<InventoryDice> GetInventoryDices()
    {
        return diceInventory;
    }

    public void AddDiceToInventory(Vector2 diceRange)
    {
        InventoryDice newDice = new InventoryDice(); 
        newDice.SetDiceRange(diceRange);
        newDice.SetUniqueDiceNumber(uniqueDiceNumber);
        uniqueDiceNumber++;
        diceInventory.Add(newDice);
    }

    public void Block(int amountToBlock)
    {
        if (amountToBlock > 0)
        {
            BattleController.AudioManager.PlayAudio("shield-hit", 0.8f);
            StartCoroutine(BlockParticles());
            currentBlock += amountToBlock;
        }

    }

    public void ResetBlock()
    {
        currentBlock = 0;
    }

    public void Heal(int amountToHeal)
    {
        if (amountToHeal > 0 && currentHealth < maxHealth)
        {
            StartCoroutine(HealParticles());
            BattleController.AudioManager.PlayAudio("heal", 0.8f);
        } 

        if (amountToHeal < 0)
        {
            StartCoroutine(DamageParticles());
            BattleController.AudioManager.PlayAudio("hit", 0.3f);
        } 

        currentHealth += amountToHeal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DealDamage(int damageToDeal)
    {
        if (damageToDeal > 0)
        {
            if (currentBlock > 0)
            {
                StartCoroutine(BlockParticles());
                if (currentBlock >= damageToDeal)
                {
                    BattleController.AudioManager.PlayAudio("shield-hit", 0.8f);
                    currentBlock -= damageToDeal;
                }
                else 
                {
                    BattleController.AudioManager.PlayAudio("hit", 0.3f);
                    StartCoroutine(DamageParticles());
                    currentHealth -= (damageToDeal - currentBlock);
                    currentBlock = 0;
                }
            }
            else 
            {
                BattleController.AudioManager.PlayAudio("hit", 0.3f);
                StartCoroutine(DamageParticles());
                currentHealth -= damageToDeal;
            }
        }

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

    public IEnumerator BlockParticles()
    {
        blockParticleSystem.Play();
        yield return new WaitForSeconds(0.1f); // Adjust the delay duration as needed
        blockParticleSystem.Stop();
    }

    public IEnumerator HealParticles()
    {
        healParticleSystem.Play();
        yield return new WaitForSeconds(0.5f); // Adjust the delay duration as needed
        healParticleSystem.Stop();
    }

    public void ResetStats()
    {
        attackDamage = 10;
        maxHealth = 20;
        currentHealth = 20;
        currentBlock = 0;
        uniqueDiceNumber = 0;
        
        diceInventory.Clear();
        AddDiceToInventory(new Vector2((float)1, (float)6));
        AddDiceToInventory(new Vector2((float)1, (float)7));
        AddDiceToInventory(new Vector2((float)1, (float)8));
    }

}
