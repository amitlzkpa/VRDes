using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class RefObjectPoint : MonoBehaviour {



    private HashSet<GameObject> assocObjects;



    public void addToAssocList(GameObject obj)
    {
        assocObjects.Add(obj);
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



    void Awake()
    {
        assocObjects = new HashSet<GameObject>();
    }








}
