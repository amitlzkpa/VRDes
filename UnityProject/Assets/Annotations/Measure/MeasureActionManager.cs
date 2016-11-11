using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureActionManager : MonoBehaviour, ActionManager
{

    public GameObject tickMarker;
    public Material dimensionLineMaterial;

    public GameObject measureMenuObject;
    private MeasureMenuManager measureMenuManager;

    private LaserPicker laser;
    private GameObject dimensionSet;



    private GameObject currentDim;
    private string dimObjectName = "DimensionObject";
    private int dimCount = -1;


    private GameObject currentTickObj;
    private string tickObjectName = "TickObject";
    private int tickCount = 0;


    //---------------------------------------------------------------



    private void createNewTick(Vector3 targetPoint)
    {
        currentTickObj = Instantiate(tickMarker, targetPoint, Quaternion.identity);
    }



    private void createNewDimSet()
    {
        currentDim = new GameObject();
        dimCount++;
        currentDim.transform.SetParent(dimensionSet.transform);
        currentDim.name = dimObjectName + "_" + dimCount;
        currentDim.AddComponent<DimensionLineRenderer>();
        currentDim.GetComponent<DimensionLineRenderer>().dimensionLineMaterial = dimensionLineMaterial;
    }



    private void manageDimLineAdd()
    {
        Vector3 targetPoint = laser.getTerminalPoint();
        createNewTick(targetPoint);
        if (tickCount == 0)
        {
            // start new dimension line
            createNewDimSet();
            currentTickObj.transform.SetParent(currentDim.transform);
            currentTickObj.name = tickObjectName + "_" + dimCount + "_" + tickCount;
            GeneralSettings.addLineToConsole("New dimension line started.");
        }
        else
        {
            // close existing dimension line
            currentTickObj.transform.SetParent(currentDim.transform);
            currentTickObj.name = tickObjectName + "_" + dimCount + "_" + tickCount;
            currentDim.GetComponent<DimensionLineRenderer>().updateDimensionLine();
            GeneralSettings.addLineToConsole((System.String.Format("Dimension line completed: {0}", currentDim.name)));
        }
        tickCount++;
        tickCount %= 2;
    }



    //---------------------------------------------------------------



    public void amStart(LaserPicker laser)
    {
        this.laser = laser;
        measureMenuManager = measureMenuObject.GetComponent<MeasureMenuManager>();
        dimensionSet = GeneralSettings.model.transform.FindChild("_Sets").FindChild("_DimensionSetManager").gameObject;
    }



    public void amUpdate(LaserPicker laser)
    {


        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            manageDimLineAdd();
        }


    }
}
