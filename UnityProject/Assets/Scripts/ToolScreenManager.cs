using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToolScreenManager : MonoBehaviour, IToolsScreenManager
{
    public void clickActiveButton()
    {
        StartCoroutine(clickButtonColors());
        buttonList[activeButtonIdx].GetComponent<Button>().onClick.Invoke();
    }

    public void nextButton()
    {
        dehighlightButtonColors(buttonList[activeButtonIdx].GetComponent<Button>());
        activeButtonIdx++;
        activeButtonIdx %= buttonList.Count;
        highlightButtonColors(buttonList[activeButtonIdx].GetComponent<Button>());
    }

    public void prevButton()
    {
        dehighlightButtonColors(buttonList[activeButtonIdx].GetComponent<Button>());
        activeButtonIdx--;
        if (activeButtonIdx < 0) activeButtonIdx = buttonList.Count-1;
        highlightButtonColors(buttonList[activeButtonIdx].GetComponent<Button>());
    }



    private void highlightButtonColors(Button inputButton)
    {
        buttonList[activeButtonIdx].GetComponent<Image>().color = hlColor;
    }

    private void dehighlightButtonColors(Button inputButton)
    {
        buttonList[activeButtonIdx].GetComponent<Image>().color = defaultColor;
    }


    IEnumerator clickButtonColors()
    {
        buttonList[activeButtonIdx].GetComponent<Image>().color = clickColor;
        for (int i=0; i<2; i++)
        {
            yield return new WaitForSeconds(0.1f);
        }
        buttonList[activeButtonIdx].GetComponent<Image>().color = hlColor;
    }







    List<GameObject> buttonList;
    private Color defaultColor;
    private Color hlColor;
    private Color clickColor;

    private int activeButtonIdx = 0;


    // Use this for initialization
    void Start ()
    {
        buttonList = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name.ToLower().Contains("button"))
            {
                buttonList.Add(transform.GetChild(i).gameObject);
            }
        }

        float hl = 0.7f;
        float click = 1.4f;
        defaultColor = new Color(0.25f, 0.25f, 0.25f, 0.5f);
        hlColor = new Color(defaultColor.r * hl, defaultColor.g * hl, defaultColor.b * hl, 1f);
        clickColor = new Color(defaultColor.r * click, defaultColor.g * click, defaultColor.b * click, 1f);
        highlightButtonColors(buttonList[activeButtonIdx].GetComponent<Button>());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
