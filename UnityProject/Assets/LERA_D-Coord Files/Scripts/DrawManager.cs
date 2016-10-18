using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour {


    /*

    [Range(0.001f, 2f)]
    public float lineWidth = 0.02f;

    public Color blackStrokeColor = Color.black;
    public Color errorStrokeColor = Color.red;
    public Color suggestionStrokeColor = Color.green;
    public Color commentStrokeColor = Color.blue;



    ////////////////////////////////////////////////////////////////////////////



    private LaserPicker laser;


    private GameObject drawingSet;
    private GameObject currentSketch;
    private string sketchObjectName = "SketchObject";
    private int sketchCount = 0;


    private GameObject currentStrokeObj;
    private LineRenderer currentStroke;
    private string strokeObjectName = "StrokeObject";
    private int strokeCount = 0;


    private GameObject currentStrokeDisplayBuffer;
    private LineRenderer currentStrokeDisplayBufferLineRenderer;
    List<Vector3> currPoints;


    private Material blackLineMaterial;
    private Material errorLineMaterial;
    private Material suggestionLineMaterial;
    private Material commentLineMaterial;
    private Material[] lineMaterialArray;
    private int currentMaterialIndex = 0;
    private Material blackBufferLineMaterial;
    private Material errorBufferLineMaterial;
    private Material suggestionBufferLineMaterial;
    private Material commentBufferLineMaterial;
    private Material[] lineBufferMaterialArray;
    private float bufferColorTint = 0.4f;

    private Image UIColorHighlight;



    ////////////////////////////////////////////////////////////////////////////



    private string getVecArrayString(Vector3[] inputArray)
    {
        string output = "";
        for (int i = 0; i < inputArray.Length; i++)
        {
            output += inputArray[i].ToString().Replace("(", "").Replace(")", "").Replace(" ", "");
        }
        return output;
    }




    private string getRGBString(Color inputColor)
    {
        return (System.String.Format("{0}-{1}-{2}",
                                      Mathf.RoundToInt(inputColor.r * 255).ToString(),
                                      Mathf.RoundToInt(inputColor.g * 255).ToString(),
                                      Mathf.RoundToInt(inputColor.b * 255).ToString()));
    }




    public string getExportString()
    {
        string output = "";
        string header = "Sketch Name, Stroke Name, Colour, No of Points";
        string seperator = ",";
        output += header;
        output += System.Environment.NewLine;
        GameObject currSketchInProcess;
        GameObject currStrokeInProcess;
        for(int i=1; i < drawingSet.transform.childCount; i++)
        {
            currSketchInProcess = drawingSet.transform.GetChild(i).gameObject;
            for (int j = 0; j < currSketchInProcess.transform.childCount; j++)
            {
                currStrokeInProcess = currSketchInProcess.transform.GetChild(j).gameObject;
                output += currSketchInProcess.name;
                output += seperator;
                output += currStrokeInProcess.name;
                output += seperator;
                output += getRGBString(currStrokeInProcess.GetComponent<LineRenderer>().material.color);
                output += seperator;
                output += currStrokeInProcess.GetComponent<StrokeArray>().strokePoints.Length;
                output += seperator;
                output += getVecArrayString(currStrokeInProcess.GetComponent<StrokeArray>().strokePoints);
                output += System.Environment.NewLine;
            }
        }
        return output;
    }



    ////////////////////////////////////////////////////////////////////////////



	// stroke buffer is used to hold the renderer for the stroke which the user is in process of creating
	// when the stroke is completed its is flushed to a new stroke game object which gets nested under the currently
	// active sketch
    private void setupStrokeBuffer()
    {
        currentStrokeDisplayBuffer = new GameObject();
        currentStrokeDisplayBuffer.name = "StrokesBuffer";
        currentStrokeDisplayBuffer.transform.parent = drawingSet.transform;
        currentStrokeDisplayBuffer.AddComponent<LineRenderer>();
        currentStrokeDisplayBufferLineRenderer = currentStrokeDisplayBuffer.GetComponent<LineRenderer>();
        currentStrokeDisplayBufferLineRenderer.receiveShadows = false;
        currentStrokeDisplayBufferLineRenderer.useLightProbes = false;
        currentStrokeDisplayBufferLineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        currentStrokeDisplayBufferLineRenderer.SetWidth(lineWidth, lineWidth);
        // start with an empty buffer, overriding the default of 2 points in a line renderer
        currentStrokeDisplayBufferLineRenderer.SetVertexCount(0);
    }




    private void setupLineMaterials()
    {
        lineMaterialArray = new Material[4];

        blackLineMaterial = new Material(Shader.Find("Sprites/Default"));
        blackLineMaterial.SetColor("_Color", blackStrokeColor);
        lineMaterialArray[0] = blackLineMaterial;

        errorLineMaterial = new Material(Shader.Find("Sprites/Default"));
        errorLineMaterial.SetColor("_Color", errorStrokeColor);
        lineMaterialArray[1] = errorLineMaterial;

        suggestionLineMaterial = new Material(Shader.Find("Sprites/Default"));
        suggestionLineMaterial.SetColor("_Color", suggestionStrokeColor);
        lineMaterialArray[2] = suggestionLineMaterial;

        commentLineMaterial = new Material(Shader.Find("Sprites/Default"));
        commentLineMaterial.SetColor("_Color", commentStrokeColor);
        lineMaterialArray[3] = commentLineMaterial;



		// buffer lines stroke materials are created by applying a slight tint to the colors selected for the actual strokes
        lineBufferMaterialArray = new Material[4];

        blackBufferLineMaterial = new Material(Shader.Find("Sprites/Default"));
        blackBufferLineMaterial.SetColor("_Color", new Color(blackStrokeColor.r + (1 - blackStrokeColor.r) * bufferColorTint * 2f,
                                                             blackStrokeColor.g + (1 - blackStrokeColor.r) * bufferColorTint * 2f,
                                                             blackStrokeColor.b + (1 - blackStrokeColor.r) * bufferColorTint * 2f));
        lineBufferMaterialArray[0] = blackBufferLineMaterial;

        errorBufferLineMaterial = new Material(Shader.Find("Sprites/Default"));
        errorBufferLineMaterial.SetColor("_Color", new Color(errorStrokeColor.r + (1 - blackStrokeColor.r) * bufferColorTint,
                                                             errorStrokeColor.g + (1 - blackStrokeColor.r) * bufferColorTint,
                                                             errorStrokeColor.b + (1 - blackStrokeColor.r) * bufferColorTint));
        lineBufferMaterialArray[1] = errorBufferLineMaterial;

        suggestionBufferLineMaterial = new Material(Shader.Find("Sprites/Default"));
        suggestionBufferLineMaterial.SetColor("_Color", new Color(suggestionStrokeColor.r + (1 - blackStrokeColor.r) * bufferColorTint,
                                                                  suggestionStrokeColor.g + (1 - blackStrokeColor.r) * bufferColorTint,
                                                                  suggestionStrokeColor.b + (1 - blackStrokeColor.r) * bufferColorTint));
        lineBufferMaterialArray[2] = suggestionBufferLineMaterial;

        commentBufferLineMaterial = new Material(Shader.Find("Sprites/Default"));
        commentBufferLineMaterial.SetColor("_Color", new Color(commentStrokeColor.r + (1 - blackStrokeColor.r) * bufferColorTint,
                                                               commentStrokeColor.g + (1 - blackStrokeColor.r) * bufferColorTint,
                                                               commentStrokeColor.b + (1 - blackStrokeColor.r) * bufferColorTint));
        lineBufferMaterialArray[3] = commentBufferLineMaterial;
    }




	private void attachCurrentSketchToActiveTag()
	{
		GeneralSettings.currentlyActiveTagObject.transform.GetChild(0).gameObject.GetComponent<TagInfo>().attachItem(currentSketch.name);
	}




    private void createNewSketch()
	{
        GameObject newSketchInstance = new GameObject();
        newSketchInstance.transform.parent = drawingSet.transform;
        newSketchInstance.name = sketchObjectName + "_" + sketchCount;
		currentSketch = newSketchInstance;
        sketchCount++;
		strokeCount = 0;
    }




    private void createStroke()
    {
        currentStrokeObj = new GameObject();
        currentStrokeObj.name = strokeObjectName + "_" + (sketchCount - 1) + "_" + strokeCount;
        strokeCount++;
        currentStrokeObj.transform.parent = currentSketch.transform;
        currentStrokeObj.AddComponent<LineRenderer>();
        currentStrokeObj.AddComponent<StrokeArray>();
        currentStroke = currentStrokeObj.GetComponent<LineRenderer>();
        currentStroke.receiveShadows = false;
        currentStroke.useLightProbes = false;
        currentStroke.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        currentStroke.SetWidth(lineWidth, lineWidth);
        currentStroke.material = lineMaterialArray[currentMaterialIndex];
        // start with an empty stroke, overriding the default of 2 points in a line renderer
        currentStroke.SetVertexCount(0);
        currentStrokeDisplayBufferLineRenderer.material = lineBufferMaterialArray[currentMaterialIndex];
    }



	// strokes are updated in the buffer only for display
    private void updateStroke()
    {
        currPoints.Add(laser.getHitPoint());
        Vector3[] currPtsArray = currPoints.ToArray();
        currentStrokeDisplayBufferLineRenderer.SetVertexCount(currPtsArray.Length);
        currentStrokeDisplayBufferLineRenderer.SetPositions(currPtsArray);
    }




    private void emptyStrokeBuffer()
	{
		// dont empty the buffer if its empty
		if (currentSketch != null && currentSketch.transform.childCount == 0) return;
		// when the stroke is completed add it to currentStroke object and empty the strokeBuffer
        Vector3[] overridingArray = currPoints.ToArray();
        currentStrokeObj.GetComponent<StrokeArray>().strokePoints = overridingArray;
        currentStroke.SetVertexCount(overridingArray.Length);
        currentStroke.SetPositions(overridingArray);
        currPoints.Clear();
        currentStrokeDisplayBufferLineRenderer.SetVertexCount(0);
        GeneralSettings.addLineToConsole(System.String.Format("New stroke added to current sketch: {0}", currentStroke.name));
    }



	// deletes the last stroke object irretrievably
    private void undoStroke()
    {
        if (drawingSet.transform.childCount == 0)
            return;
        GameObject lastSketchObj = drawingSet.transform.GetChild(drawingSet.transform.childCount - 1).gameObject;
        if (lastSketchObj.transform.childCount == 0)
            return;
        GameObject lastStrokeObj = lastSketchObj.transform.GetChild(lastSketchObj.transform.childCount - 1).gameObject;
        Destroy(lastStrokeObj);
    }




    private void nextLineColor()
    {
        currentMaterialIndex++;
        currentMaterialIndex %= lineMaterialArray.Length;
        UIColorHighlight.color = lineMaterialArray[currentMaterialIndex].color;
    }




    private void prevLineColor()
    {
        currentMaterialIndex--;
        if (currentMaterialIndex < 0) currentMaterialIndex = lineMaterialArray.Length-1;
        UIColorHighlight.color = lineMaterialArray[currentMaterialIndex].color;
    }




    ////////////////////////////////////////////////////////////////////////////




    // Use this for initialization
    void Start ()
    {
        laser = transform.FindChild("LaserPicker").GetComponent<LaserPicker>();
        drawingSet = GeneralSettings.drawingSetManager;
        currPoints = new List<Vector3>();
        setupLineMaterials();
        setupStrokeBuffer();
		createNewSketch();
        UIColorHighlight = transform.FindChild("Canvas").FindChild("Image").GetComponent<Image>();
        UIColorHighlight.color = lineMaterialArray[currentMaterialIndex].color;
    }



	
	// Update is called once per frame
	void Update ()
    {
        

        if (WandControlsManager.WandControllerRight.getTouchPadButtonDown())
        {
            nextLineColor();
        }




        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            createStroke();
        }

        if (WandControlsManager.WandControllerRight.getTriggerPressed())
        {
            updateStroke();
        }

        if (WandControlsManager.WandControllerRight.getTriggerUp())
        {
            emptyStrokeBuffer();
        }



        if (WandControlsManager.WandControllerRight.getTouchPadSwipeDown())
        {
            laser.inAir = true;
            laser.maxLength = 0.1f;
        }



        if (WandControlsManager.WandControllerRight.getTouchPadSwipeUp())
        {
            laser.inAir = false;
            laser.maxLength = 10000f;
        }




        if (WandControlsManager.WandControllerRight.getGripDown())
        {
            attachCurrentSketchToActiveTag();
            GeneralSettings.addLineToConsole(System.String.Format("Sketch saved: {0}", currentSketch.name));
			// theres always a sketch waiting for strokes
			createNewSketch();
            GeneralSettings.resetActiveTagAndSwitchToTagManager();
        }

            
    }

    */
}
