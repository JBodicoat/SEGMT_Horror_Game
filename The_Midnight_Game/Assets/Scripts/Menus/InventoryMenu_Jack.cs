// Jack 23/02/2020 Created script
// Jack 01/04/2020 Added mouse support

using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the in-game inventory menus navigation and display.
/// </summary>
public class InventoryMenu_Jack : Menu
{
    public GameObject inventoryMenu;

    public Text descriptionText;

    [System.Serializable]
    public struct Item
    {
        public GameObject panel;
        public Text quantity;
        public string description;
    }
    [SerializeField]public List<Item> items = new List<Item>();

    private ushort displayedItemsCount = 11; // Starts with only matches and salt displayed

    private const ushort width = 4;
    private const ushort height = 3;
    private const ushort maxItems = width * height;
    private bool[] itemsDisplayed = new bool[maxItems];

    private int selectedColumn = 0;
    private int selectedRow = 0;

    // Input
    private InputDevice inputDevice;

    private void Awake()
    {
        itemsDisplayed[0] = true;
        itemsDisplayed[1] = true;
        for (ushort i = 2; i < maxItems; ++i)
        {
            //itemsDisplayed[i] = false;
            itemsDisplayed[i] = true;
        }
        itemsDisplayed[2] = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Temporary for testing
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        }

        if (inventoryMenu.activeSelf)
        {
            if (HasSelectionChanged())
            {
                for (int i = 0; i < buttons.Count; ++i)
                {
                    if (buttons[i].IsSelected())
                    {
                        // i = items[index]


                        int numItemsDisplayed = 1;
                        for (int j = 0; j < maxItems; ++j)
                        {
                            if (j == i)
                            {
                                // temp 
                                Select(numItemsDisplayed);
                                break;
                            }
                            else if (itemsDisplayed[j])
                            {
                                ++numItemsDisplayed;
                            }
                        }
                        break;
                    }
                }
            }

            inputDevice = InputManager.ActiveDevice;
            if (InputManager.Devices.Count > 0)
            {
                // Controller input
                if (inputDevice.Direction.Left.WasPressed)
                {
                    SelectLeft();
                }
                else if (inputDevice.Direction.Right.WasPressed)
                {
                    SelectRight();
                }

                if (inputDevice.Direction.Up.WasPressed)
                {
                    SelectUp();
                }
                else if (inputDevice.Direction.Down.WasPressed)
                {
                    SelectDown();
                }
            }
            else
            {
                // Keyboard input
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SelectLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SelectRight();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SelectUp();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SelectDown();
                }
            }
        }
    }

    /// <summary>
    /// Increments selectedColumn.
    /// </summary>
    private void IncrementColumn()
    {
        if (++selectedColumn >= width)
        {
            selectedColumn = 0;
        }
    }

    /// <summary>
    /// Decrements selectedColumn.
    /// </summary>
    private void DecrementColumn()
    {
        if (--selectedColumn < 0)
        {
            selectedColumn = width - 1;
        }
    }

    /// <summary>
    /// Increments selectedRow.
    /// </summary>
    private void IncrementRow()
    {
        if (++selectedRow >= width)
        {
            selectedRow = 0;
        }
    }

    /// <summary>
    /// Decrements selectedRow.
    /// </summary>
    private void DecrementRow()
    {
        if (--selectedRow < 0)
        {
            selectedRow = height - 1;
        }
    }

    /// <summary>
    /// Selects the item to the left.
    /// </summary>
    /// If the currently selected item is the furtherst left the furthest right
    /// item in the same row is selected.
    private void SelectLeft()
    {
        DeSelectAllButtons();

        int originalColumn = selectedColumn;

        DecrementColumn();

        int selectPosition;

        bool canMove = false;
        do
        {
            selectPosition = GetSelectPosition();
            if (selectPosition > displayedItemsCount)
            {
                DecrementColumn();
                if (selectedColumn == originalColumn)
                {
                    selectPosition = GetSelectPosition();
                    break;
                }
            }
            else
            {
                canMove = true;
            }
        } while (!canMove);

        Select(selectPosition);
    }

    /// <summary>
    /// Selects the item to the right.
    /// </summary>
    /// If the currently selected item is the furtherst right the furthest left
    /// item in the same row is selected.
    private void SelectRight()
    {
        DeSelectAllButtons();

        int originalColumn = selectedColumn;

        IncrementColumn();

        int selectPosition;

        bool canMove = false;
        do
        {
            selectPosition = GetSelectPosition();
            if (selectPosition > displayedItemsCount)
            {
                IncrementColumn();
                if (selectedColumn == originalColumn)
                {
                    selectPosition = GetSelectPosition();
                    break;
                }
            }
            else
            {
                canMove = true;
            }
        } while (!canMove);

        Select(selectPosition);
    }

    /// <summary>
    /// Selects the item above.
    /// </summary>
    /// If the currently selected item is the top the bottom
    /// item in the same column is selected.
    private void SelectUp()
    {
        DeSelectAllButtons();

        int originalRow = selectedRow;

        DecrementRow();

        int selectPosition;

        bool canMove = false;
        do
        {
            selectPosition = GetSelectPosition();
            if (selectPosition > displayedItemsCount)
            {
                DecrementRow();
                if (selectedRow == originalRow)
                {
                    selectPosition = GetSelectPosition();
                    break;
                }
            }
            else
            {
                canMove = true;
            }
        } while (!canMove);

        Select(selectPosition);
    }

    /// <summary>
    /// Selects the item below.
    /// </summary>
    /// If the currently selected item is the bottom the top
    /// item in the same column is selected.
    private void SelectDown()
    {
        DeSelectAllButtons();

        int originalRow = selectedRow;

        IncrementRow();

        int selectPosition;

        bool canMove = false;
        do
        {
            selectPosition = GetSelectPosition();
            if (selectPosition > displayedItemsCount)
            {
                IncrementRow();
                if (selectedRow == originalRow)
                {
                    selectPosition = GetSelectPosition();
                    break;
                }
            }
            else
            {
                canMove = true;
            }
        } while (!canMove);

        Select(selectPosition);
    }

    /// <summary>
    /// Returns the currently selected items count position in the itemsDisplayed array.
    /// </summary>
    /// <returns></returns>
    private int GetSelectPosition()
    {
        return selectedColumn + 1 + width * selectedRow;
    }

    /// <summary>
    /// Selects the item at selectPosition in itemsDisplayed.
    /// </summary>
    /// Example:
    /// selectPosition = 4. The 4th item that is set as displayed in itemsDisplayed will be selected.
    /// <param name="selectPosition"></param>
    private void Select(int selectPosition)
    {
        ushort itemToSelect = 0;

        ushort tempDisplayedCount = 0;
        for (ushort i = 0; i < itemsDisplayed.Length; ++i)
        {
            if (itemsDisplayed[i])
            {
                if (++tempDisplayedCount == selectPosition)
                {
                    itemToSelect = i;
                    break;
                }
            }
        }

        //items[itemToSelect].button.Select();
        buttons[itemToSelect].Select();
        descriptionText.text = items[itemToSelect].description;
    }

    /// <summary>
    /// Sets the display of items.
    /// </summary>
    /// Matches and Salt are always displayed.
    /// Once dolls are displayed they are always displayed.
    /// Other items are only displayed when their quantity is above 0.
    /// <param name="itemType"></param>
    /// <param name="newQuantity"></param>
    public void UpdateItem(ItemType itemType, ushort newQuantity)
    {
        if(itemType == ItemType.dolls)
        {
            // Once displayed dolls will always appear in inventory.
            if (newQuantity > 0 && !items[(ushort)itemType].panel.activeSelf)
            {
                items[(ushort)itemType].panel.SetActive(true);
                ++displayedItemsCount;
                itemsDisplayed[(ushort)ItemType.dolls] = true;
                items[(ushort)itemType].quantity.text = newQuantity.ToString();
            }
        }
        else
        {
            if (newQuantity > 0)
            {
                if (!items[(ushort)itemType].panel.activeSelf)
                {
                    items[(ushort)itemType].panel.SetActive(true);
                    ++displayedItemsCount;
                    itemsDisplayed[(ushort)itemType] = true;
                    items[(ushort)itemType].quantity.text = newQuantity.ToString();
                }
            }
            else if (items[(ushort)itemType].panel.activeSelf)
            {
                items[(ushort)itemType].panel.SetActive(false);
                --displayedItemsCount;
                itemsDisplayed[(ushort)itemType] = false;
            }
        }
    }
}
