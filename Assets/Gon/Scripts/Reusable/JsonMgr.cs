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
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌

        return JsonUtility.FromJson<T>(jsonData);
    }

    void SaveData<T>()
    {
        string name = typeof(T).Name;
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(UserData.Setting);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, $"{name}.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }
}
