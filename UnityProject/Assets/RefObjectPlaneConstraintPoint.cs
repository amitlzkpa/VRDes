using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class RefObjectPlaneConstraintPoint : MonoBehaviour {




    public GameObject getConstrainObject()
    {
        return GeneralSettings.getParentClone(gameObject, "app_Plane");
    }



    //---------------------------------------------------------------



    public void moveObject(Vector3 tgtPos)
    {
        transform.position = tgtPos;
        GameObject parentCloneObj = GeneralSettings.getParentClone(gameObject, "app_");
        parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjectManager>().adjustEdgeHandles();
        parentCloneObj.transform.FindChild("_Model").gameObject.GetComponent<MeshMaker>().updateMesh();
        parentCloneObj.transform.FindChild("_SnapObjects").gameObject.GetComponent<SnapObjectManager>().updateSnapObjects();
    }



    //---------------------------------------------------------------








}
