using UnityEngine;
using System.Collections;

public class PlaneActionManager : MonoBehaviour, ActionManager {


    public GameObject app_Plane;


    public void amStart(LaserPicker laser)
    {
    }

    public void amUpdate(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
                Vector3 centerPt = laser.getTerminalPoint();
                Vector3 normal = laser.getTerminalNormal();
                GameObject currentPlane = (GameObject)Instantiate(app_Plane, centerPt, Quaternion.LookRotation(normal));
                currentPlane.transform.SetParent(GeneralSettings.modelObjects.transform);
            }
        }
    }
}
