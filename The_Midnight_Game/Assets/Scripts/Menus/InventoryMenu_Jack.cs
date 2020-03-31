// Jack 23/02/2020 Created script

using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the in-game inventory menus navigation and display.
/// </summary>
public class InventoryMenu_Jack : MonoBehaviour
{
    public GameObject inventoryMenu;

    public Text descriptionText;

    public GameObject matchesPanel;
    public Text matchesQuantity;
    public string matchesDescription;
    public Image matchesHighlight;

    public GameObject saltPanel;
    public Text saltQuantity;
    public string saltDescription;
    public Image saltHighlight;

    public GameObject dollsPanel;
    public Text dollsQuantity;
    public string dollsDescription;
    public Image dollsHighlight;

    public GameObject item4Panel;
    public Text item4Quantity;
    public string item4Description;
    public Image item4Highlight;

    public GameObject item5Panel;
    public Text item5Quantity;
    public string item5Description;
    public Image item5Highlight;

    public GameObject item6Panel;
    public Text item6Quantity;
    public string item6Description;
    public Image item6Highlight;

    public GameObject item7Panel;
    public Text item7Quantity;
    public string item7Description;
    public Image item7Highlight;

    public GameObject item8Panel;
    public Text item8Quantity;
    public string item8Description;
    public Image item8Highlight;

    public GameObject item9Panel;
    public Text item9Quantity;
    public string item9Description;
    public Image item9Highlight;

    public GameObject item10Panel;
    public Text item10Quantity;
    public string item10Description;
    public Image item10Highlight;

    public GameObject item11Panel;
    public Text item11Quantity;
    public string item11Description;
    public Image item11Highlight;

    public GameObject item12Panel;
    public Text item12Quantity;
    public string item12Description;
    public Image item12Highlight;

    private ushort displayedItemsCount = 2; // Starts with only matches and salt displayed

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
        for(ushort i = 2; i < width; ++i)
        {
            itemsDisplayed[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary for testing
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        }

        inputDevice = InputManager.ActiveDevice;
        if (InputManager.Devices.Count > 0)
        {
            // Controller input
            if(inputDevice.Direction.Left.WasPressed)
            {
                SelectLeft();
            }
            else if(inputDevice.Direction.Right.WasPressed)
            {
                SelectRight();
            }

            if(inputDevice.Direction.Up.WasPressed)
            {
                SelectUp();
            }
            else if(inputDevice.Direction.Down.WasPressed)
            {
                SelectDown();
            }
        }
        else
        {
            // Keyboard input
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectLeft();
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectRight();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectUp();
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectDown();
            }
        }
    }

    /// <summary>
    /// Increments selectedColumn.
    /// </summary>
    private void IncrementColumn()
    {
        if(++selectedColumn >= width)
        {
            selectedColumn = 0;
        }
    }

    /// <summary>
    /// Decrements selectedColumn.
    /// </summary>
    private void DecrementColumn()
    {
        if(--selectedColumn < 0)
        {
            selectedColumn = width - 1;
        }
    }

    /// <summary>
    /// Increments selectedRow.
    /// </summary>
    private void IncrementRow()
    {
        if(++selectedRow >= width)
        {
            selectedRow = 0;
        }
    }

    /// <summary>
    /// Decrements selectedRow.
    /// </summary>
    private void DecrementRow()
    {
        if(--selectedRow < 0)
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
        DeSelect();

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
        DeSelect();

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
        DeSelect();

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
        DeSelect();

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
    /// Deselects the currently selected item.
    /// </summary>
    private void DeSelect()
    {
        int selectPosition = GetSelectPosition();

        ItemType itemToDeSelect = ItemType.sizeOf;

        ushort tempDisplayedCount = 0;
        for (ushort i = 0; i < itemsDisplayed.Length; ++i)
        {
            if (itemsDisplayed[i])
            {
                if (++tempDisplayedCount == selectPosition)
                {
                    itemToDeSelect = (ItemType)i;
                    break;
                }
            }
        }

        switch (itemToDeSelect)
        {
            case ItemType.matches:
                matchesHighlight.enabled = false;
                break;

            case ItemType.salt:
                saltHighlight.enabled = false;
                break;

            case ItemType.dolls:
                dollsHighlight.enabled = false;
                break;

            case ItemType.bottles:
                item4Highlight.enabled = false;
                break;

            case ItemType.item5:
                item5Highlight.enabled = false;
                break;

            case ItemType.item6:
                item6Highlight.enabled = false;
                break;

            case ItemType.item7:
                item7Highlight.enabled = false;
                break;

            case ItemType.item8:
                item8Highlight.enabled = false;
                break;

            case ItemType.item9:
                item9Highlight.enabled = false;
                break;

            case ItemType.item10:
                item10Highlight.enabled = false;
                break;

            case ItemType.item11:
                item11Highlight.enabled = false;
                break;

            case ItemType.item12:
                item12Highlight.enabled = false;
                break;

            default:
                print("Cannot DeSelect: Invalid ItemType");
                break;
        }
    }

    /// <summary>
    /// Selects the item at selectPosition in itemsDisplayed.
    /// </summary>
    /// Example:
    /// selectPosition = 4. The 4th item that is set as displayed in itemsDisplayed will be selected.
    /// <param name="selectPosition"></param>
    private void Select(int selectPosition)
    {
        ItemType itemToSelect = ItemType.sizeOf;

        ushort tempDisplayedCount = 0;
        for (ushort i = 0; i < itemsDisplayed.Length; ++i)
        {
            if (itemsDisplayed[i])
            {
                if (++tempDisplayedCount == selectPosition)
                {
                    itemToSelect = (ItemType)i;
                }
            }
        }   

        switch (itemToSelect)
        {
            case ItemType.matches:
                matchesHighlight.enabled = true;
                descriptionText.text = matchesDescription;
                break;

            case ItemType.salt:
                saltHighlight.enabled = true;
                descriptionText.text = saltDescription;
                break;

            case ItemType.dolls:
                dollsHighlight.enabled = true;
                descriptionText.text = dollsDescription;
                break;

            case ItemType.bottles:
                item4Highlight.enabled = true;
                descriptionText.text = item4Description;
                break;

            case ItemType.item5:
                item5Highlight.enabled = true;
                descriptionText.text = item5Description;
                break;

            case ItemType.item6:
                item6Highlight.enabled = true;
                descriptionText.text = item6Description;
                break;

            case ItemType.item7:
                item7Highlight.enabled = true;
                descriptionText.text = item7Description;
                break;

            case ItemType.item8:
                item8Highlight.enabled = true;
                descriptionText.text = item8Description;
                break;

            case ItemType.item9:
                item9Highlight.enabled = true;
                descriptionText.text = item9Description;
                break;

            case ItemType.item10:
                item10Highlight.enabled = true;
                descriptionText.text = item10Description;
                break;

            case ItemType.item11:
                item11Highlight.enabled = true;
                descriptionText.text = item11Description;
                break;

            case ItemType.item12:
                item12Highlight.enabled = true;
                descriptionText.text = item12Description;
                break;

            default:
                print("Cannot Select: Invalid ItemType");
                break;
        }
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
        switch(itemType)
        {
            case ItemType.matches:
                // Matches always appear in inventory.
                matchesQuantity.text = newQuantity.ToString();
                break;

            case ItemType.salt:
                // Salt always appears in inventory.
                saltQuantity.text = newQuantity.ToString();
                break;

            case ItemType.dolls:
                // Once displayed dolls will always appear in inventory.
                if (newQuantity > 0 && !dollsPanel.activeSelf)
                {
                    dollsPanel.SetActive(true);
                    ++displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.dolls] = true;
                }
                dollsQuantity.text = newQuantity.ToString();
                break;

            // All other items will only appear if their quantity is above 0.
            case ItemType.bottles:
                if(newQuantity > 0)
                {
                    if(!item4Panel.activeSelf)
                    {
                        item4Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.bottles] = true;
                    }
                    
                    item4Quantity.text = newQuantity.ToString();
                }
                else if(item4Panel.activeSelf)
                {
                    item4Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.bottles] = false;
                }
                break;

            case ItemType.item5:
                if (newQuantity > 0)
                {
                    if (!item5Panel.activeSelf)
                    {
                        item5Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item5] = true;
                    }

                    item5Quantity.text = newQuantity.ToString();
                }
                else if (item5Panel.activeSelf)
                {
                    item5Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item5] = false;
                }
                break;

            case ItemType.item6:
                if (newQuantity > 0)
                {
                    if (!item6Panel.activeSelf)
                    {
                        item6Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item6] = true;
                    }

                    item6Quantity.text = newQuantity.ToString();
                }
                else if (item6Panel.activeSelf)
                {
                    item6Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item6] = false;
                }
                break;

            case ItemType.item7:
                if (newQuantity > 0)
                {
                    if (!item7Panel.activeSelf)
                    {
                        item7Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item7] = true;
                    }

                    item7Quantity.text = newQuantity.ToString();
                }
                else if (item7Panel.activeSelf)
                {
                    item7Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item7] = false;
                }
                break;

            case ItemType.item8:
                if (newQuantity > 0)
                {
                    if (!item8Panel.activeSelf)
                    {
                        item8Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item8] = true;
                    }

                    item8Quantity.text = newQuantity.ToString();
                }
                else if (item8Panel.activeSelf)
                {
                    item8Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item8] = false;
                }
                break;

            case ItemType.item9:
                if (newQuantity > 0)
                {
                    if (!item9Panel.activeSelf)
                    {
                        item9Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item9] = true;
                    }

                    item9Quantity.text = newQuantity.ToString();
                }
                else if (item9Panel.activeSelf)
                {
                    item9Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item9] = false;
                }
                break;

            case ItemType.item10:
                if (newQuantity > 0)
                {
                    if (!item10Panel.activeSelf)
                    {
                        item10Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item10] = true;
                    }

                    item10Quantity.text = newQuantity.ToString();
                }
                else if (item10Panel.activeSelf)
                {
                    item10Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item10] = false;
                }
                break;

            case ItemType.item11:
                if (newQuantity > 0)
                {
                    if (!item11Panel.activeSelf)
                    {
                        item11Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item11] = true;
                    }

                    item11Quantity.text = newQuantity.ToString();
                }
                else if (item11Panel.activeSelf)
                {
                    item11Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item11] = false;
                }
                break;

            case ItemType.item12:
                if (newQuantity > 0)
                {
                    if (!item12Panel.activeSelf)
                    {
                        item12Panel.SetActive(true);
                        ++displayedItemsCount;
                        itemsDisplayed[(ushort)ItemType.item12] = true;
                    }

                    item12Quantity.text = newQuantity.ToString();
                }
                else if (item12Panel.activeSelf)
                {
                    item12Panel.SetActive(false);
                    --displayedItemsCount;
                    itemsDisplayed[(ushort)ItemType.item12] = false;
                }
                break;

            default:
                print("Invalid ItemType");
                break;
        }
    }
}
