using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton_Jack : MonoBehaviour, IPointerEnterHandler
{
    public Menu menu;
    
    private Image buttonImage;
    public Color unselectedColour;
    public Color selectedColour;

    public bool selected = false;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.MouseSelection();
        menu.DeSelectAllButtons();
        Select();
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
