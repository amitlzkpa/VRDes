using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurfaceActionManager : MonoBehaviour, ActionManager {

    public GameObject pointMarker;
    public GameObject app_Surface;

    private GameObject hostSurface;
    private List<GameObject> selectedPtObjects;
    private GameObject lastPt;

    private float offset = 0.0001f;


    public void amStart(LaserPicker laser)
    {
        laser.setRestrictedObjectStartName("app_Plane");
        selectedPtObjects = new List<GameObject>();
        hostSurface = null;
    }

    public void amUpdate(LaserPicker laser)
    {
        if (hostSurface != null)
        {
            if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                if (laser.isHit())
                {
                    Vector3 pt = laser.getHitPoint() + (offset * laser.getHitNormal());
                    lastPt = Instantiate(pointMarker, pt, Quaternion.identity);
                    selectedPtObjects.Add(lastPt);


                    if (selectedPtObjects.Count == 4)
                    {
                        List<Vector3> pts = new List<Vector3>();
                        foreach (GameObject ptObj in selectedPtObjects)
                        {
                            pts.Add(ptObj.transform.position);
                        }

                        GameObject newSrf = Instantiate(app_Surface, laser.getHitPoint(), Quaternion.identity);
                        newSrf.GetComponent<app_Surface>().init(pts);
                        hostSurface.GetComponent<app_Plane>().enhostObject(newSrf);

                        clearPointCollectionMode(laser);
                        return;
                    }
                }
            }

            if (WandControlsManager.WandControllerRight.getGripDown())
            {
                clearPointCollectionMode(laser);
            }

            return;
        }

        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            hostSurface = laser.getHitObject();
            laser.setRestrictedObject(hostSurface);
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
