//Created by Dan - 24/03/2020
// Jack 31/03/2020 - Reviewed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect_Dan : MonoBehaviour
{
    public Camera playerCamera;

    private const string clockKeyTag = "ClockKey";


    void Start()
    {
        if (!playerCamera)
        {
            playerCamera = GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "ClockKey")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
