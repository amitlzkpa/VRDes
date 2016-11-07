using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshMakerPlane : MonoBehaviour
{


    private GameObject parentCloneObj;
    private RefObjects_Plane refObj;
    private GameObject meshObj;
    private MeshFilter meshFilter;



    //---------------------------------------------------------------



    private void setupMeshObjects()
    {
        meshObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Destroy(meshObj.GetComponent<MeshCollider>());
        Material defMat = Resources.Load("Materials/defaultObjectMaterial", typeof(Material)) as Material;
        meshObj.GetComponent<MeshRenderer>().material = defMat;
        meshObj.transform.SetParent(transform);
        meshFilter = meshObj.GetComponent<MeshFilter>();
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
        meshFilter.mesh.Clear();
    }



    //---------------------------------------------------------------



    public void updateMesh()
    {
        clearMesh();

        Vector3 lT = refObj.getPtLeftTop();
        Vector3 lB = refObj.getPtLeftBottom();
        Vector3 rB = refObj.getPtRightBottom();
        Vector3 rT = refObj.getPtRightTop();

        List<Vector3> cornerPoints = new List<Vector3>();
        cornerPoints.Add(lB);
        cornerPoints.Add(rB);
        cornerPoints.Add(lT);
        cornerPoints.Add(rT);

        meshFilter.mesh = getMesh(cornerPoints, transform.forward);
    }



    //---------------------------------------------------------------



    void Start()
    {
        parentCloneObj = transform.parent.gameObject;
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Plane>();
        setupMeshObjects();
        updateMesh();
    }
}
