using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    void Awake()
    {
        base.Initialize();
    }

    public int curLoading;
    // �� �ε� ���� -> Loading Scene�� ��ĥ �� ���
    public void ChangeMap(int destMap)
    {
        curLoading = destMap;
        UIManager.instance.gameObject.SetActive(false);
        Gamemanager.inst.gameObject.SetActive(false);
        StartCoroutine(MapLoading(destMap));
    }

    IEnumerator MapLoading(int destMap)
    {
        yield return SceneManager.LoadSceneAsync(1); // Loads the Scene asynchronously(�񵿱�) in the background.
        AsyncOperation op = SceneManager.LoadSceneAsync(destMap); // �ڷ�ƾ�� ������¸� Ȯ���ϴ� ���
        op.allowSceneActivation = false; // Scene Loading �� ������ �ٷ� Ȱ��ȭ �ǰ� �ϴ� �� �� => false

        Slider slider = FindObjectOfType<Slider>();

        while (!op.isDone)
        {
            slider.value = op.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(5.0f); // Debuging�� ������ �߰�  
                UIManager.instance.gameObject.SetActive(true);
                Gamemanager.inst.gameObject.SetActive(true);
                op.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(2.0f); //   
        }
    }

    // Encounter CutScene �� ��
    public void SceneLoad(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex);
    }

    // Play-In CutScene �� ��
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

    public IEnumerator Unloading(int sceneIndex)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneIndex);

        while (!unloadOperation.isDone)
        {
            
            yield return null;
        }
    }




}
