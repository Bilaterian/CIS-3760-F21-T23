using Unity;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver
{

    public static void Save<T>(string name, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(name, jsonData);
        PlayerPrefs.Save();
    }

    public static void SaveList<T>(string name, List<T> data)
    {
        Debug.Log(data[0]);
        var serializableList = new SerializableListContainer<T>(data);
        string jsonData = JsonUtility.ToJson(serializableList);
        Debug.Log("json data to be saved");
        Debug.Log(jsonData);
        PlayerPrefs.SetString(name, jsonData);
        PlayerPrefs.Save();
    }

    public static void SaveNewGame(GameStats newGame)
    {
        var currentMatches = LoadList<GameStats>("matches");
        Debug.Log(currentMatches);
        if (currentMatches == null)
        {
            currentMatches = new List<GameStats>();
        }

        currentMatches.Add(newGame);
        SaveList<GameStats>("matches", currentMatches);
    }

    public static T Load<T>(string name)
    {
        string jsonData = PlayerPrefs.GetString(name);
        if (jsonData.Length == 0) return default(T);
        return JsonUtility.FromJson<T>(jsonData);
    }

    public static List<T> LoadList<T>(string name)
    {
        string jsonData = PlayerPrefs.GetString(name);
        Debug.Log(jsonData);
        if (jsonData.Length == 0)
        {
            return new List<T>();
        }
        var loadedData = JsonUtility.FromJson<SerializableListContainer<T>>(jsonData);
        return loadedData.list;
    }
}