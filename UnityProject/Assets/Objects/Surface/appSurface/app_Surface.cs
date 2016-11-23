using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class app_Surface : MonoBehaviour {
    

    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject refObjs;
    

    void Awake()
    {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        refObjs = transform.FindChild("_RefObjects").gameObject;
    }


    public void init(List<Vector3> points)
    {
        if (points.Count != 4)
        {
            Debug.LogError("Can't create surface with less than 4 points at " + gameObject.name);
            return;
        }


        refObjs.GetComponent<RefObjects_Surface>().addToPtSet(points, true);

        refObjs.GetComponent<RefObjects_Surface>().hideRefObjects();

    }


}
