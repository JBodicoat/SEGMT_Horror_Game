// Dan
// Jack 25/02/2020 Reviewed - cached WaitForSeconds objects.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISubtitles_Dan : MonoBehaviour
{
    public GameObject textBox;
    private readonly WaitForSeconds waitUntilLineStart = new WaitForSeconds(1);
    private readonly WaitForSeconds waitUntilLineFinish = new WaitForSeconds(3);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextSequence());
    }

   IEnumerator TextSequence()
    {
        yield return waitUntilLineStart;
        textBox.GetComponent<Text>().text = "Where am I?";
        yield return waitUntilLineFinish;
        textBox.GetComponent<Text>().text = "";
        yield return waitUntilLineStart;
        textBox.GetComponent<Text>().text = "It's so cold in here..";
        yield return waitUntilLineStart;
        textBox.GetComponent<Text>().text = "";
    }

}
