using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tile[] _playerTileList;
    // We'll need an array of the ships on the side so we can turn them gray/translucent when destroyed
    // or set them on fire, whatever lol
    [SerializeField] private Tile[] _enemyTileList;
    [SerializeField] private GameObject _ship;
    [SerializeField] private int _shipLimit;

    private GameManager _gameManager;

    private int _playerShipCount = 0;

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PlayerShipCountAtMax()
    {
        return _playerShipCount == _shipLimit;
    }

    public void SpawnShip(Tile tile)
    {
        if (_playerShipCount < _shipLimit)
        {
            Instantiate(_ship, tile.transform.position, Quaternion.identity, tile.transform);
            _playerShipCount++;
            Debug.Log("PlayerShips = " + _playerShipCount);
        } 
    }

    public void DecreasePlayerShipCount()
    {
        _playerShipCount--;
    }

    // Need to create a ship prefab
    // Add a serialized field for ship and drag in the ship prefab


    // PLAYER SETUP

    // Player must click 5 squares - done

    // Each time a player clicks a square && player has not clicked 5 times, add ship prefab as a child - done

    // If a player clicks a square that has a ship prefab child, remove the ship prefab child & increase 
    // number of clicks allowed by one - done

    // Disable button until player has applied ships to 5 squares - done



    //ENEMY SETUP

    // pick a random square index between 0-24 and place a ship prefab, repeat 5 times



    //GAMEPLAY

    // Enemy selects random tile || Player clicks on a tile
    // Check if tile has a ship
    // If tile has a ship, either destroy child and play animation or replace image and play animation
    // If tile does not have a ship, replace tile img source with one showing a miss
    // continuously check if player ships is greater than 0, if yes, show: game over, you lose 
    // continuously check if enemy ships is greater than 0, if yes, show: you win


    //PLAY AGAIN

    // Need to reset all variables
    // Take the user back to player set up
}
