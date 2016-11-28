using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_PlaneSpace : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;
    private GameObject parentSpaceObj;


    public void delete()
    {
        GeneralSettings.deleteObjectMenu();
        GeneralSettings.deleteObject(parentSpaceObj);
    }

    public void edit()
    {
        parentSpaceObj.GetComponent<EditSpace>().toggleEditMode();
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
        parentSpaceObj = GeneralSettings.getParentClone(parentObj, "app_Space");
    }
}
