using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
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
        yield return SceneManager.LoadSceneAsync(1); // Loads the Scene asynchronously(�񵿱�) in the background.
        AsyncOperation op = SceneManager.LoadSceneAsync(i); // �ڷ�ƾ�� ������¸� Ȯ���ϴ� ���
        op.allowSceneActivation = false; // Scene Loading �� ������ �ٷ� Ȱ��ȭ �ǰ� �ϴ� �� �� => false

        Slider slider = FindObjectOfType<Slider>();

        while (!op.isDone)
        {
            slider.value = op.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(1.0f); // Debuging�� ������ �߰�  
                op.allowSceneActivation = true;
            }
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
