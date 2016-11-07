using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefObject : MonoBehaviour {

    public Plane getRefPlane()
    {
        return transform.parent.gameObject.GetComponent<RefObjects_Plane>().getRefPlane();
    }



    public void moveObject(Vector3 inpLoc)
    {
        Vector3 trPos = transform.InverseTransformVector(inpLoc);
        trPos.y = 0f;
        trPos.z = 0f;
        transform.localPosition = trPos;
    }



}
