using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ó���� ���� ���ӽ����̽� ����� �ϰڴٴ� ���ù� ����

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ConvertingNextScene());
    }

    void Update()
    {
        
    }

    IEnumerator ConvertingNextScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);  // Unity�� one-source-multi-flatform tool
    }
}
