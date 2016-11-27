using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBoxButtonTransforms : MonoBehaviour
{


    private bool editOnCheck()
    {
        if (GeneralSettings.editOn())
        {
            GeneralSettings.flashInteractWindow();
            GeneralSettings.updateInteractText("Please close edit mode first.");
            return true;
        }
        GeneralSettings.updateInteractText("");
        return false;
    }


    //---------------------------------------------------------------


    public GameObject rectArrayCreatorPrefab;
    public void createRectArray()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(rectArrayCreatorPrefab);
        }
    }


    //---------------------------------------------------------------


    public GameObject polarArrayCreatorPrefab;
    public void createPolarArray()
    {
        if (!editOnCheck())
        {
            GeneralSettings.deleteObjectMenu();
            GeneralSettings.setActiveActionObject(polarArrayCreatorPrefab);
        }
    }


    //---------------------------------------------------------------



    private bool endSnapOn = true;
    private bool midSnapOn = true;


    public void toggleEndSnaps()
    {
        if (endSnapOn)
        {
            GeneralSettings.removeFromActiveSnaps(SnapType.END);
            endSnapOn = false;
        }
        else
        {
            GeneralSettings.addToActiveSnaps(SnapType.END);
            endSnapOn = true;
        }
    }


    public void toggleMidSnaps()
    {
        if (midSnapOn)
        {
            GeneralSettings.removeFromActiveSnaps(SnapType.MID);
            midSnapOn = false;
        }
        else
        {
            GeneralSettings.addToActiveSnaps(SnapType.MID);
            midSnapOn = true;
        }
    }


    //---------------------------------------------------------------


    private enum FaceDir
    {
        X, Y, Z, NONE
    }

    private FaceDir faceDir = FaceDir.NONE;


    public void setToFaceX()
    {
        if (faceDir == FaceDir.X)
        {
            GeneralSettings.rightLaser.reverseFacingDir();
            return;
        }
        faceDir = FaceDir.X;
        GeneralSettings.rightLaser.setFacingX();
    }


    public void setToFaceY()
    {
        if (faceDir == FaceDir.Y)
        {
            GeneralSettings.rightLaser.reverseFacingDir();
            return;
        }
        faceDir = FaceDir.Y;
        GeneralSettings.rightLaser.setFacingY();
    }


    public void setToFaceZ()
    {
        if (faceDir == FaceDir.Z)
        {
            GeneralSettings.rightLaser.reverseFacingDir();
            return;
        }
        faceDir = FaceDir.Z;
        GeneralSettings.rightLaser.setFacingZ();
    }


    public void clearFaceDir()
    {
        faceDir = FaceDir.NONE;
        GeneralSettings.rightLaser.clearFacingDir();
    }




}
