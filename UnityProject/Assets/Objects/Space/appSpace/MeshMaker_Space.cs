using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker_Space : MonoBehaviour, MeshMaker
{

    private GameObject parentCloneObj;
    private RefObjects_Space refObj;
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
}
