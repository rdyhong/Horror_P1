using UnityEngine;

public static class PlayerPrefsHelper
{
    // PlayerPrefs key
    public const string PPKEY_MOUSE_SENSITIVE_X = "MouseSensitiveX";
    public const string PPKEY_MOUSE_SENSITIVE_Y = "MouseSensitiveY";

    // Base value
    const float BASE_VALUE_MOUSE_SENSITIVE_X = 2;
    const float BASE_VALUE_MOUSE_SENSITIVE_Y = 2;

    // Save
    public static void SaveIntData(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }
    public static void SaveFltData(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }
    public static void SaveStrData(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
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
                SaveFltData(PPKEY_MOUSE_SENSITIVE_X, BASE_VALUE_MOUSE_SENSITIVE_X);
                break;
            case PPKEY_MOUSE_SENSITIVE_Y:
                SaveFltData(PPKEY_MOUSE_SENSITIVE_Y, BASE_VALUE_MOUSE_SENSITIVE_Y);
                break;

            default:
                DebugUtil.LogErr($"Unsinged Key ({key})");
                break;
        }
    }

    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}