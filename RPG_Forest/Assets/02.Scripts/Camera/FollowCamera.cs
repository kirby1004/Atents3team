using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool isOnShake;
    public Transform myTarget;
    Vector3 Dir = Vector3.zero;
    float Dist = 0.0f;
    float TargetDist = 0.0f;

    public float RotSpeed = 5.0f;
    public float ZoomSpeed = 30.0f;
    public LayerMask crashMask;

    private void Awake()
    {
        transform.LookAt(myTarget);
        rotX = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        Dir = transform.position - myTarget.position;
        TargetDist = Dist = Dir.magnitude;
        Dir.Normalize();
        Dir = transform.InverseTransformDirection(Dir);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse Y") * RotSpeed;
            float y = -Input.GetAxis("Mouse X") * RotSpeed;

            rotX *= Quaternion.Euler(x, 0, 0);
            rotY *= Quaternion.Euler(0, y, 0);
            //Quaternion rot = Quaternion.Euler(0, y, 0)*Quaternion.Euler(x, 0, 0);
            //Dir = rot * Dir;

            //transform.forward = -Dir; //dir�� ���� �ٶ󺸴� Ÿ���� ����, �װ� -�ϐ� ���� ���溤��...
            //transform.LookAt(myTarget);
            //transform.rotation = Quaternion.LookRotation(-Dir);
            float angle = rotX.eulerAngles.x;
            if (angle > 180.0f) angle -= 360;
            angle = Mathf.Clamp(angle, -60, 80);
            rotX = Quaternion.Euler(angle, 0, 0);
        }
        TargetDist -= Input.GetAxis("Mouse ScrollWheel");
        TargetDist = Mathf.Clamp(TargetDist, 1.0f, 10.0f);

        Dist = Mathf.Lerp(Dist, TargetDist, Time.deltaTime * 3.0f);

        Vector3 dir = rotY * rotX * Dir;
        float radius = 0.5f;
        if (Physics.Raycast(new Ray(myTarget.position, dir), out RaycastHit hit, Dist + radius, crashMask))
        {
            /* transform.position = hit.point+-dir * radius; *///�浹�� �������� �̵��ϱ� ������ �ذ�Ǵ� ������ ���
            Dist = hit.distance - radius; //ray ������ �������� �ε��� �������� �Ÿ����� ���� �������� ��.

        }
        //else
        //{
        transform.position = myTarget.position + dir * Dist;
        //}

        //transform.position = myTarget.position + rotY * rotX * Dir * Dist;
        transform.LookAt(myTarget);
    }
}
