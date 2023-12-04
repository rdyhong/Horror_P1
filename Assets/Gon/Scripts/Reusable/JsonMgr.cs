using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonMgr : Singleton<JsonMgr>
{
    List<Data_Item> data_Items = new List<Data_Item>();
    
    public void Init()
    {
        
    }

    T LoadData<T>()
    {
        string name = typeof(T).Name;
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� playerData�� �־���

        return JsonUtility.FromJson<T>(jsonData);
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
    string Name_KR = string.Empty;
    string Name_EN = string.Empty;
    bool Consumable;
    int[] ConbinableKey;
    bool Holdable;
}
