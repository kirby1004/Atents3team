using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene_V2 : MonoBehaviour
{
    public enum eCutScene
    {
        Encounter,
        InGame
    }

    public eCutScene cutSceneType;
    public int sceneIndex;

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = SceneLoader.Inst;
    }

    public void PlayCutScene()
    {

    }

}
