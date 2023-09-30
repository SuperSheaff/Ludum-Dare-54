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

    private int tileType;

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

}
