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
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().showMoveGizmo();
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().showRotateGizmo();
        editOn = true;
    }


    public void exitEditMode()
    {
        GeneralSettings.clearEditObject();
        transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjectManager>().hideRefObjects();
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().hideMoveGizmo();
        transform.FindChild("_Gizmo").gameObject.GetComponent<GizmoManager>().hideRotateGizmo();
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
