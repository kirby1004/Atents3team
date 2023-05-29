using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneView : MonoBehaviour
{
    
    public List<Image> ForestImages;
    public List<Image> VillageImages;
    public List<Image> BossmapImages;

    // Start is called before the first frame update
    void Start()
    {
        switch (SceneLoader.Inst.curLoading)
        {
            case 5:
                ForestImages[Random.Range(0, ForestImages.Count)].gameObject.SetActive(true);
                break;
            case 6:
                VillageImages[Random.Range(0,VillageImages.Count)].gameObject.SetActive(true);
                break;
            case 2:
                BossmapImages[Random.Range(0,VillageImages.Count)].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
