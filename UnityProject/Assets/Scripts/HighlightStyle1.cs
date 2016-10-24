﻿    using UnityEngine;
using System.Collections;

public class HighlightStyle1 : MonoBehaviour, Highlightable
{
    public Material highlightMaterial;

    private Material[] realMaterialArray;

    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject objectMenuObj;

    private Vector3 directionVec;
    Quaternion tgtRotation;
    private float verticalOffsetVal;


    private bool highlightOn;
    private bool prevHighlight;


    private bool highlightDown;
    private bool highlightPressed;
    private bool highlightUp;


    //---------------------------------------------------------------


    public void highlightObject()
    {
        transform.FindChild("_ObjectInfo").gameObject.GetComponent<UIUpdater>().updateUI();
        highlightOn = true;
    }


    //---------------------------------------------------------------


    private void setHighlightMaterial()
    {
        for (int i = 0; i < modelObj.transform.childCount; i++)
        {
            modelObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
    }


    private void unsetHighlightMaterial()
    {
        for (int i = 0; i < modelObj.transform.childCount; i++)
        {
            modelObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = realMaterialArray[i];
        }
    }


    private void orientUICanvas()
    {
        directionVec = (GeneralSettings.player.transform.position - modelObj.transform.position).normalized;
        tgtRotation = Quaternion.LookRotation(directionVec);
        infoCanvasObj.transform.rotation = tgtRotation;
        // rotate canvas so it faces the user instead of away from the user
        infoCanvasObj.transform.RotateAround(infoCanvasObj.transform.position, infoCanvasObj.transform.up, 180f);
        // move the canvas vertically a little wrt where the user is looking from
        // i.e. when user looks from exactly below move it down a little; useful when the tag is on the ceiling
        // and user tries to look at it from the same level
        verticalOffsetVal = (infoCanvasObj.transform.localScale.y * directionVec.y);
        infoCanvasObj.transform.position = modelObj.transform.position + (directionVec * 0.6f) + new Vector3(0, verticalOffsetVal, 0);
    }


    private void resetUICanvasOrient()
    {
        infoCanvasObj.transform.position = modelObj.transform.position;
        infoCanvasObj.transform.rotation = Quaternion.identity;
    }



    private void setObjectMenu()
    {
        objectMenuObj.SetActive(true);
        GeneralSettings.setObjectMenu(objectMenuObj);
    }


    private void unsetObjectMenu()
    {
        GeneralSettings.detachObjectMenu();
        objectMenuObj.transform.position = modelObj.transform.position;
        objectMenuObj.transform.localRotation = Quaternion.identity;
        objectMenuObj.SetActive(false);
    }


    //---------------------------------------------------------------


    // Use this for initialization
    void Start()
    {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        infoCanvasObj.SetActive(false);
        objectMenuObj = transform.FindChild("_ObjectMenu").gameObject;
        objectMenuObj.SetActive(false);
        realMaterialArray = new Material[modelObj.transform.childCount];
        for (int i = 0; i < modelObj.transform.childCount; i++)
        {
            realMaterialArray[i] = modelObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (!prevHighlight && highlightOn)
        {
            highlightDown = true;
            highlightPressed = false;
            highlightUp = false;
        }

        if (prevHighlight && !highlightOn)
        {
            highlightDown = false;
            highlightPressed = false;
            highlightUp = true;
        }

        if (prevHighlight && highlightOn)
        {
            highlightDown = false;
            highlightPressed = true;
            highlightUp = false;
        }


        if(highlightDown)
        {
            setHighlightMaterial();
            infoCanvasObj.SetActive(true);
            setObjectMenu();
        }

        if (highlightPressed)
        {
            orientUICanvas();
        }

        if (highlightUp)
        {
            unsetHighlightMaterial();
            infoCanvasObj.SetActive(false);
            resetUICanvasOrient();
            unsetObjectMenu();
        }


        prevHighlight = highlightOn;
        highlightOn = false;
    }
}
