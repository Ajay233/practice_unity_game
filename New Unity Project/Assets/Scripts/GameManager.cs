using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _playerTileList = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemyTileList = new List<GameObject>();
    [SerializeField] private List<int> _playerTilesAttacked = new List<int>();
    [SerializeField] private GameObject _playerBoard;
    [SerializeField] private GameObject _EnemyBoard;
    [SerializeField] private GameObject _ship;
    [SerializeField] private GameObject _krispin;
    [SerializeField] private int _shipLimit;
    [SerializeField] GameObject _missedPrefab;
    [SerializeField] GameObject _hitPrefab;
    private TurnTransition _playerTurnTransition;
    private TurnTransition _enemyTurnTransition;
    private GameManager _gameManager;
    private SinglePlayerSetupCanvas _singlePlayerSetupCanvasVar;
    private int _playerShipCount = 0;
    private int _enemyShipCount = 0;
    bool _gameStart = false;
    private bool _playerTurn = false;

    //private void Awake()
    //{
    //    if (_gameManager == null)
    //    {
    //        _gameManager = this;
    //        DontDestroyOnLoad(gameObject);
    //    } else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    void Start()
    {
        _singlePlayerSetupCanvasVar = GameObject.Find("SinglePlayerSetupCanvas").GetComponent<SinglePlayerSetupCanvas>();
        if (_singlePlayerSetupCanvasVar == null)
        {
            Debug.LogError("Unable to find SinglePlayerSetupCanvas");
        }

        _playerTurnTransition = GameObject.Find("PlayerTurnTransition").GetComponent<TurnTransition>();
        _enemyTurnTransition = GameObject.Find("EnemyTurnTransition").GetComponent<TurnTransition>();
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
            yield return new WaitForSeconds(3f);
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
            if (_playerTurn)
            {
                tileList[index].transform.GetChild(0).gameObject.SetActive(true);
            }
            Instantiate(_hitPrefab, tileList[index].transform.position, Quaternion.identity, tileList[index].transform);
            DecrementShipList(tileList);
        }
        CheckGameOver();
    }

    void DecrementShipList(List<GameObject> tileList)
    {
        if (tileList == _enemyTileList)
        {
            _enemyShipCount--;
        } else
        {
            _playerShipCount--;
        }
    }

    public void EnemyMove()
    {
        int index = GetUnusedRandomNumber(_playerTilesAttacked);
        StartCoroutine(SlowTurnVisualisation(_playerTileList, index));
    }

    public void PlayerMove(GameObject tile)
    {
        int index = _enemyTileList.IndexOf(tile);
        StartCoroutine(SlowTurnVisualisation(_enemyTileList, index));
    }

    int GetUnusedRandomNumber(List<int> list)
    {
        int num = Random.Range(0, 36);
        while (list.Contains(num))
        {
            num = Random.Range(0, 36);
        }
        list.Add(num); //Keep track of what tiles have been selected
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
            PlaceKrispin(_playerTileList);
        }
    }

    public bool GameStarted()
    {
        return _gameStart;
    }

    IEnumerator playerBoardRoutine()
    {
        yield return new WaitForSeconds(0.8f);
        _enemyTurnTransition.PlayTransition();
        yield return new WaitForSeconds(2);
        _singlePlayerSetupCanvasVar.transform.GetChild(5).gameObject.SetActive(true);
        _singlePlayerSetupCanvasVar.transform.GetChild(4).gameObject.SetActive(false);
        _EnemyBoard.SetActive(false);
        _playerBoard.SetActive(true);
        EnemyMove();
    }

    IEnumerator EnemyBoardRoutine(bool firstGo)
    {
        if (!firstGo)
        {
            yield return new WaitForSeconds(1.5f);
        } 
        _playerTurnTransition.PlayTransition();
        yield return new WaitForSeconds(2f);
        _singlePlayerSetupCanvasVar.transform.GetChild(4).gameObject.SetActive(true);
        _singlePlayerSetupCanvasVar.transform.GetChild(5).gameObject.SetActive(false);
        _playerBoard.SetActive(false);
        _EnemyBoard.SetActive(true);
    }

    public void ToggleTurn(bool firstGo)
    {
        if (_playerTurn == true)
        {
            _playerTurn = false;
            StartCoroutine(playerBoardRoutine());
        } else
        {
            _playerTurn = true;
            StartCoroutine(EnemyBoardRoutine(firstGo));
        }

        Debug.Log("Player turn set to: " + _playerTurn);
    }

    public bool IsPlayerTurn()
    {
        return _playerTurn;
    }

    void CheckGameOver()
    {
        if (_playerShipCount == 0 || _enemyShipCount == 0)
        {
            _gameStart = false;
            _singlePlayerSetupCanvasVar.ShowGameOverPanel();
        } else
        {
            ToggleTurn(false);
        }
    }

    public void DeployEnemyFleet()
    {
        List<int> enemyTilesWithShips = new List<int>();
        for (int x = 0; x < 5; x++)
        {
            int index = GetUnusedRandomNumber(enemyTilesWithShips);
            GameObject enemyShip = Instantiate(_ship, _enemyTileList[index].transform.position, Quaternion.identity, _enemyTileList[index].transform);
            //enemyShip.SetActive(false);
            _enemyShipCount++;
        }
        PlaceKrispin(_enemyTileList);
    }

    public void ResetGame()
    {
        _playerTurn = false;
        _playerTileList = new List<GameObject>();
        _enemyTileList = new List<GameObject>();
        _playerTilesAttacked = new List<int>();
        _playerShipCount = 0;
        _enemyShipCount = 0;
    }

    public bool PlayerHasWon()
    {
        return _enemyShipCount == 0;
    }

    void PlaceKrispin(List<GameObject> tileList)
    {
        List<int> numsToAvoid = new List<int>();
        bool krispinPlaced = false;
        while (krispinPlaced == false)
        {
            int num = GetUnusedRandomNumber(numsToAvoid);
            if (tileList[num].transform.childCount == 0)
            {
                GameObject krispinPrefab = Instantiate(_krispin, tileList[num].transform.position, Quaternion.identity, tileList[num].transform);
                //krispinPrefab.SetActive(false);
                krispinPlaced = true;
            }
        }

    }

    // select a random number between 0-36
    // check the board to see if there is a ship on that tile
    // if there is repeat the process
    // if there isn't place Krispin and setActive to false



}
