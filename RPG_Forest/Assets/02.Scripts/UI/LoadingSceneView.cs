using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneView : MonoBehaviour
{
    public enum eCutScene
    {
        Boss =4 ,
        Forest = 5,
        Village 
        

    }

    public List<Image> ForestImages;
    public List<Image> VillageImages;
    public List<Image> BossmapImages;

    // Start is called before the first frame update
    void Start()
    {
        switch ((eCutScene)SceneLoader.Inst.curLoading)
        {
            case eCutScene.Forest:
                ForestImages[Random.Range(0, ForestImages.Count)].gameObject.SetActive(true);
                break;
            case eCutScene.Village:
                VillageImages[Random.Range(0,VillageImages.Count)].gameObject.SetActive(true);
                break;
            case eCutScene.Boss:
                BossmapImages[Random.Range(0,VillageImages.Count)].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
