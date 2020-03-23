// Jack 23/02/2020 Script created.
// Jack 19/03/2020 Renamed eums to match puzzle hints.
// Jack 23/03/2020 Added saving support.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookType
{
    None,
    Circle,
    Dash,
    Cross
}

/// <summary>
/// Handles the individual books in the library puzzle.
/// </summary>
public class Book_Jack : MonoBehaviour
{
    // Animaton
    public Animator bookAnimator;
    private const string pullTrigger = "Pull";

    private bool pulledOut = false;
    private bool pulling = false;

    public BookType bookType;

    public BookController_Jack bookController;

    // Start is called before the first frame update
    void Start()
    {
        if(!bookController)
        {
            bookController = FindObjectOfType<BookController_Jack>();
        }
    }

    /// <summary>
    /// Pulls the book forward using animation.
    /// </summary>
    /// <returns></returns>
    public bool PullOutBook()
    {
        if(!pulledOut && !pulling)
        {
            pulling = true;
            bookAnimator.SetTrigger(pullTrigger);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Pulls the book back using animation.
    /// </summary>
    /// <returns></returns>
    public bool PullInBook()
    {
        if(pulledOut && !pulling)
        {
            pulling = true;
            bookAnimator.SetTrigger(pullTrigger);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns true if the book has been pulled out.
    /// </summary>
    /// <returns></returns>
    public bool IsPulledOut()
    {
        return pulledOut;
    }

    /// <summary>
    /// To be called at the end of the pull out animation.
    /// </summary>
    public void EndPullOut()
    {
        pulledOut = true;
        pulling = false;
        bookController.BookPulledOut(bookType);
    }

    /// <summary>
    /// To be called at the end of the pull in animation.
    /// </summary>
    public void EndPullIn()
    {
        pulledOut = false;
        pulling = false;
    }

    /// <summary>
    /// Sets pulledOut to the passed parameter.
    /// </summary>
    /// <param name="newPulledOut"></param>
    public void SetPulledOut(bool newPulledOut)
    {
        pulledOut = newPulledOut;
    }
}
