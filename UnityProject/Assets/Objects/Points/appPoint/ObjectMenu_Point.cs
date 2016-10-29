using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Point : MonoBehaviour, ObjectMenu {



    private GameObject parentObj;


    public void delete()
    {
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
