using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefObjects_Wall : MonoBehaviour, RefObjectManager
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


    private List<List<GameObject>> refPtObjs = new List<List<GameObject>>();
    private List<bool> visibilityList = new List<bool>();


    public void addToPtSet(List<Vector3> pts, bool visibility)
    {
        if (refPtObjs.Count != visibilityList.Count && refPtObjs.Count != transform.childCount)
        {
            Debug.LogError("Invalid state with list and child lengths, before adding.");
        }

        GameObject newSet = new GameObject("srfPtSet_" + transform.childCount);
        newSet.transform.position = transform.position;
        newSet.transform.SetParent(transform);
        List<GameObject> refPtList = new List<GameObject>();
        foreach (Vector3 pt in pts)
        {
            GameObject ptRef = Instantiate(pointRepPrefab, pt, Quaternion.identity, newSet.transform);
            ptRef.AddComponent<RefObjectPlaneConstraintPoint>();
            refPtList.Add(ptRef);
        }
        refPtObjs.Add(refPtList);
        visibilityList.Add(visibility);

        if (refPtObjs.Count != visibilityList.Count && refPtObjs.Count != transform.childCount)
        {
            Debug.LogError("Invalid state with list and child lengths, after adding.");
        }
    }



    private List<List<Vector3>> retList;
    private List<Vector3> ptList;

    public List<List<Vector3>> getAllPtSets()
    {
        retList = new List<List<Vector3>>();
        for (int i=0; i<transform.childCount; i++)
        {
            ptList = new List<Vector3>();
            for (int j=0; j<transform.GetChild(i).childCount; j++)
            {
                ptList.Add(transform.GetChild(i).GetChild(j).position);
            }
            retList.Add(ptList);
        }
        return retList;
    }


    public List<bool> getAllVisibilityList()
    {
        return visibilityList;
    }



    //---------------------------------------------------------------



    public Vector3 getPtCenter()
    {
        return transform.position;
    }



    //---------------------------------------------------------------



    public void adjustEdgeHandles()
    {
        // no handles to adjust
    }



    //---------------------------------------------------------------



    void Awake ()
    {
    }



}
