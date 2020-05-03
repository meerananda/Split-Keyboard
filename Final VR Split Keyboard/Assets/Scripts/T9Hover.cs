using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class T9Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{


    public GameObject CanvasABC;

    void Start()
    {
        //image = GetComponent<Image>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    void OnHoverEnter()
    {
        CanvasABC.SetActive(true);
    }

    void OnHoverExit()
    {
        CanvasABC.SetActive(false);
    }

    void OnClick()
    {
        //image.color = Color.blue;
    }

}