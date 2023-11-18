using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneType
{
    Title,
    Main,
}

public static class LoadSceneMgr
{
    static bool isLoading = false;
    public static void LoadScene(ESceneType sceneType)
    {
        if (isLoading) return;

        isLoading = true;
        SceneManager.LoadScene(sceneType.ToString());
    }
}
