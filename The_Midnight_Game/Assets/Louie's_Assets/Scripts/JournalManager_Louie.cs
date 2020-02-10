using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager_Louie : MonoBehaviour
{
    // Start is called before the first frame update
	private bool isJournalOn;
    private CanvasGroup journal;

    void Start()
    {
		//handle text 
		isJournalOn = false;
        journal = GameObject.Find("Journal").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleJournal();
        }
    }

	void toggleJournal()
	{

		if (isJournalOn)
		{
            journal.alpha = 0;
            isJournalOn = false;
        }
        else 
		{
            journal.alpha = 0.8f;
            isJournalOn = true;
		}
	}
}
