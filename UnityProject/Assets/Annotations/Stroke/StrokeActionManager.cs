using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeActionManager : MonoBehaviour, ActionManager
{


    public GameObject strokeMenuObject;
    private StrokeMenuManager strokeMenuManager;

    public float lineWidth = 0.02f;

    public Color blackStrokeColor = Color.black;
    public Color errorStrokeColor = Color.red;
    public Color suggestionStrokeColor = Color.green;
    public Color commentStrokeColor = Color.blue;



    //---------------------------------------------------------------


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



    //---------------------------------------------------------------



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
        currentStrokeDisplayBufferLineRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        currentStrokeDisplayBufferLineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        currentStrokeDisplayBufferLineRenderer.startWidth = lineWidth;
        currentStrokeDisplayBufferLineRenderer.endWidth = lineWidth;
        // start with an empty buffer, overriding the default of 2 points in a line renderer
        currentStrokeDisplayBufferLineRenderer.numPositions = 0;
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
        currentStroke.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        currentStroke.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        currentStroke.startWidth = lineWidth;
        currentStroke.endWidth = lineWidth;
        currentStroke.material = lineMaterialArray[currentMaterialIndex];
        // start with an empty stroke, overriding the default of 2 points in a line renderer
        currentStroke.numPositions = 0;
        currentStrokeDisplayBufferLineRenderer.material = lineBufferMaterialArray[currentMaterialIndex];
    }



    // strokes are updated in the buffer only for display
    private void updateStroke(LaserPicker laser)
    {
        currPoints.Add(laser.getTerminalPoint());
        Vector3[] currPtsArray = currPoints.ToArray();
        currentStrokeDisplayBufferLineRenderer.numPositions = currPtsArray.Length;
        currentStrokeDisplayBufferLineRenderer.SetPositions(currPtsArray);
    }




    private void emptyStrokeBuffer()
    {
        // dont empty the buffer if its empty
        if (currentSketch != null && currentSketch.transform.childCount == 0) return;
        // when the stroke is completed add it to currentStroke object and empty the strokeBuffer
        Vector3[] overridingArray = currPoints.ToArray();
        currentStrokeObj.GetComponent<StrokeArray>().strokePoints = overridingArray;
        currentStroke.numPositions = overridingArray.Length;
        currentStroke.SetPositions(overridingArray);
        currPoints.Clear();
        currentStrokeDisplayBufferLineRenderer.numPositions = 0;
        GeneralSettings.addLineToConsole(System.String.Format("New stroke added to current sketch: {0}", currentStroke.name));
    }



    //---------------------------------------------------------------




    public void nextLineColor()
    {
        currentMaterialIndex++;
        currentMaterialIndex %= lineMaterialArray.Length;
        strokeMenuManager.setUIColor(lineMaterialArray[currentMaterialIndex].color);
    }


    public void setLineThickness(float inpWidth)
    {
        lineWidth = inpWidth;
    }



    //---------------------------------------------------------------



    public void amStart(LaserPicker laser)
    {
        strokeMenuManager = strokeMenuObject.GetComponent<StrokeMenuManager>();
        drawingSet = GeneralSettings.model.transform.FindChild("_Sets").FindChild("_DrawingSetManager").gameObject;
        currPoints = new List<Vector3>();
        setupLineMaterials();
        setupStrokeBuffer();
        createNewSketch();
        strokeMenuManager.setUIColor(lineMaterialArray[currentMaterialIndex].color);
    }



    public void amUpdate(LaserPicker laser)
    {


        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            createStroke();
        }

        if (WandControlsManager.WandControllerRight.getTriggerPressed())
        {
            updateStroke(laser);
        }

        if (WandControlsManager.WandControllerRight.getTriggerUp())
        {
            emptyStrokeBuffer();
        }



    }
}
