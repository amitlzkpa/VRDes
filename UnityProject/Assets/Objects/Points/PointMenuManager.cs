using UnityEngine;
using System.Collections;
using System;

public class PointMenuManager : MonoBehaviour, ContextMenuManager {


    private LaserPicker l;


    public void setupForInAir()
    {
        l.setLength(3f);
    }


    public void setupForSurface()
    {
        l.setLength(1000000f);
    }





    public void cmStart(LaserPicker laser)
    {
        l = laser;
    }

    public void cmUpdate(LaserPicker laser)
    {
    }
}
