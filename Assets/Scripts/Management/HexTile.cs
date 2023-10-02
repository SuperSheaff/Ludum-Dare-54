using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{

    public int xPos;
    public int yPos;
    public int tileDirection;
    public bool isAvailable;

    public Animator TileAnimator    { get; private set; }

    private float fallSpeed = 50f;
    private float lifetime = 2f;

    private int tileType;
    private bool selfDestructActivate = false;

    public void SetTileType(string tileTypeString)
    {
        switch (tileTypeString)
        {
            case "empty":
                tileType = 0;
                TileAnimator.SetBool("isEmpty", true);
                break;

            case "enemy":
                tileType = 1;
                TileAnimator.SetBool("isEnemy", true);
                break;

            case "chest":
                tileType = 2;
                TileAnimator.SetBool("isChest", true);
                break;

            case "win":
                tileType = 3;
                TileAnimator.SetBool("isWin", true);
                break;

            default:
                tileType = 0;
                break;
        }
    }    

    public int GetTileType()
    {
        return tileType;
    }    

    private void Awake()
    {
        TileAnimator    = GetComponent<Animator>();
    }

    void OnMouseEnter()
    {
        if (isAvailable)
        {
            GameObject.FindGameObjectWithTag("BattleController").GetComponent<BattleController>().AudioManager.PlayAudio("hover", 0.8f);
        }
    }

    private void Update()
    {
        if (selfDestructActivate)
        {
        // Move the GameObject downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Decrease the lifetime
        lifetime -= Time.deltaTime;

        // Check if it's time to destroy the GameObject
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
        }
    }

    public void SelfDestruct()
    {
        GetComponent<SpriteSorter>().enabled = false;
        selfDestructActivate = true;
    }

}
