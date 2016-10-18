using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionSwitcher : MonoBehaviour
{

    public GameObject ui1;
    public GameObject ui2;



    private GameObject laserObj;
    private LaserPicker laser;

    private Creator cr;
    private GameObject amObj;
    private ActionManager am;
    private GameObject cmObj;
    private ContextMenuManager cm;


    public void setActionItem(GameObject crObj)
    {
        cr = crObj.GetComponent<Creator>();
        if (cr == null)
        {
            Debug.LogError("Creator item must have Creator(interface) script attached.");
            return;
        }

        amObj = cr.getActionObject();
        am = cr.getActionManager();
        cmObj = cr.getMenuObject();
        cm = cr.getMenuManager();

        cr.setupLaser(laser);

        Start();
    }





    void Awake()
    {
        laserObj = transform.parent.FindChild("_LaserPicker").gameObject;
        laser = laserObj.GetComponent<LaserPicker>();
    }



    void Start()
    {

        if (am == null) return;


        am.amStart(laser);
        cm.cmStart(laser);
    }


    GameObject hitObj;

    void Update()
    {
        if (WandControlsManager.WandControllerRight.getGripDown())
        {
            GameObject currUI = (GameObject) Instantiate(ui1, transform.position, transform.rotation * Quaternion.Euler(60, 0, 0));
            currUI.transform.SetParent(transform.parent.FindChild("_ContextCanvas"));
        }
        if (WandControlsManager.WandControllerRight.getTouchPadButtonDown())
        {
            GameObject currUI = (GameObject)Instantiate(ui2, transform.position, transform.rotation * Quaternion.Euler(60, 0, 0));
            currUI.transform.SetParent(transform.parent.FindChild("_ContextCanvas"));
        }


        if (laser.isHit())
        {
            hitObj = laser.getHitObject();
            if (GeneralSettings.getParentRecursive(hitObj.transform.gameObject, "_Objects", "_Model") != null)
            {
                GeneralSettings.getParentClone(hitObj, "app_").GetComponent<Highlightable>().highlightObject();
            }
        }

        if (am == null) return;


        am.amUpdate(laser);
        cm.cmUpdate(laser);
    }


}