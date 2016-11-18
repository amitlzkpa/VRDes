using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class app_Line : MonoBehaviour {
    

    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject refObjs;
    

    void Awake()
    {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        refObjs = transform.FindChild("_RefObjects").gameObject;
    }


    public void setLinePts(List<Vector3> inpPts)
    {
        refObjs.GetComponent<RefObjects_Line>().setAllPts(inpPts);
    }


}
