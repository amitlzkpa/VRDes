using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallActionManager : MonoBehaviour, ActionManager {

    public GameObject pointMarker;
    public GameObject app_Wall;

    private GameObject hostSurface;
    private List<GameObject> selectedPtObjects;
    private GameObject lastPt;


    public void amStart(LaserPicker laser)
    {
        laser.setRestrictedObjectStartName("app_Plane");
        selectedPtObjects = new List<GameObject>();
        hostSurface = null;
        GeneralSettings.updateInteractText("Please select a plane to act as host for the wall.");
    }

    public void amUpdate(LaserPicker laser)
    {
        if (hostSurface != null)
        {
            if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                if (laser.isHit())
                {
                    Vector3 pt = laser.getHitPoint();
                    lastPt = Instantiate(pointMarker, pt, Quaternion.identity);
                    selectedPtObjects.Add(lastPt);


                    if (selectedPtObjects.Count == 4)
                    {
                        List<Vector3> pts = new List<Vector3>();
                        foreach (GameObject ptObj in selectedPtObjects)
                        {
                            pts.Add(ptObj.transform.position);
                        }

                        GameObject newWall = Instantiate(app_Wall, laser.getHitPoint(), Quaternion.identity);
                        newWall.GetComponent<app_Wall>().init(pts);
                        hostSurface.GetComponent<app_Plane>().enhostObject(newWall);

                        GeneralSettings.updateInteractText("");
                        GeneralSettings.addLineToConsole(string.Format("{0} wall created under {1} host plane.", newWall.name, hostSurface.name));
                        clearPointCollectionMode(laser);
                        return;
                    }
                    GeneralSettings.updateInteractText(string.Format("Pick {0} more points to complete the wall.", 4 - selectedPtObjects.Count));
                }
            }

            if (WandControlsManager.WandControllerRight.getGripDown())
            {
                clearPointCollectionMode(laser);
                GeneralSettings.addLineToConsole("Wall creation exited.");
                GeneralSettings.updateInteractText("Please select a plane to act as host for the wall.");
            }

            return;
        }

        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            hostSurface = laser.getHitObject();
            laser.setRestrictedObject(hostSurface);
            GeneralSettings.updateInteractText("Select 4 corner points for wall.");
            GeneralSettings.addLineToConsole(string.Format("{0} selected as host plane for wall to be created.", hostSurface.name));
        }
    }



    private void clearPointCollectionMode(LaserPicker laser)
    {
        clearCollectedPts();
        hostSurface = null;
        laser.clearRestrictedObject();
    }



    private void clearCollectedPts()
    {
        foreach (GameObject ptObj in selectedPtObjects)
        {
            Destroy(ptObj);
        }
        selectedPtObjects.Clear();
    }






}
