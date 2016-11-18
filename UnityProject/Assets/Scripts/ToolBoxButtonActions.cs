﻿using UnityEngine;
using System.Collections;

public class ToolBoxButtonActions : MonoBehaviour {


    private bool editOnCheck()
    {
        if (GeneralSettings.editOn())
        {
            GeneralSettings.flashInteractWindow();
            GeneralSettings.updateInteractText("Please close edit mode first.");
            return true;
        }
        GeneralSettings.updateInteractText("");
        return false;
    }


    //---------------------------------------------------------------


    public void setToSelectionRay()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(null);
        }
    }


    public GameObject pointCreatorPrefab;
    public void createPoint()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(pointCreatorPrefab);
        }
    }


    public GameObject planeCreatorPrefab;
    public void createPlane()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(planeCreatorPrefab);
        }
    }


    public GameObject spaceCreatorPrefab;
    public void createSpace()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(spaceCreatorPrefab);
        }
    }


    public GameObject rectArrayCreatorPrefab;
    public void createRectArray()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(rectArrayCreatorPrefab);
        }
    }


    public GameObject polarArrayCreatorPrefab;
    public void createPolarArray()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(polarArrayCreatorPrefab);
        }
    }


    public GameObject lineCreatorPrefab;
    public void createLine()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(lineCreatorPrefab);
        }
    }
}
