using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ZoomData
{
    public float ZoomSpeed;
    public Vector2 ZoomRange;
    public float ZoomLerpSpeed;
    public float curDist;
    public float desireDist;
    public ZoomData(float speed)
    {
        ZoomSpeed = speed;
        ZoomRange = new Vector2(1, 2);
        ZoomLerpSpeed = 3.0f;
        curDist = 0.0f;
        desireDist = 0.0f;
    }
}

public class SpringArm : MonoBehaviour
{
    public Transform RightShoulder;
    public LayerMask crashMask;
    [SerializeField] float Offset = 0.5f;
    [SerializeField] float RotSpeed = 180.0f;
    Transform myCam = null;
    public ZoomData myZoomData = new ZoomData(3.0f);
    Vector3 curRot = Vector3.zero;
    [SerializeField] Vector2 LookUpRange = new Vector2(-60, 90);
    
    void Start()
    {
        myCam = GetComponentInChildren<Camera>().transform;
        curRot = transform.localRotation.eulerAngles;
        myZoomData.desireDist = myZoomData.curDist = myCam.localPosition.magnitude;
    }

    public bool toggleCameraRotation { get; set; } 
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            curRot.x = Mathf.Clamp(curRot.x - Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime, LookUpRange.x, LookUpRange.y);
            curRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
            if (toggleCameraRotation != true)
            {
                transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
            }
        }
        //else
        //{
        //    transform.position = RightShoulder.position + new Vector3(1, 0, 0) * 0.3f;
        //}

        myZoomData.desireDist = Mathf.Clamp(myZoomData.desireDist + Input.GetAxis("Mouse ScrollWheel") * myZoomData.ZoomSpeed, myZoomData.ZoomRange.x, myZoomData.ZoomRange.y);

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, myZoomData.curDist + Offset, crashMask))
        {
            myZoomData.curDist = hit.distance - Offset;
        }
        else
        {
            myZoomData.curDist = Mathf.Lerp(myZoomData.curDist, myZoomData.desireDist, Time.deltaTime * myZoomData.ZoomLerpSpeed);
        }

        myCam.localPosition = Vector3.back * myZoomData.curDist;
    }
}
