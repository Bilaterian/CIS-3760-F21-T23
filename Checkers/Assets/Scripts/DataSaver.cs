using Unity;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver
{
    public static void SaveList<T>(string name, List<T> data)
    {
        var serializableList = new SerializableListContainer<T>(data);
        string jsonData = JsonUtility.ToJson(serializableList);
        PlayerPrefs.SetString(name, jsonData);
        PlayerPrefs.Save();
    }

    public static List<T> LoadList<T>(string name)
    {
        string jsonData = PlayerPrefs.GetString(name);
        var loadedData = JsonUtility.FromJson<SerializableListContainer<T>>(jsonData);
        return loadedData.list;
    }
}