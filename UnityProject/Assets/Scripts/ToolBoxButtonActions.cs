using UnityEngine;
using System.Collections;

public class ToolBoxButtonActions : MonoBehaviour {


    private void deleteAnyExistingMenu()
    {
        
        if (GeneralSettings.hasObjectMenu())
        {
            GameObject delObj = new GameObject("delObj");
            GeneralSettings.detachObjectMenu(delObj);
            Destroy(delObj);
        }
        
    }


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
            deleteAnyExistingMenu();
            GeneralSettings.setActiveActionObject(null);
        }
    }


    public GameObject pointCreatorPrefab;
    public void createPoint()
    {
        if (!editOnCheck())
        {
            deleteAnyExistingMenu();
            GeneralSettings.setActiveActionObject(pointCreatorPrefab);
        }
    }


    public GameObject planeCreatorPrefab;
    public void createPlane()
    {
        if (!editOnCheck())
        {
            deleteAnyExistingMenu();
            GeneralSettings.setActiveActionObject(planeCreatorPrefab);
        }
    }


    public GameObject spaceCreatorPrefab;
    public void createSpace()
    {
        if (!editOnCheck())
        {
            deleteAnyExistingMenu();
            GeneralSettings.setActiveActionObject(spaceCreatorPrefab);
        }
    }


    //---------------------------------------------------------------


    public void toggleScale()
    {
        if (!editOnCheck())
        {
            deleteAnyExistingMenu();
            GeneralSettings.toggleTableMode();
        }
    }
}
