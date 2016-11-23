using UnityEngine;
using System.Collections;
using System;





public class LaserImplm : MonoBehaviour
{

    private string NONE_STRING = "__none__";


    private Ray raycastRay;
    private RaycastHit hitRayObj;
    private float infinity = 100000f;
    private GameObject hitObject;


    private bool int_isOn = true;
    private float maxLength = 1000000f;
    private GameObject restrictedObj;
    private GameObject topLevelObject;
    private LayerMask layerMask = ~0;

    private string tgtNameStart;
    private string endNameStart;
    //private string tgtNameContains;
    //private string endNameContains;

    private Plane restrictedPlane;
    private bool planeRestrictMode;
    private bool planeRayHit;
    private float planeHitLen;
    private Vector3 planeHitPt;

    private bool inAir = true;
    private bool isFirst = true;

    private bool stickMode;






    public void imp_setStickMode()
    {
        stickMode = true;
    }

    public void imp_clearStickMode()
    {
        stickMode = false;
    }
    
    public void imp_setLength(float len)
    {
        maxLength = len;
    }

    public void imp_enable()
    {
        int_isOn = true;
    }

    public void imp_disable()
    {
        int_isOn = false;
    }

    public void imp_toggle()
    {
        int_isOn = !int_isOn;
    }

    public void imp_setRestrictedObject(GameObject tgtObject)
    {
        imp_setRestrictedObject(tgtObject, null);
    }

    public void imp_setRestrictedObject(GameObject tgtObject, GameObject endObject)
    {
        restrictedObj = tgtObject;
        topLevelObject = endObject;
    }

    public void imp_clearRestrictedObject()
    {
        imp_setRestrictedObject(null, null);
    }

    public void imp_setRestrictedPlane(Plane tgtPlane)
    {
        restrictedPlane = tgtPlane;
        planeRestrictMode = true;
    }

    public void imp_clearRestrictedPlane()
    {
        restrictedPlane = new Plane();
        planeRestrictMode = false;
    }

    public void imp_setLayerMask(LayerMask l)
    {
        layerMask = l;
    }

    public void imp_clearLayerMask()
    {
        imp_setLayerMask(~0);
    }

    public void imp_setRestrictedObjectStartName(string tgtObjectName, string endObjectName)
    {
        tgtNameStart = tgtObjectName;
        endNameStart = endObjectName;
    }

    public void imp_setRestrictedObjectStartName(string tgtObjectName)
    {
        imp_setRestrictedObjectStartName(tgtObjectName, NONE_STRING);
    }

    public void imp_clearRestrictedObjectStartName()
    {
        imp_setRestrictedObjectStartName(NONE_STRING, NONE_STRING);
    }

    public void imp_setRestrictedObjectContainsName(string tgtObjectName, string endObjectName)
    {
        //tgtNameContains = tgtObjectName;
        //endNameContains = endObjectName;
    }

    public void imp_setRestrictedObjectContainsName(string tgtObjectName)
    {
        imp_setRestrictedObjectContainsName(tgtObjectName, NONE_STRING);
    }

    public void imp_clearRestrictedObjectContainsName()
    {
        imp_setRestrictedObjectContainsName(NONE_STRING, NONE_STRING);
    }



    //---------------------------------------------------------------



    private bool parentHasStartName(GameObject hitObj, String tgtObjectName, String endObjectName, out GameObject foundObject)
    {
        if (hitObj.name.StartsWith(tgtObjectName))
        {
            foundObject = hitObj;
            return true;
        }
        if (hitObj == null || hitObj.transform == null)
        {
            foundObject = null;
            return false;
        }
        if (hitObj.transform.parent == null || hitObj.transform.parent.gameObject.name.Equals(NONE_STRING))
        {
            foundObject = null;
            return false;
        }
        return (parentHasStartName(hitObj.transform.parent.gameObject, tgtObjectName, endObjectName, out foundObject));
    }



    private bool parentHasObj(GameObject hitObj, GameObject tgtObject, GameObject endObject, out GameObject foundObject)
    {
        if (hitObj == tgtObject)
        {
            foundObject = hitObj;
            return true;
        }
        if (hitObj == null || hitObj.transform == null)
        {
            foundObject = null;
            return false;
        }
        if (hitObj.transform.parent == null || hitObj.transform.parent.gameObject == endObject)
        {
            foundObject = null;
            return false;
        }
        return (parentHasObj(hitObj.transform.parent.gameObject, tgtObject, endObject, out foundObject));
    }



    // Update is called once per frame
    void Update ()
    {
        hitObject = null;
        raycastRay = new Ray(transform.position, transform.forward);
        if (stickMode)
        {
            stickModeCheck(raycastRay);
            return;
        }
        if (planeRestrictMode)
        {
            planeHitCheck(raycastRay);
            return;
        }
        if (Physics.Raycast(raycastRay, out hitRayObj, maxLength, layerMask))
        {
            if (restrictedObj != null) { parentHasObj(hitRayObj.transform.gameObject, restrictedObj, topLevelObject, out hitObject); }
            else if (tgtNameStart != null && !tgtNameStart.Equals(NONE_STRING)) { parentHasStartName(hitRayObj.transform.gameObject, tgtNameStart, endNameStart, out hitObject); }
            else hitObject = hitRayObj.transform.gameObject;
        }
    }


    private void stickModeCheck(Ray raycastRay)
    {
        // no checks done in stick mode
    }


    private void planeHitCheck(Ray raycastRay)
    {
        planeRayHit = restrictedPlane.Raycast(raycastRay, out planeHitLen);
        planeHitPt = raycastRay.GetPoint(planeHitLen);
    }



    //---------------------------------------------------------------



    public bool imp_isOn()
    {
        return int_isOn;
    }

    public GameObject imp_getHitObject()
    {
        return hitObject;
    }

    public Vector3 imp_getHitPoint()
    {
        if (stickMode) return Vector3.zero;
        if (planeRestrictMode) return planeHitPt;
        return hitRayObj.point;
    }

    public Vector3 imp_getHitNormal()
    {
        if (stickMode) return Vector3.zero;
        if (planeRestrictMode) return restrictedPlane.normal;
        return hitRayObj.normal;
    }

    public float imp_getHitDistance()
    {
        if (stickMode || !(imp_isHit())) return -1f;
        if (planeRestrictMode) return planeHitLen;
        return hitRayObj.distance;
    }

    public Vector3 imp_getEndPoint()
    {
        if (maxLength > infinity && !inAir) return Vector3.zero;
        return raycastRay.GetPoint(maxLength);
    }

    public Vector3 imp_getEndNormal()
    {
        return -raycastRay.direction;
    }

    public float imp_getEndDistance()
    {
        if (maxLength > infinity && !inAir) return -1f;
        return maxLength;
    }

    public Vector3 imp_getStartPoint()
    {
        return raycastRay.origin;
    }

    public Vector3 imp_getDirection()
    {
        return raycastRay.direction;
    }

    public Vector3 imp_getTerminalPoint()
    {
        if (stickMode) return imp_getEndPoint();
        if (inAir && !isFirst) return imp_getEndPoint();
        if (imp_isHit()) return imp_getHitPoint();
        return imp_getEndPoint();
    }

    public Vector3 imp_getTerminalNormal()
    {
        if (stickMode) return imp_getEndNormal();
        if (inAir && !isFirst) return imp_getEndNormal();
        if (imp_isHit()) return imp_getHitNormal();
        return imp_getEndNormal();
    }

    public float imp_getTerminalDistance()
    {
        if (stickMode) return imp_getEndDistance();
        if (inAir && !isFirst) return imp_getEndDistance();
        if (imp_isHit()) return imp_getHitDistance();
        return imp_getEndDistance();
    }

    public bool imp_isHit()
    {
        if (stickMode) return false;
        if (planeRestrictMode) return planeRayHit;
        return hitObject != null;
    }

    public bool imp_hasEnd()
    {
        if (stickMode) return true;
        if (planeRestrictMode) return planeRayHit;
        return maxLength > infinity;
    }




}
