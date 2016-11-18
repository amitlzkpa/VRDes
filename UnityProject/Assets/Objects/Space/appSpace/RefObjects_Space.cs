using UnityEngine;
using System.Collections;
using System;

public class RefObjects_Space : MonoBehaviour, RefObjectManager
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
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isVisible);
        }
    }


    //---------------------------------------------------------------


    public GameObject pointRepPrefab;
    public GameObject edgeRepPrefab;
    public GameObject planeRepPrefab;



    private float halfWidth = 5f;
    private float halfHeight = 3f;
    private float halfDepth = 5f;


    //---------------------------------------------------------------


    public void adjustEdgeHandles()
    {
        edLeftFront.transform.up = getPtLeftBottomFront() - getPtLeftTopFront();
        edLeftFront.transform.position = (getPtLeftBottomFront() + getPtLeftTopFront()) / 2;
        edBottomFront.transform.up = getPtLeftBottomFront() - getPtRightBottomFront();
        edBottomFront.transform.position = (getPtLeftBottomFront() + getPtRightBottomFront()) / 2;
        edRightFront.transform.up = getPtRightBottomFront() - getPtRightTopFront();
        edRightFront.transform.position = (getPtRightBottomFront() + getPtRightTopFront()) / 2;
        edTopFront.transform.up = getPtLeftTopFront() - getPtRightTopFront();
        edTopFront.transform.position = (getPtLeftTopFront() + getPtRightTopFront()) / 2;
        edLeftMid.transform.up = getPtLeftTopFront() - getPtLeftTopBack();
        edLeftMid.transform.position = (getPtLeftTopFront() + getPtLeftTopBack()) / 2;
        edBottomMid.transform.up = getPtLeftBottomFront() - getPtLeftBottomBack();
        edBottomMid.transform.position = (getPtLeftBottomFront() + getPtLeftBottomBack()) / 2;
        edRightMid.transform.up = getPtRightBottomFront() - getPtRightBottomBack();
        edRightMid.transform.position = (getPtRightBottomFront() + getPtRightBottomBack()) / 2;
        edTopMid.transform.up = getPtRightTopFront() - getPtRightTopBack();
        edTopMid.transform.position = (getPtRightTopFront() + getPtRightTopBack()) / 2;
        edLeftBack.transform.up = getPtLeftTopBack() - getPtLeftBottomBack();
        edLeftBack.transform.position = (getPtLeftTopBack() + getPtLeftBottomBack()) / 2;
        edBottomBack.transform.up = getPtLeftBottomBack() - getPtRightBottomBack();
        edBottomBack.transform.position = (getPtLeftBottomBack() + getPtRightBottomBack()) / 2;
        edRightBack.transform.up = getPtRightBottomBack() - getPtRightTopBack();
        edRightBack.transform.position = (getPtRightBottomBack() + getPtRightTopBack()) / 2;
        edTopBack.transform.up = getPtLeftTopBack() - getPtRightTopBack();
        edTopBack.transform.position = (getPtLeftTopBack() + getPtRightTopBack()) / 2;

        /*
        plFront.transform.position = (getPtLeftTopFront() + getPtLeftBottomFront() + getPtRightBottomFront() + getPtRightTopFront()) / 4;
        plBack.transform.position = (getPtLeftTopBack() + getPtLeftBottomBack() + getPtRightBottomBack() + getPtRightTopBack()) / 4;
        plTop.transform.position = (getPtLeftTopFront() + getPtRightTopFront() + getPtRightTopBack() + getPtLeftTopBack()) / 4;
        plBottom.transform.position = (getPtLeftBottomFront() + getPtRightBottomFront() + getPtRightBottomBack() + getPtLeftBottomBack()) / 4;
        plRight.transform.position = (getPtRightTopFront() + getPtRightBottomFront() + getPtRightBottomBack() + getPtRightTopBack()) / 4;
        plLeft.transform.position = (getPtLeftTopFront() + getPtLeftBottomFront() + getPtLeftBottomBack() + getPtLeftTopBack()) / 4;
        */
    }

    public Vector3 getPtCenter()
    {
        return ptCenter.transform.position;
    }


    //---------------------------------------------------------------


    public Vector3 getPtLeftTopFront()
    {
        return ptLeftTopFront.transform.position;
    }


    public Vector3 getPtLeftBottomFront()
    {
        return ptLeftBottomFront.transform.position;
    }


    public Vector3 getPtRightBottomFront()
    {
        return ptRightBottomFront.transform.position;
    }


    public Vector3 getPtRightTopFront()
    {
        return ptRightTopFront.transform.position;
    }


    public Vector3 getPtLeftTopBack()
    {
        return ptLeftTopBack.transform.position;
    }


    public Vector3 getPtLeftBottomBack()
    {
        return ptLeftBottomBack.transform.position;
    }


    public Vector3 getPtRightBottomBack()
    {
        return ptRightBottomBack.transform.position;
    }


    public Vector3 getPtRightTopBack()
    {
        return ptRightTopBack.transform.position;
    }


    //---------------------------------------------------------------


    private GameObject ptCenter;
    private GameObject ptLeftTopFront;
    private GameObject ptLeftBottomFront;
    private GameObject ptRightBottomFront;
    private GameObject ptRightTopFront;
    private GameObject ptLeftTopBack;
    private GameObject ptLeftBottomBack;
    private GameObject ptRightBottomBack;
    private GameObject ptRightTopBack;


    private GameObject edLeftFront;
    private GameObject edBottomFront;
    private GameObject edRightFront;
    private GameObject edTopFront;
    private GameObject edLeftMid;
    private GameObject edBottomMid;
    private GameObject edRightMid;
    private GameObject edTopMid;
    private GameObject edLeftBack;
    private GameObject edBottomBack;
    private GameObject edRightBack;
    private GameObject edTopBack;

    private float edSpan = 0.75f;


    private GameObject plFront;
    private GameObject plLeft;
    private GameObject plBack;
    private GameObject plRight;
    private GameObject plTop;
    private GameObject plBottom;

    private float plXSpan = 0.75f;
    private float plYSpan = 0.75f;


    private Plane refPlaneFront;
    private Plane refPlaneLeft;
    private Plane refPlaneBack;
    private Plane refPlaneRight;
    private Plane refPlaneTop;
    private Plane refPlaneBottom;


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

        ptCenter = Instantiate(pointRepPrefab, Vector3.zero, Quaternion.identity, emptyObject.transform);
        ptLeftTopFront = Instantiate(pointRepPrefab, new Vector3(-halfWidth, +halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptLeftBottomFront = Instantiate(pointRepPrefab, new Vector3(-halfWidth, -halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightBottomFront = Instantiate(pointRepPrefab, new Vector3(+halfWidth, -halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightTopFront = Instantiate(pointRepPrefab, new Vector3(+halfWidth, +halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptLeftTopBack = Instantiate(pointRepPrefab, new Vector3(-halfWidth, +halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);
        ptLeftBottomBack = Instantiate(pointRepPrefab, new Vector3(-halfWidth, -halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightBottomBack = Instantiate(pointRepPrefab, new Vector3(+halfWidth, -halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightTopBack = Instantiate(pointRepPrefab, new Vector3(+halfWidth, +halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);


        edLeftFront = Instantiate(edgeRepPrefab, new Vector3(-halfWidth, 0, +halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edLeftFront.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edBottomFront = Instantiate(edgeRepPrefab, new Vector3(0, -halfHeight, +halfDepth), Quaternion.Euler(0, 0, 90), emptyObject.transform);
        edBottomFront.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edRightFront = Instantiate(edgeRepPrefab, new Vector3(+halfWidth, 0, +halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edRightFront.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edTopFront = Instantiate(edgeRepPrefab, new Vector3(0, +halfHeight, +halfDepth), Quaternion.Euler(0, 0, -90), emptyObject.transform);
        edTopFront.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edLeftMid = Instantiate(edgeRepPrefab, new Vector3(-halfWidth, +halfHeight, 0), Quaternion.Euler(90, 0, 0), emptyObject.transform);
        edLeftMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edBottomMid = Instantiate(edgeRepPrefab, new Vector3(-halfWidth, -halfHeight, 0), Quaternion.Euler(90, 0, 0), emptyObject.transform);
        edBottomMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edRightMid = Instantiate(edgeRepPrefab, new Vector3(+halfWidth, -halfHeight, 0), Quaternion.Euler(-90, 0, 0), emptyObject.transform);
        edRightMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edTopMid = Instantiate(edgeRepPrefab, new Vector3(+halfWidth, +halfHeight, 0), Quaternion.Euler(-90, 0, 0), emptyObject.transform);
        edTopMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edLeftBack = Instantiate(edgeRepPrefab, new Vector3(-halfWidth, 0, -halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edLeftBack.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edBottomBack = Instantiate(edgeRepPrefab, new Vector3(0, -halfHeight, -halfDepth), Quaternion.Euler(0, 0, 90), emptyObject.transform);
        edBottomBack.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edRightBack = Instantiate(edgeRepPrefab, new Vector3(+halfWidth, 0, -halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edRightBack.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edTopBack = Instantiate(edgeRepPrefab, new Vector3(0, +halfHeight, -halfDepth), Quaternion.Euler(0, 0, -90), emptyObject.transform);
        edTopBack.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);

        // TO-DO: make the plane ref objects

        return emptyObject;
    }


    //---------------------------------------------------------------





    //---------------------------------------------------------------







    // Use this for initialization
    void Awake ()
    {
        // create the frame at origin
        GameObject frame = createFrameAtOrigin();


        // move the empty parent to the required position and facing the needed direction
        frame.transform.position = transform.position;
        frame.transform.rotation = transform.rotation;


        // change the parent of each object to the object the script is on
        ptCenter.transform.SetParent(transform);
        ptLeftTopFront.transform.SetParent(transform);
        ptLeftBottomFront.transform.SetParent(transform);
        ptRightBottomFront.transform.SetParent(transform);
        ptRightTopFront.transform.SetParent(transform);
        ptLeftTopBack.transform.SetParent(transform);
        ptLeftBottomBack.transform.SetParent(transform);
        ptRightBottomBack.transform.SetParent(transform);
        ptRightTopBack.transform.SetParent(transform);

        edLeftFront.transform.SetParent(transform);
        edBottomFront.transform.SetParent(transform);
        edRightFront.transform.SetParent(transform);
        edTopFront.transform.SetParent(transform);
        edLeftMid.transform.SetParent(transform);
        edBottomMid.transform.SetParent(transform);
        edRightMid.transform.SetParent(transform);
        edTopMid.transform.SetParent(transform);
        edLeftBack.transform.SetParent(transform);
        edBottomBack.transform.SetParent(transform);
        edRightBack.transform.SetParent(transform);
        edTopBack.transform.SetParent(transform);


        // add the RefObject script and add the the reference of the plane to the refobject
        RefObjectEdge addedScript;

        addedScript = edLeftFront.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftTopFront);
        addedScript.addToAssocList(ptLeftBottomFront);
        addedScript = edBottomFront.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftBottomFront);
        addedScript.addToAssocList(ptRightBottomFront);
        addedScript = edRightFront.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightBottomFront);
        addedScript.addToAssocList(ptRightTopFront);
        addedScript = edTopFront.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightTopFront);
        addedScript.addToAssocList(ptLeftTopFront);
        addedScript = edLeftMid.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftTopBack);
        addedScript.addToAssocList(ptLeftTopFront);
        addedScript = edBottomMid.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftBottomBack);
        addedScript.addToAssocList(ptLeftBottomFront);
        addedScript = edRightMid.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightBottomBack);
        addedScript.addToAssocList(ptRightBottomFront);
        addedScript = edTopMid.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightTopBack);
        addedScript.addToAssocList(ptRightTopFront);
        addedScript = edLeftBack.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptLeftTopBack);
        addedScript.addToAssocList(ptLeftBottomBack);
        addedScript = edBottomBack.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightBottomBack);
        addedScript.addToAssocList(ptLeftBottomBack);
        addedScript = edRightBack.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightBottomBack);
        addedScript.addToAssocList(ptRightTopBack);
        addedScript = edTopBack.AddComponent<RefObjectEdge>() as RefObjectEdge;
        addedScript.addToAssocList(ptRightBottomBack);
        addedScript.addToAssocList(ptRightTopBack);

        // rename all the objects
        ptCenter.name = "ptCenter";
        ptLeftTopFront.name = "ptLeftTopFront";
        ptLeftBottomFront.name = "ptLeftBottomFront";
        ptRightBottomFront.name = "ptRightBottomFront";
        ptRightTopFront.name = "ptRightTopFront";
        ptLeftTopBack.name = "ptLeftTopBack";
        ptLeftBottomBack.name = "ptLeftBottomBack";
        ptRightBottomBack.name = "ptRightBottomBack";
        ptRightTopBack.name = "ptRightTopBack";

        edLeftFront.name = "edLeftFront";
        edBottomFront.name = "edBottomFront";
        edRightFront.name = "edRightFront";
        edTopFront.name = "edTopFront";
        edLeftMid.name = "edLeftMid";
        edBottomMid.name = "edBottomMid";
        edRightMid.name = "edRightMid";
        edTopMid.name = "edTopMid";
        edLeftBack.name = "edLeftBack";
        edBottomBack.name = "edBottomBack";
        edRightBack.name = "edRightBack";
        edTopBack.name = "edTopBack";


        // destroy empty object
        Destroy(frame);


        hideRefObjects();
    }
}
