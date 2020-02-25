//Dan - Created 25/02/2020
using UnityEngine;
using UnityEngine.UI;

public class SubtitleGUIManager_Dan : MonoBehaviour
{
    public Text textBox;

    public void Clear()
    {
        textBox.text = string.Empty;
    }

    public void SetText(string text)
    {
        textBox.text = text;
    }

}
