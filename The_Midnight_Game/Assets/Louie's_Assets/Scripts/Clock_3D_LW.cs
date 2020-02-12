using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_3D_LW : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform handPivot;

    public GameObject hourHand;
    public GameObject minuteHand;

    public int speed = 1;
    void Start()
    {
        handPivot = GameObject.Find("Pivot").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RotateMinute();
        }
    }
    void RotateMinute()
    {

    }
    void RotateHour()
    {
        hourHand.transform.RotateAround(handPivot.position, Vector3.up, speed);
        hourHand.transform.RotateAroundLocal(handPivot.position, speed);


    }
}
