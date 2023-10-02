using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int attackDamage = 10;
    private int maxHealth = 20;
    private int currentHealth = 20;
    private int currentBlock = 0;
    private int uniqueDiceNumber;

    private TextMeshPro healthTextObject;
    private TextMeshPro blockTextObject;

    public List<InventoryDice> diceInventory = new List<InventoryDice>();

    private void Start()
    {
        uniqueDiceNumber = 0;

        AddDiceToInventory(new Vector2((float)1, (float)6));
        AddDiceToInventory(new Vector2((float)1, (float)6));
        AddDiceToInventory(new Vector2((float)1, (float)6));

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
        currentBlock += amountToBlock;
    }

    public void ResetBlock()
    {
        currentBlock = 0;
    }

    public void Heal(int amountToHeal)
    {
        currentHealth += amountToHeal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DealDamage(int damageToDeal)
    {
        if (currentBlock > 0)
        {
            if (currentBlock >= damageToDeal)
            {
                currentBlock -= damageToDeal;
            }
            else 
            {
                currentHealth -= (damageToDeal - currentBlock);
                currentBlock = 0;
            }
        }
        else 
        {
            currentHealth -= damageToDeal;
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
