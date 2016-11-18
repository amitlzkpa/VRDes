using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class LineActionManager : MonoBehaviour, ActionManager {


    public GameObject app_Line;


    List<Vector3> pts;
    LineRenderer lineRenderer;



    public void amStart(LaserPicker laser)
    {
        pts = new List<Vector3>();
        lineRenderer = GeneralSettings.model.GetComponent<LineRenderer>();
    }


    public void amUpdate(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            pts.Add(laser.getTerminalPoint());
            refreshLineRenderer();
        }

        if (WandControlsManager.WandControllerRight.getGripDown())
        {
            Vector3 center = getCenter();
            GameObject g = Instantiate(app_Line, center, Quaternion.identity, GeneralSettings.modelObjects.transform);
            g.GetComponent<app_Line>().setLinePts(pts);
            pts.Clear();
            clearLineRenderer();
        }
    }



    private Vector3 getCenter()
    {
        Vector3 superVector = Vector3.zero;
        foreach(Vector3 v in pts)
        {
            superVector += v;
        }
        return superVector / pts.Count;
    }



    private void refreshLineRenderer()
    {
        lineRenderer.numPositions = pts.Count;
        lineRenderer.SetPositions(pts.ToArray());
    }


    private void clearLineRenderer()
    {
        lineRenderer.numPositions = 0;
    }

}
