// Jack - 31/03/2020 Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Used to bring mouse support to controller/keyboard controlled buttons.
/// </summary>
public class MenuButton_Jack : MonoBehaviour, IPointerEnterHandler
{
    public Menu_Jack menu;
    
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

    /// <summary>
    /// Selects this button when the mouse collides with the attached image.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.MouseSelection();
        menu.DeSelectAllButtons();
        Select();
    }
    
    /// <summary>
    /// Returns true if this button is selected.
    /// </summary>
    /// <returns></returns>
    public bool IsSelected()
    {
        return selected;
    }

    /// <summary>
    /// Selects this button and highlights it.
    /// </summary>
    public void Select()
    {
        selected = true;
        buttonImage.color = selectedColour;
    }

    /// <summary>
    /// Deselectes this button hiding the highlight.
    /// </summary>
    public void DeSelect()
    {
        selected = false;
        buttonImage.color = unselectedColour;
    }
}
