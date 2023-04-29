using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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

    public bool isPlayer=true; //�÷��̾ �پ����� �� true, �ƴϸ� false
    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;

    private void Awake()
    {
        transform.LookAt(myTarget);
        rotX = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        Dir = transform.position - myTarget.position;
        TargetDist = Dist = Dir.magnitude;
        Dir.Normalize();
        Dir = transform.InverseTransformDirection(Dir);
    }
    void Update()
    {

        if (isPlayer) //�÷��̾ �پ����� ���� ����ǵ���.
        {
            if (Input.GetMouseButton(1))
            {
                float x = Input.GetAxis("Mouse Y") * RotSpeed;
                float y = -Input.GetAxis("Mouse X") * RotSpeed;

                rotX *= Quaternion.Euler(x, 0, 0);
                rotY *= Quaternion.Euler(0, y, 0);

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
                Dist = hit.distance - radius;

            }

            transform.position = myTarget.position + dir * Dist;

            transform.LookAt(myTarget);
        }
       
    }

    public  void ChangeViewPoint(Transform ViewPoint) //�ڷ�ƾ�� �����Ű�� �Լ�
    {
        StartCoroutine(ChangingViewPoint(ViewPoint));
    }

    IEnumerator ChangingViewPoint(Transform ViewPoint) //ī�޶��� ������ ��ȯ�����ִ� �ڷ�ƾ, ViewPoint�� �޾� ViewPoint�� �Űܼ� �̵���.
    {
        Vector3 v = ViewPoint.position - transform.position; 
        float Dist = v.magnitude;

        while (Dist>0) 
        {
            float delta = RotSpeed * Time.deltaTime;
            if (Dist < delta) delta = Dist;
            transform.position = Vector3.Lerp(transform.position, ViewPoint.position, delta); //��ġ, ȸ�� ������ ���� ������
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(ViewPoint.rotation.eulerAngles), 10.0f * Time.deltaTime);
            Dist-= delta;
            yield return null;
        }
    }

    public void Camera_PlayerToOther(Transform convertPoint) //�÷��̾�� �ٸ� �������� �ű�� �Լ�, �÷��̾���� �θ� ���踦 ���´�.
    {
        transform.SetParent(null);
        ChangeViewPoint(convertPoint);
        isPlayer = false;   //�ٸ� �������� �Ű�� ������ isPlayer�� false��
    }

    public void Camera_OtherToPlayer(Transform Parent) //�ٸ� �������� �ٽ� �÷��̾�� ���ƿ��� �ϴ� �Լ�. �θ� ���踦 �ٽ� �������ش�.
    {
        transform.SetParent(Parent);    
        isPlayer = true;    
        ResetRotate(); //�ʱ�ȭ
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void ResetRotate() //�ٸ� �������� ĳ���ͷ� ���ƿ� ��, Dist, Dir�� ���� ���� ������ �ֱ� ������ ������ �߻��ؼ� ������ �ʱ�ȭ ��Ű�� �Լ�
    {
        rotX = Quaternion.identity;
        rotY = Quaternion.identity;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        Dist = 0.0f;
        TargetDist = 0.0f;
        Dir = transform.position - myTarget.position;
    }
}
