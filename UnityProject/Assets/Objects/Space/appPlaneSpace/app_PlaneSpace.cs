using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class app_PlaneSpace : MonoBehaviour {
    

    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject refObjs;
    private GameObject hostObjs;




    void Awake()
    {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        refObjs = transform.FindChild("_RefObjects").gameObject;
        hostObjs = transform.FindChild("_HostedObjects").gameObject;
    }


    //---------------------------------------------------------------


    public void enhostObject(GameObject inpObj)
    {
        inpObj.transform.SetParent(hostObjs.transform);
    }


    //---------------------------------------------------------------


    public void init(List<Vector3> pts)
    {
        refObjs.GetComponent<RefObjects_PlaneSpace>().updateRefObjects(pts);
    }


}
