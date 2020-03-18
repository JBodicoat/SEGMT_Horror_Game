using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle_Dan : MonoBehaviour
{

    private Animator ClockPuzzle;

    void Start()
    {        
        StartCoroutine(ClockSequence());
    }
    
    IEnumerator ClockSequence()
    {
        yield return new WaitForSeconds(10);
        ClockPuzzle.Play("Clock Puzzle");
    }
}

