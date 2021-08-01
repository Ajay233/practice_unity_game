using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _playerTileList = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemyTileList = new List<GameObject>();
    [SerializeField] private GameObject _playerBoard;
    [SerializeField] private GameObject _EnemyBoard;
    [SerializeField] private GameObject _ship;
    [SerializeField] private int _shipLimit;
    [SerializeField] GameObject _missedPrefab;
    [SerializeField] GameObject _hitPrefab;
    private GameManager _gameManager;
    private SinglePlayerSetupCanvas _singlePlayerSetupCanvas;
    private int _playerShipCount = 0;
    private int _enemyShipCount = 0;
    [SerializeField] private List<int> _playerTilesAttacked = new List<int>();
    bool _gameStart = false;
    private bool _playerTurn = false;

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

    void Start()
    {
        _singlePlayerSetupCanvas = GameObject.Find("SinglePlayerSetupCanvas").GetComponent<SinglePlayerSetupCanvas>();
        if (_singlePlayerSetupCanvas == null)
        {
            Debug.LogError("Unable to find SinglePlayerSetupCanvas");
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

    public void AddTileToCorrectList(GameObject tile)
    {
        if (_gameStart && _playerTurn)
        {
            _enemyTileList.Add(tile);
        } else
        {
            _playerTileList.Add(tile);
        }
    }

    IEnumerator SlowTurnVisualisation(List<GameObject> tileList, int index)
    {
        // Used delay tactics here because otherwise the enemy takes its go so quickly, it looks like it's
        // always the user's go.  This also gives the user time to see what is happening and we can use it later
        // on when we add in animations we want to wait for etc.
        if (!_playerTurn)
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (tileList[index].transform.childCount == 0)
        {
            // Instantiate a miss prefab and append it as a child
            Instantiate(_missedPrefab, tileList[index].transform.position, Quaternion.identity, tileList[index].transform);
        }
        else
        {
            // option1 - Animate explosion, get the ship and replace it's image with a burning ship
            // option2 - Another option could be to animate explosion, overlay fire

            // Temp solution below so we can visualise enemy hits on player ships
            Instantiate(_hitPrefab, tileList[index].transform.position, Quaternion.identity, tileList[index].transform);
            DecrementShipList(tileList);
        }
        yield return new WaitForSeconds(1.5f);
        ToggleTurn();
    }

    void DecrementShipList(List<GameObject> tileList)
    {
        if (tileList == _enemyTileList)
        {
            //_enemyShipCount--;
        } else
        {
            _playerShipCount--;
        }
    }

    public void EnemyMove()
    {
        int index = GetUnusedRandomNumber();
        StartCoroutine(SlowTurnVisualisation(_playerTileList, index));
    }

    public void PlayerMove(GameObject tile)
    {
        int index = _enemyTileList.IndexOf(tile);
        StartCoroutine(SlowTurnVisualisation(_enemyTileList, index));
    }

    int GetUnusedRandomNumber()
    {
        int num = Random.Range(0, 36);
        while (_playerTilesAttacked.Contains(num))
        {
            num = Random.Range(0, 36);
        }
        _playerTilesAttacked.Add(num); //Keep track of what tiles have been selected
        return num;
    }

    public void ToggleStartGame()
    {
        if (_gameStart)
        {
            _gameStart = false;
        } else
        {
            _gameStart = true;
        }
    }

    public bool GameStarted()
    {
        return _gameStart;
    }

    public void ToggleTurn()
    {
        if (_playerTurn == true)
        {
            _playerTurn = false;
            _EnemyBoard.SetActive(false);
            _playerBoard.SetActive(true);
            EnemyMove();
        } else
        {
            _playerTurn = true;
            _playerBoard.SetActive(false);
            _EnemyBoard.SetActive(true);

        }

        Debug.Log("Player turn set to: " + _playerTurn);
    }

    public bool IsPlayerTurn()
    {
        return _playerTurn;
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
