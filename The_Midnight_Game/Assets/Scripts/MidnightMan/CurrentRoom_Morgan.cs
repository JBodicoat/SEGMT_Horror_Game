using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom_Morgan : MonoBehaviour
{
    public MMTargeting_Morgan targetScript;
    //set name of room manually
    //doesnt matter, as long as no 2 rooms are named the same
    public string roomName;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            targetScript.playerRoom = roomName;
        }
        else if (other.tag == "midnightMan")
        {
            targetScript.midnightManRoom = roomName;
        }
    }
}
