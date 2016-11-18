﻿using UnityEngine;
using System.Collections;

public class EditSpace : MonoBehaviour, Editable
{



    //---------------------------------------------------------------



    private bool editOn = false;


    public void enterEditMode()
    {
        GeneralSettings.setEditObject(gameObject);
        editOn = true;
    }


    public void exitEditMode()
    {
        GeneralSettings.clearEditObject();
        editOn = false;
    }


    public void moveObject(Vector3 tgtPos)
    {
        transform.position = tgtPos;
    }


    public void toggleEditMode()
    {
        if (editOn) exitEditMode();
        else enterEditMode();
    }



    //---------------------------------------------------------------



}
