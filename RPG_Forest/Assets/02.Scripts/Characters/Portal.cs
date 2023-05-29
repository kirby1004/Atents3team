using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    int SceneNum;
    [SerializeField]
    GameObject Effect;
    [SerializeField]
    LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            Effect.SetActive(true);
            other.GetComponent<IinterPlay>()?.SetisObjectNear(true);
            other.GetComponent<IinterPlay>()?.OpenUi.AddListener(() => SceneLoader.Inst.ChangeMap(SceneNum));
        }
                
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            Effect.SetActive(false);
            other.GetComponent<IinterPlay>()?.SetisObjectNear(false);
            other.GetComponent<IinterPlay>()?.OpenUi.RemoveAllListeners();
        }
            

    }
}
