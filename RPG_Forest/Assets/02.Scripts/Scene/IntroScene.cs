using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ó���� ���� ���ӽ����̽� ����� �ϰڴٴ� ���ù� ����

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ConvertingToLoadingScene());
        SceneManager.activeSceneChanged += SetUI;
    }

    void Update()
    {
        
    }

    IEnumerator ConvertingToLoadingScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(7);  // Unity�� one-source-multi-flatform tool
                                    // Loading Scene �� 1�� �ε���
    }

    void SetUI(Scene preScene, Scene postScene)
    {
        if(postScene.isLoaded)
        {
            UIManager.instance.Refresh();
        }
    }

}
