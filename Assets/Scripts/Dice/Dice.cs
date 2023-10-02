using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public BattleController BattleController    { get; private set; }
    public Animator Animator                    { get; private set; }
    public FloatObject FloatObject              { get; private set; }

    private int rolledAmount;
    private bool isBeingDragged = false;
    private bool isDraggable = false;

    private TextMeshPro textMeshProComponent;

    private void Start()
    {
        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Animator            = GetComponent<Animator>();
        FloatObject         = GetComponent<FloatObject>();

        FloatObject.canMove = false;
    }

    private void Awake()
    {
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


    private void OnMouseDown()
    {
        if (isDraggable)
        {
            isBeingDragged = true;
            FloatObject.canMove = false;
        }
    }

    private void OnMouseUp()
    {
        if (isDraggable)
        {
            isBeingDragged = false;
            CheckDiceLocation();
        }
    }

    private void OnMouseEnter()
    {
        Animator.SetBool("isHover", true);
    }

    private void OnMouseExit()
    {
        Animator.SetBool("isHover", false);
    }

    private void Update()
    {

        if (isBeingDragged && isDraggable)
        {
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // Ensure the same z-coordinate as the dice

            // Move the dice to the mouse position
            transform.position = mousePosition;
        } 
    }

    private void updateRollIndicator()
    {
        textMeshProComponent.text = rolledAmount.ToString();
    }

    public void SetRolledAmount(int amount)
    {
        rolledAmount = amount;
    }

    public int GetRolledAmount()
    {
        return rolledAmount;
    }

    public void CheckDiceLocation()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("DiceSlot"))
            {
                // Snap the dice to the slot's position
                FloatObject.canMove = false;
                transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y, collider.transform.position.z);
                isDraggable = false;
                collider.GetComponent<DiceSlot>().AddDiceToSlot(this);
                BattleController.SetCanRollDice(true);
                return; // Exit the loop once a slot is found
            }
            else
            {
                // Return the dice to its original position
                transform.position = BattleController.diceRollSpot.position;
                FloatObject.canMove = true;
            }
        }
        
        // if (hit.collider != null && hit.collider.CompareTag("DiceSlot")) // Check if the hit object is a slot
        // {
        //     // Snap the dice to the slot's position
        //     transform.position = hit.collider.transform.position;
        // }
        // else
        // {
        //     // Return the dice to its original position
        //     transform.position = BattleController.diceRollSpot.position;
        // }
    }

    public void AnimationFinish()
    {
        isDraggable = true;
        FloatObject.canMove = true;
        updateRollIndicator();
    }

    public void SelfDelete()
    {
        Destroy(gameObject);
    }

}
