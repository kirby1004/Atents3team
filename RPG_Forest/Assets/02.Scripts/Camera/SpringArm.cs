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
    public Transform cameraPoint;
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
        cameraPoint = myCam.transform;
        curRot = transform.localRotation.eulerAngles;
        myZoomData.desireDist = myZoomData.curDist = myCam.localPosition.magnitude;
    }

    [field: SerializeField] public bool toggleCameraRotation { get; set; }
    void Update()
    {

        if (Input.GetMouseButton(1) && !CameraChange)
        {
            curRot.x = Mathf.Clamp(curRot.x - Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime, LookUpRange.x, LookUpRange.y);
            curRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
        }

        if (!toggleCameraRotation)
        {
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0), Time.deltaTime * 10.0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }

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

    bool CameraChange = false;
    public void ViewPointTransformation(Transform ViewPoint)
    {
        myCam.transform.SetParent(null);
        ChangeViewPoint(ViewPoint);
        CameraChange = true;
    }

    public void ViewPointReset(Transform Parent)
    {
        transform.SetParent(Parent);
        CameraChange = false;
        ResetSetting();
        myCam.position = cameraPoint.position;
        myCam.rotation = cameraPoint.rotation;
    }

    public void ResetSetting()
    {
        curRot = transform.localRotation.eulerAngles;
        myZoomData.desireDist = myZoomData.curDist = myCam.localPosition.magnitude;
    }

    public void ChangeViewPoint(Transform ViewPoint) //코루틴을 실행시키는 함수
    {
        StartCoroutine(ChangingViewPoint(ViewPoint));
    }

    IEnumerator ChangingViewPoint(Transform ViewPoint) //카메라의 시점을 전환시켜주는 코루틴, ViewPoint를 받아 ViewPoint로 옮겨서 이동함.
    {
        Vector3 v = ViewPoint.position - myCam.transform.position;
        float Dist = v.magnitude;

        while (Dist > 0)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (Dist < delta) delta = Dist;
            transform.position = Vector3.Lerp(myCam.transform.position, ViewPoint.position, delta); //위치, 회전 보간을 통해 움직임
            transform.rotation = Quaternion.Slerp(myCam.transform.rotation, Quaternion.Euler(ViewPoint.rotation.eulerAngles), 10.0f * Time.deltaTime);
            Dist -= delta;
            yield return null;
        }
    }
}
