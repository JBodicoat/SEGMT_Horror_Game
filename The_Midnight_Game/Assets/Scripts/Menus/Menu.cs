using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Handles changing the games input and it's input menu.
public class Menu : MonoBehaviour
{
    public List<MenuButton_Jack> buttons = new List<MenuButton_Jack>();

    protected int selectedButton = 0;

    private bool selectionChanged = false;

    /// <summary>
    /// Increments the selected player action.
    /// </summary>
    protected void IncrementSelect()
    {
        DeSelectAllButtons(); 
        if (++selectedButton >= buttons.Count)
        {
            selectedButton = 0;
        }
        buttons[selectedButton].Select();
    }

    /// <summary>
    /// Decrements the selected player action.
    /// </summary>
    protected void DecrementSelect()
    {
        DeSelectAllButtons();
        if (--selectedButton < 0)
        {
            selectedButton = buttons.Count - 1;
        }
        buttons[selectedButton].Select();
    }

    public void DeSelectAllButtons()
    {
        foreach(MenuButton_Jack button in buttons)
        {
            if (button.isActiveAndEnabled)
            {
                button.DeSelect();
            }
        }
    }

    public void MouseSelection()
    {
        selectionChanged = true;
    }

    public bool HasSelectionChanged()
    {
        bool returnValue = selectionChanged;
        selectionChanged = false;
        return returnValue;
    }
}
