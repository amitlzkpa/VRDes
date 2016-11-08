﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;



enum SelectMoveType
{
    MoveRef, MoveGiz, MoveObj, None
}



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


    private GameObject hitAppObj;

    void Update()
    {
        // highlight object being edited if edit mode is on or the pointed object
        hitAppObj = GeneralSettings.editOn() ? GeneralSettings.getEditObject() : GeneralSettings.getParentClone(laser.getHitObject(), "app_");
        if (hitAppObj != null)
        {
            hitAppObj.GetComponent<Highlightable>().highlightObject();
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

    private SelectMoveType activeMoveType;
    
    private float laserEditStartLen;
    private GameObject objToMove;
    private Vector3 offsetVal;

    private Vector3 startPos;
    private Vector3 tgtPos;

    private GameObject prevHitAppObj;
    private GameObject currHitAppObj;

    private GameObject incidentObj;


    private void selActionMethods()
    {
        // if the hit object is a prefab
        currHitAppObj = hitAppObj;
        // and is not pointing to the same prefab as it was in the last frame
        if (currHitAppObj != prevHitAppObj)
        {
            // collapse the edit menu on the object it was previously pointing
            if (prevHitAppObj != null) prevHitAppObj.GetComponent<HighlightStyle1>().hideObjectMenu();
            // expand the edit menu for current object
            if (currHitAppObj != null) currHitAppObj.GetComponent<HighlightStyle1>().displayObjectMenu();
        }
        prevHitAppObj = currHitAppObj;
        
        
        if (GeneralSettings.editOn())
        {
            if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                if (laser.isHit())
                {
                    // look at the parameters for the thing just hit
                    RaycastHit detectRay;
                    Physics.Raycast(new Ray(laser.getStartPoint(), laser.getDirection()), out detectRay);
                    incidentObj = detectRay.transform.gameObject;


                    // check whether hittin a refObject; in that case move only the ref object
                    RefObject refScript = incidentObj.GetComponent<RefObject>();
                    if (refScript != null)
                    {
                        laser.setRestrictedPlane(incidentObj.GetComponent<RefObject>().getRefPlane());
                        objToMove = incidentObj;
                        startPos = objToMove.transform.localPosition;
                        activeMoveType = SelectMoveType.MoveRef;
                    }
                    // otherwise move the whole edit object
                    else
                    {
                        // set laser length equal to gap between laser and the center of the object highlighted
                        laser.setToStickMode(detectRay.distance);
                        objToMove = GeneralSettings.getEditObject();
                        offsetVal = objToMove.transform.position - detectRay.point;
                        activeMoveType = SelectMoveType.MoveObj;
                    }
                }
                return;
            }
            if (WandControlsManager.WandControllerRight.getTriggerUp())
            {
                laser.clearRestrictedPlane();
                laser.clearStickMode();
                objToMove = null;
                activeMoveType = SelectMoveType.None;
                return;
            }
        }

        if (objToMove != null)
        {
            switch (activeMoveType)
            {
                // FIX-THIS: working only near origin
                case SelectMoveType.MoveRef:
                    {
                        tgtPos = objToMove.transform.parent.InverseTransformVector(laser.getTerminalPoint());
                        if (WandControlsManager.WandControllerRight.getGripPressed()) { tgtPos.x = startPos.x; }
                        else { tgtPos.y = startPos.y; }
                        tgtPos.z = startPos.z;
                        objToMove.transform.localPosition = tgtPos;
                        break;
                    }
                case SelectMoveType.MoveObj:
                    {
                        tgtPos = laser.getTerminalPoint();
                        objToMove.transform.position = tgtPos + offsetVal;
                        break;
                    }
                case SelectMoveType.None:
                    {
                        break;
                    }
            }

        }
    }


}