using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker_PlaneSpaceManager : MonoBehaviour, MeshMaker
{

    public GameObject app_PlaneSpacePrefab;


    private GameObject parentCloneObj;
    private RefObjects_Space refObj;



    // Use this for initialization
    void Start()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Space>();
        updateMesh();
    }


    private void clearMesh()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    public void createMesh()
    {
        GameObject newSide;

        Vector3 leftTopFront = refObj.getPtLeftTopFront();
        Vector3 leftBottomFront = refObj.getPtLeftBottomFront();
        Vector3 rightBottomFront = refObj.getPtRightBottomFront();
        Vector3 rightTopFront = refObj.getPtRightTopFront();
        Vector3 leftTopBack = refObj.getPtLeftTopBack();
        Vector3 leftBottomBack = refObj.getPtLeftBottomBack();
        Vector3 rightBottomBack = refObj.getPtRightBottomBack();
        Vector3 rightTopBack = refObj.getPtRightTopBack();
        Vector3 centerFront = refObj.getPtCenterFront();
        Vector3 centerLeft = refObj.getPtCenterLeft();
        Vector3 centerBack = refObj.getPtCenterBack();
        Vector3 centerRight = refObj.getPtCenterRight();
        Vector3 centerTop = refObj.getPtCenterTop();
        Vector3 centerBottom = refObj.getPtCenterBottom();


        List<Vector3> frontSidePts = new List<Vector3>();
        frontSidePts.Add(leftBottomFront);
        frontSidePts.Add(rightBottomFront);
        frontSidePts.Add(rightTopFront);
        frontSidePts.Add(leftTopFront);
        newSide = Instantiate(app_PlaneSpacePrefab, centerFront, Quaternion.LookRotation(centerFront - centerBack), transform);
        newSide.GetComponent<app_PlaneSpace>().init(frontSidePts);
        newSide.name = "app_PlaneSpace_Front";


        List<Vector3> rightSidePts = new List<Vector3>();
        rightSidePts.Add(rightBottomFront);
        rightSidePts.Add(rightBottomBack);
        rightSidePts.Add(rightTopBack);
        rightSidePts.Add(rightTopFront);
        newSide = Instantiate(app_PlaneSpacePrefab, centerRight, Quaternion.LookRotation(centerRight - centerLeft), transform);
        newSide.GetComponent<app_PlaneSpace>().init(rightSidePts);
        newSide.name = "app_PlaneSpace_Right";


        List<Vector3> backSidePts = new List<Vector3>();
        backSidePts.Add(rightBottomBack);
        backSidePts.Add(rightTopBack);
        backSidePts.Add(leftTopBack);
        backSidePts.Add(leftBottomBack);
        newSide = Instantiate(app_PlaneSpacePrefab, centerBack, Quaternion.LookRotation(centerBack - centerFront), transform);
        newSide.GetComponent<app_PlaneSpace>().init(backSidePts);
        newSide.name = "app_PlaneSpace_Back";


        List<Vector3> leftSidePts = new List<Vector3>();
        leftSidePts.Add(leftBottomBack);
        leftSidePts.Add(leftTopBack);
        leftSidePts.Add(leftTopFront);
        leftSidePts.Add(leftBottomFront);
        newSide = Instantiate(app_PlaneSpacePrefab, centerLeft, Quaternion.LookRotation(centerLeft - centerRight), transform);
        newSide.GetComponent<app_PlaneSpace>().init(leftSidePts);
        newSide.name = "app_PlaneSpace_Left";


        List<Vector3> topSidePts = new List<Vector3>();
        topSidePts.Add(rightTopFront);
        topSidePts.Add(rightTopBack);
        topSidePts.Add(leftTopBack);
        topSidePts.Add(leftTopFront);
        newSide = Instantiate(app_PlaneSpacePrefab, centerTop, Quaternion.LookRotation(centerTop - centerBottom), transform);
        newSide.GetComponent<app_PlaneSpace>().init(topSidePts);
        newSide.name = "app_PlaneSpace_Top";


        List<Vector3> bottomSidePts = new List<Vector3>();
        bottomSidePts.Add(rightBottomFront);
        bottomSidePts.Add(rightBottomBack);
        bottomSidePts.Add(leftBottomBack);
        bottomSidePts.Add(leftBottomFront);
        newSide = Instantiate(app_PlaneSpacePrefab, centerBottom, Quaternion.LookRotation(centerBottom - centerTop), transform);
        newSide.GetComponent<app_PlaneSpace>().init(bottomSidePts);
        newSide.name = "app_PlaneSpace_Bottom";

    }





    public void updateMesh()
    {
        clearMesh();
        createMesh();
    }
}
