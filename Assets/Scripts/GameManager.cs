using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataSaver))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private int _pointsToNextLevel = 10;
    private DataSaver _dataSaver;
    private int _score;
    private int _difficulty;

    public int Score { get { return _score; } }
    public int Difficulty { get { return _difficulty; } }

    private void OnEnable()
    {
        PlayerCardMovement.OnMoved += AddScore;
        PlayerCardMovement.IsDead += SaveScoreAndLoose;
    }

    private void OnDisable()
    {
        PlayerCardMovement.OnMoved -= AddScore;
        PlayerCardMovement.IsDead -= SaveScoreAndLoose;
    }

    private void Start()
    {
        _difficulty = 1;
        _score = 0;
        _dataSaver = GetComponent<DataSaver>();
    }

    private void SaveScoreAndLoose()
    {
        _dataSaver.NewGameData();
        _dataSaver.SaveLevel();
        _dataSaver.SaveScore();
        Invoke("LoadLooseScene", 1f);
    }

    private void LoadLooseScene()
    {
        SceneManager.LoadScene(2);
    }

    private void AddScore()
    {
        _score++;
        if (_score >= _pointsToNextLevel & _difficulty !< 5)
        {
            _difficulty++;
            _pointsToNextLevel += 10;
        }
        Debug.Log("Level = " + _difficulty + " Score = " + _score);
    }
}
