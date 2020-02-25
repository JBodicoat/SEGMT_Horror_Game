//Dan - Created 25/02/2020
// Jack 25/02/2020 Reviewed - set resourceFile to const and changed specified var types
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages narration and character voice lines text.   
/// </summary>
public class Scriptmanager_Dan : MonoBehaviour
{
    //OrdinalIgnoreCase makes comparing the strings not case sensitive
    private Dictionary<string, string> lines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public const string resourceFile = "script";


    /// Audio clip must have the same name as the key in the script.txt
    /// E.g. 'audio-trigger' audio clip and 'audio-trigger' key in script.txt
    public string GetText(string textKey)
    {
        //Displays text
        if (lines.TryGetValue(textKey, out string tmp))
            return tmp;

        //DEBUG if text is missing + where it's missing from
        return "<color=#ff0000>TEXT MISSING FROM '" + textKey + "'</color>";
    }
    private void Awake()
    {
        //Loading in text asset, jsonUtility gives us 'voiceOvertext' object
        TextAsset textAsset = Resources.Load<TextAsset>(resourceFile);
        VoiceOverText_Dan voText = JsonUtility.FromJson<VoiceOverText_Dan>(textAsset.text);
        
        //For each above value, add them to dictionary
        foreach(VoiceOverLine_Dan t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

}
