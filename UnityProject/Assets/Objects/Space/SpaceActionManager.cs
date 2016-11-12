using UnityEngine;
using System.Collections;

public class SpaceActionManager : MonoBehaviour, ActionManager {


    public GameObject app_Space;


    public void amStart(LaserPicker laser)
    {
    }

    public void amUpdate(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            Vector3 centerPt = laser.getTerminalPoint();
            Vector3 normal = laser.getTerminalNormal();
            GameObject currentSpace = Instantiate(app_Space, centerPt, Quaternion.LookRotation(normal));
            currentSpace.transform.SetParent(GeneralSettings.getActiveLayerObject().transform);
        }
    }
}
