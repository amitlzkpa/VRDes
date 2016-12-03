using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoMoveLinear : MonoBehaviour {


    public string axis;


    private GameObject parentObj;
    private Editable editScript;
    private float speed = 0.02f;


    private Vector3 getDirection(Vector3 inp)
    {
        Vector3 retVec = Vector3.zero;
        if (axis.Contains("x")) retVec.x = speed * Mathf.Sign(inp.x);
        if (axis.Contains("y")) retVec.y = speed * Mathf.Sign(inp.y);
        if (axis.Contains("z")) retVec.z = speed * Mathf.Sign(inp.z);
        return retVec;
    }


    public Plane getRefPlane()
    {
        return new Plane(transform.forward, transform.position);
    }



    // DIRTY-FIX
    public void moveObject(Vector3 tgtPos)
    {
        // Vector3 localPos = getDirection(tgtPos);
        // Vector3 worldPos = transform.TransformPoint(localPos);
        // editScript.moveObject(worldPos);
        transform.localPosition = getDirection(tgtPos);
        editScript.moveObject(transform.position);
    }






	// Use this for initialization
	void Start () {
        parentObj = GeneralSettings.getParentClone(gameObject, "app_");
        editScript = parentObj.GetComponent<Editable>();
    }
}
