using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관련 처리를 위한 네임스페이스 사용을 하겠다는 지시문 선언
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


    // IntroScene에서의 동작
    // 게임매니저 없는 시점
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
            SceneManager.LoadScene(7);  // Unity는 one-source-multi-flatform tool
        }
        else if(curType == StartType.Continue)
        {
            SceneManager.LoadScene(DataSaverManager.Inst.testData.PlayerData.LastMapIndex);
        }
                                    // Loading Scene 은 1번 인덱스
        
    }

    // 다음씬으로 넘어갈때의 동작
    // 게임매니저 존재하는시점
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
