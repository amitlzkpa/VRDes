using UnityEngine;
using System.Collections;

public class app_Plane_try : MonoBehaviour {
    
    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject refObjs;
    

    void Start()
    {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        refObjs = transform.FindChild("_RefObjects").gameObject;
    }


}
