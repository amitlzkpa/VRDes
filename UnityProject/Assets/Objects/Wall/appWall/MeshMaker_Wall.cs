using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshMaker_Wall : MonoBehaviour, MeshMaker
{


    private GameObject parentCloneObj;
    private RefObjects_Wall refObj;



    //---------------------------------------------------------------



    private void recreateMeshObjects()
    {
        List<List<Vector3>> cornerPoints = refObj.getAllPtSets();
        List<bool> visibilityList = refObj.getAllVisibilityList();
        int subMeshCount = cornerPoints.Count;
        for (int i=0; i<subMeshCount; i++)
        {
            if (!visibilityList[i]) continue;
            GameObject meshObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Material defMat = Resources.Load("Materials/White", typeof(Material)) as Material;
            meshObj.GetComponent<MeshRenderer>().material = defMat;
            meshObj.transform.SetParent(transform);
            Mesh newMesh = getBoxMesh(cornerPoints[i], transform.forward);
            meshObj.GetComponent<MeshFilter>().mesh = newMesh;
            meshObj.GetComponent<MeshCollider>().sharedMesh = newMesh;
        }
        // method call yo update the real material array in highlight script since the model has changed
        parentCloneObj.GetComponent<HighlightStyle1>().setupRealMaterialArray();
    }



    //---------------------------------------------------------------


    private List<Vector3> getBoxPts(List<Vector3> points, Vector3 normal)
    {
        float d = 0.2f;
        List<Vector3> retList = new List<Vector3>();
        retList.Add(points[0] + (normal * d));  // 0
        retList.Add(points[0] - (normal * d));  // 1
        retList.Add(points[1] + (normal * d));  // 2
        retList.Add(points[1] - (normal * d));  // 3

        retList.Add(points[1] + (normal * d));  // 4
        retList.Add(points[1] - (normal * d));  // 5
        retList.Add(points[2] + (normal * d));  // 6
        retList.Add(points[2] - (normal * d));  // 7

        retList.Add(points[2] + (normal * d));  // 8
        retList.Add(points[2] - (normal * d));  // 9
        retList.Add(points[3] + (normal * d));  // 10
        retList.Add(points[3] - (normal * d));  // 11

        retList.Add(points[3] + (normal * d));  // 12
        retList.Add(points[3] - (normal * d));  // 13
        retList.Add(points[0] + (normal * d));  // 14
        retList.Add(points[0] - (normal * d));  // 15

        retList.Add(points[0] + (normal * d));  // 16
        retList.Add(points[1] + (normal * d));  // 17
        retList.Add(points[2] + (normal * d));  // 18
        retList.Add(points[3] + (normal * d));  // 19

        retList.Add(points[0] - (normal * d));  // 20
        retList.Add(points[1] - (normal * d));  // 21
        retList.Add(points[2] - (normal * d));  // 22
        retList.Add(points[3] - (normal * d));  // 23

        return retList;
    }




    // CONTINUE HERE: check normal correctness
    private Mesh getBoxMesh(List<Vector3> points, Vector3 normal)
    {
        Mesh m = new Mesh();
        List<Vector3> crPts = getBoxPts(points, normal);

        m.SetVertices(crPts);


        Vector3[] normales = new Vector3[]
        {
            // bottom
            // front
            // top
            // back
            normal,     normal,     normal,     normal,
            -normal,    -normal,    -normal,    -normal,
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,

            7, 6, 4,
            4, 5, 7,

            10, 9, 11,
            9, 10, 8,

            13, 15, 12,
            14, 12, 15,

            16, 17, 19,
            18, 19, 17,

            22, 20, 23,
            20, 22, 21
        };
        m.triangles = triangles;


        return m;
    }
    















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
        for (int i=0; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }



    //---------------------------------------------------------------



    public void updateMesh()
    {
        clearMesh();
        recreateMeshObjects();
    }



    //---------------------------------------------------------------



    void Start()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Wall>();
        updateMesh();
    }
}
