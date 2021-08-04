using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : Tile
{
    // Had been getting the following error:
    // The same field name is serialized multiple times in the class or its parent class. This is not supported: Base(EnemyTile) _gameManager

    // Tried renaming all instances of _gameManager so they use different names but no joy
    // Tried making the Tile's _gameManager public which gave the EnemeyTile automatic access to it.  This seems to have gotten rid of the error

    private void OnMouseDown()
    {
        Debug.Log("TEST ENEMY TILE");
        if (_gameManager.IsPlayerTurn() && !HasMiss())
        {
            _gameManager.PlayerMove(this.gameObject);
        }
    }

    private bool HasMiss()
    {
        if (transform.childCount == 0)
        {
            return false;
        } else
        {
            return transform.GetChild(0).gameObject.tag == "Miss";
        }
    }

}
