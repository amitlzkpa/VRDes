using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PolarArrayActionManager : MonoBehaviour, ActionManager {

    private enum ActionMode
    {
        SELOBJ, SELSTART, SELVEC1, POLARCOUNT, ARRAYCOUNT
    }


    private GameObject arrObj;
    private Vector3 startPos;
    private Vector3 vec1;
    private int polarCount = 4;
    private int arrayCount = 4;
    private ActionMode actionMode = ActionMode.SELOBJ;

    private WaitingObject currWaitingObj;




    public void amStart(LaserPicker laser)
    {
        actionMode = ActionMode.SELOBJ;
        GeneralSettings.updateInteractText("Please select object to be arrayed.");
        arrObj = null;
    }

    public void amUpdate(LaserPicker laser)
    {
        switch(actionMode)
        {
            case ActionMode.SELOBJ:
                {
                    getSelObj(laser);
                    break;
                }
            case ActionMode.SELSTART:
                {
                    getStartPos(laser);
                    break;
                }
            case ActionMode.SELVEC1:
                {
                    getVec1(laser);
                    break;
                }
            case ActionMode.POLARCOUNT:
                {
                    getPolarCount(laser);
                    break;
                }
            case ActionMode.ARRAYCOUNT:
                {
                    getArrayCount(laser);
                    break;
                }
        }
    }



    //---------------------------------------------------------------



    private void getSelObj(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
                arrObj = GeneralSettings.getParentClone(laser.getHitObject(), "app_");
                actionMode = ActionMode.SELSTART;
                GeneralSettings.updateInteractText("Please select the center point for the array.");
            }
        }
    }



    private void getStartPos(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
                startPos = laser.getTerminalPoint();
                actionMode = ActionMode.SELVEC1;
                GeneralSettings.updateInteractText("Please select the distance between the elements for the array.");
            }
        }
    }



    private void getVec1(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
                vec1 = laser.getTerminalPoint() - startPos;
                actionMode = ActionMode.POLARCOUNT;
                currWaitingObj = ScriptableObject.CreateInstance<WaitingObject>();
                GeneralSettings.setNumPad(currWaitingObj);
                GeneralSettings.updateInteractText("Please specify number of poles in polar array.");
            }
        }
    }



    private void getPolarCount(LaserPicker laser)
    {
        if (!currWaitingObj.isSet()) return;
        polarCount = int.Parse(currWaitingObj.readString());
        actionMode = ActionMode.ARRAYCOUNT;
        currWaitingObj = ScriptableObject.CreateInstance<WaitingObject>();
        GeneralSettings.setNumPad(currWaitingObj);
        GeneralSettings.updateInteractText("Please count for array of objects in each polar direction.");
    }



    private void getArrayCount(LaserPicker laser)
    {
        if (!currWaitingObj.isSet()) return;
        arrayCount = int.Parse(currWaitingObj.readString());
        GeneralSettings.reinstatePreviousMenu();
        createArray();
        amStart(laser);
        GeneralSettings.updateInteractText("Array created. Please select next object to be arrayed.");
    }



    //---------------------------------------------------------------



    private void createArray()
    {
        List<Vector3> arrPts = getListOfPos();
        foreach (Vector3 pos in arrPts)
        {
            Instantiate(arrObj, pos, Quaternion.identity, GeneralSettings.modelObjects.transform);
        }
    }
    


    private List<Vector3> getListOfPos()
    {
        List<Vector3> retList = new List<Vector3>();
        if (polarCount < 2 || arrayCount == 0) return retList;
        retList.Add(startPos);
        float angle = 360f / polarCount;
        float gap = Vector3.Distance(startPos, vec1)/2;
        float currAngle;
        float currGap;
        Vector3 pt;
        for (int i = 0; i < polarCount; i++)
        {
            for (int j = 1; j <= arrayCount; j++)
            {
                currAngle = Mathf.Deg2Rad * i * angle;
                currGap = j * gap;
                pt = new Vector3(startPos.x + Mathf.Cos(currAngle) * currGap, startPos.y, startPos.z + Mathf.Sin(currAngle) * currGap);
                retList.Add(pt);
            }
        }
        return retList;
    }




}
