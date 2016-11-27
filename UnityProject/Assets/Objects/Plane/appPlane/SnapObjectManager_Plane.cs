using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectManager_Plane : MonoBehaviour, SnapObjectManager
{


    private GameObject parentCloneObj;
    private RefObjects_Plane refObj;



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
        Vector3 lT = refObj.getPtLeftTop();
        Vector3 lB = refObj.getPtLeftBottom();
        Vector3 rB = refObj.getPtRightBottom();
        Vector3 rT = refObj.getPtRightTop();

        createAndPlaceSnapObj(lT, rT);
        createAndPlaceSnapObj(rT, rB);
        createAndPlaceSnapObj(rB, lB);
        createAndPlaceSnapObj(lB, lT);

        createAndPlaceSnapObj(lT, lT, SnapType.END);
        createAndPlaceSnapObj(rT, rT, SnapType.END);
        createAndPlaceSnapObj(rB, rB, SnapType.END);
        createAndPlaceSnapObj(lB, lB, SnapType.END);

        Vector3 mid;
        mid = (lT + rT) / 2;
        createAndPlaceSnapObj(mid, mid, SnapType.MID);
        mid = (rT + rB) / 2;
        createAndPlaceSnapObj(mid, mid, SnapType.MID);
        mid = (rB + lB) / 2;
        createAndPlaceSnapObj(mid, mid, SnapType.MID);
        mid = (lB + lT) / 2;
        createAndPlaceSnapObj(mid, mid, SnapType.MID);
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
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Plane>();
        updateSnapObjects();
    }
}
