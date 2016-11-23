﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBoxButtonAnnotations : MonoBehaviour {



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


    public GameObject sketchCreatorPrefab;
    public void createSketch()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(sketchCreatorPrefab);
        }
    }


    public GameObject boardSketchCreatorPrefab;
    public void createBoardSketch()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(boardSketchCreatorPrefab);
        }
    }


    public GameObject measurementsCreatorPrefab;
    public void createMeasurements()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(measurementsCreatorPrefab);
        }
    }


    //---------------------------------------------------------------


    public void toggleScale()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.toggleTableMode();
            GeneralSettings.addLineToConsole(System.String.Format("Viewing the model at 1:{0} scale.", 1 / GeneralSettings.model.transform.localScale.x));
        }
    }




}