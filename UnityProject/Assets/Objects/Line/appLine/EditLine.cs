using UnityEngine;
using System.Collections;
using System;

public class EditLine : MonoBehaviour, Editable
{


    private MeshMaker meshMaker;


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
        meshMaker.updateMesh();
    }

    public void toggleEditMode()
    {
        if (editOn) exitEditMode();
        else enterEditMode();
    }



    //---------------------------------------------------------------
    

    void Start()
    {
        meshMaker = transform.FindChild("_Model").gameObject.GetComponent<MeshMaker>();
    }


}
