using UnityEngine;
using System.Collections;


// this script is only a refernece script
// the actual raycast and detection is done by a child object
// the hit values are propagated to this script to expose the
// results without exposing the detection script and code and
// also lends extra flexibility of having more children nested
// under laser object



public class LaserPicker : MonoBehaviour {


    private LaserImplm l;

    void Start()
    {
        l = transform.FindChild("_LaserRay").gameObject.GetComponent<LaserImplm>();
    }








    public void setSnapPoint(Vector3 inp)
    {
        l.imp_setSnapPoint(inp);
    }


    public void clearSnappedPoint()
    {
        l.imp_clearSnappedPoint();
    }


    public void clearFacingDir()
    {
        l.imp_clearFacingDir();
    }


    public void reverseFacingDir()
    {
        l.imp_reverseFacingDir();
    }


    public void setFacingX()
    {
        l.imp_setFacingX();
    }


    public void setFacingY()
    {
        l.imp_setFacingY();
    }


    public void setFacingZ()
    {
        l.imp_setFacingZ();
    }


    public void setLengthToInfinity()
    {
        l.imp_setLength(100000f);
    }


    public void setToStickMode(float len)
    {
        l.imp_setStickMode();
        setLength(len);
    }

    public void clearStickMode()
    {
        l.imp_clearStickMode();
        setLengthToInfinity();
    }

    public void setLength(float len)
    {
        l.imp_setLength(len);
    }

    public void enable()
    {
        l.imp_enable();
    }

    public void disable()
    {
        l.imp_disable();
    }

    public void toggle()
    {
        l.imp_toggle();
    }

    public void setRestrictedObject(GameObject tgtObject)
    {
        l.imp_setRestrictedObject(tgtObject);
    }

    public void setRestrictedObject(GameObject tgtObject, GameObject endObject)
    {
        l.imp_setRestrictedObject(tgtObject, endObject);
    }

    public void clearRestrictedObject()
    {
        l.imp_clearRestrictedObject();
    }

    public void setRestrictedPlane(Plane tgtPlane)
    {
        l.imp_setRestrictedPlane(tgtPlane);
    }

    public void clearRestrictedPlane()
    {
        l.imp_clearRestrictedPlane();
    }

    public void setLayerMask(LayerMask layerMask)
    {
        l.imp_setLayerMask(layerMask);
    }

    public void clearLayerMask()
    {
        l.imp_clearLayerMask();
    }

    public void setRestrictedObjectStartName(string tgtObjectName, string endObjectName)
    {
        l.imp_setRestrictedObjectStartName(tgtObjectName, endObjectName);
    }

    public void setRestrictedObjectStartName(string tgtObjectName)
    {
        l.imp_setRestrictedObjectStartName(tgtObjectName);
    }

    public void clearRestrictedObjectStartName()
    {
        l.imp_clearRestrictedObjectStartName();
    }

    public void setRestrictedObjectContainsName(string tgtObjectName, string endObjectName)
    {
        l.imp_setRestrictedObjectContainsName(tgtObjectName, endObjectName);
    }

    public void setRestrictedObjectContainsName(string tgtObjectName)
    {
        l.imp_setRestrictedObjectContainsName(tgtObjectName);
    }

    public void clearRestrictedObjectContainsName()
    {
        l.imp_clearRestrictedObjectContainsName();
    }


















    public bool isOn()
    {
        return l.imp_isOn();
    }

    public GameObject getHitObject()
    {
        return l.imp_getHitObject();
    }

    public Vector3 getHitPoint()
    {
        return l.imp_getHitPoint();
    }

    public Vector3 getHitNormal()
    {
        return l.imp_getHitNormal();
    }

    public float getHitDistance()
    {
        return l.imp_getHitDistance();
    }

    public Vector3 getEndPoint()
    {
        return l.imp_getEndPoint();
    }

    public Vector3 getEndNormal()
    {
        return l.imp_getEndNormal();
    }

    public float getEndDistance()
    {
        return l.imp_getEndDistance();
    }

    public Vector3 getStartPoint()
    {
        return l.imp_getStartPoint();
    }

    public Vector3 getDirection()
    {
        return l.imp_getDirection();
    }

    public Vector3 getTerminalPoint()
    {
        return l.imp_getTerminalPoint();
    }

    public Vector3 getTerminalNormal()
    {
        return l.imp_getTerminalNormal();
    }

    public float getTerminalDistance()
    {
        return l.imp_getTerminalDistance();
    }

    public bool isHit()
    {
        return l.imp_isHit();
    }

    public bool hasEnd()
    {
        return l.imp_hasEnd();
    }




}
