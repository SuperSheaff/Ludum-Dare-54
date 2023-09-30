using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    #region State Variables

        public BattleStateMachine       StateMachine            { get; private set; }

        public BattleStartState         BattleStartState        { get; private set; }
        public BattlePlayerRoundState   BattlePlayerRoundState  { get; private set; }
        public BattleOverState          BattleOverState         { get; private set; }
        public BattleWaitState          BattleWaitState         { get; private set; }
        
    #endregion

    #region Stuff
        public GameController GameController    { get; private set; }

        public int minEnemies = 1;
        public int maxEnemies = 3;
        public int minHealth = 3;
        public int maxHealth = 12;
        public int minDamage = 1;
        public int maxDamage = 10;

        public Transform enemySpotA;
        public Transform enemySpotB;
        public Transform enemySpotC;

        public GameObject enemyPrefab;

        public List<Enemy> enemies = new List<Enemy>();
    #endregion

    private void Awake() 
    {

        StateMachine            = new BattleStateMachine();

        BattleStartState        = new BattleStartState(this, StateMachine, "Battle Start");
        BattlePlayerRoundState  = new BattlePlayerRoundState(this, StateMachine, "Players Round");
        BattleOverState         = new BattleOverState(this, StateMachine, "Battle Over");
        BattleWaitState         = new BattleWaitState(this, StateMachine, "Battle Wait");
    }

    // Start is called before the first frame update
    void Start()
    {
        StateMachine.Initialize(BattleWaitState);

        GameController    = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
        int numEnemies = Random.Range(minEnemies, maxEnemies + 1);
        Vector3 spawnLocation = enemySpotB.position;
        
        for (int i = 0; i < numEnemies; i++)
        {
            switch (i)
            {
                case 0:
                    spawnLocation = enemySpotB.position;
                    break;

                case 1:
                    spawnLocation = enemySpotA.position;
                    break;

                case 2:
                    spawnLocation = enemySpotC.position;
                    break;

                default:
                    spawnLocation = enemySpotB.position;
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

                // Add the enemy to the list
                enemies.Add(enemy);
            }
        }
    }
}
