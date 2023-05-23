using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;                // ShakeCamera�� �ν��Ͻ� ���� ������ ����
    public static ShakeCamera Instance => instance;     // Instance ������Ƽ�� ���ٽ����� instance ���� ���� �Ҵ�

    private float shakeTime;                            // ��鸮�� �ð� ����
    private float shakeIntensity;                       // ��鸮�� ���� ����

    private SpringArm cameraController;          // CameraController �ν��Ͻ� ����

    public ShakeCamera()
    {
        instance = this;                                // �ν��Ͻ��� �ڱ��ڽ� �Ҵ�
    }

    private void Awake()
    {
        cameraController = GetComponent<SpringArm>();    // CameraController ������Ʈ ����
    }

    public void OnShakeCamera(float shakeTime = 0.1f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");       // ������ ����� ShakeByRotation �ڷ�ƾ ���� 
        StartCoroutine("ShakeByRotation");      // ShakeByRotation �ڷ�ƾ �Լ� ����
    }

    // ShakeByRotation�� ���� �� ȭ���� ������ ä��
    private IEnumerator ShakeByRotation()
    {
        //cameraController.isOnShake = true;      // ī�޶� ��Ʈ�ѷ��� isOnShake �ο� �� true ����

        Vector3 startRotation = transform.eulerAngles;  // �ʱ� ȸ���� ���Ϸ����� Vector3 ���·� startRotation�� ����

        float power = 5f;

        while (shakeTime > 0.0f)
        {
            float x = Random.Range(-1f, 1f); // ���� x, y, z �� ����
            float y = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);   // ���Ϸ��� ���ʹϾ����� ��ȯ �� transform.rotation�� ����

            shakeTime -= Time.deltaTime;

            yield return null;  // shakeTime while�� ���������� ������ ���� Update()�� ������ yield return null ���� ���� ����(while�� �ݺ�)
        }
        transform.rotation = Quaternion.Euler(startRotation);   // ȸ���� ����ġ

        //cameraController.isOnShake = false;     // isOnShake false�� ����

    }

    /*private IEnumerator ShakeByPosition()
    {
     Vector3 startPosition = transform.position;
     while (shakeTime > 0.0f)
         {
         transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
         shakeTime -= Time.deltaTime;
         yield return null;
         }
     transform.position = startPosition;
    }*/


}
