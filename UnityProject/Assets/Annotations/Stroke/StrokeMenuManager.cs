﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrokeMenuManager : MonoBehaviour, ContextMenuManager
{

    public GameObject strokeActionObject;
    private StrokeActionManager strokeActionManager;
    private LaserPicker l;
    private float[] widthChoices = { 0.02f, 0.04f, 0.08f, 0.1f, 0.15f, 0.2f, 0.5f, 0.7f, 1.0f };
    private int widthIdx = 0;
    private Text widthText;



    //---------------------------------------------------------------



    // updates the color in the UI menu
    public void setUIColor(Color inpColor)
    {
        transform.FindChild("Button_color").GetComponent<Image>().color = inpColor;
    }


    private void updateStrokeWidth()
    {
        strokeActionManager.setLineThickness(widthChoices[widthIdx]);
        widthText.text = System.String.Format("{0} CM", Mathf.RoundToInt(widthChoices[widthIdx] * 100));
    }



    //---------------------------------------------------------------



    public void setupForInAir()
    {
        l.setLength(3f);
    }


    public void setupForSurface()
    {
        l.setLength(1000000f);
    }


    public void nextColor()
    {
        strokeActionManager.nextLineColor();
    }


    public void nextWidth()
    {
        widthIdx++;
        widthIdx %= widthChoices.Length;
        updateStrokeWidth();
    }



    //---------------------------------------------------------------



    public void cmStart(LaserPicker laser)
    {
        strokeActionManager = strokeActionObject.GetComponent<StrokeActionManager>();
        widthText = transform.FindChild("Button_strokeWidth").FindChild("Text").gameObject.GetComponent<Text>();
        updateStrokeWidth();
        l = laser;
    }

    public void cmUpdate(LaserPicker laser)
    {
    }
}
