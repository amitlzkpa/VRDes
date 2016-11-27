using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshMaker_Surface : MonoBehaviour, MeshMaker
{


    private GameObject parentCloneObj;
    private RefObjects_Surface refObj;



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
            Mesh newMesh = getMesh(cornerPoints[i], transform.forward);
            meshObj.GetComponent<MeshFilter>().mesh = newMesh;
            meshObj.GetComponent<MeshCollider>().sharedMesh = newMesh;
        }
        // method call yo update the real material array in highlight script since the model has changed
        parentCloneObj.GetComponent<HighlightStyle1>().setupRealMaterialArray();
    }



    //---------------------------------------------------------------



    private Mesh getMesh(List<Vector3> points, Vector3 normal)
    {
        // rework order to ensure meshes are created where points are considered to be stored cyclically
        Vector3 tempPt = points[2];
        points[2] = points[3];
        points[3] = tempPt;

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
        refObj = parentCloneObj.transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjects_Surface>();
        updateMesh();
    }
}
