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



    public void moveObject(Vector3 inpLoc)
    {
        Dictionary<GameObject, Vector3> prevPos = new Dictionary<GameObject, Vector3>();
        foreach(GameObject o in assocObjects) {
            prevPos.Add(o, o.transform.position - transform.position);
        }
        transform.localPosition = inpLoc;
        foreach (GameObject o in assocObjects)
        {
            o.transform.position = (transform.position) + prevPos[o];
        }
        GeneralSettings.getParentClone(gameObject, "app_").transform.FindChild("_Model").gameObject.GetComponent<MeshMakerPlane>().updateMesh();
    }



    //---------------------------------------------------------------



    void Awake()
    {
        assocObjects = new HashSet<GameObject>();
    }








}
