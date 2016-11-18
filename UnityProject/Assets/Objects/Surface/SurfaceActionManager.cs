using UnityEngine;
using System.Collections;

public class SurfaceActionManager : MonoBehaviour, ActionManager {


    private GameObject hostSurface;


    public void amStart(LaserPicker laser)
    {
        laser.setRestrictedObjectStartName("app_Plane");
    }

    public void amUpdate(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            hostSurface = laser.getHitObject();
            laser.setRestrictedObject(hostSurface);
        }
    }
}
