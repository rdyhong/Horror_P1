using UnityEngine;

public static class PlayerPrefsData
{
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
    public static int GetIntData(string key)
    {
        if(IsKeyExist(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            DebugUtil.LogErr($"({key}) Not Exist");
            return int.MinValue;
        }
    }
    public static float GetFltData(string key)
    {
        if (IsKeyExist(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            DebugUtil.LogErr($"({key}) Not Exist");
            return float.MinValue;
        }
    }
    public static string GetStrData(string key)
    {
        if (IsKeyExist(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            DebugUtil.LogErr($"({key}) Not Exist");
            return string.Empty;
        }
    }

    // Check
    static bool IsKeyExist(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
