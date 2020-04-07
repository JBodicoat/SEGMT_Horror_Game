// Jack - 31/03/2020 Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles changing the games input and it's input menu.
/// </summary>
public class Menu_Jack : MonoBehaviour
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

    /// <summary>
    /// Sets all attached buttons to unselected.
    /// </summary>
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

    /// <summary>
    /// Sets selectionChanged to true.
    /// Used by Button scripts when mouse has been used to select it.
    /// </summary>
    public void MouseSelection()
    {
        selectionChanged = true;
    }

    /// <summary>
    /// Returns true if a button has been selected using the mouse.
    /// Sets selectionChanged to false on being called.
    /// Use within child classes to check if the highlighted button needs to be changed.
    /// </summary>
    /// <returns></returns>
    public bool HasSelectionChanged()
    {
        bool returnValue = selectionChanged;
        selectionChanged = false;
        return returnValue;
    }
}
