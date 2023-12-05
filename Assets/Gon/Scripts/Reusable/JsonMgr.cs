using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonMgr : Singleton<JsonMgr>
{
    public Dictionary<int, Data_Item> data_Items = new Dictionary<int, Data_Item>();

    public void Init()
    {
        data_Items = LoadData<Data_Item>();

        foreach(int key in data_Items.Keys)
        {
            DebugUtil.Log($"{data_Items[key].Name_KR}");
        }
    }

    Dictionary<int, T> LoadData<T>()
    {
        string name = typeof(T).Name;
        // �����͸� �ҷ��� ��� ����
        //string path = Path.Combine(Application.dataPath, $"{name}.json");
        // ������ �ؽ�Ʈ�� string���� ����
        TextAsset textData = Resources.Load($"JsonData/{name}") as TextAsset;

        DebugUtil.Log($"{textData.text}");
        //string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� playerData�� �־���
        //T[] val = JsonUtility.FromJson<T[]>(textData.ToString());
        //JsonConvert.DeserializeObject < Dictionary<int, T>>(textData.ToString());
        Dictionary<int, T> val = JsonConvert.DeserializeObject<Dictionary<int, T>>(textData.text);

        return val;
    }

    void SaveData<T>(object data)
    {
        string name = typeof(T).Name;
        // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
        string jsonData = JsonUtility.ToJson(data);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }
}

[Serializable]
public class Data_Item
{
    public string Name_KR = string.Empty;
    public string Name_EN = string.Empty;
    public bool Consumable;
    public int[] ConbinableKey;
    public bool Holdable;
}
