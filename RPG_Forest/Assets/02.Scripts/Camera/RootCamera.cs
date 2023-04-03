using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCamera : MonoBehaviour
{

    public Transform myTarget;
    Vector3 Dir = Vector3.zero;
    float Dist = 0.0f;
    private void Awake()
    {
        Dir = transform.position - myTarget.position;
        Dist = Dir.magnitude;
        Dir.Normalize();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse Y");
            float y = Input.GetAxis("Mouse X");
            Quaternion rot = Quaternion.Euler(x, y, 0);
            Dir = rot * Dir;
            transform.position = myTarget.position + Dir * Dist;
            transform.LookAt(myTarget);
        }
        
    }
}
