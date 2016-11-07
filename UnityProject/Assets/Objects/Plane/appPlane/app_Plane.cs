﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class app_Plane : MonoBehaviour {
    
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
