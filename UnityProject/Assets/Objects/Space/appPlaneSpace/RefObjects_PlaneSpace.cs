using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefObjects_PlaneSpace : MonoBehaviour, RefObjectManager
{


    private bool isVisible = true;


    public void hideRefObjects()
    {
        isVisible = false;
        updateVisisble();
    }


    public void showRefObjects()
    {
        isVisible = true;
        updateVisisble();
    }


    public void toggleRefObjects()
    {
        isVisible = !isVisible;
        updateVisisble();
    }


    private void updateVisisble()
    {
        for (int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isVisible);
        }
    }



    //---------------------------------------------------------------


    public GameObject pointRepPrefab;
    public GameObject edgeRepPrefab;
    public GameObject planeRepPrefab;



    //---------------------------------------------------------------



    private GameObject plCenter;


    private GameObject ptLeftTop;
    private GameObject ptLeftBottom;
    private GameObject ptRightTop;
    private GameObject ptRightBottom;


    private GameObject edLeft;
    private GameObject edBottom;
    private GameObject edRight;
    private GameObject edTop;
    private float edSpan = 0.75f;



    //---------------------------------------------------------------



    public Vector3 getPtCenter()
    {
        return plCenter.transform.position;
    }


    public Vector3 getPtLeftTop()
    {
        return ptLeftTop.transform.position;
    }


    public Vector3 getPtLeftBottom()
    {
        return ptLeftBottom.transform.position;
    }


    public Vector3 getPtRightBottom()
    {
        return ptRightBottom.transform.position;
    }


    public Vector3 getPtRightTop()
    {
        return ptRightTop.transform.position;
    }



    //---------------------------------------------------------------



    private GameObject getEmptyGameObjectAtOrigin()
    {
        GameObject emptyObject = new GameObject();
        emptyObject.transform.position = Vector3.zero;
        emptyObject.transform.rotation = Quaternion.identity;
        return emptyObject;
    }


    private float def = 0.2f;
    private float gap = 0.4f;


    public void adjustEdgeHandles()
    {
        edLeft.transform.up = getPtLeftBottom() - getPtLeftTop();
        edLeft.transform.position = (getPtLeftBottom() + getPtLeftTop()) / 2;
        edLeft.transform.localScale = new Vector3(def, Vector3.Distance(getPtLeftBottom(), getPtLeftTop()) * gap, def);
        edTop.transform.up = getPtLeftTop() - getPtRightTop();
        edTop.transform.position = (getPtLeftTop() + getPtRightTop()) / 2;
        edTop.transform.localScale = new Vector3(def, Vector3.Distance(getPtLeftTop(), getPtRightTop()) * gap, def);
        edRight.transform.up = getPtRightTop() - getPtRightBottom();
        edRight.transform.position = (getPtRightTop() + getPtRightBottom()) / 2;
        edRight.transform.localScale = new Vector3(def, Vector3.Distance(getPtRightTop(), getPtRightBottom()) * gap, def);
        edBottom.transform.up = getPtRightBottom() - getPtLeftBottom();
        edBottom.transform.position = (getPtRightBottom() + getPtLeftBottom()) / 2;
        edBottom.transform.localScale = new Vector3(def, Vector3.Distance(getPtRightBottom(), getPtLeftBottom()) * gap, def);
        plCenter.transform.position = (getPtLeftTop() + getPtLeftBottom() + getPtRightBottom() + getPtRightTop()) / 4;
    }



    private GameObject createFrame(List<Vector3> pts)
    {
        GameObject frameObject = new GameObject();

        Vector3 center = Vector3.zero;
        foreach(Vector3 pt in pts)
        {
            center += pt;
        }
        center /= pts.Count;

        // get the normal for the given points
        // point at this stage should always be planar since they come from a reference place
        Vector3 dir = Vector3.Cross(pts[1] - pts[0], pts[2] - pts[0]);
        Vector3 normal = Vector3.Normalize(dir);

        // move the empty parent to the required position and facing the needed direction
        frameObject.transform.position = center;
        frameObject.transform.forward = normal;

        plCenter = Instantiate(planeRepPrefab, center, Quaternion.identity, frameObject.transform);
        ptLeftBottom = Instantiate(pointRepPrefab, pts[0], Quaternion.LookRotation(normal, frameObject.transform.forward), frameObject.transform);
        ptRightBottom = Instantiate(pointRepPrefab, pts[1], Quaternion.LookRotation(normal, frameObject.transform.forward), frameObject.transform);
        ptRightTop = Instantiate(pointRepPrefab, pts[2], Quaternion.LookRotation(normal, frameObject.transform.forward), frameObject.transform);
        ptLeftTop = Instantiate(pointRepPrefab, pts[3], Quaternion.LookRotation(normal, frameObject.transform.forward), frameObject.transform);
        plCenter.transform.up = normal;

        edLeft = Instantiate(edgeRepPrefab, Vector3.zero, Quaternion.identity, frameObject.transform);
        edBottom = Instantiate(edgeRepPrefab, Vector3.zero, Quaternion.identity, frameObject.transform);
        edRight = Instantiate(edgeRepPrefab, Vector3.zero, Quaternion.identity, frameObject.transform);
        edTop = Instantiate(edgeRepPrefab, Vector3.zero, Quaternion.identity, frameObject.transform);
        adjustEdgeHandles();

        return frameObject;
    }



    //---------------------------------------------------------------



    private void createRefObjects(List<Vector3> pts)
    {
        // create the frame at origin
        //GameObject frame = createFrameAtOrigin();
        GameObject frame = createFrame(pts);


        // change the parent of each object to the object the script is on
        plCenter.transform.SetParent(transform);

        ptLeftTop.transform.SetParent(transform);
        ptLeftBottom.transform.SetParent(transform);
        ptRightBottom.transform.SetParent(transform);
        ptRightTop.transform.SetParent(transform);

        edLeft.transform.SetParent(transform);
        edBottom.transform.SetParent(transform);
        edRight.transform.SetParent(transform);
        edTop.transform.SetParent(transform);


        // add the RefObject script and add the the reference of the plane to the refobject
        RefObjectEdge addedScript;

        addedScript = edLeft.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftTop);
        addedScript.addToAssocList(ptLeftBottom);
        addedScript = edBottom.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftBottom);
        addedScript.addToAssocList(ptRightBottom);
        addedScript = edRight.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightTop);
        addedScript.addToAssocList(ptRightBottom);
        addedScript = edTop.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftTop);
        addedScript.addToAssocList(ptRightTop);


        // rename all the objects
        // add the ref object name prepend to all names
        string refStrt = GeneralSettings.REF_OBJ_START_NAME;

        plCenter.name = refStrt + "plCenter";

        ptLeftTop.name = refStrt + "ptLeftTop";
        ptLeftBottom.name = refStrt + "ptLeftBottom";
        ptRightBottom.name = refStrt + "ptRightBottom";
        ptRightTop.name = refStrt + "ptRightTop";

        edLeft.name = refStrt + "edLeft";
        edBottom.name = refStrt + "edBottom";
        edRight.name = refStrt + "edRight";
        edTop.name = refStrt + "edTop";


        // destroy empty object
        Destroy(frame);

        hideRefObjects();
    }


    private void clearRefObjects()
    {
        for (int i=0; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }



    public void updateRefObjects(List<Vector3> pts)
    {
        clearRefObjects();
        createRefObjects(pts);
    }



}
