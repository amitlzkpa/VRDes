using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class app_Plane : MonoBehaviour {
    

    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject refObjs;
    private GameObject hostObjs;
    

    void Start()
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


    private List<AppListener> listeners = new List<AppListener>();


    public void registerListener(AppListener l)
    {
        listeners.Add(l);
    }


    public void deRegisterListener(AppListener l)
    {
        listeners.Remove(l);
    }


    public void notifyPosChange()
    {
        foreach(AppListener l in listeners)
        {
            l.onPositionChange(transform.position);
        }
    }


    public void notifyRotChange()
    {
        foreach (AppListener l in listeners)
        {
            l.onRotationChange(transform.rotation);
        }
    }


    public void notifyScaleChange()
    {
        foreach (AppListener l in listeners)
        {
            l.onScaleChange(transform.localScale);
        }
    }


    public void notifyTransformChange()
    {
        notifyPosChange();
        notifyRotChange();
        notifyScaleChange();
    }




}
