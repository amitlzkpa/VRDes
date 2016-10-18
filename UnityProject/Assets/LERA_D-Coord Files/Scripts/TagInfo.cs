 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TagInfo : MonoBehaviour {
    /*


	///////////////////////////////////////////////////



	private GameObject tagModel;
	private GameObject UITagCanvasObject;
	private Text UITagCreatedText;
	private Text UITagNameText;
	private Text UITagTypeText;
	private Text UIAttachedItemsText;
	private GameObject tagManagerRef;

	private bool updateFrame = false;



	///////////////////////////////////////////////////



	// internal variables used to hold values
	private DateTime internal_TimeCreated;
	private string internal_TagName;
	private string internal_TagType;
	private Dictionary<string, List<string>> internal_AttachedItems;
	private Material matToApply;



	// public variables exposed for accessing from other scripts
	public DateTime timeCreated
	{
		get { return internal_TimeCreated; }
		set
		{
			internal_TimeCreated = value;
			updateFrame = true;
		}
	}

	public string tagName
	{
		get { return internal_TagName; }
		set 
		{
			internal_TagName = value;
			updateFrame = true;
		}
	}

	public string tagType
	{
		get { return internal_TagType; }
		set
		{
			internal_TagType = value;
			updateFrame = true;
		}
	}

	public Dictionary<string, List<string>> attachedItems
	{
		get { return internal_AttachedItems; }
	}



	///////////////////////////////////////////////////



	private string getTypeString(string objectName)
	{
        if (objectName.ToLower().Contains("audioobject"))
			return "audio-note";
		if (objectName.ToLower().Contains("audio-sketch"))
			return "audio-sketch";
		if (objectName.ToLower().Contains("canvas-sketch"))
			return "canvas-sketch";
		if (objectName.ToLower().Contains("sketchobject"))
			return "inplace-sketch";
        if (objectName.ToLower().Contains("dimensionobject"))
			return "dimension";
		return "undefined type";
	}



	///////////////////////////////////////////////////



	public void attachItem(string inputItemObjectNameStr)
    {
		string inputItemTypeStr = getTypeString(inputItemObjectNameStr);
		if (internal_AttachedItems.ContainsKey(inputItemTypeStr))
		{
			List<string> existingItems = internal_AttachedItems[inputItemTypeStr];
			existingItems.Add(inputItemObjectNameStr);

		}
		else
		{
			List<string> newListOfItems = new List<string>();
			newListOfItems.Add(inputItemObjectNameStr);
			internal_AttachedItems.Add(inputItemTypeStr, newListOfItems);
		}
		GeneralSettings.addLineToConsole(System.String.Format("Added new {0} to {1}.",
															   inputItemTypeStr,
															   transform.parent.gameObject.name));
	}



	// gets the next tag from the array of all tags stored in the TagManager
	public void nextTag()
	{
		string prevTag = tagType;
		tagType = tagManagerRef.GetComponent<TagManager>().getNextTag(tagType);
		GeneralSettings.addLineToConsole(System.String.Format("Tag for {0} changed from {1} to {2}.",
															   transform.parent.gameObject.name, 
															   prevTag, tagType));
	}



    // TO-DO: fix this for better use; the laser length should remain constant once an object has been selected
	public void moveTag(Vector3 inputPos, Vector3 direction)
	{
		float distance = Vector3.Distance(inputPos, transform.position);
		Vector3 diffVector = direction.normalized * distance;
		Vector3 tgtPos = inputPos + diffVector;
		transform.position = tgtPos;
	}



	///////////////////////////////////////////////////



	///////////////////////////////////////////////////




	// Use this for initialization
	void Start () {
		tagManagerRef = GameObject.Find("TagManager"); // TO-DO: make this relative call to get the tagManagerRef
		tagModel = transform.FindChild("_Model").GetChild(0).gameObject; // gets only the first object in the "_Model"
		tagModel.GetComponent<MeshRenderer>().material = tagManagerRef.GetComponent<TagManager>().getTagMaterialByType(tagType);
		timeCreated = DateTime.Now;
		internal_AttachedItems = new Dictionary<string, List<string>>();

		UITagCanvasObject = transform.FindChild("_Canvas").gameObject;
		UITagCreatedText = UITagCanvasObject.transform.FindChild("InfoPane").FindChild("_DateText").gameObject.GetComponent<Text>();
		UITagNameText = UITagCanvasObject.transform.FindChild("InfoPane").FindChild("_NameText").gameObject.GetComponent<Text>();
		UITagTypeText = UITagCanvasObject.transform.FindChild("InfoPane").FindChild("_TypeText").gameObject.GetComponent<Text>();
		UIAttachedItemsText = UITagCanvasObject.transform.FindChild("InfoPane").FindChild("_AttachedItemsText").gameObject.GetComponent<Text>();
	}





	// Update is called once per frame
	void Update () {


		if (updateFrame)
		{
            UITagCreatedText.text = internal_TimeCreated.ToLongDateString() + Environment.NewLine + internal_TimeCreated.ToShortTimeString();
			UITagNameText.text = internal_TagName;
			UITagTypeText.text = internal_TagType;
            UIAttachedItemsText.text = TagManager.getAttachedItemText(attachedItems);
			tagModel.GetComponent<MeshRenderer>().material = tagManagerRef.GetComponent<TagManager>().getTagMaterialByType(tagType);
			updateFrame = true;
		}

	}

    */
}
