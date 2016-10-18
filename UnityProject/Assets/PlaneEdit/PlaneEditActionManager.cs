using UnityEngine;
using System.Collections;
using System;

public class PlaneEditActionManager : MonoBehaviour, ActionManager {
    

    public void amStart(LaserPicker laser)
    {
    }

    public void amUpdate(LaserPicker laser)
    {
        if (laser.isHit())
        {
            if (WandControlsManager.WandControllerRight.getTriggerPressed())
            {
                if (GeneralSettings.getParentRecursive(laser.getHitObject(), "_RefObjects", "app_") != null)
                {
                    // move the current refObject along the plane contained as a reference among the siblings
                }
            }
        }
    }
}
