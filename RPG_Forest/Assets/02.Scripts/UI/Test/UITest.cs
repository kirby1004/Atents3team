using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public void MoveBox(float v)
    {
        transform.position = orgPos + Vector2.right * v;
    }


    Vector2 orgPos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
