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
        public GamePlayerMoveState          PlayerMoveState         { get; private set; }
        public GameOverState                GameOverState           { get; private set; }
        
    #endregion

    #region Stuff
        public GameObject       MiniPlayerPrefab;
        public GameObject       TilePrefab;

        public int gridWidth        = 5;
        public int gridHeight       = 5;
        public float hexRadiusX     = 2.8f;
        public float hexRadiusY     = 1.8f;

        public GameObject       Cursor;
        public MiniPlayer       MiniPlayer;

        private Vector3 currentTilePosition;
        private Vector3 miniPlayerTargetLocation;
    #endregion

    private void Awake() 
    {

        StateMachine            = new GameStateMachine();

        StartState              = new GameStartState(this, StateMachine, "Game Start");
        PlayerChooseTileState   = new GamePlayerChooseTileState(this, StateMachine, "Player Choose Tile");
        PlayerMoveState         = new GamePlayerMoveState(this, StateMachine, "Player Move");
        GameOverState           = new GameOverState(this, StateMachine, "Game Over");
    }

    // Start is called before the first frame update
    void Start()
    {
        // GameAudioManager    = GetComponent<AudioManager>();
        // GridController      = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        // MainCamera          = GameObject.FindGameObjectWithTag("MainCamera");
        // CameraController    = GameObject.FindGameObjectWithTag("CMCamera").GetComponent<CameraController>();

        // GameAudioManager.PlaySound("theme");

        // GridController.SetGridNodes();

        generateStartingTile();
        generateThreeChoiceTiles();

        MiniPlayer = Instantiate(MiniPlayerPrefab, currentTilePosition, Quaternion.identity).GetComponent<MiniPlayer>();;

        StateMachine.Initialize(PlayerChooseTileState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();   

        HexTile cursorHexTile = GetFocusedOnHexTile();

        if (cursorHexTile != null)
        {
            Cursor.SetActive(true);
            Cursor.transform.position = cursorHexTile.transform.position;
        }
        else
        {
            Cursor.SetActive(false);
        }   
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();    
    }

    private void generateStartingTile()
    {
        GameObject hexTile = Instantiate(TilePrefab, transform.position, Quaternion.identity);
        hexTile.transform.SetParent(transform);

        currentTilePosition = hexTile.transform.position;
    }

    private void generateThreeChoiceTiles()
    {
        Vector3 position0 = new Vector3(currentTilePosition.x - 40f, currentTilePosition.y + 16f, currentTilePosition.z);
        Vector3 position1 = new Vector3(currentTilePosition.x, currentTilePosition.y + 31f, currentTilePosition.z);
        Vector3 position2 = new Vector3(currentTilePosition.x + 40f, currentTilePosition.y + 16f, currentTilePosition.z);

        for (int tiles = 0; tiles < 3; tiles++)
        {
            if (tiles == 0) 
            {
                GameObject hexTile = Instantiate(TilePrefab, position0, Quaternion.identity);
                hexTile.transform.SetParent(transform);
            }
            if (tiles == 1) 
            {
                GameObject hexTile = Instantiate(TilePrefab, position1, Quaternion.identity);
                hexTile.transform.SetParent(transform);
            }
            if (tiles == 2) 
            {
                GameObject hexTile = Instantiate(TilePrefab, position2, Quaternion.identity);
                hexTile.transform.SetParent(transform);
            }
        }
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

    public Vector3 GetMiniPlayerTargetLocation()
    {
        return miniPlayerTargetLocation;
    }

    public void SetMiniPlayerTargetLocation(Vector3 newLocation)
    {
        miniPlayerTargetLocation = newLocation;
    }
}
