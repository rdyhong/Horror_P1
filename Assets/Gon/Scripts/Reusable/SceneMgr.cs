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
    public static bool _isLoading = false;
    static ESceneType currentScene = ESceneType.Title;
    LoadSceneCover loadSceneCover;
    public static ESceneType GetCurrentScene() => currentScene;

    public void LoadScene(ESceneType sceneType)
    {
        if (_isLoading) return;

        currentScene = sceneType;

        UIMgr.Inst.ForceLockPop(true); // Lock UI Pop
        UIMgr.Inst.ForceLockPush(true); // Lock UI Push
        UIMgr.Inst.ClearAll(); // UI clear
        loadSceneCover = UIMgr.Inst.Push<LoadSceneCover>(true); // Show Cover

        _isLoading = true;

        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(currentScene.ToString());
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            loadSceneCover.SetLoadingBar(asyncOp.progress);

            if (asyncOp.progress >= 0.9f)
            {
                if(!asyncOp.allowSceneActivation) asyncOp.allowSceneActivation = true;
            }
            yield return null;
        }
        
        yield return new WaitForSecondsRealtime(1f);

        UIMgr.Inst.ForceLockPush(false);
        UIMgr.Inst.ForceLockPop(false);
        UIMgr.Inst.ClearAll();

        SetScene();

        _isLoading = false;
    }

    void SetScene()
    {
        switch(currentScene)
        {
            case ESceneType.Title:
                break;

            case ESceneType.Main:
                UIMgr.Inst.Push<PlayerBasePanel>(true);
                break;
        }
    }
}
