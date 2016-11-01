using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionSwitcher : MonoBehaviour
{

    private GameObject laserObj;
    private LaserPicker laser;

    private Creator cr;
    private GameObject amObj;
    private ActionManager am;
    private GameObject cmObj;
    private ContextMenuManager cm;


    public void setActionItem(GameObject crObj)
    {
        // setting to selection ray
        if (crObj == null)
        {
            amObj = null;
            am = null;
            cmObj = null;
            cm = null;
            return;
        }

        cr = crObj.GetComponent<Creator>();
        if (cr == null)
        {
            Debug.LogError("Creator item must have Creator(interface) script attached.");
            return;
        }

        amObj = cr.getActionObject();
        am = cr.getActionManager();
        cmObj = (GameObject) Instantiate(cr.getMenuObject(), Vector3.zero, Quaternion.identity);
        cm = cmObj.GetComponent<ContextMenuManager>();
        GeneralSettings.setObjectMenu(cmObj);


        cr.setupLaser(laser);

        Start();
    }



    public GameObject getActionItem()
    {
        return amObj;
    }


    //---------------------------------------------------------------


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


    //---------------------------------------------------------------


    GameObject hitObj;


    void Update()
    {


        if (laser.isHit())
        {
            hitObj = laser.getHitObject();
            if (GeneralSettings.getParentRecursive(hitObj.transform.gameObject, "_Objects", "_Model") != null)
            {
                GeneralSettings.getParentClone(hitObj, "app_").GetComponent<Highlightable>().highlightObject();
            }
        }


        // actions in selection mode
        if (am == null)
        {
            selActionMethods();
            return;
        }


        am.amUpdate(laser);
        cm.cmUpdate(laser);
    }


    //---------------------------------------------------------------


    private float laserEditStartLen;
    private bool keepMoving;


    private void selActionMethods()
    {

        if (GeneralSettings.editOn())
        {
            if (laser.isHit())
            {
                if (WandControlsManager.WandControllerRight.getTriggerDown())
                {
                    laserEditStartLen = laser.getTerminalDistance();
                    keepMoving = true;
                }
            }
            if (WandControlsManager.WandControllerRight.getTriggerUp())
            {
                keepMoving = false;
            }
            if (keepMoving)
            {
                GeneralSettings.getEditObject().transform.position = (laser.getDirection() * laserEditStartLen) + laser.getStartPoint();
            }
        }
    }


}