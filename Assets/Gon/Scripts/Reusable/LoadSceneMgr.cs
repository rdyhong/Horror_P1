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

public class LoadSceneMgr : Singleton<LoadSceneMgr>
{
    static bool _isLoading = false;
    public void LoadScene(ESceneType sceneType)
    {
        if (_isLoading) return;

        _isLoading = true;

        StartCoroutine(LoadAsyncScene(sceneType));
    }

    IEnumerator LoadAsyncScene(ESceneType sceneType)
    {
        // UI ����
        UIMgr.Inst.ClearAll();

        // �ε� Ŀ��ȭ�� ����
        LoadSceneCover loadSceneCover = UIMgr.Inst.Push<LoadSceneCover>();
        UIMgr.Inst.SetForceLock(true);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneType.ToString());
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            loadSceneCover.SetLoadingBar(asyncOp.progress);

            if (asyncOp.progress >= 0.9f)
            {
                if (!asyncOp.allowSceneActivation)
                {
                    asyncOp.allowSceneActivation = true;
                }
            }
            yield return null;
        }

        loadSceneCover.SetLoadingBar(asyncOp.progress);

        yield return new WaitForSecondsRealtime(1f);

        UIMgr.Inst.SetForceLock(false);
        UIMgr.Inst.ClearAll();

        _isLoading = false;
    }
}
