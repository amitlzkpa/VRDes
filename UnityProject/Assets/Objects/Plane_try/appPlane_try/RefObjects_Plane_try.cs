using UnityEngine;
using System.Collections;

public class RefObjects_Plane_try : MonoBehaviour
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



    private float halfWidth = 3f;
    private float halfHeight = 2f;



    //---------------------------------------------------------------



    private GameObject ptCenter;
    private GameObject ptLeftTop;
    private GameObject ptLeftBottom;
    private GameObject ptRightTop;
    private GameObject ptRightBottom;


    private GameObject edLeft;
    private GameObject edBottom;
    private GameObject edRight;
    private GameObject edTop;
    private float edSpan = 0.75f;

    private Plane refPlane;



    //---------------------------------------------------------------



    public Vector3 getPtCenter()
    {
        return ptCenter.transform.position;
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



    private GameObject createFrameAtOrigin()
    {
        // create an empty object at origin
        GameObject emptyObject = getEmptyGameObjectAtOrigin();

        ptCenter = (GameObject)Instantiate(pointRepPrefab, Vector3.zero, Quaternion.identity, emptyObject.transform);
        ptLeftTop = (GameObject)Instantiate(pointRepPrefab, new Vector3(-halfWidth, +halfHeight, 0), Quaternion.identity, emptyObject.transform);
        ptLeftBottom = (GameObject)Instantiate(pointRepPrefab, new Vector3(-halfWidth, -halfHeight, 0), Quaternion.identity, emptyObject.transform);
        ptRightBottom = (GameObject)Instantiate(pointRepPrefab, new Vector3(+halfWidth, -halfHeight, 0), Quaternion.identity, emptyObject.transform);
        ptRightTop = (GameObject)Instantiate(pointRepPrefab, new Vector3(+halfWidth, +halfHeight, 0), Quaternion.identity, emptyObject.transform);

        edLeft = (GameObject)Instantiate(edgeRepPrefab, new Vector3(-halfWidth, 0, 0), Quaternion.identity, emptyObject.transform);
        edLeft.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edBottom = (GameObject)Instantiate(edgeRepPrefab, new Vector3(0, -halfHeight, 0), Quaternion.identity, emptyObject.transform);
        edBottom.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edBottom.transform.Rotate(0, 0, 90);
        edRight = (GameObject)Instantiate(edgeRepPrefab, new Vector3(+halfWidth, 0, 0), Quaternion.identity, emptyObject.transform);
        edRight.transform.localScale = new Vector3(0.2f, halfHeight * edSpan, 0.2f);
        edTop = (GameObject)Instantiate(edgeRepPrefab, new Vector3(0, +halfHeight, 0), Quaternion.identity, emptyObject.transform);
        edTop.transform.localScale = new Vector3(0.2f, halfWidth * edSpan, 0.2f);
        edTop.transform.Rotate(0, 0, 90);

        return emptyObject;
    }



    //---------------------------------------------------------------



    void Awake ()
    {
        refPlane = new Plane(transform.forward, transform.position);


        // create the frame at origin
        GameObject frame = createFrameAtOrigin();


        // move the empty parent to the required position and facing the needed direction
        frame.transform.position = transform.position;
        frame.transform.rotation = transform.rotation;


        // change the parent of each object to the object the script is on
        ptCenter.transform.SetParent(transform);
        ptLeftTop.transform.SetParent(transform);
        ptLeftBottom.transform.SetParent(transform);
        ptRightBottom.transform.SetParent(transform);
        ptRightTop.transform.SetParent(transform);

        edLeft.transform.SetParent(transform);
        edBottom.transform.SetParent(transform);
        edRight.transform.SetParent(transform);
        edTop.transform.SetParent(transform);


        // rename all the objects
        ptCenter.name = "ptCenter";
        ptLeftTop.name = "ptLeftTop";
        ptLeftBottom.name = "ptLeftBottom";
        ptRightBottom.name = "ptRightBottom";
        ptRightTop.name = "ptRightTop";

        edLeft.name = "edLeft";
        edBottom.name = "edBottom";
        edRight.name = "edRight";
        edTop.name = "edTop";


        // destroy empty object
        Destroy(frame);

        hideRefObjects();
    }



}
