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
        if (laser.isHit())
        {
            if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                GameObject currentPoint = (GameObject)Instantiate(app_Point, laser.getHitPoint(), Quaternion.identity);
                currentPoint.transform.SetParent(GeneralSettings.modelObjects.transform);
            }
        }
    }


}
