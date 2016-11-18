using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshMaker_Line : MonoBehaviour, MeshMaker
{


    private GameObject parentCloneObj;
    private RefObjects_Line refObj;
    private LineRenderer lineRenderer;



    //---------------------------------------------------------------



    public void updateMesh()
    {
        lineRenderer.numPositions = refObj.getAllPts().Count;
        lineRenderer.SetPositions(refObj.getAllPts().ToArray());
    }



    //---------------------------------------------------------------



    void Start()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Line>();
        lineRenderer = GetComponent<LineRenderer>();
        updateMesh();
    }
}
