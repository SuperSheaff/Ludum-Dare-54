using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public BattleController BattleController    { get; private set; }
    public GameController GameController        { get; private set; }
    public Animator Animator                    { get; private set; }

    private string awardtype;
    private int healReward;
    private int maxhpReward;
    private Vector2 diceReward;

    private TextMeshPro rewardTitle;

    private void Start()
    {
        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        GameController      = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Animator            = GetComponent<Animator>();

        Transform RewardTitleObject = transform.Find("Reward Title");
        if (RewardTitleObject != null)
        {
            // Access the TextMeshPro component within the child object
            rewardTitle = RewardTitleObject.GetComponent<TextMeshPro>();

            if (rewardTitle != null)
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
        UpdateRewardText();
    }

    private void OnMouseDown()
    {
        BattleController.AudioManager.PlayAudio("click", 0.8f);
        GameController.GiveReward(awardtype, maxhpReward, healReward, diceReward);
    }

    private void OnMouseEnter()
    {
        Animator.SetBool("isHover", true);
        BattleController.AudioManager.PlayAudio("hover", 0.8f);
    }

    private void OnMouseExit()
    {
        Animator.SetBool("isHover", false);
    }

    public void UpdateRewardText()
    {
        if (awardtype == "maxhp")
        {
            rewardTitle.text = "Increase Max HP by " + maxhpReward.ToString();
        }

        if (awardtype == "heal")
        {
            rewardTitle.text = "Heal by " + healReward.ToString();
        }

        if (awardtype == "dice")
        {
            rewardTitle.text = "New Dice with a range from " + Mathf.RoundToInt(diceReward.x).ToString() + " to " + Mathf.RoundToInt(diceReward.y).ToString();
        }
    }

    public void SetAwardType(string value)
    {
        awardtype = value;
    }

    public void SetHealReward(int value)
    {
        healReward = value;
    }

    public void SetMaxHPReward(int value)
    {
        maxhpReward = value;
    }

    public void SetDiceReward(Vector2 value)
    {
        diceReward = value;
    }

    public void SelfDelete()
    {
        Destroy(gameObject);
    }

}
