using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;





///////// IMPORTANT SETUP

// LAYERS
// 2 layers needed:
// layer8: "UIObjects"
// layer9: "ModelObjects"
// after importing put all objects and children under "_Model" on layer9

// SCRIPT EXECUTION ORDER
// "GeneralSettings" should run before the "Default Time"
// "DrawManager"  should run after "Default Time"


public class GeneralSettings : MonoBehaviour {



    // global params
    ////////////////////////////////////////////////////////////////////////////////
    



	public static string developerName = "Amit Nambiar";
	public static string applicationName = "VR Design System";
	public static double version = 0.1;

	public static string sessionName = "demo";
	public static DateTime sessionStartTime;
    public static string sessionID;



    // default and current tag setup
    ////////////////////////////////////////////////////////////////////////////////



    private static GameObject objBeingEdited;



    public static void clearEditObject()
    {
        GeneralSettings.addLineToConsole(System.String.Format("Exiting edit for {0}.", objBeingEdited.name));
        objBeingEdited = null;
        rightLaser.clearRestrictedObject();
    }



    public static void setEditObject(GameObject inpObj)
    {
        // if an object is being edited, reslease it and set the new object to edit mode
        if (editOn()) clearEditObject();
        objBeingEdited = inpObj;
        rightLaser.setRestrictedObject(objBeingEdited, GeneralSettings.modelObjects);
        GeneralSettings.addLineToConsole(System.String.Format("Editing {0}.", objBeingEdited.name));
    }



    public static GameObject getEditObject()
    {
        return objBeingEdited;
    }



    public static bool editOn()
    {
        return objBeingEdited != null;
    }



    // snapping methods
    ////////////////////////////////////////////////////////////////////////////////



    private static HashSet<SnapType> activeSnaps;


    public static void clearActiveSnaps()
    {
        activeSnaps.Clear();
    }


    public static void addToActiveSnaps(SnapType st)
    {
        activeSnaps.Add(st);
    }


    public static void removeFromActiveSnaps(SnapType st)
    {
        activeSnaps.Remove(st);
    }


    public static bool hasOneActiveSnap(HashSet<SnapType> stSet)
    {
        return activeSnaps.Overlaps(stSet);
    }


    public static bool hasOneActiveSnap(List<SnapType> stSet)
    {
        return activeSnaps.Overlaps(stSet);
    }



    // common utility methods
    ////////////////////////////////////////////////////////////////////////////////




    public static GameObject getParentRecursive(GameObject inputObj, string tgtParentName, string endObjectName)
    {
        if (inputObj == null || inputObj.transform.parent == null || inputObj.name.Equals(endObjectName)) return null;
        if (inputObj.name.Equals(tgtParentName)) return inputObj;
        return getParentRecursive((inputObj.transform.parent.gameObject), tgtParentName, endObjectName);
    }




    public static GameObject getParentClone(GameObject inputObj, string parentName)
    {
        if (inputObj == null || inputObj.transform.parent == null) return null;
        if (inputObj.name.StartsWith(parentName)) return inputObj;
        return getParentClone(inputObj.transform.parent.gameObject, parentName);
    }




    // user console setup
    ////////////////////////////////////////////////////////////////////////////////

    
    

    private static List<string> consoleList;
    private static bool consoleUpdate;
    private static int consoleLimit = 1000;


    private Text UIConsoleText;
    private static Text UIInteractText;
    private static Image UIInteractBackground;




	private static void checkConsoleSize()
	{
		if (consoleList.Count >= consoleLimit)
		{
			// clears off the first half of logs on hitting limit
			consoleList.RemoveRange(0, Mathf.RoundToInt(consoleLimit * 0.5f));
		}
	}



	public static void addLineToConsole(string inputText)
	{
		consoleList.Add(inputText);
		checkConsoleSize();
		consoleUpdate = true;
	}



	public static void removeLastLineFromConsole()
	{
		if (consoleList.Count == 0)
			return;
		consoleList.RemoveAt(consoleList.Count-1);
		consoleUpdate = true;
	}




	private static string getConsoleText()
	{
		string bufferText = "";
		for(int i=consoleList.Count-1; i >= 0; i--)
		{
            bufferText += consoleList[i] + Environment.NewLine;
		}
		return bufferText;
	}




    // flash effect setup when user tries to switch tools midway when editing some tag
    ////////////////////////////////////////////////////////////////////////////////




    public static Color highLightColor;
    public static Color flashLightColor;
    public static Color defaultLightColor;
    public static GeneralSettings instance;

    


    public static void updateInteractText(string inputText)
    {
        UIInteractText.text = inputText;
        UIInteractBackground.color = highLightColor;
    }



    private static IEnumerator flashyEffect()
    {
        for (int cnt = 0; cnt < 10; cnt++)
        {
            if (cnt % 2 == 0)
                UIInteractBackground.color = flashLightColor;
            else
                UIInteractBackground.color = highLightColor;
            yield return new WaitForSeconds(0.05f);
        }
        UIInteractBackground.color = highLightColor;
    }



    public static void flashInteractWindow()
    {
        instance.StartCoroutine(flashyEffect());
    }



    ////////////////////////////////////////////////////////////////////////////////



    private static GameObject int_Player;
    public static GameObject player
    {
        get { return int_Player;  }
    }

    private static GameObject int_Model;
    public static GameObject model
    {
        get { return int_Model; }
    }

    private static GameObject int_ModelObjects;
    public static GameObject modelObjects
    {
        get { return int_ModelObjects; }
    }


    private static LaserPicker rightLaser;



    ////////////////////////////////////////////////////////////////////////////////


    private static GameObject deleteHoldingObject;

    public static void deleteObject(GameObject inpObject)
    {
        // if the object is in edit more; clear it before deleting
        if (GeneralSettings.getEditObject() == inpObject) GeneralSettings.clearEditObject();
        inpObject.transform.SetParent(deleteHoldingObject.transform);
        GeneralSettings.addLineToConsole(string.Format("{0} object deleted.", inpObject.name));
    }


    ////////////////////////////////////////////////////////////////////////////////



    private static Image blackScreen;
    private static bool startFade;



    IEnumerator fadeCoroutine()
    {
        startFade = false;
        Color holdingColor;
        int frames = 90;
        float changeVal = 1f / (frames/8);
        for (int i = 0; i < frames; i++)
        {
            holdingColor = blackScreen.color;
            if (i < frames / 2) holdingColor.a += changeVal;
            else holdingColor.a -= changeVal;
            blackScreen.color = holdingColor;
            yield return null;
        }
        holdingColor = blackScreen.color;
        holdingColor.a = 0;
        blackScreen.color = holdingColor;
    }


    public static void fadeScreen()
    {
        startFade = true;
    }




    ////////////////////////////////////////////////////////////////////////////////


    
    public static bool tableModeOn = false;

    private static Vector3 modelStartPos;
    private static Vector3 modelStartScale;
    private static Vector3 scaledModelLocation;
    private static Vector3 scaledModelScale;



    private static void setModelScale(Vector3 input)
    {
        scaledModelScale = input;
        model.transform.localScale = scaledModelScale;
    }



    private static void enableTableMode()
    {
        fadeScreen();
        model.transform.position = scaledModelLocation;
        setModelScale(scaledModelScale);
        tableModeOn = true;
    }


    private static void disableTableMode()
    {
        fadeScreen();
        model.transform.position = modelStartPos;
        model.transform.localScale = modelStartScale;
        tableModeOn = false;
    }



    public static void toggleTableMode()
    {
        if (tableModeOn)
        {
            disableTableMode();
            return;
        }
        enableTableMode();
    }



    ////////////////////////////////////////////////////////////////////////////////


    private static GameObject actionSwitcherObject;
    private static ActionSwitcher actionSwitcher;


    public static void setActiveActionObject(GameObject creatorObject)
    {
        actionSwitcher.setActionItem(creatorObject);
    }


    public static bool selActionOn()
    {
        return actionSwitcher.getActionItem() == null;
    }



    ////////////////////////////////////////////////////////////////////////////////


    private static GameObject objectMenuHolderObject;
    private static ObjectMenuManager objMenuManager;


    public static void setObjectMenu(GameObject objectMenu)
    {
        objMenuManager.setObjectMenu(objectMenu);
    }


    public static void deleteObjectMenu()
    {
        objMenuManager.deleteObjectMenu();
    }


    public static bool hasObjectMenu()
    {
        return objMenuManager.hasObjectMenu();
    }



    ////////////////////////////////////////////////////////////////////////////////



    private static WaitingObject waitingObj;
    private static GameObject prevMenu;


    /// <summary>
    /// Destroys the current menu object and adds the menu item loaded into prevMenu to the active menu.
    /// </summary>
    public static void reinstatePreviousMenu()
    {
        deleteObjectMenu();
        for (int i=0; i<prevMenu.transform.childCount; i++)
        {
            setObjectMenu(prevMenu.transform.GetChild(i).gameObject);
        }
    }



    ////////////////////////////////////////////////////////////////////////////////



    private static void resetPrevMenuObjPosAndRot()
    {
        for (int i = 0; i < prevMenu.transform.childCount; i++)
        {
            prevMenu.transform.GetChild(i).localPosition = Vector3.zero;
            prevMenu.transform.GetChild(i).localRotation = Quaternion.identity;
        }
    }


    public GameObject numPadUIPrefab;


    private static GameObject getNumPadPrefab()
    {
        return Instantiate(instance.numPadUIPrefab);
    }


    public static void setNumPad(WaitingObject inpWaitObj)
    {
        waitingObj = inpWaitObj;
        objMenuManager.getAndEmptyObjectMenu(prevMenu);
        resetPrevMenuObjPosAndRot();
        setObjectMenu(getNumPadPrefab());
    }


    public static void returnNumPadVal(string retStr)
    {
        waitingObj.setString(retStr);
        waitingObj = null;
    }



    ////////////////////////////////////////////////////////////////////////////////



    private static LayerManager layerManager;

    public static GameObject getActiveLayerObject()
    {
        return layerManager.getActiveLayerObject();
    }



    ////////////////////////////////////////////////////////////////////////////////




    // Use this for initialization
    void Awake ()
    {
        instance = this;
        DateTime epochTime = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int currTime = (int)(System.DateTime.UtcNow - epochTime).TotalSeconds;

		sessionStartTime = DateTime.Now;
		sessionID = SystemInfo.deviceUniqueIdentifier.ToString() + "_" + currTime.ToString();

        consoleList = new List<string>();
        UIConsoleText = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild ("_Canvas").FindChild ("ConsoleSet").FindChild ("_ConsoleWindow").gameObject.GetComponent<Text>();
        UIInteractText = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild ("_Canvas").FindChild ("InteractSet").FindChild ("_InteractWindow").gameObject.GetComponent<Text>();
        UIInteractBackground = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild ("_Canvas").FindChild ("InteractSet").FindChild ("Background").gameObject.GetComponent<Image>();

        int_Player = transform.FindChild("[CameraRig]").FindChild("Camera (eye)").gameObject;

        rightLaser = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild("_LaserPicker").gameObject.GetComponent<LaserPicker>();

        int_Model = transform.FindChild("_Model").gameObject;
        modelStartPos = model.transform.position;
        modelStartScale = model.transform.localScale;
        scaledModelLocation = modelStartPos;
        scaledModelScale = new Vector3(0.005f, 0.005f, 0.005f);

        int_ModelObjects = model.transform.FindChild("_Objects").gameObject;
        deleteHoldingObject = model.transform.FindChild("_Deleted").gameObject;
        deleteHoldingObject.SetActive(false);

        layerManager = transform.FindChild("[CameraRig]").FindChild("Controller (left)").FindChild("_ToolBox").FindChild("Screen_LayerManagement").GetComponent<LayerManager>();

        actionSwitcherObject = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild("_ActionSwitcher").gameObject;
        actionSwitcher = actionSwitcherObject.GetComponent<ActionSwitcher>();

        objectMenuHolderObject = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild("_ObjectMenuCanvas").gameObject;
        objMenuManager = objectMenuHolderObject.GetComponent<ObjectMenuManager>();
        prevMenu = new GameObject();
        prevMenu.SetActive(false);

        blackScreen = player.transform.FindChild("_BlackScreen").FindChild("Image").gameObject.GetComponent<Image>();

        activeSnaps = new HashSet<SnapType>();
        addToActiveSnaps(SnapType.MID);
        addToActiveSnaps(SnapType.END);

        addLineToConsole(System.String.Format("Session Start Time: {0}", sessionStartTime.ToLongTimeString()));
		addLineToConsole(System.String.Format("Session ID: {0}", sessionID));
		addLineToConsole(System.String.Format("Session Name: {0}", sessionName));
		addLineToConsole("v " + version);
		addLineToConsole(applicationName);
		addLineToConsole(developerName);
		addLineToConsole("Hi there!");


        defaultLightColor = UIInteractBackground.color;
        highLightColor = new Color(0.3f, 0.55f, 0f, 0.8f);
        flashLightColor = new Color(0.1f, 0.45f, 0.3f, 0.8f);

	}



	// Update is called once per frame
	void Update () {

        if (startFade)
        {
            StartCoroutine(fadeCoroutine());
        }





		if (consoleUpdate)
		{
			UIConsoleText.text = getConsoleText();
			consoleUpdate = false;
		}

    }



    ////////////////////////////////////////////////////////////////////////////////


    public static string REF_OBJ_START_NAME = "ref_";








}
