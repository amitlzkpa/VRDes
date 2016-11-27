using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject_GenLine : MonoBehaviour, SnapObject {


    private CapsuleCollider capsuleCollider;
    private Vector3 startPt;
    private Vector3 endPt;
    public List<SnapType> snapTypes;


    //---------------------------------------------------------------


    // FIX-THIS: make it snap based on snaptype
    public bool isSnap()
    {
        if (!startPt.Equals(endPt)) return true;
        return GeneralSettings.hasOneActiveSnap(snapTypes);
    }


    public Vector3 getSnapPt(Vector3 inPos)
    {
        if (startPt.Equals(endPt)) return transform.position;
        Vector3 locInpPos = transform.InverseTransformPoint(inPos);
        Vector3 locRetPos = new Vector3(0f, locInpPos.y, 0f);
        return transform.TransformPoint(locRetPos);
    }


    //---------------------------------------------------------------


    public void setEnds(Vector3 startPt, Vector3 endPt)
    {
        this.startPt = startPt;
        this.endPt = endPt;
        float len = Vector3.Distance(startPt, endPt);
        capsuleCollider.height = len;
        Vector3 dir = endPt - startPt;
        Vector3 midPt = (startPt + endPt) / 2;
        transform.position = midPt;
        transform.up = dir;
        if (startPt.Equals(endPt)) capsuleCollider.radius *= 1.5f;
    }


    public void setType(SnapType st)
    {
        if (!snapTypes.Contains(st)) snapTypes.Add(st);
    }


    //---------------------------------------------------------------


    void Awake () {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
	}
}
