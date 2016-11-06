using UnityEngine;
using System.Collections;
using System;

public class PointActionManager : MonoBehaviour, ActionManager {


    public GameObject app_Point;


    public void amStart(LaserPicker laser)
    {
    }

    public void amUpdate(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            GameObject currentPoint = Instantiate(app_Point, laser.getTerminalPoint(), Quaternion.LookRotation(laser.getHitNormal()));
            currentPoint.transform.SetParent(GeneralSettings.getActiveLayerObject().transform);
        }
    }


}
