using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryDice : MonoBehaviour
{
    private Vector2 diceRange;
    private int uniqueDiceNumber;
    private TextMeshPro textMeshProComponent;

    private void Start()
    {
        // EnemyAnimator    = GetComponent<Animator>();

        Transform HealthAmountText = transform.Find("Dice Amount");
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
        // updateRollIndicator();

    }

    // private void updateRollIndicator()
    // {
    //     textMeshProComponent.text = diceRange.ToString();
    // }

    public void SetDiceRange(Vector2 range)
    {
        diceRange = range;
    }

    public Vector2 GetDiceRange()
    {
        return diceRange;
    }

    public void SetUniqueDiceNumber(int number)
    {
        uniqueDiceNumber = number;
    }

    public int GetUniqueDiceNumber()
    {
        return uniqueDiceNumber;
    }

}
