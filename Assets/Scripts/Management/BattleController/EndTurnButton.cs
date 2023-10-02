using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public BattleController BattleController    { get; private set; }
    public Animator Animator                    { get; private set; }

    private bool canPressButton = false;

    // Start is called before the first frame update
    void Start()
    {
        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Animator            = GetComponent<Animator>();
        Animator.SetBool("canPressButton", false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (canPressButton)
        {
            BattleController.AudioManager.PlayAudio("click", 0.8f);
            BattleController.CalculatePlayerTurn();
            canPressButton = false;
        }
    }

    private void OnMouseEnter()
    {
        if (canPressButton)
        {
            BattleController.AudioManager.PlayAudio("hover", 0.8f);
            Animator.SetBool("isHover", true);
        }
    }

    private void OnMouseExit()
    {
        Animator.SetBool("isHover", false);
    }

    public void SetCanPressButton(bool value)
    {
        canPressButton = value;
        Animator.SetBool("canPressButton", value);
    }
}


        