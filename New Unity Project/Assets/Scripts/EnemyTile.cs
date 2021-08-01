using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : Tile
{

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find GameManager");
        }
    }


    private void OnMouseDown()
    {
        Debug.Log("TEST ENEMY TILE");
        _gameManager.PlayerMove(this.gameObject);
        //_gameManager.ToggleTurn();
    }
}
