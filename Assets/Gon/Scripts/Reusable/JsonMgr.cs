using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonMgr : Singleton<JsonMgr>
{
    public Dictionary<int, Data_Item> data_Items = new Dictionary<int, Data_Item>();
    
    public void Init()
    {
        data_Items = LoadData<Data_Item>();
    }

    Dictionary<int, T> LoadData<T>()
    {
        string name = typeof(T).Name;
        // 데이터를 불러올 경로 지정
        //string path = Path.Combine(Application.dataPath, $"{name}.json");
        // 파일의 텍스트를 string으로 저장
        TextAsset textData = Resources.Load($"JsonData/{name}") as TextAsset;


        //string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌

        return JsonUtility.FromJson<Dictionary<int, T>>(textData.ToString());
    }

    void SaveData<T>(object data)
    {
        string name = typeof(T).Name;
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(data);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }
}

[Serializable]
public class Data_Item
{
    string Name_KR = string.Empty;
    string Name_EN = string.Empty;
    bool Consumable;
    int[] ConbinableKey;
    bool Holdable;
}
