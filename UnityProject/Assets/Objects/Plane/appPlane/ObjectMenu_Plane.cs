using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Plane : MonoBehaviour, ObjectMenu
{



    private GameObject parentObj;


    public void delete()
    {
        GeneralSettings.deleteObjectMenu();
        GeneralSettings.deleteObject(parentObj);
    }

    public void edit()
    {
        parentObj.GetComponent<EditPlane>().toggleEditMode();
    }


    public void move()
    {
        parentObj.GetComponent<EditPlane>().toggleMoveMode();
    }


    public void rotate()
    {
        parentObj.GetComponent<EditPlane>().toggleRotateMode();
    }





    void Awake()
    {
        parentObj = transform.parent.gameObject;
    }
}
