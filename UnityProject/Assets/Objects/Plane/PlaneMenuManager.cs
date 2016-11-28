using UnityEngine;
using System.Collections;

public class PlaneMenuManager : MonoBehaviour, ContextMenuManager
{


    public GameObject actionManagerObj;
    private PlaneActionManager actionManager;


    private LaserPicker l;


    public void setupForInAir()
    {
        l.setLength(3f);
    }


    public void setupForSurface()
    {
        l.setLengthToInfinity();
    }


    public void setupForDefByPt()
    {
        actionManager.switchDefMode(PlaneDefMode.BYPOINT);
    }


    public void setupForDefByCorners()
    {
        actionManager.switchDefMode(PlaneDefMode.BYCORNERS);
    }





    public void cmStart(LaserPicker laser)
    {
        l = laser;
        actionManager = actionManagerObj.GetComponent<PlaneActionManager>();
    }

    public void cmUpdate(LaserPicker laser)
    {
    }
}
