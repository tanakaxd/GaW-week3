using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;

    public void OnPointerEnter(PointerEventData eventData)
    {
        popup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popup.SetActive(false);
    }
}