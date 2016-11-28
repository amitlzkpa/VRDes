using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum PlaneDefMode
{
    BYPOINT, BYCORNERS
}

public class PlaneActionManager : MonoBehaviour, ActionManager {


    private PlaneDefMode activeDefMode = PlaneDefMode.BYPOINT;
    private List<Vector3> collectedPts;


    //---------------------------------------------------------------


    public GameObject app_Plane;


    public void amStart(LaserPicker laser)
    {
        collectedPts = new List<Vector3>();
    }


    public void amUpdate(LaserPicker laser)
    {
        switch (activeDefMode)
        {
            case PlaneDefMode.BYPOINT:
                {
                    defByPoint(laser);
                    break;
                }
            case PlaneDefMode.BYCORNERS:
                {
                    defByCorners(laser);
                    break;
                }
        }
    }


    //---------------------------------------------------------------



    private float halfWidth = 3f;
    private float halfHeight = 2f;



    private void defByPoint(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            Vector3 centerPt = laser.getTerminalPoint();
            Vector3 normal = laser.getTerminalNormal();
            collectedPts = getPtsForPt(centerPt, normal);
            GameObject currentPlane = Instantiate(app_Plane, centerPt, Quaternion.LookRotation(normal));
            currentPlane.GetComponent<app_Plane>().init(collectedPts);
            currentPlane.transform.SetParent(GeneralSettings.getActiveLayerObject().transform);
        }
    }


    private void defByCorners(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            collectedPts.Add(laser.getTerminalPoint());
            if (collectedPts.Count == 4)
            {
                makePlane(laser);
                collectedPts.Clear();
            }
        }
    }


    private void makePlane(LaserPicker laser)
    {
        Vector3 centerPt = laser.getTerminalPoint();
        Vector3 normal = laser.getTerminalNormal();
        GameObject currentPlane = Instantiate(app_Plane, centerPt, Quaternion.LookRotation(normal));
        currentPlane.GetComponent<app_Plane>().init(collectedPts);
        currentPlane.transform.SetParent(GeneralSettings.getActiveLayerObject().transform);
    }


    private List<Vector3> getPtsForPt(Vector3 c, Vector3 n)
    {
        List<Vector3> retList = new List<Vector3>();

        GameObject empty = new GameObject();
        empty.transform.position = Vector3.zero;
        empty.transform.up = Vector3.up;

        GameObject dummy = new GameObject();

        GameObject lT = Instantiate(dummy, new Vector3(-halfWidth, 0, halfHeight), Quaternion.identity, empty.transform);
        GameObject lB = Instantiate(dummy, new Vector3(-halfWidth, 0, -halfHeight), Quaternion.identity, empty.transform);
        GameObject rB = Instantiate(dummy, new Vector3(halfWidth, 0, -halfHeight), Quaternion.identity, empty.transform);
        GameObject rT = Instantiate(dummy, new Vector3(halfWidth, 0, halfHeight), Quaternion.identity, empty.transform);

        empty.transform.position = c;
        empty.transform.up = n;

        retList.Add(lT.transform.position);
        retList.Add(lB.transform.position);
        retList.Add(rB.transform.position);
        retList.Add(rT.transform.position);

        Destroy(dummy);
        Destroy(empty);

        return retList;
    }


    //---------------------------------------------------------------


    public void switchDefMode(PlaneDefMode m)
    {
        activeDefMode = m;
        collectedPts.Clear();
    }


}
