using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RectArrayActionManager : MonoBehaviour, ActionManager {

    private enum ActionMode
    {
        SELOBJ, SELSTART, SELVEC1, SELVEC2
    }


    private GameObject arrObj;
    private Vector3 startPos;
    private Vector3 vec1;
    private Vector3 vec2;
    private int xCount = 4;
    private int yCount = 4;
    private ActionMode actionMode = ActionMode.SELOBJ;




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
            case ActionMode.SELVEC2:
                {
                    getVec2(laser);
                    break;
                }
        }
    }



    private void getSelObj(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
                GeneralSettings.setNumPad();
                if (GeneralSettings.getParentClone(laser.getHitObject(), "app_") == null)
                    return;
                arrObj = GeneralSettings.getParentClone(laser.getHitObject(), "app_");
                actionMode = ActionMode.SELSTART;
                GeneralSettings.updateInteractText("Please select start point for the array.");
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
                GeneralSettings.updateInteractText("Please select vector in first direction for array.");
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
                actionMode = ActionMode.SELVEC2;
                GeneralSettings.updateInteractText("Please select vector in second direction for array.");
            }
        }
    }




    private void getVec2(LaserPicker laser)
    {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
                vec2 = laser.getTerminalPoint() - startPos;
                createArray();
                amStart(laser);
                GeneralSettings.updateInteractText("Array created. Please select next object to be arrayed.");
            }
        }
    }

    

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
        for(int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                retList.Add(startPos + (j * vec1) + (i * vec2));
            }
        }
        return retList;
    }




}
