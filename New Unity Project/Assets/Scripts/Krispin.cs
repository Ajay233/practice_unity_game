using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krispin : MonoBehaviour
{
    private bool _OnWarPath = false;
    private string _board;
    private List<int> _tilesVisited = new List<int>();
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find GameManager");
        }
       _board =  transform.parent.transform.parent.gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if (_OnWarPath && IsTurn())
        {
            _gameManager.SetKrakenTurn(false); // Setting this immediately prevents more than one go at a time
            StartCoroutine(_gameManager.MoveKrispin(this.gameObject, _tilesVisited, _board));
        }
    }

    private bool IsTurn()
    {
        if (_board == "PlayerBoard")
        {
            return _gameManager.IsPlayerTurn() == false && _gameManager.IsKrispinsTurn();
        } else
        {
            return _gameManager.IsPlayerTurn() == true && _gameManager.IsKrispinsTurn();
        }
    }

    public void SetKrispinOnTheWarpath()
    {
        _OnWarPath = true;
    }
}
