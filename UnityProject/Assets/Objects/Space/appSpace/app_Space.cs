using UnityEngine;
using System.Collections;

public class app_Space : MonoBehaviour
{

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


}
