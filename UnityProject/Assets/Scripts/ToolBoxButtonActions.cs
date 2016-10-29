using UnityEngine;
using System.Collections;

public class ToolBoxButtonActions : MonoBehaviour {

    public GameObject pointCreatorPrefab;
    public void createPoint()
    {
        GeneralSettings.setActiveActionObject(pointCreatorPrefab);
    }


    public GameObject planeCreatorPrefab;
    public void createPlane()
    {
        GeneralSettings.setActiveActionObject(planeCreatorPrefab);
    }


    public GameObject spaceCreatorPrefab;
    public void createSpace()
    {
        GeneralSettings.setActiveActionObject(spaceCreatorPrefab);
    }


    public GameObject planeCreatorPrefab_try;
    public void createPlane_try()
    {
        GeneralSettings.setActiveActionObject(planeCreatorPrefab_try);
    }


    //---------------------------------------------------------------


    public void toggleScale()
    {
        GeneralSettings.toggleTableMode();
    }
}
