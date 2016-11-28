using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnapObjectManager : MonoBehaviour
{



    private GameObject newSnapObj;


    protected void createAndPlaceSnapObj(Vector3 start, Vector3 end)
    {
        newSnapObj = GeneralSettings.getSnapGenLinePrefab();
        newSnapObj.transform.SetParent(transform);
        newSnapObj.GetComponent<SnapObject_GenLine>().setEnds(start, end);
    }



    protected void createAndPlaceSnapObj(Vector3 start, Vector3 end, SnapType st)
    {
        createAndPlaceSnapObj(start, end);
        newSnapObj.GetComponent<SnapObject_GenLine>().setType(st);
    }



    protected void clearSnapObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }



    public virtual void updateSnapObjects() { }


}
