using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class selfDropdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    RectTransform r;
    bool isOpen;

    void Start()
    {
        r = transform.FindChild("containerDropDown").GetComponent<RectTransform>();
        isOpen = false;
    }


    void Update()
    {
      
        Vector3 scale = r.localScale;
        scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 10f);
        r.localScale = scale;
    }

  


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isOpen = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isOpen = false;
    }
}
