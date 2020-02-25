//Dan - Created 25/02/2020
using System;
using System.Collections.Generic;
using UnityEngine;

public class Scriptmanager_Dan : MonoBehaviour
{
    //OrdinalIgnoreCase makes comparing the strings not case sensitive
    private Dictionary<string, string> lines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public string resourceFile = "script";


    //Audio clip must have the same name as the key in the script.txt
    //E.g. 'audio-trigger' audio clip and 'audio-trigger' key in script.txt
    public string GetText(string textKey)
    {
        //Displays text
        string tmp = "";
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;

        //DEBUG if text is missing + where it's missing from
        return "<color=#ff0000>TEXT MISSING FROM '" + textKey + "'</color>";
    }
    private void Awake()
    {
        //Loading in text asset, jsonUtility gives us 'voiceOvertext' object
        var textAsset = Resources.Load<TextAsset>(resourceFile);
        var voText = JsonUtility.FromJson<VoiceOverText_Dan>(textAsset.text);
        
        //For each above value, add them to dictionary
        foreach(var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

}
