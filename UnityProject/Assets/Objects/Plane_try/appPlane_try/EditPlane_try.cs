using UnityEngine;
using System.Collections;

public class EditPlane_try : MonoBehaviour, Editable
{


    private GameObject refObjs;



    //---------------------------------------------------------------



    private bool editOn = false;


    public void enterEditMode()
    {
        GeneralSettings.addLineToConsole(string.Format("Editing {0}.", gameObject.name));
        refObjs.GetComponent<RefObjects_Plane_try>().showRefObjects();
        GeneralSettings.setEditObject(gameObject);
        editOn = true;
    }


    public void exitEditMode()
    {
        GeneralSettings.addLineToConsole(string.Format("Exiting edit for {0}.", gameObject.name));
        refObjs.GetComponent<RefObjects_Plane_try>().hideRefObjects();
        GeneralSettings.clearEditObject();
        editOn = false;
    }


    public void toggleEditMode()
    {
        if (editOn) exitEditMode();
        else enterEditMode();
    }



    //---------------------------------------------------------------



    // Use this for initialization
    void Start()
    {
        refObjs = transform.FindChild("_RefObjects").gameObject;
    }


}
