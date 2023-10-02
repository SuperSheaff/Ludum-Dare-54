using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorDice : MonoBehaviour
{
    public BattleController BattleController    { get; private set; }

    private Vector2 diceRange;
    private int uniqueDiceNumber;
    private TextMeshPro textMeshProComponent;
    private bool isHovered = false;


    private void Start()
    {
        // EnemyAnimator    = GetComponent<Animator>();

        // Transform HealthAmountText = transform.Find("Dice Amount");
        // if (HealthAmountText != null)
        // {
        //     // Access the TextMeshPro component within the child object
        //     textMeshProComponent = HealthAmountText.GetComponent<TextMeshPro>();

        //     if (textMeshProComponent != null)
        //     {
        //     }
        //     else
        //     {
        //         Debug.LogError("TextMeshPro component not found in childObject.");
        //     }
        // }
        // else
        // {
        //     Debug.LogError("ChildObject not found.");
        // }
        BattleController  = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();

    }
    
    private void Update()
    {
        // updateRollIndicator();

    }

    private void OnMouseDown()
    {
        // This code will execute when the object is clicked.
        Debug.Log("Object Clicked!");
        if (BattleController.GetCanRollDice())
        {
            RollThisDice();
            BattleController.diceIndicators.Remove(this);
        }
        // You can add more actions or logic here.
    }

    private void OnMouseEnter()
    {
        // This code runs when the mouse pointer enters the collider.
        isHovered = true;
        Debug.Log("Mouse entered!");
        
        // You can add code to change the appearance or behavior of the GameObject here.
    }

    private void OnMouseExit()
    {
        // This code runs when the mouse pointer exits the collider.
        isHovered = false;
        Debug.Log("Mouse exited!");
        // You can add code to revert any changes made in OnMouseEnter here.
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

    public void RollThisDice()
    {
        BattleController.RollNewDice(diceRange, uniqueDiceNumber);
        Destroy(gameObject);
    }

}
