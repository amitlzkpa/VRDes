using UnityEngine;
using System.Collections;
using System;

public class ObjectMenu_Point : MonoBehaviour, ObjectMenu {



    private GameObject parentObj;



    public void transformObject()
    {
        Debug.Log(parentObj.name + "will be transformed");
    }


    public void delete()
    {
        // get the object menu back and
        Destroy(parentObj);
    }

    public void edit()
    {
        Debug.Log(parentObj.name + "will be edited");
    }

    public void rename()
    {
        Debug.Log(parentObj.name + "will be renamed");
    }






    // Use this for initialization
    void Start () {
        parentObj = transform.parent.gameObject;
    }
}
