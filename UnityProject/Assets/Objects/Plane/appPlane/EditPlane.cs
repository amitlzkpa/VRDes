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
        transform.FindChild("_MoveGizmo").gameObject.GetComponent<GizmoManager>().showMoveGizmo();
        editOn = true;
    }


    public void exitEditMode()
    {
        GeneralSettings.clearEditObject();
        transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjectManager>().hideRefObjects();
        transform.FindChild("_MoveGizmo").gameObject.GetComponent<GizmoManager>().hideMoveGizmo();
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
