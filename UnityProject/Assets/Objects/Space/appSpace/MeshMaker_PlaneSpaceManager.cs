using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker_PlaneSpaceManager : MonoBehaviour, MeshMaker
{

    public GameObject app_PlaneSpacePrefab;


    private GameObject parentCloneObj;
    private RefObjects_Space refObj;
    /*
    private GameObject[] meshObj;
    private MeshFilter[] meshFilter;
    private MeshCollider[] meshCollider;
    private int sides = 6;
    private Vector3[][] ptSets;
    private Vector3[] normalSet;



    //---------------------------------------------------------------


    private void setupMeshObjects()
    {
        Material defMat = Resources.Load("Materials/defaultObjectMaterial", typeof(Material)) as Material;
        for (int i=0; i<sides; i++)
        {
            meshObj[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
            meshObj[i].GetComponent<MeshRenderer>().material = defMat;
            meshObj[i].transform.SetParent(transform);
            meshFilter[i] = meshObj[i].GetComponent<MeshFilter>();
            meshCollider[i] = meshObj[i].GetComponent<MeshCollider>();
        }
        // method call yo update the real material array in highlight script since the model has changed
        parentCloneObj.GetComponent<HighlightStyle1>().setupRealMaterialArray();
    }



    //---------------------------------------------------------------



    private Mesh getMesh(List<Vector3> points, Vector3 normal)
    {
        Mesh m = new Mesh();
        m.SetVertices(points);
        //create both side facing mesh
        int[] triArr = new int[12];
        triArr[0] = 0;
        triArr[1] = 2;
        triArr[2] = 1;
        triArr[3] = 2;
        triArr[4] = 3;
        triArr[5] = 1;
        triArr[6] = 1;
        triArr[7] = 2;
        triArr[8] = 0;
        triArr[9] = 1;
        triArr[10] = 3;
        triArr[11] = 2;

        Vector3[] norArr = new Vector3[points.Count];
        for (int i = 0; i < norArr.Length; i++)
        { norArr[i] = normal; }

        m.triangles = triArr;
        m.normals = norArr;

        return m;
    }



    private void clearMesh()
    {
        for (int i = 0; i < sides; i++)
        {
            meshFilter[i].mesh.Clear();
            meshCollider[i].sharedMesh.Clear();
        }
    }



    //---------------------------------------------------------------



    public void updateMesh()
    {
        clearMesh();

        ptSets[0] = new Vector3[4] { refObj.getPtLeftBottomBack(), refObj.getPtLeftBottomFront(), refObj.getPtRightBottomBack(), refObj.getPtRightBottomFront() };
        ptSets[1] = new Vector3[4] { refObj.getPtLeftTopBack(), refObj.getPtLeftTopFront(), refObj.getPtRightTopBack(), refObj.getPtRightTopFront() };
        ptSets[2] = new Vector3[4] { refObj.getPtLeftTopBack(), refObj.getPtLeftBottomBack(), refObj.getPtRightTopBack(), refObj.getPtRightBottomBack() };
        ptSets[3] = new Vector3[4] { refObj.getPtLeftTopFront(), refObj.getPtLeftBottomFront(), refObj.getPtRightTopFront(), refObj.getPtRightBottomFront() };
        ptSets[4] = new Vector3[4] { refObj.getPtLeftTopFront(), refObj.getPtLeftBottomFront(), refObj.getPtLeftTopBack(), refObj.getPtLeftBottomBack() };
        ptSets[5] = new Vector3[4] { refObj.getPtRightTopBack(), refObj.getPtRightBottomBack(), refObj.getPtRightTopFront(), refObj.getPtRightBottomFront() };

        normalSet[0] = -transform.up;
        normalSet[1] = transform.up;
        normalSet[2] = -transform.forward;
        normalSet[3] = transform.forward;
        normalSet[4] = -transform.right;
        normalSet[5] = transform.right;

        for (int i = 0; i < sides; i++)
        {
            List<Vector3> cornerPoints = new List<Vector3>(ptSets[i]);
            Mesh newMesh = getMesh(cornerPoints, normalSet[i]);
            meshFilter[i].mesh = newMesh;
            meshCollider[i].sharedMesh = newMesh;
            meshObj[i].transform.position = Vector3.zero;
        }
    }



    //---------------------------------------------------------------



    // Use this for initialization
    void Start () {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Space>();

        meshObj = new GameObject[sides];
        meshFilter = new MeshFilter[sides];
        meshCollider = new MeshCollider[sides];
        ptSets = new Vector3[sides][];
        for (int i = 0; i < sides; i++)
        {
            ptSets[i] = new Vector3[4];
        }
        normalSet = new Vector3[sides];

        setupMeshObjects();
        updateMesh();
    }
    */



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
