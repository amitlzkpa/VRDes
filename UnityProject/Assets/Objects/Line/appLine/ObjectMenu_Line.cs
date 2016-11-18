using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Line : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;


    public void delete()
    {
        GeneralSettings.deleteObjectMenu();
        GeneralSettings.deleteObject(parentObj);
    }

    public void edit()
    {
        parentObj.GetComponent<EditLine>().toggleEditMode();
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
    }
}
