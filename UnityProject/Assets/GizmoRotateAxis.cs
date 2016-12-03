using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRotateAxis : MonoBehaviour {



    public string axis;



    private GameObject parentObj;









    public Plane getRefPlane()
    {
        return new Plane(transform.up, transform.position);
    }



    public void rotateObject(Vector3 endPos)
    {
        Vector3 endVector = endPos - transform.position;
        
        switch(axis)
        {
            case "x":
                {
                    parentObj.transform.forward = endVector;
                    break;
                }
            case "y":
                {
                    parentObj.transform.right = endVector;
                    break;
                }
            case "z":
                {
                    parentObj.transform.up = endVector;
                    break;
                }
        }
        
    }




    // Use this for initialization
    void Start () {
        parentObj = GeneralSettings.getParentClone(gameObject, "app_");
    }
}
