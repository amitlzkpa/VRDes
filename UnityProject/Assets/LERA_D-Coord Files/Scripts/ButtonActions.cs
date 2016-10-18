using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonActions : MonoBehaviour {

    /*

	private TagInfo tagInfo;



	public void addAudioNote()
    {
        GeneralSettings.currentlyActiveTagObject = gameObject.transform.parent.gameObject;
        GeneralSettings.setActiveLaser(4);
        GeneralSettings.updateInteractText(System.String.Format("Currently adding audio-notes to {0}. Press 'Grip' to exit.", GeneralSettings.currentlyActiveTagObject.name));
	}



	public void addInPlaceSketchObject()
	{
		GeneralSettings.currentlyActiveTagObject = gameObject.transform.parent.gameObject;
        GeneralSettings.setActiveLaser(3);
        GeneralSettings.updateInteractText(System.String.Format("Currently adding a sketch to {0}. Press 'Grip' to exit.", GeneralSettings.currentlyActiveTagObject.name));
	}



	public void addDimensionObject()
	{
		// setting the parent as the` active gameobject since we dont want the prefab instance as the active object
		GeneralSettings.currentlyActiveTagObject = gameObject.transform.parent.gameObject;
        GeneralSettings.setActiveLaser(0);
        GeneralSettings.updateInteractText(System.String.Format("Currently adding dimensions to {0}. Press 'Grip' to exit.", GeneralSettings.currentlyActiveTagObject.name));
    }



    public void deleteTag()
    {
        // never delete the default tag
        if (transform.parent.gameObject.name.Contains("default"))
        {
            Debug.Log("Cannot delete default tag.");
            return;
        }
        // transfer all attched items to the default tag
        foreach(KeyValuePair<string, List<string>> existingKeyVal in tagInfo.attachedItems)
        {
            foreach (string existingItem in existingKeyVal.Value)
            {
                GeneralSettings.defaultTagObject.transform.GetChild(0).gameObject.GetComponent<TagInfo>().attachItem(existingItem);
            }
        }
        Destroy(transform.parent.gameObject);
    }



	// Use this for initialization
	void Start () {
		tagInfo = gameObject.GetComponent<TagInfo>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    */
}
