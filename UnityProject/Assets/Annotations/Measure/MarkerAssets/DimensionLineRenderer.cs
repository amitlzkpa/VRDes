using UnityEngine;
using System.Collections;

public class DimensionLineRenderer : MonoBehaviour {

    
    private int tickCount;
    private float strokeWidth = 0.5f;
    private bool update = false;

    private Vector3 startPt;
    private Vector3 endPt;
    private LineRenderer dimensionLineRenderer;
    public Material dimensionLineMaterial;




    public void updateDimensionLine()
    {
        update = true;
    }


    public void setEndPt(Vector3 point)
    {
        endPt = point;
    }



    private int getChildTickCount()
    {
        int count = 0;
        for (int i=0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i).name.ToLower().Contains("tick"))
            {
                count++;
            }
        }
        return count;
    }



    private void createFixedDimensionLine()
    {
        startPt = transform.GetChild(0).transform.position;
        endPt = transform.GetChild(1).transform.position;
        dimensionLineRenderer.numPositions = 2;
        dimensionLineRenderer.SetPosition(0, startPt);
        dimensionLineRenderer.SetPosition(1, endPt);
        string distanceText = Vector3.Distance(startPt, endPt).ToString("F2");
        string xDistance = Mathf.Abs(endPt.x - startPt.x).ToString("F2");
        string yDistance = Mathf.Abs(endPt.y - startPt.y).ToString("F2");
        string zDistance = Mathf.Abs(endPt.z - startPt.z).ToString("F2");
        string deltaText = System.String.Format("({0}, {1}, {2})", xDistance, yDistance, zDistance);
        Vector3 midPt = new Vector3(startPt.x + (0.5f * (endPt.x - startPt.x)),
                                    startPt.y + (0.5f * (endPt.y - startPt.y)),
                                    startPt.z + (0.5f * (endPt.z - startPt.z)));
        
        GameObject distTextGameObj = new GameObject();
        distTextGameObj.name = "DimTextObj";
        distTextGameObj.transform.position = midPt;
        distTextGameObj.transform.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.down) * (endPt-startPt));
        distTextGameObj.transform.SetParent(transform);
        distTextGameObj.AddComponent<TextMesh>();
        TextMesh newTextInstance = distTextGameObj.GetComponent<TextMesh>();
        newTextInstance.text = distanceText;
        newTextInstance.characterSize = 0.1f;
        newTextInstance.color = Color.black;
		newTextInstance.alignment = TextAlignment.Center;

        GameObject deltaDistTextGameObj = new GameObject();
        deltaDistTextGameObj.name = "DeltaDimTextObj";
        deltaDistTextGameObj.transform.position = midPt;
        deltaDistTextGameObj.transform.Translate(new Vector3(0, -0.15f, 0.1f));
        deltaDistTextGameObj.transform.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(90, Vector3.down) * (endPt-startPt));
        deltaDistTextGameObj.transform.SetParent(transform);
        deltaDistTextGameObj.AddComponent<TextMesh>();
        newTextInstance = deltaDistTextGameObj.GetComponent<TextMesh>();
        newTextInstance.text = deltaText;
        newTextInstance.characterSize = 0.05f;
        newTextInstance.color = Color.black;
        newTextInstance.alignment = TextAlignment.Center;
    }



    // Use this for initialization
    void Start () {
        tickCount = getChildTickCount();
        gameObject.AddComponent<LineRenderer>();
        dimensionLineRenderer = gameObject.GetComponent<LineRenderer>();
        dimensionLineRenderer.numPositions = 0;
        dimensionLineRenderer.receiveShadows = false;
        dimensionLineRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        dimensionLineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        dimensionLineRenderer.startWidth = 0.01f;
        dimensionLineRenderer.endWidth = 0.01f;
        dimensionLineRenderer.material = dimensionLineMaterial;
        update = false;
    }



	
	// Update is called once per frame
	void Update () {


        if (!update)
        {
            return;
        }


        tickCount = getChildTickCount();


        if (tickCount == 2)
        {
            createFixedDimensionLine();
        }


        update = false;


    }
    
}
