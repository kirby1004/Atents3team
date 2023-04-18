using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragItem : MonoBehaviour ,IDragHandler ,IBeginDragHandler , IEndDragHandler , IPointerClickHandler
{
    public Transform orgParent
    {
        get;private set;
    }
    Vector2 mousePos = Vector2.zero;

    public Image myIcon = null;
    public float coolTime = 3.0f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Mathf.Approximately(myIcon.fillAmount, 1.0f))
        {
            myIcon.fillAmount = 0.0f;
            StopAllCoroutines();
            StartCoroutine(Cooling());
        }
    }

    IEnumerator Cooling()
    {
        float speed = 1.0f / coolTime;
        while (myIcon.fillAmount < 1)
        {
            myIcon.fillAmount += speed * Time.deltaTime;
            yield return null;
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = (Vector2)transform.position - eventData.position;
        orgParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + mousePos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(orgParent);
        transform.localPosition = Vector3.zero;
        GetComponent<Image>().raycastTarget = true;
    }

    public void ChangeParent(Transform p,bool update =false)
    {
        orgParent = p;
        if (update)
        {
            transform.SetParent(p);
            transform.localPosition = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //orgPos = transform.position;
        myIcon.fillAmount = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
