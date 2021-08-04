using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private int width = 6;
    private int height = 6;
    private int[,] gridArr;
    [SerializeField]
    GameObject tile;
    private GameManager _gameManager;
    Vector3 tile_vector;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Camera cam = Camera.main;
        // height and width are amount of objects of size '1' that fit - this is for positioning
         float height = 2f * cam.orthographicSize;
         float width = height * cam.aspect;
        createBoard();
        // Getting size of tile for vector3 calculation in loop - needs looking at
        Renderer render = tile.GetComponent<Renderer>();
        float tile_vector = render.bounds.size.x;

    }    
    int[,] createBoard()
    {
        gridArr = new int[width, height];
        int tileCount = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject createdTile = Instantiate(tile, new Vector3(x * 1.42f, y * 1.42f, 0), Quaternion.identity, this.transform);
                _gameManager.AddTileToCorrectList(createdTile);
                createdTile.name = "Tile " + tileCount.ToString();
                tileCount++;
            }
        }
        return gridArr;
    }
}
