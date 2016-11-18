using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RefObjects_Line : MonoBehaviour, RefObjectManager
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


    //---------------------------------------------------------------



    private GameObject ptCenter;
    private List<GameObject> pts = new List<GameObject>();



    //---------------------------------------------------------------



    public Vector3 getPtCenter()
    {
        return ptCenter.transform.position;
    }



    public List<Vector3> getAllPts()
    {
        List<Vector3> retList = new List<Vector3>();
        foreach(GameObject p in pts)
        {
            retList.Add(p.transform.position);
        }
        return retList;
    }



    //---------------------------------------------------------------



    public void setAllPts(List<Vector3> inpPts)
    {
        foreach (Vector3 v in inpPts)
        {
            pts.Add(Instantiate(pointRepPrefab, v, Quaternion.identity, transform));
        }
    }



    //---------------------------------------------------------------



    public void adjustEdgeHandles()
    {
        // no handles for lines
    }



    //---------------------------------------------------------------



    void Start ()
    {
        ptCenter = Instantiate(pointRepPrefab, transform.position, Quaternion.identity, transform);


        // rename all the objects
        // add the ref object name prepend to all names
        string refStrt = GeneralSettings.REF_OBJ_START_NAME;

        int i = 0;
        ptCenter.name = refStrt + "ptCenter";
        foreach (GameObject p in pts)
        {
            p.name = refStrt + "ptVertex_" + i;
            i++;
        }



        foreach (GameObject p in pts)
        {
            p.AddComponent<RefObjectPoint>();
        }








        hideRefObjects();
    }
}
