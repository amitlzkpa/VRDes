﻿using UnityEngine;
using System.Collections;

public class RefObjects_Plane : MonoBehaviour, RefObjectManager
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



    private float halfWidth = 3f;
    private float halfHeight = 2f;



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



    public Plane getRefPlane()
    {
        return new Plane(transform.parent.forward, getPtCenter());
    }



    //---------------------------------------------------------------



    private GameObject getEmptyGameObjectAtOrigin()
    {
        GameObject emptyObject = new GameObject();
        emptyObject.transform.position = Vector3.zero;
        emptyObject.transform.rotation = Quaternion.identity;
        return emptyObject;
    }



    private GameObject createFrameAtOrigin()
    {
        // create an empty object at origin
        GameObject emptyObject = getEmptyGameObjectAtOrigin();

        plCenter = Instantiate(planeRepPrefab, Vector3.zero, Quaternion.LookRotation(Vector3.up), emptyObject.transform);

        ptLeftTop = Instantiate(pointRepPrefab, new Vector3(-halfWidth, +halfHeight, 0), Quaternion.identity, emptyObject.transform);
        ptLeftBottom = Instantiate(pointRepPrefab, new Vector3(-halfWidth, -halfHeight, 0), Quaternion.identity, emptyObject.transform);
        ptRightBottom = Instantiate(pointRepPrefab, new Vector3(+halfWidth, -halfHeight, 0), Quaternion.identity, emptyObject.transform);
        ptRightTop = Instantiate(pointRepPrefab, new Vector3(+halfWidth, +halfHeight, 0), Quaternion.identity, emptyObject.transform);

        edLeft = Instantiate(edgeRepPrefab, new Vector3(-halfWidth, 0, 0), Quaternion.identity, emptyObject.transform);
        edLeft.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edBottom = Instantiate(edgeRepPrefab, new Vector3(0, -halfHeight, 0), Quaternion.identity, emptyObject.transform);
        edBottom.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edBottom.transform.Rotate(0, 0, 90);
        edRight = Instantiate(edgeRepPrefab, new Vector3(+halfWidth, 0, 0), Quaternion.identity, emptyObject.transform);
        edRight.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edTop = Instantiate(edgeRepPrefab, new Vector3(0, +halfHeight, 0), Quaternion.identity, emptyObject.transform);
        edTop.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edTop.transform.Rotate(0, 0, 90);

        return emptyObject;
    }



    //---------------------------------------------------------------



    void Awake ()
    {
        // create the frame at origin
        GameObject frame = createFrameAtOrigin();


        // move the empty parent to the required position and facing the needed direction
        frame.transform.position = transform.position;
        frame.transform.rotation = transform.rotation;


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
        RefObject addedScript;

        addedScript = plCenter.AddComponent<RefObject>() as RefObject;

        addedScript = ptLeftTop.AddComponent<RefObject>() as RefObject;
        addedScript = ptLeftBottom.AddComponent<RefObject>() as RefObject;
        addedScript = ptRightBottom.AddComponent<RefObject>() as RefObject;
        addedScript = ptRightTop.AddComponent<RefObject>() as RefObject;

        addedScript = edLeft.AddComponent<RefObject>() as RefObject;
        addedScript = edBottom.AddComponent<RefObject>() as RefObject;
        addedScript = edRight.AddComponent<RefObject>() as RefObject;
        addedScript = edTop.AddComponent<RefObject>() as RefObject;


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



}
