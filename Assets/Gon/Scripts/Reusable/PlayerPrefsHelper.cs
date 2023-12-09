using Unity.VisualScripting;
using UnityEngine;

public static class PlayerPrefsHelper
{
    // PlayerPrefs key
    public const string PPKEY_MOUSE_SENSITIVE_X = "PPKEYMouseSensitiveX";
    public const string PPKEY_MOUSE_SENSITIVE_Y = "PPKEYMouseSensitiveY";
    public const string PPKEY_SCREEN_FOV = "PPKEYScreenFov";
    public const string PPKEY_OWNED_ITEM = "PPKEYOwnedItems";
    public const string PPKEY_ON_HAND_ITEM = "PPKEYOnHandItem";

    // Defalut value for empty key
    const float DEFAULT_VALUE_MOUSE_SENSITIVE_X = 2;
    const float DEFAULT_VALUE_MOUSE_SENSITIVE_Y = 2;
    const float DEFAULT_VALUE_SCREEN_FOV = 75;

    // Save
    public static void SaveIntData(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
        UserData.RefreshData();
    }
    public static void SaveFltData(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
        UserData.RefreshData();
    }
    public static void SaveStrData(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
        UserData.RefreshData();
    }
    public static void SaveIntArr(string key, int[] val)
    {
        string strArr = string.Empty; // 문자열 생성

        for (int i = 0; i < val.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + val[i];
            if (i < val.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                strArr = strArr + ",";
            }
        }

        PlayerPrefs.SetString(key, strArr); // PlyerPrefs에 문자열 형태로 저장
    }

    // Load
    public static int GetInt(string key)
    {
        if(!IsKeyExist(key)) SetBaseValue(key);

        return PlayerPrefs.GetInt(key);
    }
    public static float GetFlt(string key)
    {
        if (!IsKeyExist(key)) SetBaseValue(key);

        return PlayerPrefs.GetFloat(key);
    }
    public static string GetStr(string key)
    {
        if (!IsKeyExist(key)) SetBaseValue(key);

        return PlayerPrefs.GetString(key);
    }
    public static int[] GetIntArr(string key)
    {
        string[] dataArr = PlayerPrefs.GetString(key).Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장

        return new int[dataArr.Length];
    }

    // Check
    static bool IsKeyExist(string key)
    {
        bool isExist = PlayerPrefs.HasKey(key);

        if(!isExist)
        {
            DebugUtil.LogErr($"Key is not exist ({key})");
        }

        return isExist;
    }

    static void SetBaseValue(string key)
    {
        switch(key)
        {
            case PPKEY_MOUSE_SENSITIVE_X:
                SaveFltData(PPKEY_MOUSE_SENSITIVE_X, DEFAULT_VALUE_MOUSE_SENSITIVE_X);
                break;
            case PPKEY_MOUSE_SENSITIVE_Y:
                SaveFltData(PPKEY_MOUSE_SENSITIVE_Y, DEFAULT_VALUE_MOUSE_SENSITIVE_Y);
                break;
            case PPKEY_SCREEN_FOV:
                SaveFltData(key, DEFAULT_VALUE_SCREEN_FOV);
                break;
            default:
                DebugUtil.LogErr($"Unsinged Key ({key})");
                break;
        }
    }

    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
