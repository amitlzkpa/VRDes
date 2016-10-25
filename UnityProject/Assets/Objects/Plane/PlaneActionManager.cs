using UnityEngine;
using System.Collections;

public class PlaneActionManager : MonoBehaviour, ActionManager {


    public GameObject app_Plane;


    public void amStart(LaserPicker laser)
    {
    }

    public void amUpdate(LaserPicker laser)
    {
        if (laser.isHit())
        {
            if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                GameObject currentPoint = (GameObject)Instantiate(app_Plane, laser.getHitPoint(), Quaternion.LookRotation(laser.getHitNormal()));
                currentPoint.transform.SetParent(GeneralSettings.modelObjects.transform);
            }
        }
    }
}
