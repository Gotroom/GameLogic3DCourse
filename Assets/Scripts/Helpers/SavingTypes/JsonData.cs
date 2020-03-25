using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public sealed class JsonData<T> : ISavable<T>
{
    public T Load(string path = null)
    {
        var jsonText = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(jsonText);
    }

    public List<T> LoadList(string path = null)
    {
        var jsonText = File.ReadAllLines(path);
        List<T> objectList = new List<T>();
        foreach (var line in jsonText)
        {
            objectList.Add(JsonUtility.FromJson<T>(line));
        }
        return objectList;
    }

    public void Save(T data, string path = null)
    {
        var jsonString = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonString);
    }

    public void Save(List<T> data, string path = null)
    {
        string jsonString = "";
        foreach (var item in data)
        {
            jsonString += JsonUtility.ToJson(item);
            jsonString += "\n";
        }
        File.WriteAllText(path, jsonString);
    }

    
}
