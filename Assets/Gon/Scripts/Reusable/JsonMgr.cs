using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonMgr : Singleton<JsonMgr>
{
    Item_Json _item_json;
    Dialogue_Story_Json _dialogue_story;

    public void Init()
    {
        _dialogue_story = LoadData<Dialogue_Story_Json>();
        _item_json = LoadData<Item_Json>();
    }

    T LoadData<T>() where T : class
    {
        string name = typeof(T).Name;
        TextAsset textData = Resources.Load($"JsonData/{name}") as TextAsset;

        if (textData == null)
        {
            DebugUtil.LogErr($"Json load fail({name})");
            return null;
        }

        DebugUtil.Log($"{textData.text}");
        
        return JsonUtility.FromJson<T>(textData.text);
    }

    void SaveData<T>(object data)
    {
        string name = typeof(T).Name;
        string jsonData = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        File.WriteAllText(path, jsonData);
    }
}

[Serializable]
public class Item_Json
{
    public Item_Data[] Item;
}
[Serializable]
public class Item_Data
{
    public int Id;
    public string Name_KR = string.Empty;
    public string Name_EN = string.Empty;
    public bool Consumable;
    public int CombinableKey;
    public bool Holdable;
}

[Serializable]
public class Dialogue_Story_Json
{
    public Dialogue_Story_Data[] Dialogue_Story;
}
[Serializable]
public class Dialogue_Story_Data
{
    public int Id;
    public string EN;
    public string KR;
}
