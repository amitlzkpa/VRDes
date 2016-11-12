using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class RefObject : MonoBehaviour {



    private HashSet<GameObject> assocObjects;



    public void addToAssocList(GameObject obj)
    {
        assocObjects.Add(obj);
    }



    //---------------------------------------------------------------



    public Plane getRefPlane()
    {
        return transform.parent.gameObject.GetComponent<RefObjects_Plane>().getRefPlane();
    }



    //---------------------------------------------------------------



    public void moveObject(Vector3 startPos, Vector3 tgtPos)
    {
        if (gameObject.name.ToLower().Contains("left") || gameObject.name.ToLower().Contains("right"))
        {
            tgtPos.y = startPos.y;
        }
        else
        {
            tgtPos.x = startPos.x;
        }
        tgtPos.z = startPos.z;


        Dictionary<GameObject, Vector3> prevPos = new Dictionary<GameObject, Vector3>();
        foreach(GameObject assocObj in assocObjects) {
            prevPos.Add(assocObj, assocObj.transform.position - transform.position);
        }
        transform.localPosition = tgtPos;
        foreach (GameObject assocObj in assocObjects) {
            assocObj.transform.position = (transform.position) + prevPos[assocObj];
        }
        GameObject parentCloneObj = GeneralSettings.getParentClone(gameObject, "app_");
        parentCloneObj.transform.FindChild("_Model").gameObject.GetComponent<MeshMakerPlane>().updateMesh();
        parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Plane>().adjustEdgeHandles();
    }



    //---------------------------------------------------------------



    void Awake()
    {
        assocObjects = new HashSet<GameObject>();
    }








}
