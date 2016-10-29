using UnityEngine;
using System.Collections;
using System;





public class LaserImplm : MonoBehaviour
{

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
    private string tgtNameContains;
    private string endNameContains;

    private bool inAir = true;
    private bool isFirst = true;




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
        imp_setRestrictedObjectStartName(tgtObjectName, null);
    }

    public void imp_clearRestrictedObjectStartName()
    {
        imp_setRestrictedObjectStartName(null, null);
    }

    public void imp_setRestrictedObjectContainsName(string tgtObjectName, string endObjectName)
    {
        tgtNameContains = tgtObjectName;
        endNameContains = endObjectName;
    }

    public void imp_setRestrictedObjectContainsName(string tgtObjectName)
    {
        imp_setRestrictedObjectContainsName(tgtObjectName, null);
    }

    public void imp_clearRestrictedObjectContainsName()
    {
        imp_setRestrictedObjectContainsName(null, null);
    }



    //---------------------------------------------------------------



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
        if (Physics.Raycast(raycastRay, out hitRayObj, maxLength, layerMask))
        {
            if (restrictedObj != null) { parentHasObj(hitRayObj.transform.gameObject, restrictedObj, topLevelObject, out hitObject); }
            else hitObject = hitRayObj.transform.gameObject;
        }
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
        return hitRayObj.point;
    }

    public Vector3 imp_getHitNormal()
    {
        return hitRayObj.normal;
    }

    public float imp_getHitDistance()
    {
        if (!(imp_isHit())) return -1f;
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
        if (inAir && !isFirst) return imp_getEndPoint();
        if (imp_isHit()) return imp_getHitPoint();
        return imp_getEndPoint();
    }

    public Vector3 imp_getTerminalNormal()
    {
        if (inAir && !isFirst) return imp_getEndNormal();
        if (imp_isHit()) return imp_getHitNormal();
        return imp_getEndNormal();
    }

    public float imp_getTerminalDistance()
    {
        if (inAir && !isFirst) return imp_getEndDistance();
        if (imp_isHit()) return imp_getHitDistance();
        return imp_getEndDistance();
    }

    public bool imp_isHit()
    {
        return hitObject != null;
    }

    public bool imp_hasEnd()
    {
        return maxLength > infinity;
    }




}
