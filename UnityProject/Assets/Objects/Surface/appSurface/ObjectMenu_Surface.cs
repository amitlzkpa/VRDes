using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Surface : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;


    public void delete()
    {
        GeneralSettings.deleteObjectMenu();
        GeneralSettings.deleteObject(parentObj);
    }

    public void edit()
    {
        parentObj.GetComponent<EditSurface>().toggleEditMode();
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
    }
}
