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


    //---------------------------------------------------------------


    public void setActionItem(GameObject crObj)
    {
        // setting to selection ray; null used to represent selection ray
        if (crObj == null)
        {
            amObj = null;
            am = null;
            cmObj = null;
            cm = null;
            laser.setLength(1000000f);
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
        // Context manager prefabs to be instantiated, as all context menus defined are destroyed on end of use
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


    private GameObject hitObj;

    void Update()
    {
        // highlight object being edited if edit mode is on or the pointed object
        hitObj = GeneralSettings.editOn() ? GeneralSettings.getEditObject() : GeneralSettings.getParentClone(laser.getHitObject(), "app_");
        if (hitObj != null)
        {
            hitObj.GetComponent<Highlightable>().highlightObject();
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

    private GameObject prevHitObj;
    private GameObject currHitObj;


    private void selActionMethods()
    {
        // if the hit object is a prefab
        currHitObj = hitObj;
        // and is not pointing to the same prefab as it was in the last frame
        if (currHitObj != prevHitObj)
        {
            // collapse the edit menu on the object it was previously pointing
            if (prevHitObj != null) prevHitObj.GetComponent<HighlightStyle1>().hideObjectMenu();
            // expand the edit menu for current object
            if (currHitObj != null) currHitObj.GetComponent<HighlightStyle1>().displayObjectMenu();
        }
        prevHitObj = currHitObj;




        if (laser.isHit())
        {
            if (GeneralSettings.editOn())
            {
                if (WandControlsManager.WandControllerRight.getTriggerDown())
                {
                    // set laser length equal to gap between laser and the center of the object highlighted
                    laserEditStartLen = Vector3.Distance(laser.getHitObject().transform.position, laser.getStartPoint());
                    keepMoving = true;
                }
            }
            if (WandControlsManager.WandControllerRight.getTriggerUp())
            {
                keepMoving = false;
            }
        }
        if (keepMoving)
        {
            GeneralSettings.getEditObject().transform.position = (laser.getDirection() * laserEditStartLen) + laser.getStartPoint();
        }
    }


}