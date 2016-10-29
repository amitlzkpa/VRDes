﻿using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Plane : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;


    public void delete()
    {
        GeneralSettings.detachObjectMenu(parentObj);
        GeneralSettings.deleteObject(parentObj);
    }

    public void edit()
    {
        parentObj.GetComponent<EditPlane>().toggleEditMode();
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
    }
}
