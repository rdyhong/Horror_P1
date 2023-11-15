using UnityEngine;

public static class DebugUtil
{
    public static void Log(string log)
    {
#if UNITY_EDITOR
        Debug.Log(log);
#endif
    }
    public static void LogWarn(string log)
    {
#if UNITY_EDITOR
        Debug.LogWarning(log);
#endif
    }
    public static void LogErr(string log)
    {
#if UNITY_EDITOR
        Debug.LogError(log);
#endif
    }
    public static void LogAssert(bool condition, string log)
    {
#if UNITY_EDITOR
        Debug.Assert(condition, log);
#endif
    }
}
