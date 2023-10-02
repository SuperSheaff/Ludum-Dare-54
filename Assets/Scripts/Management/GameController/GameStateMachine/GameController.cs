using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region State Variables

        public GameStateMachine             StateMachine            { get; private set; }

        public GameStartState               StartState              { get; private set; }
        public GamePlayerChooseTileState    PlayerChooseTileState   { get; private set; }
        public GameGenerateTileState        GenerateTileState       { get; private set; }
        public GamePlayerMoveState          PlayerMoveState         { get; private set; }
        public GameBattleState              BattleState             { get; private set; }
        public GameOverState                GameOverState           { get; private set; }
        public GameWinState                GameWinState           { get; private set; }
        public GameChooseRewardState        ChooseRewardState       { get; private set; }
        
    #endregion

    #region Stuff
        public GameObject       MiniPlayerPrefab;
        public GameObject       TilePrefab;
        public GameObject       RewardPrefab;
        public GameObject       GameOverScreen;
        public GameObject       GameWinScreen;
        public GameObject       CurrentHexes;

        public int gridWidth        = 5;
        public int gridHeight       = 5;
        public float hexRadiusX     = 2.8f;
        public float hexRadiusY     = 1.9f;

        public GameObject       Cursor;
        public MiniPlayer       MiniPlayer;

        public Transform       RewardSpotA;
        public Transform       RewardSpotB;
        public Reward          RewardA;
        public Reward          RewardB;


        private int previousDirection;
        private Vector3 currentHexTilePosition;
        private HexTile currentHexTile;
        private Vector3 miniPlayerTargetLocation;
        private int nextInteraction;

        public BattleController BattleController    { get; private set; }
        public CameraController CameraController    { get; private set; }
        public GameObject BattleCam                 { get; private set; }
        public GeneralAudioManager GameAudioManager { get; private set; }
        public Player Player                        { get; private set; }

    #endregion

    private void Awake() 
    {

        StateMachine            = new GameStateMachine();

        StartState              = new GameStartState(this, StateMachine, "Game Start");
        PlayerChooseTileState   = new GamePlayerChooseTileState(this, StateMachine, "Player Choose Tile");
        GenerateTileState       = new GameGenerateTileState(this, StateMachine, "Generate Tile");
        PlayerMoveState         = new GamePlayerMoveState(this, StateMachine, "Player Move");
        BattleState             = new GameBattleState(this, StateMachine, "Battling");
        GameOverState           = new GameOverState(this, StateMachine, "Game Over");
        GameWinState            = new GameWinState(this, StateMachine, "Game Win");
        ChooseRewardState       = new GameChooseRewardState(this, StateMachine, "Choose Reward");
    }

    // Start is called before the first frame update
    void Start()
    {
        GameAudioManager    = GetComponent<GeneralAudioManager>();
        // GridController      = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        // MainCamera          = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController    = GameObject.FindGameObjectWithTag("CMCamera").GetComponent<CameraController>();
        BattleCam           = GameObject.FindGameObjectWithTag("Battle Cam");

        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();
        Player              = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // GameAudioManager.PlaySound("theme");

        previousDirection   = 1;
        nextInteraction     = 0;

        generateStartingTile();
        MiniPlayer = Instantiate(MiniPlayerPrefab, currentHexTilePosition, Quaternion.identity).GetComponent<MiniPlayer>();
        CameraController.SetCameraLookAt(MiniPlayer.transform);

        StateMachine.Initialize(GenerateTileState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();   
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();    
    }

    private void generateStartingTile()
    {
        HexTile startingHexTile = Instantiate(TilePrefab, transform.position, Quaternion.identity).GetComponent<HexTile>();
        startingHexTile.xPos = 0;
        startingHexTile.yPos = 0;
        startingHexTile.tileDirection = 1;
        startingHexTile.transform.SetParent(transform);
        startingHexTile.SetTileType("empty");
        
        SetCurrentHexTile(startingHexTile);
        SetCurrentHexTilePosition(startingHexTile.transform.position);
    }

    #region Other Methods

        public void GenerateHexTile(Vector3 position, int xPos, int yPos, int tileDirection)
        {
            HexTile newHexTile = Instantiate(TilePrefab, position, Quaternion.identity).GetComponent<HexTile>();
            newHexTile.xPos = xPos;
            newHexTile.yPos = yPos;
            newHexTile.tileDirection = tileDirection;
            newHexTile.SetTileType(GenerateRandomTileType());
            newHexTile.transform.SetParent(transform);
        }

        public string GenerateRandomTileType()
        {
            int randomNumber    = Random.Range(0, 100); // Generate a random number between 0 and 99
            string returnType;

            if (BattleController.GetBattlesWon() > 22)
            {
                returnType = "win";
            }
            else
            {
                if (randomNumber < 20)
                {
                    returnType = "empty";
                }
                else if (randomNumber < 90)
                {
                    returnType = "enemy";
                }
                else
                {
                    returnType = "chest";
                }
            }

            return returnType;
        }

        public string GenerateRandomRewardType()
        {
            int randomNumber = Random.Range(0, 100); // Generate a random number between 0 and 99
            string returnType;
            
            if (randomNumber < 60)
            {
                returnType = "maxhp";
            }
            else if (randomNumber < 80)
            {
                returnType = "heal";
            }
            else
            {
                returnType = "dice";
            }

            return returnType;
        }

    #endregion

    #region Set Methods

        public void SetMiniPlayerTargetLocation(Vector3 newLocation)
        {
            miniPlayerTargetLocation = newLocation;
        }

        public void SetCurrentHexTilePosition(Vector3 newPosition)
        {
            currentHexTilePosition = newPosition;
        }

        public void SetCurrentHexTile(HexTile newHexTile)
        {
            currentHexTile = newHexTile;
            newHexTile.transform.SetParent(CurrentHexes.transform);
            currentHexTile.isAvailable = false;
        }

        public void SetNextInteraction(int interactionType)
        {
            nextInteraction = interactionType;
        }

        public void SetPreviousDirection(int newDirection) 
        {
            previousDirection = newDirection;
        }

        public void SetAvailableHexTiles()
        {
            foreach (Transform child in transform)
            {
                // Check if the child has a HexTile component
                HexTile hexTile = child.GetComponent<HexTile>();
                
                if (hexTile != null)
                {
                    if ((hexTile.xPos == currentHexTile.xPos - 1 && hexTile.yPos == currentHexTile.yPos) ||
                        (hexTile.xPos == currentHexTile.xPos && hexTile.yPos == currentHexTile.yPos + 1) ||
                        (hexTile.xPos == currentHexTile.xPos + 1 && hexTile.yPos == currentHexTile.yPos + 1))             
                    {
                        hexTile.isAvailable = true;
                    }
                    else
                    {
                        hexTile.isAvailable = false;
                    }
                }
            }
        }   

    #endregion

    #region Get Methods

        public Vector3 GetMiniPlayerTargetLocation()
        {
            return miniPlayerTargetLocation;
        }

        public Vector3 GetCurrentHexTilePosition()
        {
            return currentHexTilePosition;
        }

        public HexTile GetCurrentHexTile()
        {
            return currentHexTile;
        }

        public int GetNextInteraction()
        {
            return nextInteraction;
        }

        public int GetPreviousDirection() 
        {
            return previousDirection;
        }

        public HexTile GetFocusedOnHexTile()
        {
            Vector3 mousePosition       = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D     = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D[] hits         = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
            HexTile result              = null;

            if (hits.Length > 0)
            {
                hits.OrderByDescending(i => i.collider.transform.position.z);
            }

            foreach (var hit in hits) 
            {
                if (hit.collider.gameObject.GetComponent<HexTile>() != null)
                {
                    result = hit.collider.gameObject.GetComponent<HexTile>();
                    break;
                }
            }

            return result;
        }

        public void GenerateRewards()
        {

            string RewardChoiceA = GenerateRandomRewardType();
            string RewardChoiceB = "";

            RewardA = Instantiate(RewardPrefab, RewardSpotA.transform.position, Quaternion.identity).GetComponent<Reward>();
            RewardA.SetAwardType(RewardChoiceA);
            RewardA.SetHealReward(GenerateHealReward());
            RewardA.SetMaxHPReward(GenerateMaxHPReward());
            RewardA.SetDiceReward(GenerateDiceReward());

            if (RewardChoiceA == "maxhp")
            {
                int randomNumber = Random.Range(0, 100); // Generate a random number between 0 and 99
                if (randomNumber < 60)
                {
                    RewardChoiceB = "heal";
                }
                else
                {
                    RewardChoiceB = "dice";
                }
            }

            if (RewardChoiceA == "heal")
            {
                int randomNumber = Random.Range(0, 100); // Generate a random number between 0 and 99
                if (randomNumber < 60)
                {
                    RewardChoiceB = "maxhp";
                }
                else
                {
                    RewardChoiceB = "dice";
                }
            }

            if (RewardChoiceA == "dice")
            {
               RewardChoiceB = GenerateRandomRewardType();
            }

            RewardB = Instantiate(RewardPrefab, RewardSpotB.transform.position, Quaternion.identity).GetComponent<Reward>();
            RewardB.SetAwardType(RewardChoiceB);
            RewardB.SetHealReward(GenerateHealReward());
            RewardB.SetMaxHPReward(GenerateMaxHPReward());
            RewardB.SetDiceReward(GenerateDiceReward());
        }

        public void GiveReward(string awardtype, int maxhp, int heal, Vector2 dice)
        {
            if (awardtype == "maxhp")
            {
                Player.SetMaxHealth(Player.GetMaxHealth() + maxhp);
                StateMachine.ChangeState(GenerateTileState);
            }

            if (awardtype == "heal")
            {
                Player.Heal(heal);
                StateMachine.ChangeState(GenerateTileState);
            }

            if (awardtype == "dice")
            {
                if (Player.GetInventoryDices().Count() >= 6)
                {
                    Player.diceInventory.RemoveAt(0);
                    Player.AddDiceToInventory(dice);
                    StateMachine.ChangeState(GenerateTileState);
                }
                else
                {
                    Player.AddDiceToInventory(dice);
                    StateMachine.ChangeState(GenerateTileState);
                }
            }
        }
        
        public int GenerateHealReward()
        {
            return Random.Range(1, Player.BattleController.GetBattlesWon() + 1); 
        }

        public int GenerateMaxHPReward()
        {
            return Random.Range(1, Player.BattleController.GetBattlesWon() + 1);
        }

        public Vector2 GenerateDiceReward()
        {
            int x = 0;
            int xChance = Random.Range(0, 100);

            if (0 > xChance && xChance >= 10)
            {
                x = Random.Range(-4, -2);
            }
            else if (10 > xChance && xChance >= 80)
            {
                x = Random.Range(-1, 2);
            }
            else
            {
                x = Random.Range(2, 4);
            }

            int y = 0;
            int yChance = Random.Range(0, 100);

            if (0 > yChance && yChance >= 10)
            {
                y = Random.Range(16, 20);
            }
            else if (10 > yChance && yChance >= 80)
            {
                y = Random.Range(4, 9);
            }
            else
            {
                y = Random.Range(10, 15);
            }

            Vector2 reward = new Vector2(x, y);

            return reward;
        }

        public void RestartGame()
        {
            foreach (Transform child in transform)
            {
                // Destroy each child GameObject
                Destroy(child.gameObject);
            }

            foreach (Transform child in CurrentHexes.transform)
            {
                // Destroy each child GameObject
                Destroy(child.gameObject);
            }

            previousDirection   = 1;
            nextInteraction     = 0;

            generateStartingTile();
            MiniPlayer.transform.position = currentHexTile.transform.position;
            CameraController.SetCameraLookAt(MiniPlayer.transform);

            StateMachine.Initialize(GenerateTileState);
            
            BattleController.ResetEnemies();
            BattleController.ResetDice();
            BattleController.Reset();
            Player.ResetStats();
            GameOverScreen.SetActive(false);
            GameWinScreen.SetActive(false);
        }

        public void RemoveOldHexTiles()
        {
            foreach (Transform child in transform)
            {
                // Destroy each child GameObject
                if ((child.gameObject.transform.position.y + 24f) < currentHexTile.transform.position.y)
                {
                    child.GetComponent<HexTile>().SelfDestruct();
                }
            }
        }

    #endregion
}
