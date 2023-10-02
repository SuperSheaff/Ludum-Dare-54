using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    #region State Variables

        public BattleStateMachine       StateMachine            { get; private set; }

        public BattleStartState         BattleStartState        { get; private set; }
        public BattlePlayerRoundState   BattlePlayerRoundState  { get; private set; }
        public BattleEnemyRoundState    BattleEnemyRoundState   { get; private set; }
        public BattleOverState          BattleOverState         { get; private set; }
        public BattleWaitState          BattleWaitState         { get; private set; }
        
    #endregion

    #region Stuff
        public GameController GameController    { get; private set; }
        public EndTurnButton EndTurnButton      { get; private set; }
        public AudioManager AudioManager        { get; private set; }
        public Player Player                    { get; private set; }
        public Dice RolledDice;

        public int minEnemies = 1;
        public int maxEnemies = 3;
        public int minHealth = 3;
        public int maxHealth = 12;
        public int minDamage = 1;
        public int maxDamage = 10;

        private int battlesWon = 0;

        private bool canRollDice = false;
        private bool enemiesAreDead;
        private bool canPickTarget;

        public Transform enemySpotA;
        public Transform enemySpotB;
        public Transform enemySpotC;
        public Transform enemySpotD;
        public Transform enemySpotE;

        public Transform diceRollSpot;

        public Transform diceIndicatorSpotA;
        public Transform diceIndicatorSpotB;
        public Transform diceIndicatorSpotC;
        public Transform diceIndicatorSpotD;
        public Transform diceIndicatorSpotE;
        public Transform diceIndicatorSpotF;
        public Transform EnemyParent;

        public DiceSlot DiceSlotA;
        public DiceSlot DiceSlotB;
        public DiceSlot DiceSlotC;

        public GameObject enemyPrefab;
        public GameObject dicePrefab;
        public GameObject diceIndicatorPrefab;

        public List<Enemy> enemies = new List<Enemy>();
        public List<IndicatorDice> diceIndicators = new List<IndicatorDice>();
    #endregion

    private void Awake() 
    {

        StateMachine            = new BattleStateMachine();

        BattleStartState        = new BattleStartState(this, StateMachine, "Battle Start");
        BattlePlayerRoundState  = new BattlePlayerRoundState(this, StateMachine, "Players Round");
        BattleEnemyRoundState   = new BattleEnemyRoundState(this, StateMachine, "Enemy Round");
        BattleOverState         = new BattleOverState(this, StateMachine, "Battle Over");
        BattleWaitState         = new BattleWaitState(this, StateMachine, "Battle Wait");
    }

    // Start is called before the first frame update
    void Start()
    {
        StateMachine.Initialize(BattleWaitState);

        GameController  = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        EndTurnButton   = GameObject.FindGameObjectWithTag("EndTurnButton").GetComponent<EndTurnButton>();
        Player          = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        AudioManager    = GetComponent<AudioManager>();

    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();   
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();    
    }

    #region Set Methods

    #endregion

    public void GenerateEnemies()
    {

        BalanceEnemies();

        int numEnemies = Random.Range(minEnemies, maxEnemies + 1);
        Vector3 spawnLocation = enemySpotA.position;
        Transform newEnemyParent = enemySpotA;
        bool isTarget = false;
        
        for (int i = 0; i < numEnemies; i++)
        {
            switch (i)
            {
                case 0:
                    spawnLocation = enemySpotA.position;
                    newEnemyParent = enemySpotA;
                    isTarget = true;
                    break;

                case 1:
                    spawnLocation = enemySpotB.position;
                    newEnemyParent = enemySpotB;
                    isTarget = false;
                    break;

                case 2:
                    spawnLocation = enemySpotC.position;
                    newEnemyParent = enemySpotC;
                    isTarget = false;
                    break;

                case 3:
                    spawnLocation = enemySpotD.position;
                    newEnemyParent = enemySpotD;
                    isTarget = false;
                    break;

                case 4:
                    spawnLocation = enemySpotE.position;
                    newEnemyParent = enemySpotE;
                    isTarget = false;
                    break;

                default:
                    spawnLocation = enemySpotA.position;
                    newEnemyParent = enemySpotA;
                    isTarget = false;
                    break;
            }

            GameObject enemyObject = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            
            // Get the Enemy component from the instantiated object
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Randomly set the enemy's health and damage values
                enemy.SetMaxHealth(Random.Range(minHealth, maxHealth + 1));
                enemy.SetCurrentHealth(enemy.GetMaxHealth());
                enemy.SetAttackDamage(Random.Range(minDamage, maxDamage + 1));
                enemy.SetIsTarget(isTarget);
                enemy.transform.SetParent(newEnemyParent);

                // Add the enemy to the list
                enemies.Add(enemy);
            }
        }

        enemiesAreDead = false;
    }

    public void GenerateIndicatorDice()
    {
        List<InventoryDice> inventoryDices = Player.GetInventoryDices();

        Vector3 diceIndicatorPosition = diceIndicatorSpotA.position;

        for (int i = 0; i < inventoryDices.Count; i++)
        {
            switch (i)
            {
                case 0:
                    diceIndicatorPosition = diceIndicatorSpotA.position;
                    break;

                case 1:
                    diceIndicatorPosition = diceIndicatorSpotB.position;
                    break;

                case 2:
                    diceIndicatorPosition = diceIndicatorSpotC.position;
                    break;

                case 3:
                    diceIndicatorPosition = diceIndicatorSpotD.position;
                    break;

                case 4:
                    diceIndicatorPosition = diceIndicatorSpotE.position;
                    break;

                case 5:
                    diceIndicatorPosition = diceIndicatorSpotF.position;
                    break;

                default:
                    diceIndicatorPosition = diceIndicatorSpotA.position;
                    break;
            }

            IndicatorDice newIndicatorDice = Instantiate(diceIndicatorPrefab, diceIndicatorPosition, Quaternion.identity).GetComponent<IndicatorDice>();
            diceIndicators.Add(newIndicatorDice);
            newIndicatorDice.SetDiceRange(inventoryDices[i].GetDiceRange());
            newIndicatorDice.SetUniqueDiceNumber(inventoryDices[i].GetUniqueDiceNumber());
        }
    }

    public void MoveIndicatorDice()
    {   

    }

    public void RollNewDice(Vector2 diceRange, int uniqueDiceNumber)
    {
        RolledDice = Instantiate(dicePrefab, diceRollSpot.position, Quaternion.identity).GetComponent<Dice>();
        RolledDice.SetRolledAmount(Random.Range((int)diceRange.x, (int)diceRange.y + 1));
        SetCanRollDice(false);
    }

    public bool GetCanRollDice()
    {
        return canRollDice;
    }

    public void SetCanRollDice(bool value)
    {
        canRollDice = value;
    }

    public bool GetCanPickTarget()
    {
        return canPickTarget;
    }

    public void SetCanPickTarget(bool value)
    {
        canPickTarget = value;
    }

    public void CalculatePlayerTurn()
    {
        StartCoroutine(PlayerTurn());
    }

    public void DealDamageToTargetEnemy(int amountToDamage)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.GetIsTarget())
            {
                enemy.DealDamage(amountToDamage);
                CheckIfEnemyIsDead(enemy);
                break;
            }
        }
    }

    public void CheckIfEnemyIsDead(Enemy enemy)
    {
        if (enemy.GetCurrentHealth() <= 0)
        {
            Destroy(enemy.gameObject);
            enemies.Remove(enemy);
            if (enemies.Count() > 0)
            {
                enemies[0].SetIsTarget(true);
            }
            else 
            {
                enemiesAreDead = true;
            }
        }
    }

    public void HandleEnemyAttacks()
    {
        StartCoroutine(EnemiesAttackOneByOne());
    }

    private IEnumerator EnemiesAttackOneByOne()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay duration as needed

        foreach (Enemy enemy in enemies)
        {
            // Optionally, you can play an attack animation or perform other actions here
            // For example: enemy.PlayAttackAnimation();

            // Subtract the enemy's attack damage from the player's health
            enemy.Animator.SetTrigger("attack");
            yield return new WaitForSeconds(0.1f); // Adjust the delay duration as needed
            Player.DealDamage(enemy.GetAttackDamage());

            yield return new WaitForSeconds(1.0f); // Adjust the delay duration as needed

            // Check if playerHealth is less than or equal to 0 (player is defeated)
            if (Player.GetCurrentHealth() <= 0)
            {
                StateMachine.ChangeState(BattleWaitState);
                GameController.StateMachine.ChangeState(GameController.GameOverState);
                break; // Exit the loop since the player is defeated
            }
        }

        StateMachine.ChangeState(BattlePlayerRoundState);
    }

    private IEnumerator PlayerTurn()
    {
        int amountToDamage;
        int amountToBlock;
        int amountToHeal;

        if (DiceSlotA.DiceInSlot != null)
        {
            amountToDamage = DiceSlotA.DiceInSlot.GetRolledAmount();
        } else {
            amountToDamage = 0;
        }

        if (DiceSlotB.DiceInSlot != null)
        {
            amountToBlock = DiceSlotB.DiceInSlot.GetRolledAmount();
        } else {
            amountToBlock = 0;
        }

        if (DiceSlotC.DiceInSlot != null)
        {
            amountToHeal = DiceSlotC.DiceInSlot.GetRolledAmount();
        } else {
            amountToHeal = 0;
        }

        if (amountToHeal != 0 && !(amountToHeal > 0 && (Player.GetMaxHealth() == Player.GetCurrentHealth())))
        {
            Player.Heal(amountToHeal);
            yield return new WaitForSeconds(1.0f); // Adjust the delay duration as needed
        }

        if (amountToBlock > 0)
        {            
            Player.Block(amountToBlock);
            yield return new WaitForSeconds(1.0f); // Adjust the delay duration as needed
        }

        if (amountToDamage != 0)
        {
            Player.Animator.SetTrigger("attack");
            yield return new WaitForSeconds(0.1f); // Adjust the delay duration as needed
            DealDamageToTargetEnemy(amountToDamage);
            yield return new WaitForSeconds(1.0f); // Adjust the delay duration as needed
        }

        if (enemiesAreDead)
        {
            Player.ResetBlock();
            GameController.StateMachine.ChangeState(GameController.GenerateTileState);
            battlesWon ++;
            AudioManager.PlayAudio("win", 0.666f);
            StateMachine.ChangeState(BattleWaitState);
        }
        else 
        {
            StateMachine.ChangeState(BattleEnemyRoundState);
        }
    }

    public int GetBattlesWon()
    {
        return battlesWon;
    }

    public void ResetDice()
    {
        if (DiceSlotA.DiceInSlot != null)
        {
            DiceSlotA.DiceInSlot.SelfDelete();
            DiceSlotA.SetHasDiceInSlot(false);
        }

        if (DiceSlotB.DiceInSlot != null)
        {
            DiceSlotB.DiceInSlot.SelfDelete();
            DiceSlotB.SetHasDiceInSlot(false);
        }

        if (DiceSlotC.DiceInSlot != null)
        {
            DiceSlotC.DiceInSlot.SelfDelete();
            DiceSlotC.SetHasDiceInSlot(false);
        } 

        foreach (IndicatorDice dice in diceIndicators)
        {
            Destroy(dice.gameObject);
        }

        diceIndicators.Clear();
        Destroy(RolledDice);

        if (RolledDice != null)
        {
            RolledDice.SelfDelete();
        }
    }

    public void ResetEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        enemies.Clear();
    }

    public void ResetAllTargets()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.SetIsTarget(false);
        }
    }

    public void BalanceEnemies()
    {

        if (battlesWon == 0)
        {
            maxEnemies = 1;
            minHealth = 6;
            maxHealth = 6;
            minDamage = 2;
            maxDamage = 2;
        }

        if (0 < battlesWon && battlesWon <= 1)
        {
            maxEnemies = 1;
            minHealth = 6;
            maxHealth = 9;
            minDamage = 2;
            maxDamage = 3;
        }

        if (1 < battlesWon && battlesWon <= 4)
        {
            minEnemies = 2;
            maxEnemies = 2;
            minHealth = 7;
            maxHealth = 10;
            minDamage = 4;
            maxDamage = 5;
        }

        if (4 < battlesWon && battlesWon <= 7)
        {
            minEnemies = 2;
            maxEnemies = 2;
            minHealth = 8;
            maxHealth = 11;
            minDamage = 6;
            maxDamage = 8;
        }

        if (7 < battlesWon && battlesWon <= 10)
        {
            minEnemies = 2;
            maxEnemies = 2;
            minHealth = 9;
            maxHealth = 14;
            minDamage = 7;
            maxDamage = 10;
        }

    }


    public void Reset()
    {
        battlesWon = 0;
    }

}
