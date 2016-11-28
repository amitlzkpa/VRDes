using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectManager_Line : SnapObjectManager
{


    private GameObject parentCloneObj;
    private RefObjects_Line refObj;



    //---------------------------------------------------------------



    private void createSnapObjects()
    {
        List<Vector3> pts = refObj.getAllPts();
        foreach(Vector3 pt in pts)
        {
            base.createAndPlaceSnapObj(pt, pt, SnapType.END);
        }

        for (int i=0; i<pts.Count-1; i++)
        {
            base.createAndPlaceSnapObj(pts[i], pts[i+1]);
        }
    }



    //---------------------------------------------------------------



    public override void updateSnapObjects()
    {
        base.clearSnapObjects();
        createSnapObjects();
    }



    //---------------------------------------------------------------



    // Use this for initialization
    void Start ()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Line>();
        updateSnapObjects();
    }



}
