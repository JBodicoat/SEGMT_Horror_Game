// Louie : Handles Journal data and display.
// Jack : 12/02/2020 QA Review - renamed function, removed magic number, removed unecessary GameObject.Find
//Louie : 24/03 - Added automatic journal entries and pages.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager_Louie : MonoBehaviour
{
    // Journal UI on/off values
	private bool isJournalOn;
    private CanvasGroup journal;
    private float journalDisplayAlpha = 0.8f;

    //Inventory values
    private int matches;
    private int salt;
    private int dolls;
    private int bottles;

    //Time remaining values
    private const int finalHour = 3;
    private const int finalMinute = 33;
    private const int minutesPerHour = 60;
    private int hoursLeft;
    private int minutesleft;

    //Other References
    public Text inventoryAmounts;
    public Text timeRemaining;
    public Inventory_Jack inventoryScript;
    public GameObject Page1;
    public GameObject Page2;
    public Text LeftPage;
    public Text RightPage;

    private bool isOnPage1;
    private int NumberOfEntries;
    private const int MaxEntriesPerPage = 7;
    void Start()
    { 
		isJournalOn = false;
        journal = GetComponent<CanvasGroup>();
        hoursLeft = 3;
        minutesleft = 33;
        isOnPage1 = true;
        NumberOfEntries = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleJournal();
        }
        //----------------------------------------------------TESTING
        //Used to test the page turning
        if(Input.GetKeyDown(KeyCode.O))
        {
            ToggleDisplayPage1();
        }

        //Used to test adding entries
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddNote("Test Note. Maybe I should check out this piano?");
        }
        //----------------------------------------------------TESTING

    }

    /// <summary>
    /// This is called when the player presses tab and toggles the journal's visibility
    /// </summary>
	void ToggleJournal()
	{
		if (isJournalOn)
		{
            journal.alpha = 0;
            isJournalOn = false;
        }
        else 
		{
            journal.alpha = journalDisplayAlpha;
            isJournalOn = true;
		}
	}
    /// <summary>
    /// Updates the values for the inventory items.
    /// </summary>
    /// <param name="Matches"></param>
    /// <param name="Salt"></param>
    /// <param name="Bottles"></param>
    /// <param name="Dolls"></param>
    public void UpdateValues(int Matches, int Salt, int Bottles, int Dolls)
    {
        matches = Matches;
        salt = Salt;
        dolls = Dolls;
        bottles = Bottles;

        UpdateInvText();
    }
    /// <summary>
    /// Updates the text in the journal for inventory items.
    /// </summary>
    private void UpdateInvText()
    {
        inventoryAmounts.text = "x " + matches + "\nx " + salt + "\nx " + bottles + "\nx " + dolls;
    }
    /// <summary>
    /// Updates time remaining variables.
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    public void UpdateTimeLeft(int hour, int minute)
    {
        hoursLeft = finalHour - hour;
        minutesleft = finalMinute - minute;

        if (minutesleft < 0)
        {
            hoursLeft--;
            minutesleft = minutesPerHour - minutesleft;
        }

        UpdateTimeText();
    }
    /// <summary>
    /// Updates time remaining text in the journal.
    /// </summary>
    private void UpdateTimeText()
    {
        timeRemaining.text = "(" + hoursLeft + "h " + minutesleft + "m)";
    }
    /// <summary>
    /// Toggles current journal display page.
    /// </summary>
    public void ToggleDisplayPage1()
    {
        if (isOnPage1)
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
        }
        else
        {
            Page2.SetActive(false);
            Page1.SetActive(true);
        }
            isOnPage1 = !isOnPage1;
    }
    /// <summary>
    /// This is used to add a new note to the second page of the journal.
    /// </summary>
    /// <param name="newNote"></param>
    public void AddNote(string newNote)
    {
        NumberOfEntries++;
        if (NumberOfEntries < MaxEntriesPerPage)
        {
            if (NumberOfEntries == 1)
            {
                LeftPage.text = NumberOfEntries + ". " + newNote;
            }
            else
            {
                LeftPage.text = LeftPage.text + "\n" + NumberOfEntries + ". " + newNote;
            }
        }
        else if (NumberOfEntries == MaxEntriesPerPage)
        {
            RightPage.text = NumberOfEntries + ". " + newNote;
        }
        else
        {
            RightPage.text = RightPage.text + "\n" + NumberOfEntries + ". " + newNote;
        }
    }
}
