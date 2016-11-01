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
        // setting to selection ray
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

    private GameObject prevHitObj;
    private GameObject currHitObj;


    private void selActionMethods()
    {



        // CONTINUE HERE-----------------------------------------------------------------------------------------------------------
        if (laser.isHit())
        {
            if (GeneralSettings.getParentRecursive(laser.getHitObject().transform.gameObject, "_Objects", "_Model") != null)
            {
                currHitObj = GeneralSettings.getParentClone(hitObj, "app_");
                if (currHitObj != prevHitObj)
                {
                    if (prevHitObj != null) prevHitObj.GetComponent<HighlightStyle1>().hideObjectMenu();
                    if (currHitObj != null) currHitObj.GetComponent<HighlightStyle1>().displayObjectMenu();
                }
                prevHitObj = currHitObj;
            }
        }




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
            if (keepMoving)
            {
                GeneralSettings.getEditObject().transform.position = (laser.getDirection() * laserEditStartLen) + laser.getStartPoint();
            }
        }
    }


}