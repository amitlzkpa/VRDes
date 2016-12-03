using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSpecMoveOneAxis : MonoBehaviour {


    public string axis;

    private GameObject parentObj;
    private Editable editScript;

    

    public string getAxis()
    {
        return axis.Substring(0, 1).ToUpper();
    }




    private Vector3 getTgtPos(float offset)
    {
        Vector3 retPos = Vector3.zero;
        switch (axis)
        {
            case "x+":
                {
                    retPos.x += offset;
                    break;
                }
            case "x-":
                {
                    retPos.x -= offset;
                    break;
                }
            case "y+":
                {
                    retPos.y += offset;
                    break;
                }
            case "y-":
                {
                    retPos.y -= offset;
                    break;
                }
            case "z+":
                {
                    retPos.z += offset;
                    break;
                }
            case "z-":
                {
                    retPos.z -= offset;
                    break;
                }
        }
        return retPos;
    }


    
    public void moveObject(float offset)
    {
        Vector3 tgtPos = getTgtPos(offset);
        parentObj.transform.Translate(tgtPos);
    }




    void Start()
    {
        parentObj = GeneralSettings.getParentClone(gameObject, "app_");
        editScript = parentObj.GetComponent<Editable>();
    }

}
