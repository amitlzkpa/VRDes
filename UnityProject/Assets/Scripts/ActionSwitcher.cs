using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;






public class ActionSwitcher : MonoBehaviour
{


    private enum SelectMoveType
    {
        MoveRefEd, MoveRefPt, MoveObj, MoveRefPtConstrained, MoveGizmoLinear, RotateGizmoAxis,
        MoveSpecOneAxis, MoveSpecPt, None
    }



    private GameObject laserObj;
    private LaserPicker laser;

    private Creator cr;
    private GameObject amObj;
    private ActionManager am;
    private GameObject cmObj;
    private ContextMenuManager cm;


    //---------------------------------------------------------------


    private void clearLaser()
    {
        laser.clearLayerMask();
        laser.clearRestrictedObject();
        laser.clearRestrictedObjectContainsName();
        laser.clearRestrictedObjectStartName();
        laser.clearRestrictedPlane();
        laser.clearStickMode();
    }


    public void setActionItem(GameObject crObj)
    {
        // setting to selection ray; null used to represent selection ray
        if (crObj == null)
        {
            amObj = null;
            am = null;
            cmObj = null;
            cm = null;
            clearLaser();
            laser.setLengthToInfinity();
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
    private SnapObject snapObjScript;

    void Update()
    {


        if (laser.isHit())
        {
            snapObjScript = laser.getHitObject().GetComponent<SnapObject>();
        }
        else
        {
            snapObjScript = null;
        }



        if (snapObjScript != null && snapObjScript.isSnap())
        {
            laser.setSnapPoint(snapObjScript.getSnapPt(laser.getHitPoint()));
        }
        else
        {
            laser.clearSnappedPoint();
        }
        


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
    private WaitingObject currWaitingObj;
    private string specMoveAxis;
    private List<float> tgtPosList = new List<float>();


    private void selActionMethods()
    {
        // if the hit object is a prefab
        currHitAppObj = hitAppObj;
        // and is not pointing to the same prefab as it was in the last frame
        if (currHitAppObj != prevHitAppObj)
        {
            // collapse the edit menu on the object it was previously pointing
            if (prevHitAppObj != null) prevHitAppObj.GetComponent<Highlightable>().hideObjectMenu();
            // expand the edit menu for current object
            if (currHitAppObj != null) currHitAppObj.GetComponent<Highlightable>().displayObjectMenu();
        }
        prevHitAppObj = currHitAppObj;
        
        
        if (GeneralSettings.editOn())
        {
            if (WandControlsManager.WandControllerRight.getGripDown())
            {
                GeneralSettings.clearEditObject();
                return;
            }
                if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                if (laser.isHit())
                {
                    // look at the parameters for the thing just hit
                    RaycastHit detectRay;
                    Physics.Raycast(new Ray(laser.getStartPoint(), laser.getDirection()), out detectRay);
                    incidentObj = detectRay.transform.gameObject;


                    // check whether hittin a refObject; in that case move only the ref object
                    RefObjectEdge refScriptEd = incidentObj.GetComponent<RefObjectEdge>();
                    if (refScriptEd != null)
                    {
                        laser.setRestrictedPlane(incidentObj.GetComponent<RefObjectEdge>().getRefPlane());
                        objToMove = incidentObj;
                        startPos = objToMove.transform.localPosition;
                        activeMoveType = SelectMoveType.MoveRefEd;
                        return;
                    }
                    // check whether hittin a refObject; in that case move only the ref object
                    RefObjectPoint refScriptPt = incidentObj.GetComponent<RefObjectPoint>();
                    if (refScriptPt != null)
                    {
                        laser.setToStickMode(detectRay.distance);
                        objToMove = incidentObj;
                        offsetVal = objToMove.transform.position - detectRay.point;
                        activeMoveType = SelectMoveType.MoveRefPt;
                        return;
                    }
                    // check whether hittin a refObject; in that case move only the ref object
                    RefObjectPlaneConstraintPoint refScriptConstrPt = incidentObj.GetComponent<RefObjectPlaneConstraintPoint>();
                    if (refScriptConstrPt != null)
                    {
                        laser.setRestrictedObject(refScriptConstrPt.getConstrainObject());
                        objToMove = incidentObj;
                        // offsetVal = objToMove.transform.position - detectRay.point;
                        activeMoveType = SelectMoveType.MoveRefPtConstrained;
                        objToMove.GetComponent<SphereCollider>().enabled = false;
                        return;
                    }
                    // DIRTY-FIX
                    // check whether hittin a refObject; in that case move only the ref object
                    GizmoMoveLinear gizMoveLin = incidentObj.GetComponent<GizmoMoveLinear>();
                    if (gizMoveLin != null)
                    {
                        laser.setRestrictedPlane(incidentObj.GetComponent<GizmoMoveLinear>().getRefPlane());
                        objToMove = incidentObj;
                        startPos = objToMove.transform.localPosition;
                        activeMoveType = SelectMoveType.MoveGizmoLinear;
                        return;
                    }
                    // check whether hittin a refObject; in that case move only the ref object
                    GizmoRotateAxis gizRotateAxis = incidentObj.GetComponent<GizmoRotateAxis>();
                    if (gizRotateAxis != null)
                    {
                        laser.setRestrictedPlane(incidentObj.GetComponent<GizmoRotateAxis>().getRefPlane());
                        objToMove = incidentObj;
                        activeMoveType = SelectMoveType.RotateGizmoAxis;
                        return;
                    }
                    // check whether hittin a refObject; in that case move only the ref object
                    GizmoSpecMoveOneAxis moveSpecOneAxis = incidentObj.GetComponent<GizmoSpecMoveOneAxis>();
                    if (moveSpecOneAxis != null)
                    {
                        specMoveAxis = incidentObj.GetComponent<GizmoSpecMoveOneAxis>().getAxis();
                        currWaitingObj = ScriptableObject.CreateInstance<WaitingObject>();
                        GeneralSettings.setNumPad(currWaitingObj);
                        GeneralSettings.updateInteractText("Please specify offset along " + specMoveAxis + "-axis.");
                        objToMove = incidentObj;
                        activeMoveType = SelectMoveType.MoveSpecOneAxis;
                        return;
                    }
                    // check whether hittin a refObject; in that case move only the ref object
                    GizmoMoveSpecPoint moveSpecPt = incidentObj.GetComponent<GizmoMoveSpecPoint>();
                    if (moveSpecPt != null)
                    {
                        currWaitingObj = ScriptableObject.CreateInstance<WaitingObject>();
                        GeneralSettings.setNumPad(currWaitingObj);
                        GeneralSettings.updateInteractText("Please enter target point on X-axis.");
                        objToMove = incidentObj;
                        activeMoveType = SelectMoveType.MoveSpecPt;
                        tgtPosList.Clear();
                        return;
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
                // these modes require continuation and are not exited on just trigger up
                // their reselase methods are defined in the parts where the activeMoveType is set back to default
                if (activeMoveType == SelectMoveType.MoveSpecOneAxis || activeMoveType == SelectMoveType.MoveSpecPt) return;
                if (objToMove.GetComponent<SphereCollider>() != null) objToMove.GetComponent<SphereCollider>().enabled = true;
                clearLaser();
                objToMove = null;
                activeMoveType = SelectMoveType.None;
                return;
            }
        }

        if (objToMove != null)
        {
            switch (activeMoveType)
            {
                case SelectMoveType.MoveRefEd:
                    {
                        tgtPos = objToMove.transform.parent.InverseTransformVector(laser.getTerminalPoint());
                        objToMove.GetComponent<RefObjectEdge>().moveObject(startPos, tgtPos);
                        break;
                    }
                case SelectMoveType.MoveRefPt:
                    {
                        tgtPos = laser.getTerminalPoint();
                        objToMove.GetComponent<RefObjectPoint>().moveObject(tgtPos + offsetVal);
                        break;
                    }
                case SelectMoveType.MoveRefPtConstrained:
                    {
                        if (!laser.isHit()) break;
                        tgtPos = laser.getTerminalPoint();
                        objToMove.GetComponent<RefObjectPlaneConstraintPoint>().moveObject(tgtPos);
                        break;
                    }
                case SelectMoveType.MoveObj:
                    {
                        tgtPos = laser.getTerminalPoint();
                        objToMove.GetComponent<Editable>().moveObject(tgtPos + offsetVal);
                        break;
                    }
                // DIRTY-FIX
                case SelectMoveType.MoveGizmoLinear:
                    {
                        tgtPos = objToMove.transform.parent.InverseTransformVector(laser.getTerminalPoint());
                        objToMove.GetComponent<GizmoMoveLinear>().moveObject(tgtPos);
                        break;
                    }
                case SelectMoveType.RotateGizmoAxis:
                    {
                        tgtPos = laser.getTerminalPoint();
                        objToMove.GetComponent<GizmoRotateAxis>().rotateObject(tgtPos);
                        break;
                    }
                case SelectMoveType.MoveSpecOneAxis:
                    {
                        if (!currWaitingObj.isSet()) return;
                        int amt = int.Parse(currWaitingObj.readString());
                        objToMove.GetComponent<GizmoSpecMoveOneAxis>().moveObject(amt);
                        GeneralSettings.addLineToConsole(string.Format("Moved {0} by {1} in {2}-axis", GeneralSettings.getParentClone(objToMove, "app_").name, amt, specMoveAxis));
                        objToMove = null;
                        activeMoveType = SelectMoveType.None;
                        GeneralSettings.reinstatePreviousMenu();
                        GeneralSettings.updateInteractText("");
                        if (objToMove.GetComponent<SphereCollider>() != null) objToMove.GetComponent<SphereCollider>().enabled = true;
                        clearLaser();
                        break;
                    }
                case SelectMoveType.MoveSpecPt:
                    {
                        if (!currWaitingObj.isSet()) return;
                        float amt = float.Parse(currWaitingObj.readString());
                        tgtPosList.Add(amt);
                        if (tgtPosList.Count == 1)
                        {
                            GeneralSettings.reinstatePreviousMenu();
                            currWaitingObj = ScriptableObject.CreateInstance<WaitingObject>();
                            GeneralSettings.setNumPad(currWaitingObj);
                            GeneralSettings.updateInteractText("Please enter target point on Y-axis.");
                            return;
                        }
                        if (tgtPosList.Count == 2)
                        {
                            GeneralSettings.reinstatePreviousMenu();
                            currWaitingObj = ScriptableObject.CreateInstance<WaitingObject>();
                            GeneralSettings.setNumPad(currWaitingObj);
                            GeneralSettings.updateInteractText("Please enter target point on Z-axis.");
                            return;
                        }
                        if (tgtPosList.Count == 3)
                        {
                            Vector3 tgtSpecPos = new Vector3(tgtPosList[0], tgtPosList[1], tgtPosList[2]);
                            objToMove.GetComponent<GizmoMoveSpecPoint>().moveObject(tgtSpecPos);
                            GeneralSettings.addLineToConsole(string.Format("Moved {0} to {1}", GeneralSettings.getParentClone(objToMove, "app_").name, tgtSpecPos));
                            if (objToMove.GetComponent<SphereCollider>() != null) objToMove.GetComponent<SphereCollider>().enabled = true;
                            objToMove = null;
                            activeMoveType = SelectMoveType.None;
                            GeneralSettings.reinstatePreviousMenu();
                            GeneralSettings.updateInteractText("");
                            clearLaser();
                        }
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