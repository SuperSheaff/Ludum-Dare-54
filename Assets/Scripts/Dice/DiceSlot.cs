using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiceSlot : MonoBehaviour
{
    public BattleController BattleController { get; private set; }

    public Dice DiceInSlot;
    public string SlotType;
    public bool hasDiceInSlot = false;

    // Start is called before the first frame update
    void Start()
    {
        BattleController  = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDiceToSlot(Dice DiceToAdd)
    {
        if (hasDiceInSlot)
        {
            DiceInSlot.SelfDelete();
            hasDiceInSlot = false;
        }

        BattleController.EndTurnButton.SetCanPressButton(true);
        DiceInSlot = DiceToAdd;
        hasDiceInSlot = true;
    }

    public void SetHasDiceInSlot(bool value)
    {
        hasDiceInSlot = value;
    }
}
