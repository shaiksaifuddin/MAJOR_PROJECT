using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementsScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float sizeToIncrease = 25;
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<LayoutElement>().minHeight += sizeToIncrease;
        this.gameObject.GetComponent<LayoutElement>().minWidth += sizeToIncrease;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<LayoutElement>().minHeight -= sizeToIncrease;
        this.gameObject.GetComponent<LayoutElement>().minWidth -= sizeToIncrease;
    }
}