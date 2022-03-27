using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class DeckController : MonoBehaviour
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private GameObject _greenCardPrefab;
    [SerializeField] private GameObject _redCardPrefab;
    [SerializeField] private PlayerCardMovement _playerCardMovement;
    [SerializeField] private float _maxMoveDistance;
    private Transform _playerLastParant;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        PlayerCardMovement.OnMoved += CreateNewCard;
    }

    private void OnDisable()
    {
        PlayerCardMovement.OnMoved -= CreateNewCard;
    }

    private void Start()
    {
        _playerLastParant = _playerCardMovement.gameObject.transform.parent;

        for (int i = 0; i < _positions.Count; i++)
        {
            if (i != 4)
            {
                float r = Random.value;
                if (r > _gameManager.Difficulty * 0.1)
                {
                    GameObject card = Instantiate(_greenCardPrefab, _positions[i]);
                    card.GetComponent<CardBehaviour>().GetGameProperties(_playerCardMovement, _maxMoveDistance, _gameManager.Difficulty);
                }
                else
                {
                    GameObject card = Instantiate(_redCardPrefab, _positions[i]);
                    card.GetComponent<CardBehaviour>().GetGameProperties(_playerCardMovement, _maxMoveDistance, _gameManager.Difficulty);
                }
            }
        }
    }

    private void CreateNewCard()
    {
        Transform newParant = _playerLastParant;
        float r = Random.value;
        if (r > _gameManager.Difficulty * 0.1)
        {
            GameObject card = Instantiate(_greenCardPrefab, newParant);
            card.GetComponent<CardBehaviour>().GetGameProperties(_playerCardMovement, _maxMoveDistance, _gameManager.Difficulty);
        }
        else
        {
            GameObject card = Instantiate(_redCardPrefab, newParant);
            card.GetComponent<CardBehaviour>().GetGameProperties(_playerCardMovement, _maxMoveDistance, _gameManager.Difficulty);
        }
        _playerLastParant = _playerCardMovement.gameObject.transform.parent;
        Debug.Log(_gameManager.Difficulty * 0.1);
    }
}
