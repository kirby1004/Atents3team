using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityAction action;

    void Awake()
    {
        base.Initialize();
    }

    public void ChangeScene(int i)
    {
        StartCoroutine(Loading(i));
    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync(i); // Loads the Scene asynchronously(비동기) in the background.
        AsyncOperation op = SceneManager.LoadSceneAsync(i); // 코루틴의 진행상태를 확인하는 방법
        op.allowSceneActivation = false; // Scene Loading 이 끝나면 바로 활성화 되게 하는 불 값 => false

        Slider slider = FindObjectOfType<Slider>();

        while (!op.isDone)
        {
            slider.value = op.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(10.0f); // Debuging용 딜레이 추가  
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void SceneLoadAdditive(int sceneIndex)
    {
        StartCoroutine(LoadingAdditive(sceneIndex));
    }

    IEnumerator LoadingAdditive(int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    public void SceneUnload(int sceneIndex)
    {
        StartCoroutine(Unloading(sceneIndex));
    }

    IEnumerator Unloading(int sceneIndex)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneIndex);

        while (!unloadOperation.isDone)
        {
            yield return null;
        }
    }


}
