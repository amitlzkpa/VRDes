using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectManager_Plane : SnapObjectManager
{


    private GameObject parentCloneObj;
    private RefObjects_Plane refObj;



    //---------------------------------------------------------------




    private void createSnapObjects()
    {
        Vector3 lT = refObj.getPtLeftTop();
        Vector3 lB = refObj.getPtLeftBottom();
        Vector3 rB = refObj.getPtRightBottom();
        Vector3 rT = refObj.getPtRightTop();

        base.createAndPlaceSnapObj(lT, rT);
        base.createAndPlaceSnapObj(rT, rB);
        base.createAndPlaceSnapObj(rB, lB);
        base.createAndPlaceSnapObj(lB, lT);

        base.createAndPlaceSnapObj(lT, lT, SnapType.END);
        base.createAndPlaceSnapObj(rT, rT, SnapType.END);
        base.createAndPlaceSnapObj(rB, rB, SnapType.END);
        base.createAndPlaceSnapObj(lB, lB, SnapType.END);

        Vector3 mid;
        mid = (lT + rT) / 2;
        base.createAndPlaceSnapObj(mid, mid, SnapType.MID);
        mid = (rT + rB) / 2;
        base.createAndPlaceSnapObj(mid, mid, SnapType.MID);
        mid = (rB + lB) / 2;
        base.createAndPlaceSnapObj(mid, mid, SnapType.MID);
        mid = (lB + lT) / 2;
        base.createAndPlaceSnapObj(mid, mid, SnapType.MID);
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
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Plane>();
        updateSnapObjects();
    }
}
