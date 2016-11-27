using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SnapObject_Point : MonoBehaviour, SnapObject
{


    public List<SnapType> snapTypes;


    public bool isSnap()
    {
        //return true;
        return GeneralSettings.hasOneActiveSnap(snapTypes);
    }


    public Vector3 getSnapPt(Vector3 inPos)
    {
        return transform.position;
    }


}
