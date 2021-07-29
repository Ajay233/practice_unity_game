using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private int width = 6;
    private int height = 10;
    private int[,] gridArr;
    [SerializeField]
    GameObject tile;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridArr = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tileCopy = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                gameManager.AddPlayerTileToList(tileCopy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
