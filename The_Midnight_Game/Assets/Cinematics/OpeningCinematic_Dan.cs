//Created by Dan 08/04/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCinematic_Dan: MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Pre-Beta");
    }
}
