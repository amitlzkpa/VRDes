﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectManager_Point : SnapObjectManager
{


    private GameObject parentCloneObj;
    private RefObjects_Point refObj;



    //---------------------------------------------------------------




    private void createSnapObjects()
    {
        Vector3 pt = refObj.getPtCenter();

        base.createAndPlaceSnapObj(pt, pt, SnapType.END);
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
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Point>();
        updateSnapObjects();
    }



}