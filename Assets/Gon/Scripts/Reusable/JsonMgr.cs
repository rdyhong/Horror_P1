using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonMgr : MonoBehaviour
{
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

    void SaveData<T>()
    {
        string name = typeof(T).Name;
        // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
        string jsonData = JsonUtility.ToJson(UserData.Setting);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }
}
