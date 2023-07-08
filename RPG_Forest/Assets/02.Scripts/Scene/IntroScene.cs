using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ó���� ���� ���ӽ����̽� ����� �ϰڴٴ� ���ù� ����
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{

    public Button NewStartBtn;
    public Button ContinueBtn;

    
    public enum StartType
    {
        NewStart, 
        Continue
    }

    StartType curType;
    private void Awake()
    {
        curType = new StartType();
    }


    // IntroScene������ ����
    // ���ӸŴ��� ���� ����
    void Start()
    {

        SceneManager.activeSceneChanged += SetUI;
        NewStartBtn.onClick.AddListener(() => 
        {
            curType = StartType.NewStart;
            DataSaverManager.Inst.ResetAllData();
            StartCoroutine(ConvertingToLoadingScene());    
        });
        ContinueBtn.onClick.AddListener(() =>
        {
            curType = StartType.Continue;
            if(DataSaverManager.Inst.testData.PlayerData.LastMapIndex != -1)
            {
                //SceneManager.LoadScene(DataSaverManager.Inst.testData.PlayerData.LastMapIndex);
                StartCoroutine(ConvertingToLoadingScene());
            }
            else
            {
                NewStartBtn.onClick.Invoke();
            }
        });
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }

    IEnumerator ConvertingToLoadingScene()
    {
        yield return new WaitForSeconds(3.0f);
        if(curType == StartType.NewStart)
        {
            SceneManager.LoadScene(7);  // Unity�� one-source-multi-flatform tool
        }
        else if(curType == StartType.Continue)
        {
            SceneManager.LoadScene(DataSaverManager.Inst.testData.PlayerData.LastMapIndex);
        }
                                    // Loading Scene �� 1�� �ε���
        
    }

    // ���������� �Ѿ���� ����
    // ���ӸŴ��� �����ϴ½���
    void SetUI(Scene preScene, Scene postScene)
    {
        
        //UIManager.instance.gameObject.SetActive(true);
        //UIManager.instance.Refresh();
        
        
        SceneManager.activeSceneChanged -= SetUI;
        SceneManager.activeSceneChanged += SceneLoader.Inst.SetUI;

        Gamemanager.inst.mySpawnner.Clear();

        if (curType == StartType.NewStart)
        {
            //new WaitForEndOfFrame();
            DataSaverManager.Inst.ResetAllSlots();
        }
        else if (curType == StartType.Continue)
        {
            DataSaverManager.Inst.LoadJsonFile();
        }
        if(Gamemanager.inst.myPlayer.enabled != true)
        {
            Gamemanager.inst.myPlayer.enabled = true;
        }
        Destroy(this.gameObject);
    }

    

}
