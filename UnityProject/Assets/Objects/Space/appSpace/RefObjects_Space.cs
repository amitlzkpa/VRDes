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

    // Quaternion.LookRotation(Vector3.forward, ptLeftBottomFront.transform.position - ptRightBottomFront.transform.position)

    private GameObject createFrameAtOrigin()
    {
        // create an empty object at origin
        GameObject emptyObject = getEmptyGameObjectAtOrigin();

        ptCenter = (GameObject)Instantiate(pointRepPrefab, Vector3.zero, Quaternion.identity, emptyObject.transform);
        ptLeftTopFront = (GameObject)Instantiate(pointRepPrefab, new Vector3(-halfWidth, +halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptLeftBottomFront = (GameObject)Instantiate(pointRepPrefab, new Vector3(-halfWidth, -halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightBottomFront = (GameObject)Instantiate(pointRepPrefab, new Vector3(+halfWidth, -halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightTopFront = (GameObject)Instantiate(pointRepPrefab, new Vector3(+halfWidth, +halfHeight, +halfDepth), Quaternion.identity, emptyObject.transform);
        ptLeftTopBack = (GameObject)Instantiate(pointRepPrefab, new Vector3(-halfWidth, +halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);
        ptLeftBottomBack = (GameObject)Instantiate(pointRepPrefab, new Vector3(-halfWidth, -halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightBottomBack = (GameObject)Instantiate(pointRepPrefab, new Vector3(+halfWidth, -halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);
        ptRightTopBack = (GameObject)Instantiate(pointRepPrefab, new Vector3(+halfWidth, +halfHeight, -halfDepth), Quaternion.identity, emptyObject.transform);


        edLeftFront = (GameObject)Instantiate(edgeRepPrefab, new Vector3(-halfWidth, 0, +halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edLeftFront.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edBottomFront = (GameObject)Instantiate(edgeRepPrefab, new Vector3(0, -halfHeight, +halfDepth), Quaternion.Euler(0, 0, 90), emptyObject.transform);
        edBottomFront.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edRightFront = (GameObject)Instantiate(edgeRepPrefab, new Vector3(+halfWidth, 0, +halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edRightFront.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edTopFront = (GameObject)Instantiate(edgeRepPrefab, new Vector3(0, +halfHeight, +halfDepth), Quaternion.Euler(0, 0, -90), emptyObject.transform);
        edTopFront.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edLeftMid = (GameObject)Instantiate(edgeRepPrefab, new Vector3(-halfWidth, +halfHeight, 0), Quaternion.Euler(90, 0, 0), emptyObject.transform);
        edLeftMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edBottomMid = (GameObject)Instantiate(edgeRepPrefab, new Vector3(-halfWidth, -halfHeight, 0), Quaternion.Euler(90, 0, 0), emptyObject.transform);
        edBottomMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edRightMid = (GameObject)Instantiate(edgeRepPrefab, new Vector3(+halfWidth, -halfHeight, 0), Quaternion.Euler(-90, 0, 0), emptyObject.transform);
        edRightMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edTopMid = (GameObject)Instantiate(edgeRepPrefab, new Vector3(+halfWidth, +halfHeight, 0), Quaternion.Euler(-90, 0, 0), emptyObject.transform);
        edTopMid.transform.localScale = new Vector3(0.2f, halfDepth * edSpan, 0.2f);
        edLeftBack = (GameObject)Instantiate(edgeRepPrefab, new Vector3(-halfWidth, 0, -halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edLeftBack.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edBottomBack = (GameObject)Instantiate(edgeRepPrefab, new Vector3(0, -halfHeight, -halfDepth), Quaternion.Euler(0, 0, 90), emptyObject.transform);
        edBottomBack.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edRightBack = (GameObject)Instantiate(edgeRepPrefab, new Vector3(+halfWidth, 0, -halfDepth), Quaternion.Euler(0, 0, 0), emptyObject.transform);
        edRightBack.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edTopBack = (GameObject)Instantiate(edgeRepPrefab, new Vector3(0, +halfHeight, -halfDepth), Quaternion.Euler(0, 0, -90), emptyObject.transform);
        edTopBack.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);

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
