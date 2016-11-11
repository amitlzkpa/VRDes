using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureMenuManager : MonoBehaviour, ContextMenuManager
{


    private LaserPicker l;

    private Text measurementTextUI;

    private string defaultText = "-- m  |  -- ft";
    private string measuredText;
    private string distM;
    private string distF;


    //---------------------------------------------------------------



    private string getDistInFeet(float distM)
    {
        float distInFeet = distM * 3.2808399f; //multiplier for metres to feet conversion
        int feet = Mathf.FloorToInt(distInFeet);
        int inches = Mathf.FloorToInt((distInFeet - feet) * 12);
        return feet.ToString() + "\' " + inches.ToString() + "\"";
    }



    private void liveMeasureUpdate()
    {
        if (l.isHit())
        {
            distM = l.getHitDistance().ToString("F1");
            distF = getDistInFeet(l.getHitDistance());
            measuredText = distM + " m  |  " + distF + " ft";
            measurementTextUI.text = measuredText;
        }
        else
        {
            measurementTextUI.text = defaultText;
        }
    }



    //---------------------------------------------------------------



    public void setupForInAir()
    {
        l.setLength(3f);
    }


    public void setupForSurface()
    {
        l.setLengthToInfinity();
    }



    //---------------------------------------------------------------



    public void cmStart(LaserPicker laser)
    {
        l = laser;
        measurementTextUI = transform.FindChild("_LiveMeasurement").gameObject.GetComponent<Text>();
    }

    public void cmUpdate(LaserPicker laser)
    {
        liveMeasureUpdate();
    }
}
