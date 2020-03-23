// Jack 23/02/2020 Script created.
// Jack 23/03/2020 Added saving support.
//                 Removed door.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the books in the library puzzle.
/// 
/// Checks that the books have been pulled out in the correct order. If not
/// they are pulled back in. If they have the hidden library door is opened.
/// </summary>
public class BookController_Jack : MonoBehaviour
{
    public Book_Jack book1Script;
    public Book_Jack book2Script;
    public Book_Jack book3Script;

    private bool puzzleSolved = false;

    private const ushort numOfBooks = 3;
    private BookType[] pulledOutBooks = new BookType[numOfBooks];

    private void Awake()
    {
        for(ushort i = 0; i < numOfBooks; ++i)
        {
            pulledOutBooks[i] = BookType.None;
        }
    }

    /// <summary>
    /// To be called after a book if pulled out.
    /// </summary>
    /// Checks if all the books have been pulled out. If so it checks
    /// they have been pulled out in the correct order.
    /// 
    /// <param name="bookType"></param>
    public void BookPulledOut(BookType bookType)
    {
        if(!puzzleSolved)
        {
            ushort i;
            for (i = 0; i < numOfBooks; ++i)
            {
                if(pulledOutBooks[i] == BookType.None)
                {
                    pulledOutBooks[i] = bookType;
                    break;
                }
            }

            if(i == numOfBooks - 1)
            {
                // Check order
                if(pulledOutBooks[0] == BookType.Circle && pulledOutBooks[1] == BookType.Dash && pulledOutBooks[2] == BookType.Cross)
                {
                    puzzleSolved = true;
                }
                else
                {
                    for (ushort j = 0; j < numOfBooks; ++j)
                    {
                        pulledOutBooks[j] = BookType.None;
                    }

                    book1Script.PullInBook();
                    book2Script.PullInBook();
                    book3Script.PullInBook();
                }
            }
        }
    }

    /// <summary>
    /// Returns puzzleSolved.
    /// </summary>
    /// <returns></returns>
    public bool GetPuzzleSolved()
    {
        return puzzleSolved;
    }

    /// <summary>
    /// Sets puzzleSolved to the passed parameter.
    /// </summary>
    /// <param name="newPuzzleSolved"></param>
    public void SetPuzzleSolved(bool newPuzzleSolved)
    {
        puzzleSolved = newPuzzleSolved;
    }
}
