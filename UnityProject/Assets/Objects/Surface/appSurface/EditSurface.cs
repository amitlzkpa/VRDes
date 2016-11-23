using UnityEngine;
using System.Collections;
using System;

public class EditSurface : MonoBehaviour, Editable
{



    //---------------------------------------------------------------



    private bool editOn = false;


    public void enterEditMode()
    {
        GeneralSettings.setEditObject(gameObject);
        transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjectManager>().showRefObjects();
        editOn = true;
    }


    public void exitEditMode()
    {
        GeneralSettings.clearEditObject();
        transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjectManager>().hideRefObjects();
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



    // TO-DO: complete this part for void creation 


    private bool voidEditOn = false;


    private void enterVoidEditMode()
    {
        GeneralSettings.setEditObject(gameObject);
        voidEditOn = true;
    }


    public void exitVoidEditMode()
    {
        GeneralSettings.clearEditObject();
        voidEditOn = false;
    }


    public void toggleVoidEditMode()
    {
        if (voidEditOn) exitVoidEditMode();
        else enterVoidEditMode();
    }



}
