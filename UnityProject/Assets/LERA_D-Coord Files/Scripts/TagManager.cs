using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class TagManager : MonoBehaviour {
    /*



	// prefab for the tag GameObject
	public GameObject tagMarker;



	///////////////////////////////////////////////////



	// array of all tags that'll be available in the session; one to one associative
	// with the strings in the tagTypes array
	public Color[] tagColors;
	public string[] tagTypes;



	///////////////////////////////////////////////////



	private Material[] tagMaterialArray;
	private int activeTagIdx = 0;



	///////////////////////////////////////////////////



    private GameObject tagSetRef;
    private GameObject currentTagObj;
    private string tagObjectName = "TagObject";
    private int tagCount = 0;
	private int UILayer = 8;



	///////////////////////////////////////////////////



	private LaserPicker laser;
    GameObject hitObj;



	///////////////////////////////////////////////////



	// given currentTag it'll return the next tag from the array of all tags available
	public string getNextTag(string currentTag)
	{
		int idx = System.Array.IndexOf(tagTypes, currentTag);
		idx++;
		idx %= tagTypes.Length;
		return tagTypes[idx];
	}



	// returns the material for the currently active tags
	public Material getCurrentlyActiveTagMaterial()
	{
		return tagMaterialArray[activeTagIdx];
	}



	public Material getTagMaterialByIndex(int idx)
	{
		if (idx < 0 || idx >= tagMaterialArray.Length)
		{
			Debug.Log ("Given index for getting material beyond range of all materials.");
			return null;
		}
		return tagMaterialArray[idx];
	}



	public Material getTagMaterialByType(string typeStr)
	{
		int idx = System.Array.IndexOf(tagTypes, typeStr);
		if (idx < 0 || idx >= tagMaterialArray.Length)
		{
			Debug.Log ("No material found for given string.");
			return null;
		}
		return tagMaterialArray[idx];
	}



	///////////////////////////////////////////////////



	private void setupTagMaterials()
	{
		tagMaterialArray = new Material[tagColors.Length];
		for (int i = 0; i < tagColors.Length; i++)
		{
			tagMaterialArray[i] = new Material(Shader.Find("Standard"));
			tagMaterialArray[i].SetColor("_Color", tagColors[i]);
		}
	}



	// checks if the given inputObject is nested under a parent of name 'checkName' at any level
    private bool recursiveParentNameCheck(GameObject inputObject, string checkName, string endName = "")
    {
		if (inputObject.name.Contains (checkName))
		{
			return true;
		}
        if (inputObject.transform.parent == null || inputObject.transform.parent.gameObject.name == endName)
            return false;
        return (recursiveParentNameCheck(inputObject.transform.parent.gameObject, checkName, endName));
    }



	// returns name of the top-most parent object
    private GameObject getParentTag(GameObject inputObject)
    {
        if (inputObject.name.Contains(tagMarker.name + "(Clone)"))
            return inputObject;
        return (getParentTag(inputObject.transform.parent.gameObject));
	}



	private void createNewTag()
	{
		currentTagObj = new GameObject();
		currentTagObj.name = tagObjectName + "_" + tagCount;
		tagCount++;
		currentTagObj.transform.SetParent(tagSetRef.transform);
		GameObject markerInstance = (GameObject)Instantiate(tagMarker, laser.getHitPoint(), Quaternion.identity);
		markerInstance.GetComponent<TagInfo>().tagName = currentTagObj.name;
		markerInstance.GetComponent<TagInfo>().tagType = tagTypes[activeTagIdx];
		markerInstance.transform.SetParent(currentTagObj.transform);
		GeneralSettings.addLineToConsole(System.String.Format("New tag added: {0}", currentTagObj.name));
	}



	public void setupDefaultTagObject()
	{
		currentTagObj = new GameObject();
		currentTagObj.name = tagObjectName + "_" + "default";
		currentTagObj.transform.SetParent(tagSetRef.transform);
		GameObject markerInstance = (GameObject)Instantiate(tagMarker, Vector3.zero, Quaternion.identity);
		markerInstance.GetComponent<TagInfo>().tagName = currentTagObj.name;
		markerInstance.GetComponent<TagInfo>().tagType = tagTypes[activeTagIdx];
		markerInstance.transform.SetParent(currentTagObj.transform);
		GeneralSettings.currentlyActiveTagObject = currentTagObj;
		GeneralSettings.defaultTagObject = currentTagObj;
		//disable the default tag model so it always remains hidden
        GeneralSettings.defaultTagObject.transform.GetChild(0).FindChild("_Model").gameObject.SetActive(false);
        GeneralSettings.defaultTagObject.transform.GetChild(0).FindChild("_Canvas").gameObject.SetActive(false);
	}



	///////////////////////////////////////////////////



    // reads all the 'attachedItems' data and formats it for display as text in the canvas
    public static string getAttachedItemText(Dictionary<string, List<string>> attachedItems, bool forExport=false)
    {
        string returnText = "";
        string lineBreak = System.Environment.NewLine;
        foreach (KeyValuePair<string, List<string>> attachedItem in attachedItems) {
            if (!forExport)
            {
                returnText += attachedItem.Key.ToString();
                returnText += lineBreak;
            }
            foreach (string item in attachedItem.Value) {
                if (forExport)
                {
                    returnText += item.ToString();
                    returnText += "|";
                }
                else
                {
                    returnText += "o";
                }
            }
            if (!forExport)
            {
                returnText += lineBreak;
            }
        }
        return returnText;
    }





    public string getExportString()
    {
        string output = "";
        string header = "Name,Position-X,Position-Y,Position-Z,Type,DateTime,AttachedItems";
        string seperator = ",";
        output += header;
        output += System.Environment.NewLine;
        string workTxt;
        TagInfo currTagInProcess;
        for(int i=0; i < tagSetRef.transform.childCount; i++)
        {
            currTagInProcess = tagSetRef.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TagInfo>();
            output += currTagInProcess.tagName;
            output += seperator;

            workTxt = tagSetRef.transform.GetChild(i).GetChild(0).position.ToString().Replace("(", "").Replace(")", "").Replace(" ", "");
            output += workTxt;
            output += seperator;

            output += currTagInProcess.tagType;
            output += seperator;
            output += currTagInProcess.timeCreated;
            output += seperator;
            output += getAttachedItemText(currTagInProcess.attachedItems, true);
            output += System.Environment.NewLine;
        }
        return output;
    }



    ///////////////////////////////////////////////////



    // Use this for initialization
    void Start () {
        tagSetRef = GeneralSettings.tagSetManager;
        laser = transform.FindChild("LaserPicker").GetComponent<LaserPicker>();
		setupTagMaterials();
		// make an invisible tag object that'll hold all unassociated items
		setupDefaultTagObject();
    }


	
	// Update is called once per frame
	void Update ()
    {

        hitObj = laser.getHighlightGameObject();



        if (WandControlsManager.WandControllerRight.getTouchPadSwipeDown())
        {
            laser.inAir = true;
            laser.maxLength = 2f;
        }



        if (WandControlsManager.WandControllerRight.getTouchPadSwipeUp())
        {
            laser.inAir = false;
            laser.maxLength = 10000f;
        }

		// when hitting something on the UILayer
        if (hitObj != null && hitObj.layer == UILayer)
		{


			// highlight tag object when pointing at any of its nested child object and itself
			// tagMarker.name + "(Clone)" is the name Unity generates for prefabs
			if (recursiveParentNameCheck(hitObj, tagMarker.name + "(Clone)"))
			{
				getParentTag(hitObj).GetComponent<HighlightMarker>().highlightMarker();
			}


			// when the object hit is a button on a frame with triggerdown call the script button action attached on it
			if (WandControlsManager.WandControllerRight.getTriggerDown() && recursiveParentNameCheck(hitObj, "Button"))
			{
				hitObj.GetComponent<ButtonClick>().callButton();
                return;
			}


			// when pointing directly at the model and hitting the touchpad, cycle through its tags
			if (WandControlsManager.WandControllerRight.getTouchPadButtonDown() && recursiveParentNameCheck(hitObj, tagMarker.name + "(Clone)"))
			{
                getParentTag(hitObj).GetComponent<TagInfo>().nextTag();
                return;
			}


			// FIX-THIS: the laser length should remain constant once an object has been selected
			// when pointing at the tag and clicking trigger and moving, move it with the laser
			if (WandControlsManager.WandControllerRight.getTriggerPressed() && recursiveParentNameCheck(hitObj, tagMarker.name + "(Clone)"))
			{
                getParentTag(hitObj).GetComponent<TagInfo>().moveTag(laser.getHitPoint(), laser.getLaserDirection());
                return;
			}


        }
        else
		{


			// cycle through active tags when not pointing on a ui object and hitting touchpad
			if (WandControlsManager.WandControllerRight.getTouchPadButtonDown())
			{
				activeTagIdx++;
				activeTagIdx %= tagColors.Length;
			}


			// create a new tag on clicking on a non UILayer location
            if (WandControlsManager.WandControllerRight.getTriggerDown())
            {
                if (laser.inAir || laser.isHit())
                {
                    createNewTag();
                }
            }


        }


    }

    */
}
