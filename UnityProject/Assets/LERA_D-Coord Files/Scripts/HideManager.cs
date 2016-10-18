using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideManager : MonoBehaviour {
    /*

    private LaserPicker laser;

    private List<GameObject> inactiveObjects;




    // gets the topmost parent or when the parent has the same name as endName; whichever is first naturally
    private GameObject getParentObject(GameObject inputObject, string endName)
    {
        if (inputObject.transform.parent == null || inputObject.transform.parent.gameObject.name == endName)
        {
            return inputObject;
        }
        return getParentObject(inputObject.transform.parent.gameObject, endName);
    }



	private void hideGivenObject(GameObject inputObject)
	{
		if(inputObject == null)
		{
			return;
		}
		inactiveObjects.Add(inputObject);
		inputObject.SetActive(false);
		GeneralSettings.addLineToConsole(System.String.Format("Object hidden: {0}", inputObject.name));
	}



	private void hideGivenObjectLayer(GameObject inputObject)
	{
		if(inputObject == null)
		{
			return;
		}
		GameObject parentObj = getParentObject(inputObject, "_Model");
		inactiveObjects.Add(parentObj);
		parentObj.SetActive(false);
		GeneralSettings.addLineToConsole(System.String.Format("Layer hidden: {0}", parentObj.name));
	}



	private void unhideAllObjects()
	{
		foreach (GameObject inacObj in inactiveObjects)
		{
			inacObj.SetActive(true);
		}
		inactiveObjects.Clear();
		GeneralSettings.addLineToConsole("All objects made visible.");
	}








    // Use this for initialization
    void Start () {

        inactiveObjects = new List<GameObject>();
        laser = transform.FindChild("LaserPicker").GetComponent<LaserPicker>();
	
	}



	
	// Update is called once per frame
	void Update () {
        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            GameObject hitObj = laser.getHighlightGameObject();
			hideGivenObject(hitObj);
        }


		if (WandControlsManager.WandControllerRight.getTouchPadButtonDown())
        {
			GameObject hitObj = laser.getHighlightGameObject();
			hideGivenObjectLayer(hitObj);
        }


        if (WandControlsManager.WandControllerRight.getTouchPadSwipeUp())
        {
			unhideAllObjects();
        }



        if (WandControlsManager.WandControllerRight.getGripDown ())
        {
            GeneralSettings.resetActiveTagAndSwitchToTagManager();
        }
    }

    */
}
