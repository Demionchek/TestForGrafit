using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameManager))]
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _scoreTextObj;
    [SerializeField] private GameObject _levelTextObj;
    private TMP_Text _scoreTMP;
    private TMP_Text _levelTMP;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _scoreTMP = _scoreTextObj.GetComponent<TMP_Text>();
        _levelTMP = _levelTextObj.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _scoreTMP.text = "Score : " + _gameManager.Score.ToString();
        _levelTMP.text = "Level : " + _gameManager.Difficulty.ToString();
    }
}
