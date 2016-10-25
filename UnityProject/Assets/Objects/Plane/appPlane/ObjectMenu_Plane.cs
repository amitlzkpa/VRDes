﻿using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Plane : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;



    public void transformObject()
    {
        Debug.Log(parentObj.name + "will be transformed");
    }


    public void delete()
    {
        GeneralSettings.addLineToConsole(string.Format("{0} object deleted.", parentObj.name));
        GeneralSettings.detachObjectMenu(parentObj);
        GeneralSettings.deleteObject(parentObj);
    }

    public void edit()
    {
        Debug.Log(parentObj.name + "will be edited");
    }

    public void rename()
    {
        Debug.Log(parentObj.name + "will be renamed");
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
    }
}