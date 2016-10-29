using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Plane_try : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;


    public void delete()
    {
        GeneralSettings.addLineToConsole(string.Format("{0} object deleted.", parentObj.name));
        GeneralSettings.detachObjectMenu(parentObj);
        GeneralSettings.deleteObject(parentObj);
    }

    public void edit()
    {
        parentObj.GetComponent<EditPlane_try>().toggleEditMode();
    }

    public void rename()
    {
        Debug.Log(parentObj.name + "will be renamed");
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
    }


    void Start()
    {
    }
}
