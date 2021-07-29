using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _playerTileList = new List<GameObject>();
    // We'll need an array of the ships on the side so we can turn them gray/translucent when destroyed
    // or set them on fire, whatever lol
    [SerializeField] private Tile[] _enemyTileList;
    [SerializeField] private GameObject _ship;
    [SerializeField] private int _shipLimit;
    [SerializeField] GameObject _missedPrefab;
    [SerializeField] GameObject _hitPrefab;
    private GameManager _gameManager;
    private SinglePlayerSetupCanvas _singlePlayerSetupCanvas;
    private int _playerShipCount = 0;
    [SerializeField] private List<int> _playerTilesAttacked = new List<int>();
    private bool _gameStart = false;
    private bool _enemyThinking = false;

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
        _singlePlayerSetupCanvas = GameObject.Find("SinglePlayerSetupCanvas").GetComponent<SinglePlayerSetupCanvas>();
        if (_singlePlayerSetupCanvas == null)
        {
            Debug.LogError("Unable to find SinglePlayerSetupCanvas");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStart && !_enemyThinking)
        {
            Debug.Log("Invoking enemey logic");
            EnemyLogic();
        }
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

    public void AddPlayerTileToList(GameObject tile)
    {
        _playerTileList.Add(tile);
    }

    public void EnemyLogic()
    {
        // This if statement can be refactored into it's own method (for handling win/lose etc) and moved 
        // out when we have the turn system
        if (_playerShipCount == 0)
        {
            PlayerLose();
            return;
        }

        _enemyThinking = true;  // delete when we have the turn system (just stops enemy taking multiple turns at once for the moment)
        int index = GetUnusedRandomNumber();
        if (_playerTileList[index].transform.childCount == 0)
        {
            // Instantiate a miss prefab and append it as a child
            Instantiate(_missedPrefab, _playerTileList[index].transform.position, Quaternion.identity, _playerTileList[index].transform);
        } else
        {
            // option1 - Animate explosion, get the ship and replace it's image with a burning ship
            // option2 - Another option could be to animate explosion, overlay fire

            // Temp solution below so we can visualise enemy hits on player ships
            Instantiate(_hitPrefab, _playerTileList[index].transform.position, Quaternion.identity, _playerTileList[index].transform);
            _playerShipCount--;
        }
        _enemyThinking = false; // delete when we have the turn system (just stops enemy taking multiple turns at once for the moment)
    }

    int GetUnusedRandomNumber()
    {
        int num = Random.Range(0, 60);
        while (_playerTilesAttacked.Contains(num))
        {
            num = Random.Range(0, 60);
        }
        _playerTilesAttacked.Add(num); //Keep track of what tiles have been selected
        return num;
    }

    public void StartGame()
    {
        _gameStart = true;
    }

    public bool GameStarted()
    {
        return _gameStart;
    }

    void PlayerLose()
    {
        Debug.Log("Stopping game");
        _gameStart = false;
        _singlePlayerSetupCanvas.transform.GetChild(1).gameObject.SetActive(true);
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
    // Move camera and zoom in on tile using the tile position?  Might help users to see what's happenin better
    // Check if tile has a ship
    // If tile has a ship, either destroy child and play animation or replace image and play animation
    // If tile does not have a ship, replace tile img source with one showing a miss
    // continuously check if player ships is greater than 0, if yes, show: game over, you lose 
    // continuously check if enemy ships is greater than 0, if yes, show: you win


    //PLAY AGAIN

    // Need to reset all variables
    // Take the user back to player set up
}
