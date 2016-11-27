using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectManager_Point : MonoBehaviour, SnapObjectManager
{


    private GameObject parentCloneObj;
    private RefObjects_Point refObj;



    //---------------------------------------------------------------

    
    
    private void createAndPlaceSnapObj(Vector3 start, Vector3 end)
    {
        GameObject newSnapObj;
        newSnapObj = GeneralSettings.getSnapGenLinePrefab();
        newSnapObj.transform.SetParent(transform);
        newSnapObj.GetComponent<SnapObject_GenLine>().setEnds(start, end);
    }



    private void createAndPlaceSnapObj(Vector3 start, Vector3 end, SnapType st)
    {
        GameObject newSnapObj;
        newSnapObj = GeneralSettings.getSnapGenLinePrefab();
        newSnapObj.transform.SetParent(transform);
        newSnapObj.GetComponent<SnapObject_GenLine>().setEnds(start, end);
        newSnapObj.GetComponent<SnapObject_GenLine>().setType(st);
    }




    private void createSnapObjects()
    {
        Vector3 pt = refObj.getPtCenter();

        createAndPlaceSnapObj(pt, pt, SnapType.END);
    }



    private void clearSnapObjects()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }



    //---------------------------------------------------------------



    public void updateSnapObjects()
    {
        clearSnapObjects();
        createSnapObjects();
    }



    //---------------------------------------------------------------



    // Use this for initialization
    void Start ()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Point>();
        updateSnapObjects();
    }
}
