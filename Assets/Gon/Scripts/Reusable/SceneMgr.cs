using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneType
{
    Title,
    Main,
}

public class SceneMgr : Singleton<SceneMgr>
{
    static bool _isLoading = false;
    static ESceneType currentScene = ESceneType.Title;

    public static ESceneType GetCurrentScene() => currentScene;

    public void LoadScene(ESceneType sceneType)
    {
        if (_isLoading) return;

        currentScene = sceneType;

        _isLoading = true;

        StartCoroutine(LoadAsyncScene(sceneType));
    }

    IEnumerator LoadAsyncScene(ESceneType sceneType)
    {
        // UI clear
        UIMgr.Inst.ClearAll();

        // Show Cover
        LoadSceneCover loadSceneCover = UIMgr.Inst.Push<LoadSceneCover>();
        // Lock UI Push
        UIMgr.Inst.SetLock(true);

        // Load
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneType.ToString());
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            loadSceneCover.SetLoadingBar(asyncOp.progress);

            if (asyncOp.progress >= 0.9f)
            {
                
                break;
            }
            yield return null;
        }

        loadSceneCover.SetLoadingBar(asyncOp.progress);
        asyncOp.allowSceneActivation = true;
        UIMgr.Inst.SetLock(false);
        UIMgr.Inst.ClearAll();
        yield return new WaitForSecondsRealtime(1f);

        _isLoading = false;
    }
}
