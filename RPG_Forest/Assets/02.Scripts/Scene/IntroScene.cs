using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ó���� ���� ���ӽ����̽� ����� �ϰڴٴ� ���ù� ����

public class IntroScene : MonoBehaviour
{

    void Start()
    {
        SceneManager.activeSceneChanged += SetUI;
        StartCoroutine(ConvertingToLoadingScene());
        DontDestroyOnLoad(this);
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
        
        //UIManager.instance.gameObject.SetActive(true);
        //UIManager.instance.Refresh();
        
        
        SceneManager.activeSceneChanged -= SetUI;
         
        Gamemanager.inst.mySpawnner.Clear();
        Destroy(this.gameObject);
    }


}
