using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SnapObject_Line : MonoBehaviour, SnapObject
{


    public List<SnapType> snapTypes;


    public bool isSnap()
    {
        //return true;
        return GeneralSettings.hasOneActiveSnap(snapTypes);
    }


    public Vector3 getSnapPt(Vector3 inPos)
    {
        Vector3 locInpPos = transform.InverseTransformPoint(inPos);
        Vector3 locRetPos = new Vector3(0f, locInpPos.y, 0f);
        return transform.TransformPoint(locRetPos);
    }


}
