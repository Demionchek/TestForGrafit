using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DataSaver))]
public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _scoreTextObj;
    [SerializeField] private GameObject _levelTextObj;
    private TMP_Text _scoreTMP;
    private TMP_Text _levelTMP;
    private DataSaver _dataSaver;

    private void Start()
    {
        _scoreTMP = _scoreTextObj.GetComponent<TMP_Text>();
        _levelTMP = _levelTextObj.GetComponent<TMP_Text>();
        _dataSaver = GetComponent<DataSaver>();
        _dataSaver.LoadLevelData();
        _dataSaver.LoadScoreData();
        _levelTMP.text ="Last level : " + _dataSaver.SavedLevel.ToString();
        _scoreTMP.text ="Last score : " + _dataSaver.SavedScore.ToString();
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnClearClick()
    {
        _dataSaver.ClearData();
        _levelTMP.text = "Last level : " + _dataSaver.SavedLevel.ToString();
        _scoreTMP.text = "Last score : " + _dataSaver.SavedScore.ToString();
    }
}
