using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoMoveSpecPoint : MonoBehaviour {



    private GameObject parentObj;
    private Editable editScript;











    public void moveObject(Vector3 tgtPos)
    {
        editScript.moveObject(tgtPos);
    }




    // Use this for initialization
    void Start ()
    {
        parentObj = GeneralSettings.getParentClone(gameObject, "app_");
        editScript = parentObj.GetComponent<Editable>();
    }
}
