using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class zActionManager : MonoBehaviour
{
    public void setActionItem(GameObject crObj)
    {
        Creator cr = crObj.GetComponent<Creator>();
        GameObject cmObj = cr.getMenuObject();
        ContextMenuManager cm = cr.getMenuManager();
    }


    public void setMenuObject(ContextMenu m)
    {

    }





    /*

    public int activeIdx = 1;
    public GameObject[] actionManagers;

    private bool updateActiveObjectFlag = false;

    private Scrollbar actionModeScroll;
    private float scrollStep;


    private Text objectName;
    private Text layerName;
    private string defaultString = "-";
    private GameObject hitObj;




    ///////////////////////////////////////



	public void switchLaserByInt(int idx)
	{
		if (idx >= actionManagers.Length)
		{
			Debug.Log("Laser switch index too high. Must be less than action managers.");
			return;
		}
		activeIdx = idx;
		actionModeScroll.value = remapIntToFloat(idx, actionManagers.Length-1);
		updateActiveObjectFlag = true;
	}



	public void switchLaserByFloat(float val)
	{
		if (val > 1f)
		{
			Debug.Log("Laser switch float too high. Must be less than or equal to 1.");
			return;
		}
		actionModeScroll.value = val;
		activeIdx = remapFloatToInt(val, actionManagers.Length-1);
		updateActiveObjectFlag = true;
	}



	///////////////////////////////////////



	private float remapIntToFloat(int val, int limit)
	{
		return (val * 1f) / limit;
	}



    private int remapFloatToInt(float val, int limit)
    {
        return Mathf.FloorToInt(val*limit);
    }



    void activateGivenObject(int idx)
    {
        for(int i=0; i<actionManagers.Length; i++)
        {
            actionManagers[i].SetActive(false);
        }
		actionManagers[idx].SetActive(true);
		switchLaserByInt(idx);
    }



    ///////////////////////////////////////



    // gets the topmost parent or when the parent has the same name as endName; whichever is first naturally
    private GameObject getParentObject(GameObject inputObject, string endName)
    {
        if (inputObject.transform.parent == null || inputObject.transform.parent.gameObject.name == endName)
        {
            return inputObject;
        }
        return getParentObject(inputObject.transform.parent.gameObject, endName);
    }



    ///////////////////////////////////////







    // Use this for initialization
    void Start ()
    {
        actionModeScroll = transform.parent.FindChild("_Canvas").FindChild("SliderSet").FindChild("Scrollbar").GetComponent<Scrollbar>();
		actionModeScroll.numberOfSteps = actionManagers.Length;
        scrollStep = 1.0f / actionModeScroll.numberOfSteps;
        objectName = transform.parent.FindChild("_Canvas").FindChild("InspectorSet").FindChild("ObjectName").gameObject.GetComponent<Text>();
        layerName = transform.parent.FindChild("_Canvas").FindChild("InspectorSet").FindChild("LayerName").gameObject.GetComponent<Text>();
        objectName.text = defaultString;
        layerName.text = defaultString;
		// update it in second frame to override whatever state it is in Unity Editor
        // all tools are instatiated in the first frame and activate the relevant one in the second
		updateActiveObjectFlag = true;
        GeneralSettings.resetActiveTagAndSwitchToTagManager();
    }
	





	// Update is called once per frame
    void Update () {





        hitObj = actionManagers[activeIdx].transform.FindChild("LaserPicker").GetComponent<LaserPicker>().getHighlightGameObject();
        if (hitObj == null)
        {
            objectName.text = defaultString;
            layerName.text = defaultString;
        }
        else
        {
            objectName.text = hitObj.name;
			// layers are represented by top most parent object or when parent is represented by object named "_Model"
            layerName.text = getParentObject(hitObj, "_Model").name;
        }





        if (updateActiveObjectFlag)
        {
			activateGivenObject(activeIdx);
            updateActiveObjectFlag = false;
            return; // dont take in any more commands this frame
        }






		if (WandControlsManager.WandControllerRight.getTouchPadSwipeRight())
        {
            // dont listen to tools switching commands if some action is currently underway
            if (GeneralSettings.isActionOn())
            {
                GeneralSettings.flashInteractWindow();
                return;
            }
			updateActiveObjectFlag = true;
            actionModeScroll.value += scrollStep;
			activeIdx++;
			if (actionModeScroll.value > 1) actionModeScroll.value = 1;
			if (activeIdx >= actionManagers.Length) activeIdx = actionManagers.Length-1;
        }
		if (WandControlsManager.WandControllerRight.getTouchPadSwipeLeft())
        {
            // dont listen to tools switching commands if some action is currently underway
            if (GeneralSettings.isActionOn())
            {
                GeneralSettings.flashInteractWindow();
                return;
            }
			updateActiveObjectFlag = true;
			actionModeScroll.value -= scrollStep;
			activeIdx--;
			if (actionModeScroll.value < 0) actionModeScroll.value = 0;
			if (activeIdx <= 0) activeIdx = 0;
        }

    }
    */
}
