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


    public PlayableDirector myPD;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float time;
    

    void Start()
    {
        //UIManager.instance.gameObject.SetActive(false);
        myPD = GetComponent<PlayableDirector>();
        myPD.initialTime = 0.0f;
        duration = (float)myPD.playableAsset.duration;
        time = (float)myPD.initialTime;

        SceneManager.activeSceneChanged += SetUpUIManager;
        SceneManager.sceneLoaded += DeActivateSetUp;
    }

    void Update()
    {
        time += Time.deltaTime;

        if(time > duration)
        {
            if(this.gameObject.scene.buildIndex != 3) SceneManager.LoadScene(2);
            else
            {
                SceneLoader.Inst.SceneUnload(3);


                DisableSkinnedRenderer(Gamemanager.Inst.myPlayer.gameObject);
                DisableSkinnedRenderer(Gamemanager.Inst.myDragon.gameObject);

            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded += ActivateSetUp;

    }

    // Single Scene Unload 상황에서 다음 Scene이 게임 씬일 때
    void SetUpUIManager(Scene curScene, Scene nextScene)
    {
        Gamemanager.Inst.myPlayer = FindObjectOfType<PlayerController>();
        Gamemanager.Inst.myEnemy = FindObjectOfType<Monster>();
        Gamemanager.Inst.myDragon = FindObjectOfType<Dragon>();

        if (nextScene.buildIndex == 2)
        {
            UIManager.instance.gameObject.SetActive(true);
        }
        else
        {
            UIManager.instance.gameObject.SetActive(false);
        }
    }

    // Additive Scene이 열릴 때, UI 및 Main Scene GameObject 꺼주기
    void DeActivateSetUp(Scene nextScene, LoadSceneMode loadSceneMode)
    {
        Gamemanager.Inst.myPlayer = FindObjectOfType<PlayerController>();
        Gamemanager.Inst.myEnemy = FindObjectOfType<Monster>();
        Gamemanager.Inst.myDragon = FindObjectOfType<Dragon>();

        UIManager.instance.gameObject.SetActive(false);

        if (loadSceneMode != LoadSceneMode.Single)
        {
            DisableSkinnedRenderer(Gamemanager.Inst.myPlayer.gameObject, false);
            DisableSkinnedRenderer(Gamemanager.Inst.myDragon.gameObject, false);
        }
    }

    void ActivateSetUp(Scene nextScene)
    {
        Gamemanager.Inst.myUIManager.gameObject.SetActive(true);
    }

    void DisableSkinnedRenderer(GameObject obj, bool enable = true)
    {
        foreach(var i in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            i.enabled = enable;
        }
    }


}
