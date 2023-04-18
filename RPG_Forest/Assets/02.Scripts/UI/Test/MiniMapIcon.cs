using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapIcon : MonoBehaviour
{
    public Transform myRoot = null;
    public Image myIcon = null;

    public void updateRocate(Transform myRocate)
    {

    }

    public void Initialize(Transform p , Color color)
    {
        myRoot = p;
        myIcon.color = color;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.allCameras[2].WorldToViewportPoint(myRoot.position);
        RectTransform rt = transform.parent.GetComponent<RectTransform>();
        //rt.sizeDelta;
        pos.x = pos.x * rt.sizeDelta.x - rt.sizeDelta.x * 0.5f;
        pos.y = pos.y * rt.sizeDelta.y - rt.sizeDelta.y * 0.5f;

        transform.localPosition = pos;
    }
}
