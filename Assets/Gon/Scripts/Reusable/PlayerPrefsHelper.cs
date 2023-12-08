using UnityEngine;

public static class PlayerPrefsHelper
{
    // PlayerPrefs key
    public const string PPKEY_MOUSE_SENSITIVE_X = "PPKEYMouseSensitiveX";
    public const string PPKEY_MOUSE_SENSITIVE_Y = "PPKEYMouseSensitiveY";
    public const string PPKEY_SCREEN_FOV = "PPKEYScreenFov";
    public const string PPKEY_OWNED_ITEM = "PPKEYOwnedItem";
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
