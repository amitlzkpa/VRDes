using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBoxButtonTransforms : MonoBehaviour
{


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
}
