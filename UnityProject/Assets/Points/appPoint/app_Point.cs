using UnityEngine;
using System.Collections;

public class app_Point : MonoBehaviour {


    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject refObjs;


	// Use this for initialization
	void Start () {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        refObjs = transform.FindChild("_RefObjects").gameObject;
    }
}
