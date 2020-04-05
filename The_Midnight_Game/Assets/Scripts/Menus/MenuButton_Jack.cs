using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton_Jack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Menu menu;
    
    private Image buttonImage;
    public Color unselectedColour;
    public Color selectedColour;

    public bool selected = false;
    private bool mouseOver = false;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        
        if(selected)
        {
            buttonImage.color = selectedColour;
        }
        else
        {
            buttonImage.color = unselectedColour;
        }
    }

    private void Update()
    {
        if(mouseOver && Input.GetMouseButtonDown(0))
        {
            // Click
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        menu.MouseSelection();
        menu.DeSelectAllButtons();
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
    
    public bool IsSelected()
    {
        return selected;
    }

    public void Select()
    {
        selected = true;
        buttonImage.color = selectedColour;
    }

    public void DeSelect()
    {
        selected = false;
        buttonImage.color = unselectedColour;
    }
}
