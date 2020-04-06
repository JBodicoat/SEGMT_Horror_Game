/// Morgan Pryor 18/03/2020
/// 

//sets lighting to almost pitch black

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnStart_Morgan : MonoBehaviour
{
    private void Awake()
    {
        //sets lighting to 0.05% intensity
        RenderSettings.ambientIntensity = 0.05f;
    }
}
