using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorDice : MonoBehaviour
{
    public BattleController BattleController    { get; private set; }
    public Animator Animator                    { get; private set; }
    public FloatObjectX FloatObjectX            { get; private set; }

    private Vector2 diceRange;
    private int uniqueDiceNumber;
    private TextMeshPro textMeshProComponent;
    private bool isHovered = false;

    private void Start()
    {
        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Animator            = GetComponent<Animator>();
        FloatObjectX        = GetComponent<FloatObjectX>();

        Transform HealthAmountText = transform.Find("Tooltip");
        if (HealthAmountText != null)
        {
            // Access the TextMeshPro component within the child object
            textMeshProComponent = HealthAmountText.GetComponent<TextMeshPro>();

            if (textMeshProComponent != null)
            {
                textMeshProComponent.text = "Click to roll.\nRange: " + Mathf.RoundToInt(diceRange.x).ToString() + " to " + Mathf.RoundToInt(diceRange.y).ToString();
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
        updateToolTip();

    }

    private void updateToolTip()
    {
        Transform TargetIndicator = transform.Find("Tooltip");

        if (TargetIndicator != null)
        {
            if (isHovered && BattleController.GetCanRollDice())
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
        FloatObjectX.canMove = false;
        Animator.SetBool("isHover", true);
        Debug.Log("Mouse entered!");
        
        // You can add code to change the appearance or behavior of the GameObject here.
    }

    private void OnMouseExit()
    {
        // This code runs when the mouse pointer exits the collider.
        isHovered = false;
        FloatObjectX.canMove = true;
        Animator.SetBool("isHover", false);
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
