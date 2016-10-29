using UnityEngine;
using System.Collections;

public class EditPlane : MonoBehaviour, Editable
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


    public void toggleEditMode()
    {
        if (editOn) exitEditMode();
        else enterEditMode();
    }



    //---------------------------------------------------------------
    


}
