using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTest : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject SFXManager;
    void Start()
    {
        SFXManager = GameObject.Find("SFX_Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SFXManager.GetComponent<SFXManager_LW>().PlaySFX(SFXManager_LW.SFX.MatchLighting);
        }
    }
}
