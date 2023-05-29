using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [type: SerializeField]
    public enum eCutScene { 
        Encounter, 
        InGame
    }

    public eCutScene cutSceneType;

    public PlayableDirector myPD;
    private SceneLoader sceneLoader;
    private List<GameObject>[] list;

    public int myScene;
    public int nextScene;

    private void Awake()
    {
    }

    void Start()
    {
        myPD = GetComponent<PlayableDirector>();
        sceneLoader = SceneLoader.Inst;
        myScene = this.gameObject.scene.buildIndex;

        myPD.initialTime = 0.0f;

        if (cutSceneType == eCutScene.Encounter)
        {
            SceneManager.activeSceneChanged += SetUpUIManager;
            StartCoroutine(PlayEncounterCutScene());
        }
        else if (cutSceneType == eCutScene.InGame)
        {
            //SceneManager.sceneLoaded += DeActivateSetUp;
            StartCoroutine(PlayInGameCutScene());
        }
    }

    IEnumerator PlayEncounterCutScene()
    {
        float offset = 0.1f;
        UIManager.instance.gameObject.SetActive(false);
        myPD.Play();
        yield return new WaitForSeconds((float)myPD.duration - offset);
        
        sceneLoader.SceneLoad(nextScene);
    }

    IEnumerator PlayInGameCutScene()
    {
        float offset = 0.1f;
        SetUpUI(false);

        //Active ���� Dragon�� Player�� ��ġ ���� ���� ���� �־��ֱ�
        //foreach (GameObject i in list) i = this.gameObject.scene.GetRootGameObjects;

        myPD.Play();
        yield return new WaitForSeconds((float)myPD.duration - offset);
        sceneLoader.SceneUnload(myScene);
        SetUpUI(true);
    }

    public void SetUpUI(bool enable)
    {
        UIManager.instance.gameObject.SetActive(enable);

        DisableSkinnedRenderer(Gamemanager.inst.myPlayer.gameObject, enable);
        if (Gamemanager.inst.myDragon != null) DisableSkinnedRenderer(Gamemanager.inst.myDragon.gameObject, enable);
        else
        {
            // myEnemy�� �迭 ���¿��� �����ʴ°�...?
            DisableSkinnedRenderer(Gamemanager.inst.myEnemy.gameObject, enable);
            Gamemanager.inst.myEnemy.gameObject.GetComponentInChildren<MeshRenderer>().enabled = enable;
        }
    }


    // Single Scene Unload ��Ȳ���� ���� Scene�� ���� ���� ��
    void SetUpUIManager(Scene curScene, Scene nextScene)
    {
        Gamemanager.Inst.myPlayer = FindObjectOfType<PlayerController>();
        Gamemanager.Inst.myDragon = FindObjectOfType<Dragon>();
        if(Gamemanager.Inst.myDragon == null) Gamemanager.Inst.myEnemy = FindObjectOfType<Monster>();

        UIManager.instance.gameObject.SetActive(true);

    }

    public void DisableSkinnedRenderer(GameObject obj, bool enable)
    {
        foreach(var i in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            i.enabled = enable;
        }
    }

    // Additive Scene�� ���� ��, UI �� MainScene GameObject ���ֱ�
    void DeActivateSetUp(Scene nextScene, LoadSceneMode loadSceneMode)
    {
        Gamemanager.Inst.myPlayer = FindObjectOfType<PlayerController>();
        Gamemanager.Inst.myDragon = FindObjectOfType<Dragon>();
        if(Gamemanager.Inst.myDragon == null) Gamemanager.Inst.myEnemy = FindObjectOfType<Monster>();

        UIManager.instance.gameObject.SetActive(false);

        if (loadSceneMode != LoadSceneMode.Single)
        {
            DisableSkinnedRenderer(Gamemanager.Inst.myPlayer.gameObject, false);
            DisableSkinnedRenderer(Gamemanager.Inst.myDragon.gameObject, false);
        }
    }



}
