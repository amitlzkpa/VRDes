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





    /*
    // default and current tag setup
    ////////////////////////////////////////////////////////////////////////////////

    

    
    // default tag object is set to 'Tag_default" and should always be
    public static GameObject defaultTagObject;
    public static GameObject currentlyActiveTagObject;




	// if the default tag isnt the current active tag; some action is underway
	public static bool isActionOn()
	{
		if (currentlyActiveTagObject == defaultTagObject)
			return false;
		return true;
	}




	public static void resetCurrentTagObjectToDefaultObject()
    {
		currentlyActiveTagObject = defaultTagObject;
	}




    public static void resetActiveTagAndSwitchToTagManager()
    {
        resetCurrentTagObjectToDefaultObject();
        setActiveLaser(2);
        UIInteractText.text = "";
        UIInteractBackground.color = new Color(0.25f, 0.25f, 0.25f, 0.4901f);
    }




	public static void setActiveLaser(int idx)
	{
		actionManagerObject.GetComponent<ActionManager>().switchLaserByInt(idx);
    }

    */




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



    public static IEnumerator flashyEffect()
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



    /*
    // audio recording setup
    // only exposing the audio recording setup in a global setup to allow easy access across different objects
    ////////////////////////////////////////////////////////////////////////////////




    private static bool isRec;



    public static void recordAudioClip()
    {
        if (isRec)
        {
            actionManagerObject.GetComponent<AudioRecordManager>().stopAndSaveRecording();
            isRec = false;
        }
        else
        {
            actionManagerObject.GetComponent<AudioRecordManager>().startNewAudioRecording();
            isRec = true;
        }
    }


    
    public static void replayAudio()
    {
        actionManagerObject.GetComponent<AudioRecordManager>().replayLast();
    }
    */




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
    private static Vector3 lastPlayerPosition;
    private static Vector3 scaledModelLocation;
    private static Vector3 scaledModelScale;
    private static Vector3 scaledPlayerLocation;



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
        lastPlayerPosition = player.transform.position;
        tableModeOn = true;
    }


    private static void disableTableMode()
    {
        fadeScreen();
        model.transform.position = modelStartPos;
        model.transform.localScale = modelStartScale;
        player.transform.position = lastPlayerPosition;
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

        int_Model = transform.FindChild("_Model").gameObject;
        modelStartPos = model.transform.position;
        modelStartScale = model.transform.localScale;
        scaledModelLocation = modelStartPos;
        scaledModelScale = new Vector3(0.005f, 0.005f, 0.005f);
        scaledPlayerLocation = scaledModelLocation + new Vector3(3f, 3f, 3f);

        int_ModelObjects = model.transform.FindChild("_Objects").gameObject;

        actionSwitcherObject = transform.FindChild("[CameraRig]").FindChild("Controller (right)").FindChild("_ActionSwitcher").gameObject;
        actionSwitcher = actionSwitcherObject.GetComponent<ActionSwitcher>();

        blackScreen = player.transform.FindChild("_BlackScreen").FindChild("Image").gameObject.GetComponent<Image>();

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
}
