// Louie
// Jack : 12/02/2020 QA Review - renamed function, removed magic number, removed unecessary GameObject.Find
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager_Louie : MonoBehaviour
{
    // Start is called before the first frame update
	private bool isJournalOn;
    private CanvasGroup journal;
    private float journalDisplayAlpha = 0.8f;

    void Start()
    {
		//handle text 
		isJournalOn = false;
        journal = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleJournal();
        }
    }

    /// This should be commented
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

    void DisplayPage1()
    {

    }
    void DisplayPage2()
    {

    }
    void DisplayPage3()
    {

    }
    void DisplayPage4()
    {

    }
}
