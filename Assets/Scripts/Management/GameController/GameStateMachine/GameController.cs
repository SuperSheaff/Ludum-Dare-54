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
        
    #endregion

    #region Stuff
        public GameObject       MiniPlayerPrefab;
        public GameObject       TilePrefab;

        public int gridWidth        = 5;
        public int gridHeight       = 5;
        public float hexRadiusX     = 2.8f;
        public float hexRadiusY     = 1.9f;

        public GameObject       Cursor;
        public MiniPlayer       MiniPlayer;

        private int previousDirection;
        private Vector3 currentHexTilePosition;
        private HexTile currentHexTile;
        private Vector3 miniPlayerTargetLocation;
        private int nextInteraction;

        public BattleController BattleController    { get; private set; }
        public CameraController CameraController    { get; private set; }
        public GameObject BattleCam                 { get; private set; }

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
    }

    // Start is called before the first frame update
    void Start()
    {
        // GameAudioManager    = GetComponent<AudioManager>();
        // GridController      = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        // MainCamera          = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController    = GameObject.FindGameObjectWithTag("CMCamera").GetComponent<CameraController>();
        BattleCam           = GameObject.FindGameObjectWithTag("Battle Cam");

        BattleController    = GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>();

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
        
        SetCurrentHexTilePosition(startingHexTile.transform.position);
        SetCurrentHexTile(startingHexTile);
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
            
            if (randomNumber < 33)
            {
                returnType = "chest";
            }
            else if (randomNumber < 66)
            {
                returnType = "enemy";
            }
            else
            {
                returnType = "empty";
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

    #endregion
}
