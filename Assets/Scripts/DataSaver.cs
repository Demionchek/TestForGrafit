using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


public class DataSaver : MonoBehaviour
{
    private GameManager _gameManager;
    private int _savedScore;
    private int _savedLevel;
    private int _newLevel;
    private int _newScore;

    public int SavedScore { get { return _savedScore; } }
    public int SavedLevel { get { return _savedLevel; } }

    private void Start()
    {
        if(GetComponent<GameManager>() != null)
        {
            _gameManager = GetComponent<GameManager>();
        }
    }

    public void NewGameData()
    {
        _newLevel = _gameManager.Difficulty;
        _newScore = _gameManager.Score;
    }

    public void SaveLevel()
    {
        _savedLevel = _newLevel;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
            + "/MySaveDataLevel.dat");
        SaveData data = new SaveData();
        data.SavedLevel = _savedLevel;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved Level =" + _savedLevel);
    }

    public void SaveScore()
    {
        _savedScore = _newScore;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
            + "/MySaveDataScore.dat");
        SaveData data = new SaveData();
        data.SavedScore = _savedScore;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved Score =" + _savedScore);
    }

    public void LoadScoreData()
    {
        if (File.Exists(Application.persistentDataPath
            + "/MySaveDataScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath
            + "/MySaveDataScore.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _savedScore = data.SavedScore;
        }
        else
        {
            Debug.Log(_savedScore + " LoadScoreERROR");
            _savedScore = 0;
        }
    }

    public void LoadLevelData()
    {
        if (File.Exists(Application.persistentDataPath
            + "/MySaveDataLevel.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath
            + "/MySaveDataLevel.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _savedLevel = data.SavedLevel;
        }
        else
        {
            Debug.Log(_savedLevel + " LoadLevelERROR");
            _savedLevel = 0;
        }
    }

    public void ClearData()
    {
        if (File.Exists(Application.persistentDataPath
           + "/MySaveDataLevel.dat"))
        {
            File.Delete(Application.persistentDataPath
           + "/MySaveDataLevel.dat");
            _savedLevel = 0;
        }

        if (File.Exists(Application.persistentDataPath
           + "/MySaveDataScore.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveDataScore.dat");
            _savedScore = 0;
        }
    }
}

[Serializable]
class SaveData
{
    public int SavedScore;
    public int SavedLevel;
}