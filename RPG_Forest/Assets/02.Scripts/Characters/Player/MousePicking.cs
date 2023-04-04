using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class MousePicking : MonoBehaviour
{
    public UnityEvent<Vector3> clickAction = null;
    public UnityEvent<Transform> AttackAction = null;
    public UnityEvent RightClick = null;
    public LayerMask pickMask;
    public LayerMask enemyMask;

  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RightClick?.Invoke();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask | enemyMask))
            {
                if (((1 << hit.transform.gameObject.layer) & enemyMask) != 0)
                {

                    AttackAction?.Invoke(hit.transform);
                }
                else
                {
                    clickAction?.Invoke(hit.point);
                }

            }
        }
    }
}
