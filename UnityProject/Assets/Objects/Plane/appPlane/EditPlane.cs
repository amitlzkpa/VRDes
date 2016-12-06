using UnityEngine;
using System.Collections;
using System;

public class EditPlane : MonoBehaviour, Editable
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


    public void toggleEditMode()
    {
        if (editOn) exitEditMode();
        else enterEditMode();
    }



    //---------------------------------------------------------------



    private bool moveOn = false;


    public void enterMoveMode()
    {
        GeneralSettings.setEditObject(gameObject);
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().showMoveGizmo();
        // transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().showRotateGizmo();
        moveOn = true;
    }


    public void exitMoveMode()
    {
        GeneralSettings.clearEditObject();
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().hideMoveGizmo();
        // transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().hideRotateGizmo();
        moveOn = false;
    }


    public void toggleMoveMode()
    {
        if (moveOn) exitMoveMode();
        else enterMoveMode();
    }



    //---------------------------------------------------------------



    private bool rotateOn = false;


    public void enterRotateMode()
    {
        GeneralSettings.setEditObject(gameObject);
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().showRotateGizmo();
        rotateOn = true;
    }


    public void exitRotateMode()
    {
        GeneralSettings.clearEditObject();
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().hideRotateGizmo();
        rotateOn = false;
    }


    public void toggleRotateMode()
    {
        if (rotateOn) exitRotateMode();
        else enterRotateMode();
    }



    //---------------------------------------------------------------



    public void moveObject(Vector3 tgtPos)
    {
        transform.position = tgtPos;
    }



}
